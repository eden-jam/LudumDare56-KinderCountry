using TMPro;
using UnityEngine;

public class PuddleCounter : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    void FixedUpdate()
    {
        mainText.text = $"Puddle : {BoidsManager.Instance.GetBoidsCount().ToString()}";
    }
}
