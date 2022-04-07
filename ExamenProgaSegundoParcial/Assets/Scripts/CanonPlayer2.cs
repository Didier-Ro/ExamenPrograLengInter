using UnityEngine;

public class CanonPlayer2 : MonoBehaviour
{
    [SerializeField] private Camera _camera = default;

    void Update()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;

        LookAt2D(mousePosition);
    }

    private void LookAt2D(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        dir.Normalize();
        transform.rotation = Quaternion.FromToRotation(Vector3.left, dir);
    }
}
