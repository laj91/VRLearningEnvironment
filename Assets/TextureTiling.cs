using UnityEngine;

[ExecuteInEditMode] // G�r det muligt at opdatere i Editor Mode
public class TextureAdjuster : MonoBehaviour
{
    public Vector2 tiling = new Vector2(1, 1); // Juster tiling
    public Vector2 offset = Vector2.zero;      // Juster offset (position af tekstur)
    public float rotationAngle = 0f;            // Rotation i grader

    void OnValidate() // Kalds n�r der sker en �ndring i Inspector
    {
        ApplyTextureProperties();
        //Debug.Log("I onvalidate");
    }

    void ApplyTextureProperties()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            //Debug.Log("I if s�tning");
            //Debug.Log("Material: " + renderer.sharedMaterial);

            MaterialPropertyBlock block = new MaterialPropertyBlock();

            // Test kun tiling og offset uden rotation
            block.SetVector("_MainTex_ST", new Vector4(tiling.x, tiling.y, offset.x, offset.y));

            // Anvend Material Property Block p� objektet
            renderer.SetPropertyBlock(block);
        }
    }
}

//    void ApplyTextureProperties()
//    {
//        Renderer renderer = GetComponent<Renderer>();
//        Debug.Log("I apply metode");
//        if (renderer != null)
//        {
//            Debug.Log("I if s�tning");
//            MaterialPropertyBlock block = new MaterialPropertyBlock();

//            // Beregn rotation som offset
//            float radians = rotationAngle * Mathf.Deg2Rad;
//            Vector2 rotatedOffset = new Vector2(
//                Mathf.Cos(radians),
//                Mathf.Sin(radians)
//            );

//            // S�t tiling og offset i Material Property Block
//            block.SetVector("_MainTex_ST", new Vector4(tiling.x, tiling.y, offset.x + rotatedOffset.x, offset.y + rotatedOffset.y));

//            // Anvend Material Property Block p� objektet
//            renderer.SetPropertyBlock(block);
//        }
//    }
//}
