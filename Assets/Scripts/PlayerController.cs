using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    
    private Animator animator;
    private GameManager gm;

    [Header("Cards")]
    [SerializeField] private Card[] cardTypes;
    [SerializeField] private int[] ammountOfCardsPerType;
    public Transform cardSpawnLocation;
    
    [Header("Enemy")]
    public string EnemyTag;

    [Header("PunchAction")]
    public bool didPunch = false;

    [Header("Card Transform")]
    [SerializeField] private Vector3 cardsPosition = new Vector3(0, 0, 0);
    [SerializeField] private Quaternion cardsRotation = new Quaternion(0f, 0f, 0f, 0f);

    [Header ("PlayerHitBoxes")]
    private List<Transform> punches = new List<Transform>();
    private List<Transform> kicks = new List<Transform>();



    void Start()
    {
        InitializeComponents();
        SpawnAllCards();
        SetHitBoxes();
    }

    void InitializeComponents()
    {
        animator = GetComponent<Animator>();
        gm = FindObjectOfType<GameManager>();
    }

    void SpawnAllCards()
    {
        int i = 0;
        foreach (Card card in cardTypes)
        {
            SpawnCards(card, cardsPosition, cardsRotation, ammountOfCardsPerType[i]);
            i++;
        }
    }

    void SetHitBoxes()
    {
        setHitBox("Punch");
        setHitBox("Kick");
    }

    public void setHitBox(string hitBoxName)
    {
        GameObject parentObject = GameObject.Find(gameObject.name);

        if (parentObject != null)
        {
            Transform[] childTransforms = parentObject.GetComponentsInChildren<Transform>(true);

            foreach (Transform childTransform in childTransforms)
            {
                if (childTransform.name == hitBoxName)
                {
                    punches.Add(childTransform);
                }   
            }
        }
        else
        {
            Debug.LogError("Parent object not found!");
        }
    }

    public void SpawnCards(Card prefabToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, int amount)
    {
        for(int i=0; i<amount; i++)
        {
        Card card = Instantiate(prefabToSpawn, spawnPosition, spawnRotation);
            card.transform.SetParent(cardSpawnLocation);
            gm.deck.Add(card);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {
            Debug.Log("HIT wall");
        }
    }

    private void PunchEnd()
    {
        didPunch = false;
        foreach (Transform punch in punches)
        {
            punch.gameObject.SetActive(false);
        }
    }  
    private void LKickStart()
    {
        foreach (Transform kick in kicks)
        {
            kick.gameObject.SetActive(true);
        }
    }  
    private void LKickEnd()
    {
        foreach (Transform kick in kicks)
        {
            kick.gameObject.SetActive(false);
        }
    }
    private void Punched()
    {
        didPunch = true;
        foreach (Transform punch in punches)
        {
            punch.gameObject.SetActive(true);
        }
    }

    public void PlayAnimation(string animationTrigger)
    {
     
            animator.SetTrigger(animationTrigger);
    }

 
}
