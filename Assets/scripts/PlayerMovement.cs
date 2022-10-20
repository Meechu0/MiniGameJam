using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed ;
    public float jumpHeight;
    private Rigidbody2D rb;
    private Score scoreScript;

    // Start is called before the first frame update
    void Start() {


        scoreScript = GameObject.Find("StartingPos").GetComponent<Score>();

        // PlayerPrefs.DeleteAll();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.R))
{
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(1);
            }
            
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.O))
        {
            {
                //reset prefs data;
                 PlayerPrefs.DeleteAll();

            }

        }

        speed =  Vector3.Magnitude(rb.velocity);  // test current object speed
        processInputs();
    }
    void processInputs()
    {
        // constant movement
        //var moveX = Input.GetAxis("Horizontal");
       // transform.position += new Vector3(1, 0, 0) * Time.deltaTime * speed;
       /*
        if(Input.GetKeyDown("space")&& Mathf.Abs(rb.velocity.y) < 0.001)
        {
            rb.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Impulse);
        }
       */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag== "enemy")
        {
            Time.timeScale = 0;
            scoreScript.GameOver();
        }
    }
}
