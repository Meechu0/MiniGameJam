using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private float range = 8f;
    [SerializeField] private Animator bulletAnimatior;
    [SerializeField] private SecondShotgun _secondShotgun;
    [SerializeField] private float knockbackAmount;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -89, 89);

        if(angle >= -89 && angle < 89)
        {
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = rotation;
        }

        Shoot();
    }
    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("shoot");
            bulletAnimatior.SetTrigger("Shoot");

            //Ray
            var hit = Physics2D.Raycast(firePoint.position, transform.right, range);

            var trail = Instantiate(bulletTrail, firePoint.position, transform.rotation);

            var trailScript = trail.GetComponent<bulletTrail>();

            if(hit.collider !=null)
            {
                trailScript.setTargerPosition(hit.point);
                print(hit.collider);
                if (hit.collider.gameObject.CompareTag("enemy"))
                {
                    Destroy(hit.collider.gameObject.GetComponent<Collider2D>()) ;
                    hit.collider.GetComponent<EnemyDeath>().Die();

                    Vector2 difference = (transform.position - hit.collider.transform.position).normalized;
                    Vector2 force = difference * knockbackAmount;
                    hit.collider.GetComponent<Rigidbody2D>().AddForce(-force, ForceMode2D.Impulse);
                    _secondShotgun.AddBullet();


                }
            }
            else
            {
                
                
                var endPos = firePoint.position + transform.right * range;
                trailScript.setTargerPosition(endPos);

            }


        }
    }
}
