using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [System.NonSerialized] public bool isAlive = true;

    [SerializeField] private Healthbar healthbar;
    [SerializeField] private RectTransform healthbarTransform;

    [SerializeField] private int maxHealth = 10;

    [SerializeField]
    private float healthDecrementRate = 0.75f,
        vibrateAmplitude = 2.0f,
        vibrateFrequency = 1000.0f;

    private int currentHealth;

    private float currentHealthDecrease,
        vibrateXComponent,
        originalHealthbarY;

    private bool isTakingDamage = false,
        startTakingDamage = false;

    private void Start()
    {
        originalHealthbarY = healthbarTransform.position.y;
        currentHealth = maxHealth;
        healthbar.SetMaxHealth((int)maxHealth);
    }

    private void Update()
    {
        if (isAlive)
        {
            DamageUpdate();
            VibrateUpdate();
        }
        else if (SceneManager.GetActiveScene().name != "Lose")
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("Lose", LoadSceneMode.Single);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Tile") || collision.transform.CompareTag("Indestructible"))
            startTakingDamage = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Tile") || collision.transform.CompareTag("Indestructible"))
        {
            isTakingDamage = false;
            startTakingDamage = false;
        }
    }

    private void DamageUpdate()
    {
        //  Update isTakingDamage and reset the current health decrease
        if (startTakingDamage && !isTakingDamage)
        {
            isTakingDamage = true;
            currentHealthDecrease = 0.0f;
        }

        //  Update amount of health by which to decrease
        if (isTakingDamage)
        {
            currentHealthDecrease += healthDecrementRate * Time.deltaTime;
            if (currentHealthDecrease >= 1.0f)
            {
                currentHealthDecrease -= 1.0f;
                currentHealth -= 1;
            }
        }

        //  Update slider
        if (currentHealth != healthbar.GetSliderValue())
            healthbar.SetHealth(currentHealth);

        //  Update isAlive
        if (currentHealth == 0)
            isAlive = false;
    }

    private void VibrateUpdate()
    {
        if (isTakingDamage)
        {
            if (vibrateXComponent >= 360.0f)
                vibrateXComponent -= 360.0f;

            vibrateXComponent += Time.deltaTime;

            float newHeightOffset = vibrateAmplitude * Mathf.Sin(Mathf.Deg2Rad * vibrateXComponent * vibrateFrequency);

            healthbarTransform.position = new Vector3(healthbarTransform.position.x, originalHealthbarY + newHeightOffset, healthbarTransform.position.z);
        }
        else if (healthbarTransform.position.y != originalHealthbarY)
            healthbarTransform.position = new Vector3(healthbarTransform.position.x, originalHealthbarY, healthbarTransform.position.z);
    }
}
