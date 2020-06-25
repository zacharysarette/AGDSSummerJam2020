using UnityEngine;
public class GemInteraction : MonoBehaviour
{
    [SerializeField]
    private int scoreOnPickup;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            ScoreTracker.AddScore(scoreOnPickup);
            Destroy(gameObject);
        }
    }
}