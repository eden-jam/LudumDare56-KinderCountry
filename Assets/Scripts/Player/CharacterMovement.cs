using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public ClickDetector detector;
    public float playerSpeed = 10f;
    public bool destinationReached = false;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 destination = detector.clickWorldPosition; 
        Vector3 move = destination - transform.position; //Vecteur de d�placement

        if (move.magnitude <= 1) //Si on est tr�s proche, on stoppe
        {
            destinationReached = true;
            return;
        }
        else
        {
            destinationReached = false;
        }

        controller.Move(move.normalized * Time.deltaTime * playerSpeed); ;  //D�placement � chaque frame selon la playerSpeed

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }


    }
}
