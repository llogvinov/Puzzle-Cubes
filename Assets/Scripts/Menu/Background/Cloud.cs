using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _ySpawnPosition;

    private const float BottomBound = -12f;

    private void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < BottomBound)
        {
            transform.position = new Vector3(transform.position.x, _ySpawnPosition, transform.position.z);
        }
    }
}