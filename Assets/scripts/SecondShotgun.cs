using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondShotgun : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletTrail;
    [SerializeField] private float range = 8f;
    [SerializeField] private Animator bulletAnimatior;
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private float force;
    [SerializeField] public int BulletCount ;
    [SerializeField] public int maxBulletCount;
    [SerializeField] private Bullets _bulletsSpriteScript;
    [SerializeField] private GameObject groundCheckCastOpject;

    private float timer;
    [SerializeField]private float bulletRechargeTime ;
    public bool isOnGround;

    [SerializeField]private AudioClip jumpSound;
    [SerializeField] private AudioSource _audioSRC;



    // Start is called before the first frame update
    void Start()
    {
        _audioSRC = GameObject.Find("Audio Source").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        LayerMask LayerMask = LayerMask.GetMask("Ground");
        var groudHit = Physics2D.Raycast(groundCheckCastOpject.transform.position, -Vector2.up, 1.5f, LayerMask);
        //var hit = Physics2D.Raycast(firePoint.position, -transform.right, range);

        if (groudHit.collider != null)
        {
            isOnGround = true;
            timer += Time.deltaTime;
        }
        else
        {
            isOnGround = false;
        }
        

        if (timer >= bulletRechargeTime)
        {
            timer = 0f;
            AddBullet();
           // print(BulletCount);

        }

        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, -89, 89);

        if (angle >= -89 && angle < 89)
        {
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = rotation;
        }

        Shoot();
    }
    private void Shoot()
    {
        if (cool)
        {

            if (Input.GetMouseButtonDown(1))
            {
                //start shoot cooldown
                CooldownStart();
                // check for bullets
                if (BulletCount > 0)
                {
                    _audioSRC.PlayOneShot(jumpSound, 1f);
                    bulletAnimatior.SetTrigger("Shoot");
                    BulletCount -= 1;
                    _bulletsSpriteScript.updateBulletSprites();
                    // add force when shooting
                    var direction = transform.position - firePoint.transform.position;
                    playerRb.AddForce(direction * force, ForceMode2D.Impulse);
                    playerRb.AddForce(new Vector2(0, force * 2), ForceMode2D.Impulse);

                    //Ray
                    var hit = Physics2D.Raycast(firePoint.position, -transform.right, range);
                    var trail = Instantiate(bulletTrail, firePoint.position, transform.rotation);
                    var trailScript = trail.GetComponent<bulletTrail>();

                    if (hit.collider != null)
                    {
                        trailScript.setTargerPosition(hit.point);
                    }
                    else
                    {
                        var endPos = firePoint.position + -transform.right * range;
                        trailScript.setTargerPosition(endPos);
                    }
                }
            }
        }
        
    }
    public void AddBullet()
    {
        if (BulletCount < maxBulletCount)
        {
            BulletCount++;
            _bulletsSpriteScript.updateBulletSprites();
        }
        else
        {
            //print("cant add more bullets" + BulletCount);
        }
    }

    public float attackCooldown;
    public bool cool = true;

    public void CooldownStart()
    {
        StartCoroutine(CooldownCoroutine());
    }

    IEnumerator CooldownCoroutine()
    {
        cool = false;
        yield return new WaitForSeconds(attackCooldown);
        cool = true;
    }

}

