using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform targetPos;
    public float speed = 10f;
    public bool isActivated = false;

    Vector3 currentTargetPos;
    Vector3 originalPos;

    private void Start()
    {
        this.transform.DetachChildren();
        originalPos = this.transform.position;
        currentTargetPos = targetPos.position;
    }

    public void IsActivated(bool t)
    {
        Debug.Log(t + " but really is " + isActivated);
        isActivated = t;
        Debug.Log(t + " but really is " + isActivated);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated == false)
            return;


        transform.position = Vector3.MoveTowards(transform.position, currentTargetPos, speed * Time.deltaTime);

        if (transform.position == currentTargetPos && currentTargetPos == targetPos.position)
        {
            currentTargetPos = originalPos;
        }
        else if (transform.position == currentTargetPos && currentTargetPos == originalPos)
        {
            currentTargetPos = targetPos.position;
        }
    }

    private void OnDrawGizmos()
    {
        if (targetPos == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, targetPos.position);
        Gizmos.DrawSphere(targetPos.position, 0.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.parent = this.transform;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.parent = this.transform)
            collision.transform.parent = null;
    }
}
