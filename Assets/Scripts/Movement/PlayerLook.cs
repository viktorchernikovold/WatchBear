using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerLook : MonoBehaviour
{
    public Transform weaponOrigin;
    public float RotationSpeed = 720f;

    Vector3 mousePositionWp;

    Camera mainCam;

    private void Awake()
    {
        mainCam = Camera.main;
    }
    public float GetLookRotation()
    {
        Vector3 mousePos = GetMousePositionWp();
        return Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
    }
    public Quaternion GetLookRotationQ()
    {
        return Quaternion.Euler(0, 0, GetLookRotation());
    }
    public Vector3 GetMousePositionWp()
    {
        return mainCam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void Update()
    {

        weaponOrigin.rotation = GetLookRotationQ();
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, RotationSpeed * Time.deltaTime * Time.timeScale);
    }
}
