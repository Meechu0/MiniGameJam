using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    //public int enemyHealth;
   // public GameObject deathEffect;
   // public int pointsOnDeath;
    public Animator animComponent;
    [SerializeField] private Score scoreScript;

    [SerializeField] private AudioClip deathSound;
       
    [SerializeField] private AudioSource _audioSRC;
    // Start is called before the first frame update
    void Start()
    {
        _audioSRC = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        scoreScript = GameObject.Find("StartingPos").GetComponent<Score>();
    }
    public void Die()
    {
        // play sound + death animation and destroy after 1s. add score
        _audioSRC.PlayOneShot(deathSound, 1f);
        animComponent.Play("DeathAnim");
        Destroy(gameObject, 1);
        scoreScript.enemyScore += 25;
    }
}
