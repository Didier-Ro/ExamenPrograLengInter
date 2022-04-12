using UnityEngine;

public class PlayerTwo : MonoBehaviour
{
    [SerializeField] private CanvasController _canvasController = default;
    [SerializeField] private CanonPlayer2 _canon2 = default;
    private Rigidbody2D _rigidBody2D = default;
    [SerializeField] private float _playerSpeed = 10;
    private Vector2 _movement = default;

    [SerializeField] private int life = 100;
    [SerializeField] private int damageReceived = 25;

    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _canvasController.SetPlayerTwoLife(life);
    }

    void Update()
    {
        if (_canon2.playerTwoTurn == true)
        {
            _movement.x = Input.GetAxisRaw("Horizontal");
        }

        if (life <= 0)
        {
            GameController.Instance.PlayerTwoDied();
        }
    }

    private void FixedUpdate()
    {
        _rigidBody2D.AddForce(_movement * _playerSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            life -= damageReceived;
            _canvasController.SetPlayerTwoLife(life);
        }
    }
}
