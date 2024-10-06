using UnityEngine;

public class CharacterState : MonoBehaviour
{
	public enum State
	{
		Move,
		SpawnFlee,
		Lure,
		Cry
	}

	public Animator animator;

	public void OnMove()
	{
		animator.SetInteger("PlayerState", (int)State.Move);
	}

	public void OnLure()
    {
		animator.SetInteger("PlayerState", (int)State.Lure);
		animator.SetTrigger("PlayAnimation");
	}
	
	public void OnSpawnFlee()
	{
		animator.SetInteger("PlayerState", (int)State.SpawnFlee);
		animator.SetTrigger("PlayAnimation");
	}

	public void OnCry()
    {
		animator.SetInteger("PlayerState", (int)State.Cry);
		animator.SetTrigger("PlayAnimation");
	}
}
