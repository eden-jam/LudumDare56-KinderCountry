using System.Collections;
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
    public float _resetDelay = 2.0f;
    public Coroutine _coroutine = null;

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

        if (Input.GetKeyDown(KeyCode.Mouse0)) //Si clic gauche appuyé
		{
			hasPlayerClicked = true;
		}
		else
        {
			hasPlayerClicked = false;
        }
    }

    private void UpdateClickWorldPosition()
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
		switch (_currentState)
		{
			case State.SpawnFlee:
				{
					SpawnFlee();
					ResetAction();
				}
				break;
			case State.Lure:
				{
					Lure();
					ResetAction();
				}
				break;
			case State.Cry:
				{
					Cry();
					ResetAction();
				}
				break;
			default:
				break;
		}
	}

	public void Lure()
	{
		BoidsManager.Instance.Lure();
	}

	public void Cry()
	{
		BoidsManager.Instance.Cry();
	}

	private void ResetAction()
	{
		if (_coroutine != null)
		{
			StopCoroutine(_coroutine);
		}
		_coroutine = StartCoroutine(DelayAction(_resetDelay));
	}

	IEnumerator DelayAction(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		FindAnyObjectByType<ActiveHUDBoc>().ToggleActiveSlot(1);
	}

	public void SpawnFlee()
	{
		BoidsManager.Instance.SpawnFlee();
	}
}
