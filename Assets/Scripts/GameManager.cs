using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private Button[] _gameSpeedButtons;
    [SerializeField] private GameObject _menuPanel;
    private bool _isMenuActive;
    private Color _btnOriginColor;
    private void Start()
    {
        _btnOriginColor = _gameSpeedButtons[0].GetComponent<Image>().color;
        GlobalEvent.OnLose += HandleOnLose;
        GlobalEvent.OnWin += HandleOnWin;

        _gameSpeedButtons[0].onClick.AddListener(StopSpeedGame);
        _gameSpeedButtons[1].onClick.AddListener(NormalizeSpeedGame);
        _gameSpeedButtons[2].onClick.AddListener(SpeedUpGame);


    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isMenuActive = !_isMenuActive;
            _menuPanel.SetActive(_isMenuActive);
            Time.timeScale = _isMenuActive ? 0 : 1;
        }
    }

    public void Resume()
    {

        _isMenuActive = !_isMenuActive;
        _menuPanel.SetActive(_isMenuActive);
        Time.timeScale = _isMenuActive ? 0 : 1;
    }
    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void HandleOnLose()
    {
        _losePanel.SetActive(true);
        Time.timeScale = 0;
    }
    private void HandleOnWin()
    {
        _winPanel.SetActive(true);
        Time.timeScale = 0;
    }
    
    private void StopSpeedGame()
    {
        ResetButtonsColor();
        _gameSpeedButtons[0].GetComponent<Image>().color = Color.red;
        Time.timeScale = 0;
    }

    private void NormalizeSpeedGame()
    {
        ResetButtonsColor();
        _gameSpeedButtons[1].GetComponent<Image>().color = Color.red;
        Time.timeScale = 1;
    }

    private void SpeedUpGame()
    {
        ResetButtonsColor();
        _gameSpeedButtons[2].GetComponent<Image>().color = Color.red;
        Time.timeScale = 2;
    }

    private void ResetButtonsColor()
    {
        for (int i = 0; i < _gameSpeedButtons.Length; i++)
        {
            _gameSpeedButtons[i].GetComponent<Image>().color = _btnOriginColor;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void OnDestroy()
    {

        GlobalEvent.OnLose -= HandleOnLose;
        GlobalEvent.OnWin -= HandleOnWin;
    }

}
