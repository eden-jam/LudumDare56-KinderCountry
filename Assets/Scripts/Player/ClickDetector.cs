using UnityEngine;
using UnityEngine.EventSystems;

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
    public bool isPlayerHolding = false;
    public Collider groundCollider;
    public State _currentState = State.Move;

	private void Awake()
	{
		Instance = this;
	}

	void Update()
    {
		if (EventSystem.current.IsPointerOverGameObject())
		{
			return;
		}

		isPlayerHolding = Input.GetMouseButton(0);
        if (Input.GetKeyDown(KeyCode.Mouse0) || isPlayerHolding)
        {
			if (_currentState == State.Move)
			{
				UpdateClickWorldPosition();
			}
		}

        if (Input.GetKeyDown(KeyCode.Mouse0)) //Si clic gauche appuy�
		{
			switch (_currentState)
			{
				case State.Move:
					{
						// Already done in the previous if
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

    private void UpdateClickWorldPosition()
	{
		Vector3 mousePos = Input.mousePosition;  //R�cup de la position de la souris � l'�cran

		float horizontal = mousePos.x;
		float vertical = mousePos.y;

		//Debug.Log(horizontal + " | " + vertical);

		//Transformer position du click en position dans le World
		Ray projectedPos = Camera.main.ScreenPointToRay(mousePos); //Rayon perpendiculaire au plan de la cam�ra qui passe par le point de clic


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
		Vector3 mousePos = Input.mousePosition;  //R�cup de la position de la souris � l'�cran

		float horizontal = mousePos.x;
		float vertical = mousePos.y;

		//Debug.Log(horizontal + " | " + vertical);

		//Transformer position du click en position dans le World
		Ray projectedPos = Camera.main.ScreenPointToRay(mousePos); //Rayon perpendiculaire au plan de la cam�ra qui passe par le point de clic


		if (groundCollider.Raycast(projectedPos, out RaycastHit raycastHit, 1000.0f)) //Collision Rayon <> collider du sol
		{
			// Stocker cette position
			BoidsManager.Instance.SpawnFlee(raycastHit.point);
		}
	}
}
