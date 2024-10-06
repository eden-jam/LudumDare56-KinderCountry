using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class FinishManager : MonoBehaviour
{
	public static FinishManager Instance = null;

	private void Awake()
	{
		Instance = this;
	}

	[SerializeField] private GameObject _endUI;
	[SerializeField] private TextMeshProUGUI _endCount;
	[SerializeField] private PlayableDirector _endSequence;

	private bool _hasFinish = false;

	public void Finish()
	{
		if (_hasFinish)
			return;
		_hasFinish = true;
		ClickDetector.Instance.enabled = false;
		
		_endSequence.Play();
		_endSequence.stopped += OnAnimationEnded;
	}

	private void OnAnimationEnded(PlayableDirector director)
	{
		director.stopped -= OnAnimationEnded;
		_endCount.text = Time.timeSinceLevelLoad.ToString("00") + "s";
		_endUI.SetActive(true);
	}

	public void Restart()
	{
		SceneManager.LoadScene("MainMenu");
	}

}
