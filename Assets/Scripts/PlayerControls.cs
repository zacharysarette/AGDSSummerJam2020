using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField]
    private GameObject tileDigPreview;

    [SerializeField] 
    private KeyCode 
        left, 
        right, 
        up, 
        down,
        attackLeft,
        attackRight,
        attackUp,
        attackDown;

    private GameObject digPreviewInstance;

    private void Start() => digPreviewInstance = Instantiate(tileDigPreview, transform.position, Quaternion.identity);
    

    private void Update()
    {
        if (Input.GetKeyDown(left))
        {
            transform.position -= Vector3.right;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKeyDown(right))
        {
            transform.position += Vector3.right;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKeyDown(up))
            transform.position += Vector3.up;
        if(Input.GetKeyDown(down))
            transform.position -= Vector3.up;

        bool isAttacking = false;

        if(Input.GetKey(attackLeft))
        {
            isAttacking = true;
            digPreviewInstance.transform.position = transform.position - Vector3.right;
        }
        if (Input.GetKey(attackRight))
        {
            isAttacking = true;
            digPreviewInstance.transform.position = transform.position + Vector3.right;
        }
        if (Input.GetKey(attackUp))
        {
            isAttacking = true;
            digPreviewInstance.transform.position = transform.position + Vector3.up;
        }
        if (Input.GetKey(attackDown))
        {
            isAttacking = true;
            digPreviewInstance.transform.position = transform.position - Vector3.up;
        }

        digPreviewInstance.GetComponent<SpriteRenderer>().enabled = isAttacking;
        digPreviewInstance.GetComponent<Collider2D>().enabled = isAttacking;

        if (isAttacking)
            digPreviewInstance.GetComponent<Dig>().StartDig();
    }
}
