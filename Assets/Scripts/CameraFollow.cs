using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    private float yMin;
    [SerializeField]
    private float yMax;
    [SerializeField]
    private float xMin;
    [SerializeField]
    private float xMax;

    private Transform target;
    // Use this for initialization
    void Start () {
        target = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Debug.Log(Mathf.Clamp(4, 1, 10));
        transform.position = new Vector3(Mathf.Clamp(target.position.x, xMin, xMax), Mathf.Clamp(target.position.y, yMin, yMax), transform.position.z);
	}
}
