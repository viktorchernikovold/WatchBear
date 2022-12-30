using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerLook : MonoBehaviour
{
    public Transform weaponOrigin;
    public Transform playerGraphics;
    public Transform weaponGraphics;

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

        if (GetMousePositionWp().x > transform.position.x)
        {
            playerGraphics.localRotation = Quaternion.Euler(0f, 180f, 0f);
            weaponGraphics.localRotation = Quaternion.Euler(0f, 180f, -90f);
        }
        else
        {
            playerGraphics.localRotation = Quaternion.Euler(Vector3.zero);
            weaponGraphics.localRotation = Quaternion.Euler(0f, 0f, -90f);
        }
    }
}
