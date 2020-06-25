using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFallCheck : MonoBehaviour
{
    public bool isFalling;
    void Start() => StartCoroutine(DoFallCheck());
    private IEnumerator DoFallCheck()
    {
        while (true)
        {
            if (transform.position.y < GenerateEnvironment.minBound)
                Destroy(gameObject);

            var topHits = Physics2D.RaycastAll(transform.position, Vector2.up, 1.0f);
            var bottomHits = Physics2D.RaycastAll(transform.position, -Vector2.up, 1.0f);

            // Nothing above and below
            if (topHits.Length == 0 && bottomHits.Length == 0)
            {
                transform.position -= Vector3.up;
                isFalling = true;
                GameAudioController.playDroppingCrates();
            }
            // Gravel Tile above, nothing below
            else if (topHits.Length == 1 && topHits[0].collider.gameObject.TryGetComponent<TileFallCheck>(out var _) && bottomHits.Length == 0)
            {
                transform.position -= Vector3.up;
                isFalling = false;
            }
            // Nothing above, Enemy below
            else if (topHits.Length == 0 && bottomHits.Length == 1 && (bottomHits[0].collider.CompareTag("Enemy") || bottomHits[0].collider.CompareTag("Gem")))
            {
                transform.position -= Vector3.up;
                isFalling = true;
                GameAudioController.playDroppingCrates();
            }
            // Enemy above, nothing below
            else if (topHits.Length == 1 && bottomHits.Length == 0 && topHits[0].collider.CompareTag("Enemy"))
            {
                transform.position -= Vector3.up;
                isFalling = true;
                GameAudioController.playDroppingCrates();
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
}
