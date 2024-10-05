using UnityEngine;

public enum State
{
	Move,
	SpawnFlee,
    Lure,
    Cry
}

public class ClickDetector : MonoBehaviour
{
	public static ClickDetector Instance = null;

	public Vector3 clickWorldPosition = Vector3.zero;
    public bool hasPlayerClicked = false;
    public Collider groundCollider;
    public State _currentState = State.Move;

	private void Awake()
	{
		Instance = this;
	}

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Si clic gauche appuyé
		{
			switch (_currentState)
			{
				case State.Move:
					{
						Move();
					}
					break;
				case State.SpawnFlee:
					{
						SpawnFlee();
					}
					break;
				case State.Lure:
					{
						Lure();
					}
					break;
				case State.Cry:
					{
						Cry();
					}
					break;
				default:
					break;
			}

			hasPlayerClicked = true;
		}
        else
        {
            hasPlayerClicked = false;
        }
    }

    private void Move()
	{
		Vector3 mousePos = Input.mousePosition;  //Récup de la position de la souris à l'écran

		float horizontal = mousePos.x;
		float vertical = mousePos.y;

		//Debug.Log(horizontal + " | " + vertical);

		//Transformer position du click en position dans le World
		Ray projectedPos = Camera.main.ScreenPointToRay(mousePos); //Rayon perpendiculaire au plan de la caméra qui passe par le point de clic


		if (groundCollider.Raycast(projectedPos, out RaycastHit raycastHit, 1000.0f)) //Collision Rayon <> collider du sol
		{
			// Stocker cette position
			clickWorldPosition = raycastHit.point;
		}
	}

	public void SetState(State state)
	{
		_currentState = state;
	}

	public void Lure()
	{
		BoidsManager.Instance.Lure();
	}

	public void Cry()
	{
		BoidsManager.Instance.Cry();
	}

	public void SpawnFlee()
	{
		Vector3 mousePos = Input.mousePosition;  //Récup de la position de la souris à l'écran

		float horizontal = mousePos.x;
		float vertical = mousePos.y;

		//Debug.Log(horizontal + " | " + vertical);

		//Transformer position du click en position dans le World
		Ray projectedPos = Camera.main.ScreenPointToRay(mousePos); //Rayon perpendiculaire au plan de la caméra qui passe par le point de clic


		if (groundCollider.Raycast(projectedPos, out RaycastHit raycastHit, 1000.0f)) //Collision Rayon <> collider du sol
		{
			// Stocker cette position
			BoidsManager.Instance.SpawnFlee(raycastHit.point);
		}
	}
}
