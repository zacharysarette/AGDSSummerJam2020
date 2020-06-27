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

    private bool startTakingDamage = false;

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
            StartCoroutine(LoadSceneASync("Lose"));
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Tile") || collision.transform.CompareTag("Indestructible"))
        {
            startTakingDamage = true;
        } 

    } 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Tile") || collision.transform.CompareTag("Indestructible"))
        {
            startTakingDamage = false;
        } 
    }

    private void DamageUpdate()
    {
        if (!startTakingDamage)
        {
            return;
        }

        //  Update amount of health by which to decrease
        if (startTakingDamage)
        {   
  
            currentHealthDecrease += healthDecrementRate * Time.deltaTime;;
            if (currentHealthDecrease >= 1.0f)
            {   
                currentHealthDecrease -= 1.0f;
                currentHealth -= 1;
            } else {
                GameAudioController.playBuzz();
            }
        }

        //  Update slider
        if (currentHealth != healthbar.GetSliderValue()) 
        {
            healthbar.SetHealth(currentHealth);
            AudioManager.Instance.StopSfx();
            GameAudioController.playHurt();
        }

        //  Update isAlive
        if (currentHealth == 0)
            isAlive = false;
    }

    private void VibrateUpdate()
    {
        if (startTakingDamage)
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

    IEnumerator LoadSceneASync(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
