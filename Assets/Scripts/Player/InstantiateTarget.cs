using UnityEngine;

public class InstantiateTarget : MonoBehaviour
{
    public ClickDetector detector;
    public CharacterMovement characterMovement;
    public GameObject target;
    private GameObject targetInstance = null;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (detector.hasPlayerClicked == true)
        {
            if (targetInstance == null)
            {
                targetInstance = Instantiate(target, detector.clickWorldPosition, Quaternion.identity);
            }

            targetInstance.transform.position = detector.clickWorldPosition;
        }
        else if (characterMovement.destinationReached == true)
        {
            Destroy(targetInstance);
        }

    }
}
