using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    [SerializeField] private SpriteRenderer bullet1;
    [SerializeField] private SpriteRenderer bullet2;
    [SerializeField] private SpriteRenderer bullet3;
    [SerializeField] private SecondShotgun _scdShotgunScript;

    public void updateBulletSprites(){

        
        if(_scdShotgunScript.BulletCount == 0)
        {
            bullet1.color = new Color(0.2f, 0.2f, 0.2f, 0.5f); 
            bullet2.color = new Color(0.2f, 0.2f, 0.2f, 0.5f); 
            bullet3.color = new Color(0.2f, 0.2f, 0.2f, 0.5f); ;
        }else if (_scdShotgunScript.BulletCount == 1)
        {
            bullet1.color = new Color(1f, 1f, 1f, 1f);
            bullet2.color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
            bullet3.color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        }
        else if(_scdShotgunScript.BulletCount == 2)
        {
            bullet1.color = new Color(1f, 1f, 1f, 1f);
            bullet2.color = new Color(1f, 1f, 1f, 1f);
            bullet3.color = new Color(0.2f, 0.2f, 0.2f, 0.5f);
        }
        else if (_scdShotgunScript.BulletCount == 3)
        {
            bullet1.color = new Color(1f, 1f, 1f, 1f);
            bullet2.color = new Color(1f, 1f, 1f, 1f);
            bullet3.color = new Color(1f, 1f, 1f, 1f);
        }


    }
}
