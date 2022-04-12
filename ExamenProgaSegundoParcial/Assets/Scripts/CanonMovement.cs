using UnityEngine;
using TMPro;

public class CanonMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera = default;

    [SerializeField] private CanvasController _canvasController = default;

    [SerializeField] private Bullet _bulletPrefab = default;
    [SerializeField] private Transform _firePoint = default;

    [SerializeField] private float _finalForce = default;
    [SerializeField] private float _minForce = 100f;
    [SerializeField] private float _maxForce = 1000f;
    [SerializeField] private float _totalTimer = 3f;
    private float _currentTime = default;

    public bool playerOneTurn = false;

    public float timer = default;
    [SerializeField] private float maxTime = 45f;
    [SerializeField] private GameObject _timerText = default;

    private void Start()
    {
        _canvasController.ActivateForcePlayer(false);
    }

    void Update()
    {
        if (playerOneTurn == true)
        {
            timer += Time.deltaTime;
            _timerText.GetComponent<TextMeshProUGUI>().text = "" + timer.ToString("f0");

            if (timer >= maxTime)
            {
                GameController.Instance.PlayerTwoCoroutine();
                timer = 0;
                playerOneTurn = false;
            }
            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0.0f;

            LookAt2D(mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                _currentTime = 0.0f;
                _canvasController.SetValuePlayerOne(0);
                _canvasController.ActivateForcePlayer(true);
            }

            if (Input.GetMouseButton(0))
            {
                _currentTime += Time.deltaTime;
                _finalForce = Mathf.Lerp(_minForce, _maxForce, _currentTime / _totalTimer);
                _canvasController.SetValuePlayerOne(_currentTime / _totalTimer);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Shoot();
                _canvasController.ActivateForcePlayer(false);
            }
        }
    }

    private void LookAt2D(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        dir.Normalize();
        transform.rotation = Quaternion.FromToRotation(Vector3.right, dir);
    }

    public void ChangeTurn()
    {
        playerOneTurn = true;
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
        bullet.Projectile(gameObject.transform.right, _finalForce);
        AudioManager.Instance.AudioSelect(0, 1f);
        GameController.Instance.PlayerTwoCoroutine();
    }
}
