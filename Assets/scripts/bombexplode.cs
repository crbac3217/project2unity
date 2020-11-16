using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombexplode : MonoBehaviour
{
    public Vector3 currentpos;
    private List<Transform> destroyable = new List<Transform>();
    public Transform minObj = null;
    float minDist = Mathf.Infinity;
    Animator myanim;
    // Start is called before the first frame update
    void Start()
    {
        myanim = gameObject.GetComponent<Animator>();
        currentpos = transform.position;
        foreach (GameObject dest in GameObject.FindGameObjectsWithTag("destground"))
        {
            destroyable.Add(dest.GetComponent<Transform>());
        }
        foreach (Transform t in destroyable)
        {
            float dist = Vector3.Distance(t.position, currentpos);
            if (dist < minDist)
            {
                minObj = t;
                minDist = dist;
            }
        }
        StartCoroutine(Letfall());
    }
    IEnumerator Letfall()
    {
        yield return new WaitForSeconds(myanim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(minObj.gameObject);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
