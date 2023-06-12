using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour {

    public Vector2 cameraChange; //смена камеры
    public Vector3 playerChange;
    private CameraMovement cam; // движение частной камеры

    // Use this for initialization
    void Start () {
        cam = Camera.main.GetComponent<CameraMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
        }
    }
}
