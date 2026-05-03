using UnityEngine;

public class CrosshairShooter : MonoBehaviour
{
    public LayerMask shootableLayers = ~0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShootFromCrosshair();
        }
    }

    void ShootFromCrosshair()
    {
        if (Gun.Instance != null && Gun.Instance.isGunEmpty)
        {
            return;
        }

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
        }
    }
}