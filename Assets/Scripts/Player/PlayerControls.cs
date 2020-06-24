using System.Collections;
using UnityEngine;

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

        StartCoroutine(Invulnerability());
    }

    private void Die()
    {
        Debug.Log("Ded");
    }

    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        yield return new WaitForSeconds(0.5f);
        invulnerable = false;
    }

}