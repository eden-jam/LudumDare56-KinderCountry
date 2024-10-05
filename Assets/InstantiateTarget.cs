using UnityEngine;

public class InstantiateTarget : MonoBehaviour
{
    public ClickDetector detector;
    public CharacterMovement characterMovement;
    public GameObject target;
    private GameObject targetInstance;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (detector.hasPlayerClicked == true)
        {
            if (targetInstance)
            {
                Destroy(targetInstance);
            }
            targetInstance = Instantiate(target, detector.clickWorldPosition,Quaternion.identity);
        }
        else if (characterMovement.destinationReached == true)
        {
            Destroy(targetInstance);
        }

    }
}
