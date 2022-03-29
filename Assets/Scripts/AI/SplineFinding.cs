using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineFinding : MonoBehaviour
{
    public List<Transform> routes = new List<Transform>();
    public int routeToGO;
    public float tParam;
    public Vector3 currentPosision;
    public float speedModifier;
    public bool coroutineAllowed;

    private float speed;
    private Vector3 prevVector;
    private float prevtime;
    private Vector3 currentVector;
    private float ds;
    private float pecentDiff;
    private float variableSM;


    void Awake()
    {
        routeToGO = 0;
        tParam = 0f;
        coroutineAllowed = true;
        foreach (GameObject route in GameObject.FindGameObjectsWithTag("Spline"))
        {
            routes.Add(route.transform);
        }
        variableSM = speedModifier;
    }

    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(Travel(routeToGO));
        }
        currentVector = transform.position;
        speed = (currentVector - prevVector).magnitude / (tParam - prevtime);

        //ds = 12 - speed;
        //pecentDiff = ds / 100;
        //variableSM = (speedModifier * pecentDiff);
    }

    private void LateUpdate()
    {
        prevVector = transform.position;
        prevtime = tParam;
    }

    private IEnumerator Travel(int routeNumber)
    {
        coroutineAllowed = false;
        //Transform _p = routes[0].GetChild(routeNumber).transform;
        //get b points
        Vector3 p0 = routes[0].GetChild(routeNumber).GetChild(0).position;
        Vector3 p1 = routes[0].GetChild(routeNumber).GetChild(1).position;
        Vector3 p2 = routes[0].GetChild(routeNumber).GetChild(2).position;
        Vector3 p3 = routes[0].GetChild(routeNumber).GetChild(3).position;


        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;

            currentPosision = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = currentPosision;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;
        routeToGO += 1;

        //if (routeToGO > routes.Length - 1)
          //  routeToGO = 0;

        coroutineAllowed = true;
    }
}
