using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public ClickDetector detector;
    
    public float playerSpeed = 1.0f;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 destination = detector.clickWorldPosition; 
        Vector3 move = destination - transform.position;
        if (move.magnitude <= Mathf.Epsilon)
        {
            return;
        }
        
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }


    }
}
