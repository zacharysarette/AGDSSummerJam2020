using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFallCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() => StartCoroutine(DoFallCheck());
    private IEnumerator DoFallCheck()
    {
        while (true)
        {
            if (transform.position.y < GenerateEnvironment.minBound)
                Destroy(gameObject);

            var topHits = Physics2D.RaycastAll(transform.position, Vector2.up, 1.0f);
            var bottomHits = Physics2D.RaycastAll(transform.position, -Vector2.up,1.0f);

            if (topHits.Length == 0 && bottomHits.Length == 0)
            {
                transform.position -= Vector3.up;
                GameAudioController.playDroppingCrates();
            }
            else if (topHits.Length == 1 && topHits[0].collider.gameObject.TryGetComponent<TileFallCheck>(out var _) && bottomHits.Length == 0)
            {
                transform.position -= Vector3.up;
                GameAudioController.playDroppingCrates();
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
}
