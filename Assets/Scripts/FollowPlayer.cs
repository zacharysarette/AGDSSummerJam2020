using UnityEngine;
public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    void LateUpdate()
    {
        var targetPos = Vector3.Lerp(Camera.main.transform.position, target.position - Vector3.forward, Time.fixedDeltaTime * 1.25f);
        
        Camera.main.transform.position = new Vector3(Mathf.Clamp(targetPos.x, -61f, 61f), Mathf.Clamp(targetPos.y, -35f, 35f), targetPos.z);
        //Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, target.position - Vector3.forward, Time.fixedDeltaTime * 1.25f);
        //X should be -60, 60.5
        //Y should be -34.5, 34.5
    }
}