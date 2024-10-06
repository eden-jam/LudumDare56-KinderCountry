using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickDetector : MonoBehaviour
{
	public static ClickDetector Instance = null;

	public Vector3 clickWorldPosition = Vector3.zero;
    public bool hasPlayerClicked = false;
    public bool isPlayerHolding = false;
    public Collider groundCollider;
	public CharacterState.State _currentState = CharacterState.State.Move;
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
			if (_currentState == CharacterState.State.Move)
			{
				UpdateClickWorldPosition();
			}
		}

        if (Input.GetKeyDown(KeyCode.Mouse0)) //Si clic gauche appuyé
		{
			switch (_currentState)
			{
				case CharacterState.State.Move:
					{
						// Already done in the previous if
					}
					break;
				case CharacterState.State.SpawnFlee:
					{
						SpawnFlee();
					}
					break;
				case CharacterState.State.Lure:
					{
						Lure();
					}
					break;
				case CharacterState.State.Cry:
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

	public void SetState(CharacterState.State state)
	{
		_currentState = state;
		switch (_currentState)
		{
			case CharacterState.State.SpawnFlee:
				{
					SpawnFlee();
					ResetAction();
				}
				break;
			case CharacterState.State.Lure:
				{
					Lure();
					ResetAction();
				}
				break;
			case CharacterState.State.Cry:
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
