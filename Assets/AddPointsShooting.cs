using UnityEngine;

public class AddPointsShooting : MonoBehaviour
{
    [SerializeField] ShootingGameManager gameManager;

    private bool hasBeenHit = false;

    // Called when this object collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody Rb = gameObject.GetComponent<Rigidbody>();

        // If hit by a bullet and not already hit, add points
        if (collision.gameObject.tag == "Bullet" && hasBeenHit == false)
        {
            gameManager.AddPoints();
            hasBeenHit = true;

            Debug.Log($"Rigidbody kinematic on {gameObject.name} is {Rb.isKinematic}");
            // If the rigidbody is kinematic, set it to non-kinematic
            if (Rb.isKinematic == true)
                Rb.isKinematic = false;
            Debug.Log($"Rigidbody kinematic on {gameObject.name} is {Rb.isKinematic}");
        }
    }
}
