using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedR : MonoBehaviour
{
    public Transform target;
    RectTransform myTransform;
    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.anchoredPosition = new Vector3(target.position.x, transform.position.y, target.position.z); 
    }
}
