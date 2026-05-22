using UnityEngine;

public class OscillatorScript : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    Vector3 startPosition;
    Vector3 endPosition;
    float movementFactor;
    [SerializeField] float speed;
    void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition + movementVector;
    }

    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1.0f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }
}
