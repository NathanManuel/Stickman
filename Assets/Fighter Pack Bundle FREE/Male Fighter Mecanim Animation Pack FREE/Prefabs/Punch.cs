using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    Animator anim;
    public float pushForce = 10.0f;
    private string enemyTag;
    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        PlayerController playerController = GetComponentInParent<PlayerController>();

        if (playerController != null)
        {
            enemyTag = playerController.EnemyTag;
            Debug.Log("Enemy Tag: " + enemyTag);
        }
        else
        {
            Debug.LogWarning("PlayerController script not found in parent objects.");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            Debug.Log("HIT");
            // Check if the collided object is tagged as an enemy
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Animator enemyAnimator = collision.gameObject.GetComponent<Animator>();


            if (enemyRigidbody != null)
            {
                // Calculate the push direction from the player to the enemy
                Vector3 pushDirection = collision.transform.position - transform.position;
                pushDirection.Normalize();

                // Apply force to push the enemy back
                enemyRigidbody.AddForce(transform.root.forward * pushForce, ForceMode.Impulse);
                enemyAnimator.SetTrigger("Hit");
            }
        }
    }
}
