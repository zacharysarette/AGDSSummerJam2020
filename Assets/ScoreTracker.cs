using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    public static int score;

    private void Update() {
        scoreText.text = "x " + score.ToString();

        if(score == 20)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("Win", LoadSceneMode.Additive);

        }

    }

    public static void AddScore(int amount) => score += amount;
}