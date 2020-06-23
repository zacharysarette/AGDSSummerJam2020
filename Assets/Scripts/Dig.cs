using System.Collections;
using UnityEngine;

public class Dig : MonoBehaviour
{
    [SerializeField]
    private Sprite blackSprite;
    [SerializeField]
    private Collider2D collider;

    Coroutine isDigging;
    GameObject targetTile = null;
    public void StartDig()
    {
        if(isDigging == null)
            isDigging = StartCoroutine(Digging());
    }
    private IEnumerator Digging()
    {
        yield return new WaitForEndOfFrame();

        float progress = 0.0f;

        while(progress < 1.0f)
        {
            progress += Time.deltaTime;

            if (targetTile == null || !collider.enabled)
            {
                isDigging = null;
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }

        Destroy(targetTile);
        targetTile = null;

        isDigging = null;
        yield return null;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Tile"))
            targetTile = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Tile"))
            targetTile = null;
    }
}
