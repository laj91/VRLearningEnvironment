using UnityEngine;

public class AddPointsShooting : MonoBehaviour
{
    [SerializeField] ShootingGameManager gameManager;

    private bool hasBeenHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody Rb = gameObject.GetComponent<Rigidbody>();

        if (collision.gameObject.tag == "Bullet" && hasBeenHit == false)
        {
            //Hvis ikke allerede du er blevet ramt, så tilføj point
            gameManager.AddPoints();
            hasBeenHit = true;

            Debug.Log($"Rigidbody kinemtaic on {gameObject.name} is {Rb.isKinematic}");
            if (Rb.isKinematic == true)
                Rb.isKinematic = false;
                Debug.Log($"Rigidbody kinemtaic on {gameObject.name} is {Rb.isKinematic}");

        }
            


    }
}
