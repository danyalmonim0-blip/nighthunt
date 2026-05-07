using UnityEngine;

public class CrosshairShooter : MonoBehaviour
{
    public LayerMask shootableLayers = ~0;

    void Update()
    {
        //This statement in the mouse click command to shoot.
        if (Input.GetMouseButtonDown(0))
        {
            ShootFromCrosshair();
        }
        else { }
    }

    void ShootFromCrosshair()
    {
        //This funtion enables shooting from the crosshair instead of mouse pointer.
        if (Gun.Instance != null && Gun.Instance.isGunEmpty)
        {
            return;
        }
        else { }

        Vector2 shootPoint = transform.position;
        Collider2D[] hits = Physics2D.OverlapPointAll(shootPoint, shootableLayers);

        if (hits == null || hits.Length == 0)
        {
            return;
        }
        for (int i = 0; i < hits.Length; i++)
        {
            ClickableTarget target = hits[i].GetComponent<ClickableTarget>();

            if (target != null && target.isDead == false)
            {
                target.GetShot();
                return;
            }
            else { }
        }
    }
}