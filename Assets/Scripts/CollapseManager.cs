using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseManager : MonoBehaviour
{
    public void Collapse(Ball ballA, Ball ballB)
    {
        StartCoroutine(CollapseProcess(ballA, ballB));
    }

    IEnumerator CollapseProcess(Ball ballA, Ball ballB)
    {
        Vector3 startPosition = ballA.transform.position;
        ballA.Deactivate();
        for (float t = 0; t < 1f; t += Time.deltaTime / 0.3f)
        {
            ballA.transform.position = Vector3.Lerp(startPosition, ballB.transform.position, t);
            yield return null;
        }
        Destroy(ballA.gameObject);
        ballB.IncreaseLever();
    }
}
