using System.Collections;
using UnityEngine;

public class Dig : MonoBehaviour
{
    [SerializeField]
    private Sprite blackSprite;
    [SerializeField]
    private Collider2D digCollider;

    Coroutine isDigging;
    GameObject targetTile = null;

    public void StartDig()
    {
        if(isDigging == null)
            isDigging = StartCoroutine("Digging");
    }
    private IEnumerator Digging()
    {
        yield return new WaitForEndOfFrame();

        float progress = 0.0f;

        while(progress < 0.5f)
        {
            progress += Time.deltaTime;

            if (targetTile == null || !digCollider.enabled)
            {
                isDigging = null;
                yield break;
            }

            yield return new WaitForEndOfFrame();
        }

        Destroy(targetTile);
        GameAudioController.playDig();
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
