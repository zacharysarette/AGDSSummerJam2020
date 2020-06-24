using System.Collections;
using UnityEngine;

public class EdgeRemoveCheck : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;

    public void StartChecking() => StartCoroutine(Check());

    private IEnumerator Check()
    {
        while (true)
        {
            var topHits = Physics2D.RaycastAll(transform.position, direction, 1.0f);
            if (topHits.Length == 0)
            {
                Destroy(gameObject);
                yield break;
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
}
