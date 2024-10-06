using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider masterSlider;

    private void Start() {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            LoadMasterVolume();
        }
        else
        {
            SetMasterVolume();
        }
    }
    public void SetMasterVolume()
    {
        float volume = masterSlider.value;
        if (masterSlider.value > 0)
        {
            myMixer.SetFloat("master", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("masterVolume", volume);
        }
        else
        {
            myMixer.SetFloat("master", -80);
        }
    }

    private void LoadMasterVolume()
    {
        if (PlayerPrefs.HasKey("masterVolume"))
        {
            masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        }
        else
		{
			masterSlider.value = 1.0f;
		}

        SetMasterVolume();
    }
}
