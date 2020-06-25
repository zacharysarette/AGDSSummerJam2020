using UnityEngine;
using UnityEngine.UI;
public class ScoreTracker : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;
    public static int score;

    private void Update() => scoreText.text = "x " + score.ToString();
    public static void AddScore(int amount) => score += amount;
}