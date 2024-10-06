using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
	[SerializeField] private Transform _kingBlob;
	[SerializeField] private float _ratio = 1.0f;
	private Vector3 _startScale;

	private bool _hasFinish = false;

	private void Start()
	{
		_startScale = _kingBlob.localScale;
	}

	public void IncreaseSize(int oldSize, int newOld)
	{
		_kingBlob.localScale = newOld * _ratio * _startScale;
	}

	public void Finish()
	{
		//Destroy(SoundManager.Instance.gameObject);
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
