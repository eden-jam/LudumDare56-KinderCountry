using TMPro;
using UnityEngine;

public class PuddleCounter : MonoBehaviour
{

    public TextMeshProUGUI mainText;
    void Start()
    {
        mainText.text = BoidsManager.Instance.GetBoidsCount().ToString() ;
    }
}
