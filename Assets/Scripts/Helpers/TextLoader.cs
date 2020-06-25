using UnityEngine;
using TMPro;

public class TextLoader : MonoBehaviour
{
    [SerializeField]
    private TextAsset textFile;
    [SerializeField]
    private TextMeshProUGUI contentText;

    public void LoadText()
    {
        contentText.text = textFile.text;
    }

}