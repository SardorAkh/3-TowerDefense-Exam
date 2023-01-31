using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _settingsPanel;


    [SerializeField] private Slider _masterSlider;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundEffectsSlider;
    [SerializeField] private AudioMixer _mix;

    private void Start()
    {

        _masterSlider.onValueChanged.AddListener(ChangeMasterVolume);
        _musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        _soundEffectsSlider.onValueChanged.AddListener(ChangesoundEffectsVolume);

        _masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 0.8f);
        _musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0.8f);
        _soundEffectsSlider.value = PlayerPrefs.GetFloat("soundEffectsVolume", 0.8f);

        _mix.SetFloat("Master", Mathf.Log10(_masterSlider.value) * 80);
        _mix.SetFloat("Music", Mathf.Log10(_musicSlider.value) * 80);
        _mix.SetFloat("SoundEffects", Mathf.Log10(_soundEffectsSlider.value) * 80);
    }
    private void ChangeMasterVolume(float value)
    {
        float v = Mathf.Log10(value) * 80;
        _mix.SetFloat("Master", v);
    }
    private void ChangeMusicVolume(float value)
    {
        float v = Mathf.Log10(value) * 80;
        _mix.SetFloat("Music", v);
    }
    private void ChangesoundEffectsVolume(float value)
    {
        float v = Mathf.Log10(value) * 80;
        _mix.SetFloat("SoundEffects", v);
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("masterVolume", _masterSlider.value);
        PlayerPrefs.SetFloat("musicVolume", _musicSlider.value);
        PlayerPrefs.SetFloat("soundEffectsVolume", _soundEffectsSlider.value);
    }
    public void OpenSettings()
    {
        _mainPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }
    public void BackToMainMenu()
    {
        _mainPanel.SetActive(true);
        _settingsPanel.SetActive(false);

    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
