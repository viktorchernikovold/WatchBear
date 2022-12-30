using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float RotationSpeed = 720f;

    Vector2 mousePos;

    Camera mainCam;

    Quaternion targetRot;

    private void Awake()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        targetRot = Quaternion.Euler(0f, 0f, Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, RotationSpeed * Time.deltaTime * Time.timeScale);
    }
}
