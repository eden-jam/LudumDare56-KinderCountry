using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    public Vector3 clickWorldPosition = Vector3.zero;
    public bool hasPlayerClicked = false;
    public Collider groundCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Si clic gauche appuyé
        {
            Vector3 mousePos = Input.mousePosition;  //Récup de la position de la souris à l'écran

            float horizontal = mousePos.x;
            float vertical = mousePos.y;

            Debug.Log(horizontal + " | " + vertical);

            //Transformer position du click en position dans le World
            Ray projectedPos = Camera.main.ScreenPointToRay(mousePos); //Rayon perpendiculaire au plan de la caméra qui passe par le point de clic

            
            if (groundCollider.Raycast(projectedPos, out RaycastHit raycastHit, 1000.0f)) //Collision Rayon <> collider du sol
            {
                // Stocker cette position
                clickWorldPosition = raycastHit.point;
            }


            hasPlayerClicked = true;
            
        }
        else
        {
            hasPlayerClicked = false;
        }
    }
}
