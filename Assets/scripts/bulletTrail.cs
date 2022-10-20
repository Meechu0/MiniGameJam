using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletTrail : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 targetPos;
    private float progress;

    [SerializeField]
    private float speed = 40f;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.WithAxis(Axis.z, -1);
    }

    // Update is called once per frame
    void Update()
    {
        progress += Time.deltaTime * speed;
        transform.position = Vector3.Lerp(startPos, targetPos, progress);
    }
    public void setTargerPosition(Vector3 targetPosition)
    {
        targetPos = targetPosition.WithAxis(Axis.z, -1);
    }

}
