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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePos = Input.mousePosition;

            float horizontal = mousePos.x;
            float vertical = mousePos.y;

            Debug.Log(horizontal + " | " + vertical);

            //Transformer position du click en position dans le World
            Ray projectedPos = Camera.main.ScreenPointToRay(mousePos);

            
            if (groundCollider.Raycast(projectedPos, out RaycastHit raycastHit, 1000.0f))
            {
                // Stocker cette position
                clickWorldPosition = raycastHit.point;
            }


            hasPlayerClicked = true;
            
        }

    }
}
