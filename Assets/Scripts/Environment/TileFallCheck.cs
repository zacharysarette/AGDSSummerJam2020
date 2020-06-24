using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFallCheck : MonoBehaviour
{
    public bool isFalling;
    // Start is called before the first frame update
    void Start() => StartCoroutine(DoFallCheck());
    private IEnumerator DoFallCheck()
    {
        while (true)
        {
            if (GenerateEnvironment.instance != null && transform.position.y < GenerateEnvironment.minBound)
            {
                Destroy(gameObject);
                yield break;
            }

            var topHits = Physics2D.RaycastAll(transform.position, Vector2.up, 1.0f);
            var bottomHits = Physics2D.RaycastAll(transform.position, -Vector2.up,1.0f);

            if (topHits.Length == 0 && bottomHits.Length == 0)
            {
                transform.position -= Vector3.up;
                GameAudioController.playDroppingCrates();
                isFalling = true;
            }
            else if (topHits.Length == 1 && topHits[0].collider.gameObject.TryGetComponent<TileFallCheck>(out var _) && bottomHits.Length == 0)
            {
                transform.position -= Vector3.up;
                GameAudioController.playDroppingCrates();
                isFalling = true;
            }
            else
                isFalling = false;

            yield return new WaitForSeconds(0.25f);
        }
    }
}
