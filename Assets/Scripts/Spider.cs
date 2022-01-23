using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Spider : MonoBehaviour
{
    enum SpiderState
    {
        IdleMove = 0,
        EnteringGate = 1,
    }

    public Transform[] points;
    public Transform scarePointA;
    public Transform scarePointB;
    public UnityEvent SpiderAnimEnd;
    public Animator animator;
    private SpiderState state = SpiderState.IdleMove;
    private Transform targetPathPoint;
    private float moveSpeed = 8;
    private bool moving = false;
    private float _randomTimer = 0;
    private float _randomTime = 0;
    private static readonly int Moving = Animator.StringToHash("Moving");
    private static readonly int Scare1 = Animator.StringToHash("Scare");

    private void Update()
    {
        if (state == SpiderState.IdleMove)
        {
            _randomTimer += Time.deltaTime;
            if (_randomTimer >= _randomTime)
            {
                _randomTimer = 0;
                _randomTime = Random.Range(3f, 9f);
                targetPathPoint = points[Random.Range(0, points.Length)];
            }

            Vector3 diff = targetPathPoint.localPosition - transform.localPosition;
            float len = diff.sqrMagnitude;
            if (len > 50)
            {
                moving = true;
                transform.localPosition += transform.up * (Time.deltaTime * moveSpeed);
                diff.Normalize();
                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                Quaternion targetRot = Quaternion.Euler(0f, 0f, rot_z - 90);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 23);
            }
            else
            {
                moving = false;
            }

            animator.SetBool(Moving, moving);
        }
    }

    [Button]
    public void Scare()
    {
        if (state == SpiderState.EnteringGate)
            return;

        state = SpiderState.EnteringGate;
        StartCoroutine(EntergateAnim());
    }

    IEnumerator EntergateAnim()
    {
        var wait = new WaitForEndOfFrame();
        float timer = 0;
        animator.SetTrigger(Scare1);
        yield return new WaitForSeconds(0.5f);
        Vector3 startPos = transform.localPosition;
        Vector3 targetPos = scarePointA.localPosition;

        Vector3 diff = targetPos - startPos;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.Euler(0f, 0f, rot_z - 90);
        transform.rotation = targetRot;

        while (true)
        {
            timer += Time.deltaTime;

            transform.localPosition = Vector3.Lerp(startPos, targetPos, timer);
            if (timer > 1)
                break;
            yield return wait;
        }

        startPos = transform.localPosition;
        targetPos = scarePointB.localPosition;
        diff = targetPos - startPos;
        diff.Normalize();
        rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        targetRot = Quaternion.Euler(0f, 0f, rot_z - 90);
        transform.rotation = targetRot;
        timer = 0;
        while (true)
        {
            timer += Time.deltaTime*2;

            transform.localPosition = Vector3.Lerp(startPos, targetPos, timer);
            transform.localScale = new Vector3(1 - timer, (1 - timer)*0.5f, 1);
            if (timer > 1)
                break;
            yield return wait;
        }

        transform.localScale = Vector3.zero;
        SpiderAnimEnd?.Invoke();
    }
}