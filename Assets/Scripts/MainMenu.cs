using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using YG;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<string> _sceneList = new List<string>();
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private AudioSource _audio;
    [SerializeField] private YandexGame _yg;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private GameObject[] _lb;

    private void Start()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            _volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", 0.5f);
        }
        YandexGame.GameReadyAPI();
    }

    public void StartLevel(int id)
    {
        SceneManager.LoadScene(_sceneList[id]);
    }

    public void ChangeSettingsStatus(bool currentPhase)
    {
        _settingsPanel.SetActive(!currentPhase);
    }

    public void ChangeVolume(float volume)
    {
        _audio.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void ChangeWaitTime(string time)
    {
        PlayerPrefs.SetFloat("WaitTime", System.Single.Parse(time));
    }

    public void ChangeLanguage(string language)
    {
        YandexGame.EnvironmentData.language = language;
        Debug.Log(YandexGame.EnvironmentData.language);
    }

    public void OpenLeaderBoard(int difficult)
    {
        _lb[difficult].SetActive(true);
    }

    public void CloseLeaderBoard(int difficult)
    {
        _lb[difficult].SetActive(false);
    }

}
