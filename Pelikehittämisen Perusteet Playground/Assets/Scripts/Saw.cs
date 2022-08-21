using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public Transform targetPos;
    public float speed = 10f;

    Vector3 currentTargetPos;
    Vector3 originalPos;

    private void Start()
    {
        this.transform.DetachChildren();
        originalPos = this.transform.position;
        currentTargetPos = targetPos.position;
    }


    // Update is called once per frame
    void Update()
    {
        if (targetPos == null)
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
}
