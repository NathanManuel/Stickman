using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header ("Player Deck")]
    [SerializeField] private float timeUntilIntialDraw = 2f;
    [SerializeField] private PlayerController player;
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    public Card selectedCard;

    [Header ("Deck Text")]
    public TMP_Text deckSizeText;

    [Header ("Joystick")]
    public Joystick joystick;

    [Header ("Player Ghost")]
    [SerializeField] private PlayerGhost playerGhost;
    [SerializeField] private Transform _playerGhostTransform;

    [Header ("Player Position")]
    [SerializeField] private Vector3 _oldPlayerPosition;
    [SerializeField] private Vector3 _newPlayerPosition;
    [SerializeField] private float _playerHeight = 0.8f;



    private void Start()
    {
        InitializeComponents();
        InvokeInitialDraw();
    }



    private void Update()
    {
        UpdatePlayerPosition();
        UpdatePlayerGhost();
        UpdateDeckSizeText();
    }


    private void InitializeComponents()
    {
        SetInitialPlayerPosition();
        CachePlayerGhostTransform();
    }

    private void SetInitialPlayerPosition()
    {
        _oldPlayerPosition = player != null ? player.transform.localPosition : Vector3.zero;
    }

    private void CachePlayerGhostTransform()
    {
        _playerGhostTransform = playerGhost != null ? playerGhost.transform : null;
    }

    private void InvokeInitialDraw()
    {
        Invoke("InitialDraw", timeUntilIntialDraw);
    }

    private void UpdatePlayerPosition()
    {
        if (joystick != null)
        {
            _newPlayerPosition = CalculateNewPlayerPosition();

            // Limit the minimum y-position
            if (_newPlayerPosition.y <= _playerHeight)
            {
                _newPlayerPosition.y = _playerHeight;
            }

            if (playerGhost != null)
            {
                _playerGhostTransform.position = _newPlayerPosition;
            }
        }
    }
    private Vector3 CalculateNewPlayerPosition()
    {
        if (joystick != null)
        {
            return new Vector3(_oldPlayerPosition.x, _oldPlayerPosition.y + joystick.Direction[1], _oldPlayerPosition.z - joystick.Direction[0]);
        }
        return Vector3.zero;
    }

    private void UpdatePlayerGhost()
    {
        if (joystick != null && playerGhost != null)
        {
            if (joystick.Direction[0] != 0 || joystick.Direction[1] != 0)
            {
                playerGhost.gameObject.SetActive(true);
            }
            else
            {
                playerGhost.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateDeckSizeText()
    {
        if (deckSizeText != null)
        {
            deckSizeText.text = deck.Count.ToString();
        }
    }



    public void MakeMove()
    {
        if (selectedCard != null)
        {
            Debug.Log("JOYSTICK: " + (joystick != null ? joystick.Direction.ToString() : "Joystick is null") + " chosen animation: " + (playerGhost != null ? playerGhost.chosen : "PlayerGhost is null"));
            Debug.Log("CARD: " + (selectedCard != null ? selectedCard.ToString() : "Selected card is null"));

            PerformSelectedCardActions();
            MovePlayerToNewPosition();
            ResetPlayerState();
        }
        else
        {
            Debug.Log("SELECT A CARD");
        }
    }

    private void PerformSelectedCardActions()
    {
        if (selectedCard != null)
        {
            selectedCard.hasBeenPlayed = true;

            if (selectedCard.handIndex >= 0 && selectedCard.handIndex < availableCardSlots.Length)
            {
                availableCardSlots[selectedCard.handIndex] = true;
            }

            selectedCard.MoveToDiscardPile();
            selectedCard = null;
            DrawCard();
        }
    }

    private void MovePlayerToNewPosition()
    {
        if (player != null)
        {
            player.transform.position = _newPlayerPosition;
            player.PlayAnimation(playerGhost != null ? playerGhost.chosen : "");
        }
    }

    private void ResetPlayerState()
    {
        if (joystick != null)
        {
            joystick.Reset();
        }

        ResetPlayerGhost();
    }

    private void ResetPlayerGhost()
    {
        if (playerGhost != null)
        {
            _playerGhostTransform?.gameObject.SetActive(false);
            _oldPlayerPosition = player != null ? player.transform.position : Vector3.zero;
        }
    }

    private void InitialDraw()
    {
        for (int i = 0; i < cardSlots.Length; i++)
        {
            DrawCard();
        }
    }
    public void DrawCard()
    {
        if (deck.Count >= 1)
        {
            Card randCard = GetRandomCardFromDeck();
            int availableSlotIndex = GetAvailableCardSlotIndex();

            if (availableSlotIndex != -1)
            {
                PlaceCardInSlot(randCard, availableSlotIndex);
            }
        }
    }

    private Card GetRandomCardFromDeck()
    {
        int randomIndex = Random.Range(0, deck.Count);
        return deck[randomIndex];
    }

    private int GetAvailableCardSlotIndex()
    {
        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            if (availableCardSlots[i])
            {
                return i;
            }
        }

        return -1; // No available slot found
    }

    private void PlaceCardInSlot(Card card, int slotIndex)
    {
        card.handIndex = slotIndex;
        card.transform.position = cardSlots[slotIndex].position;
        availableCardSlots[slotIndex] = false;
        deck.Remove(card);
    }

    public void ResetPlayerDeck()
    {
        if(deck.Count <= 0)
        {
        deck = discardPile;
        discardPile.Clear();
        InitialDraw();
        }
        else
        {
            Debug.Log("NOT YET");
        }
       
    }
}
