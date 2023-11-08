using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapseManager : MonoBehaviour
{
    private void Awake()
    {
        ActiveItem[] activeItems = FindObjectsOfType<ActiveItem>();
        foreach (ActiveItem item in activeItems) 
        {
            item.Init(this);
        }
    }
    public void Collapse(ActiveItem activeItemA, ActiveItem actibeItemB)
    {
        StartCoroutine(CollapseProcess(activeItemA, actibeItemB));
    }

    IEnumerator CollapseProcess(ActiveItem acitiveItemA, ActiveItem activeItemB)
    {
        Vector3 startPosition = acitiveItemA.transform.position;
        acitiveItemA.Deactivate();
        for (float t = 0; t < 1f; t += Time.deltaTime / 0.3f)
        {
            acitiveItemA.transform.position = Vector3.Lerp(startPosition, activeItemB.transform.position, t);
            yield return null;
        }
        Destroy(acitiveItemA.gameObject);

        activeItemB.DoEffect();       
    }

    
}
