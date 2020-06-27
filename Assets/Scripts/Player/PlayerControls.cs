using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public int Health { get; private set; }
    private bool invulnerable;

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
    
    Vector2 moveDirection;
    private void Start()
    {
        digPreviewInstance = Instantiate(tileDigPreview, transform.position, Quaternion.identity);
        StartCoroutine("ResolveMovement");
    }

    private IEnumerator ResolveMovement()
    {
        while(true)
        {
            var targetPos = new Vector3(moveDirection.x, moveDirection.y) + transform.position;
            transform.position = new Vector3(Mathf.Clamp(targetPos.x, -79.5f, 79.5f), Mathf.Clamp(targetPos.y, -44.5f, 44.5f), targetPos.z);
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void Update()
    {
        moveDirection = Vector2.zero;
        if (Input.GetKey(left))
        {
            moveDirection = -Vector3.right;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey(right))
        {
            moveDirection = Vector3.right;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (Input.GetKey(up))
            moveDirection = Vector3.up;
        if(Input.GetKey(down))
            moveDirection = -Vector3.up;

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

        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            Camera.main.orthographicSize = 20f;
        else if(Camera.main.orthographicSize != 10f)
            Camera.main.orthographicSize = 10f;


        digPreviewInstance.GetComponent<SpriteRenderer>().enabled = isAttacking;
        digPreviewInstance.GetComponent<Collider2D>().enabled = isAttacking;

        if (isAttacking)
            digPreviewInstance.GetComponent<Dig>().StartDig();


    }

    public void TakeDamage()
    {
        if (!invulnerable)
            Health--;

        if (Health == 0)
            Die();

        StartCoroutine("Invulnerability");
    }



    private void Die()
    {
        Debug.Log("Dead");
    }


    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        yield return new WaitForSeconds(0.5f);
        invulnerable = false;
    }
}