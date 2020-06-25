using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private Coroutine moving = null;
    private void Start() => moving = StartCoroutine(Move());
    private bool justJumped;
    [SerializeField]
    private List<GameObject> gems;
    private IEnumerator Move()
    {
        while (true)
        {
            var bottomHits = Physics2D.RaycastAll(transform.position, -Vector2.up, 1.0f);
            var leftHits = Physics2D.RaycastAll(transform.position, -Vector2.right, 1.0f);
            var rightHits = Physics2D.RaycastAll(transform.position, Vector2.right, 1.0f);

            if (bottomHits.Length == 0 && !justJumped)
                transform.position -= Vector3.up;
            else
            {
                justJumped = false;
                if (leftHits.Length == 0 && rightHits.Length == 0)
                {
                    var dir = Random.Range(0, 4);
                    if (dir == 0)
                        transform.position -= Vector3.right;
                    else if (dir == 1)
                        transform.position += Vector3.right;
                    else if (dir == 2 && bottomHits.Length == 1)
                    {
                        justJumped = true;
                        transform.position += Vector3.up;
                    }
                }
                else if (leftHits.Length == 0)
                {
                    if(Random.Range(0,2) == 0)
                        transform.position -= Vector3.right;
                    else if (bottomHits.Length == 1)
                    {
                        justJumped = true;
                        transform.position += Vector3.up;
                    }
                }
                else if (rightHits.Length == 0)
                {
                    if (Random.Range(0, 2) == 0)
                        transform.position += Vector3.right;
                    else if (bottomHits.Length == 1)
                    {
                        justJumped = true;
                        transform.position += Vector3.up;
                    }
                }
                else if(bottomHits.Length == 1)
                {
                    justJumped = true;
                    transform.position += Vector3.up;
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Tile") && collision.collider.gameObject.TryGetComponent<TileFallCheck>(out var tfc))
        {
            if (tfc.isFalling)
            {
                var rand = Random.Range(0, 3);
                if (rand == 0)
                    Instantiate(gems[0], transform.position, Quaternion.identity);
                else if (rand == 1)
                    Instantiate(gems[1], transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}