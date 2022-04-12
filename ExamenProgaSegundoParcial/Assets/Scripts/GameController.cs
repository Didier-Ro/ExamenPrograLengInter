using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    [SerializeField] private int _playerTurn = default;

    [SerializeField] private CanonMovement _player = default;
    [SerializeField] private CanonPlayer2 _player2 = default;

    [SerializeField] private float delayTime = 3f;

    [SerializeField] private GameObject playerOneTurnText = default;
    [SerializeField] private GameObject playerTwoTurnText = default;
    [SerializeField] private GameObject playerOneVictory = default;
    [SerializeField] private GameObject playerTwoVictory = default;
    [SerializeField] private GameObject playAgainButton = default;

    [SerializeField] private GameObject Tank1 = default;
    [SerializeField] private GameObject Tank2 = default;
    [SerializeField] private GameObject destructionAnim = default;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        _playerTurn = Random.Range(0, 10);

        if (_playerTurn <= 5)
        {
            PlayerOneTurn();
        }

        if (_playerTurn > 5)
        {
            PlayerTwoTurn();
        }
    }

    private void PlayerOneTurn()
    {
        _player.playerOneTurn = true;
        _player2.playerTwoTurn = false;
        _player2.timer = 0;
        StartCoroutine(AppearPlayerOneText());
    }

    IEnumerator AppearPlayerOneText()
    {
        playerOneTurnText.SetActive(true);
        yield return new WaitForSeconds(2f);
        playerOneTurnText.SetActive(false);
    }

    private void PlayerTwoTurn()
    {
        _player.playerOneTurn = false;
        _player2.playerTwoTurn = true;
        _player.timer = 0;
        StartCoroutine(AppearPlayerTwoText());
    }

    IEnumerator AppearPlayerTwoText()
    {
        playerTwoTurnText.SetActive(true);
        yield return new WaitForSeconds(2f);
        playerTwoTurnText.SetActive(false);
    }

    public void PlayerOneDied()
    {
        _player.playerOneTurn = false;
        _player2.playerTwoTurn = false;
        playerOneTurnText.SetActive(false);
        playerTwoTurnText.SetActive(false);
        Tank1.SetActive(false);
        GameObject destruction = Instantiate(destructionAnim, Tank1.transform.position, Quaternion.identity);
        Destroy(destruction, 1.5f);
        playerTwoVictory.SetActive(true);
        playAgainButton.SetActive(true);
    }

    public void PlayerTwoDied()
    {
        _player.playerOneTurn = false;
        _player2.playerTwoTurn = false;
        Tank2.SetActive(false);
        GameObject destruction = Instantiate(destructionAnim, Tank2.transform.position, Quaternion.identity);
        Destroy(destruction, 1.5f);
        playerOneVictory.SetActive(true);
        playAgainButton.SetActive(true);
    }

    public void PlayerOneCoroutine()
    {
        StartCoroutine(PlayerOne());
    }

    public void PlayerTwoCoroutine()
    {
        StartCoroutine(PlayerTwo());
    }

    IEnumerator PlayerOne()
    {
        yield return new WaitForSeconds(delayTime);
        PlayerOneTurn();
    }

    IEnumerator PlayerTwo()
    {
        yield return new WaitForSeconds(delayTime);
        PlayerTwoTurn();
    }
}
