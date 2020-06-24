using UnityEngine;
public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    void LateUpdate() => Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, target.position - Vector3.forward, Time.fixedDeltaTime * 1.25f);
}