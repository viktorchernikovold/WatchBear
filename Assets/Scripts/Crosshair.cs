using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Crosshair : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Light2D light;
    public float rotateSpeed;
    public Camera targetCam;
    public void Update()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = 10;
        Vector3 pos = targetCam.ScreenToWorldPoint(screenPosition);
        transform.position = pos;
        transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
    }
    public void FixedUpdate()
    {
        if (GameManager.main.isPlaying)
        {
            renderer.enabled = true;
            light.enabled = true;
        }
        else
        {
            renderer.enabled = false;
            light.enabled = false;
        }
    }
}
