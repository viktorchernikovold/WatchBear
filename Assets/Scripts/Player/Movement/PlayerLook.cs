using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerLook : PlayerComponent
{
    public Camera aimCamera;
    public Transform aimPoint;
    public Transform weaponOrigin;
    public Transform playerGraphics;
    public Transform weaponGraphics;

    public float aimPointMax = 5f;
    public float aimPointMin = 1f;



    public float GetLookRotation()
    {
        Vector3 mousePos = GetMousePositionWp();
        return Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
    }

    public Quaternion GetLookRotationQ()
    {
        return Quaternion.Euler(0, 0, GetLookRotation());
    }

    public Vector2 GetMousePositionWp()
    {
        return aimCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void Update()
    {
        Quaternion rotation = GetLookRotationQ();
        float pointDistance = Vector2.Distance(GetMousePositionWp(), transform.position);
        if (pointDistance > aimPointMax) pointDistance = aimPointMax;
        if (pointDistance <= aimPointMin)
        {
            aimPoint.localPosition = Vector3.zero;
        }
        else
        {
            aimPoint.localPosition = rotation * Vector3.up * pointDistance;
        }
        weaponOrigin.rotation = rotation;

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
