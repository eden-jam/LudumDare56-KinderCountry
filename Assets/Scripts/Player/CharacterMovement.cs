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
        Vector3 move = destination - transform.position; //Vecteur de déplacement

        controller.transform.rotation = Quaternion.identity; //Forcer la rotation à 0

        if (move.magnitude <= 1) //Si on est très proche, on stoppe
        {
            destinationReached = true;
            return;
        }
        else
        {
            destinationReached = false;
        }

        //controller.Move(move.normalized * Time.deltaTime * playerSpeed);  //Déplacement à chaque frame selon la playerSpeed
        controller.SimpleMove(move.normalized * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }


    }
}
