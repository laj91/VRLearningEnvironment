using UnityEditor;
using UnityEngine;

public class AddPlateController : MonoBehaviour
{
    [MenuItem("Tools/Add PlateController to all plates")]
    private static void AddControllerToPlates()
    {
        foreach (GameObject plate in GameObject.FindGameObjectsWithTag("Plate"))
        {
            if (plate.GetComponent<PlateInteractionController>() == null)
            {
                plate.AddComponent<PlateInteractionController>();
            }
        }
    }
}