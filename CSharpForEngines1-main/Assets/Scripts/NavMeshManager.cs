using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NavMeshPlus.Components;

public class NavMeshManager : MonoBehaviour
{

    public NavMeshSurface navSurf;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(3);
        navSurf.BuildNavMeshAsync();
    }
}
