using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private GameObject hit = default;
    [SerializeField] private Rigidbody2D _rigidBody2D = default;
    //[SerializeField] private float _bulletSpeed = 5000; 
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Projectile(Vector3 direction, float force)
    {
        _rigidBody2D.AddForce(direction * force);
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hit, gameObject.transform.position, Quaternion.identity);
        AudioManager.Instance.AudioSelect(1, 1f);
        Destroy(gameObject);
        Destroy(effect, 1f);
    }
}
