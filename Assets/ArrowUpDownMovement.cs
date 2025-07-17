using UnityEngine;

public class ArrowUpDownMovement : MonoBehaviour
{
    [SerializeField] float speed = 2f;  // Hastighed for bevægelsen
    [SerializeField] float amplitude = 3f;  // Antal enheder objektet bevæger sig op og ned

    private float startY;

    void Start()
    {
        startY = transform.position.y;  // Gem startpositionen
    }

    void Update()
    {
        float newY = startY + Mathf.PingPong(Time.time * speed, amplitude) - (amplitude / 2);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
