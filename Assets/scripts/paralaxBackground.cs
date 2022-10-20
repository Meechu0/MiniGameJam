using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralaxBackground : MonoBehaviour
{
    private float len, startpos;
    [SerializeField]private GameObject camera;
    [SerializeField]private float paralaxstrength;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        // Getting bounds size from the background borders.
        len = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float t = (camera.transform.position.x * (1 - paralaxstrength));
        float dist = (camera.transform.position.x * paralaxstrength);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (t > startpos + len) startpos += len;
        else if (t < startpos - len) startpos -= len;
    }
}
