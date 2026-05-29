using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public bool lookEnabled = true;

    void Start () {
        Cursor.visible = true;
    }
    public Camera cam;
    private float xRotation = 0f;

    public float xSensitivity = 2f;
    public float ySensitivity = 2f;

    public void ProcessLook(Vector2 input)
    {
        if (!lookEnabled) return;
        float mouseX = input.x;
        float mouseY = input.y;

        //camera rotation for moving up and down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // rotate player left and right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);

    }
}
