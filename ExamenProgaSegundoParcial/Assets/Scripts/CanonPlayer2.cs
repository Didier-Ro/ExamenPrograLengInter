using UnityEngine;
using TMPro;

public class CanonPlayer2 : MonoBehaviour
{
    [SerializeField] private CanvasController _canvasController = default;

    [SerializeField] private Camera _camera = default;
    [SerializeField] private BulletPlayerTwo _bulletPrefab2 = default;
    [SerializeField] private Transform _firePoint2 = default;

    [SerializeField] private float _finalForce = default;
    [SerializeField] private float _minForce = 100f;
    [SerializeField] private float _maxForce = 1000f;
    [SerializeField] private float _totalTimer = 3f;
    private float _currentTime = default;

    public bool playerTwoTurn = default;

    public float timer = 0f;
    [SerializeField] private float maxTime = 45f;
    [SerializeField] private GameObject _timerText = default;

    private void Start()
    {
        _canvasController.ActivateForcePlayer2(false);
    }
    void Update()
    {
        if (playerTwoTurn == true)
        {
            timer += Time.deltaTime;
            _timerText.GetComponent<TextMeshProUGUI>().text = "" + timer.ToString("f0");

            if (timer >= maxTime)
            {
                GameController.Instance.PlayerOneCoroutine();
                timer = 0;
                playerTwoTurn = false;
            }

            Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0.0f;

            LookAt2D(mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                _currentTime = 0.0f;
                _canvasController.SetValuePlayerTwo(0);
                _canvasController.ActivateForcePlayer2(true);
            }

            if (Input.GetMouseButton(0))
            {
                _currentTime += Time.deltaTime;
                _finalForce = Mathf.Lerp(_minForce, _maxForce, _currentTime / _totalTimer);
                _canvasController.SetValuePlayerTwo(_currentTime / _totalTimer);
            }

            if (Input.GetMouseButtonUp(0))
            {
                Shoot();
                _canvasController.ActivateForcePlayer2(false);
            }
        }
    }

    private void LookAt2D(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        dir.Normalize();
        transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);
    }
    private void Shoot()
    {
        BulletPlayerTwo bullet = Instantiate(_bulletPrefab2, _firePoint2.position, _firePoint2.rotation);
        bullet.Projectile(-gameObject.transform.right, _finalForce);
        AudioManager.Instance.AudioSelect(0, 1f);
        GameController.Instance.PlayerOneCoroutine();
    }
}
