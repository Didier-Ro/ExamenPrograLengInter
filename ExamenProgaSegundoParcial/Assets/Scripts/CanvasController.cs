using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private Slider _forcePlayerOneSlider = default;
    [SerializeField] private Slider _forcePlayerTwoSlider = default;
    [SerializeField] private Slider _playerOneLife = default;
    [SerializeField] private Slider _playerTwoLife = default;
    
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void ActivateForcePlayer(bool activate)
    {
        _forcePlayerOneSlider.gameObject.SetActive(activate);
    }

    public void ActivateForcePlayer2(bool activate)
    {
        _forcePlayerTwoSlider.gameObject.SetActive(activate);
    }

    public void SetValuePlayerOne(float value)
    {
        _forcePlayerOneSlider.value = value;
    }

    public void SetValuePlayerTwo(float value)
    {
        _forcePlayerTwoSlider.value = value;
    }

    public void SetPlayerOneLife(int life)
    {
        _playerOneLife.value = life;
    }

    public void SetPlayerTwoLife(int life)
    {
        _playerTwoLife.value = life;
    }
}
