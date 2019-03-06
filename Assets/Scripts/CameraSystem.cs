using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour {

    // Use this for initialization

    public GameObject player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void LateUpdate()
    {
        float x = Mathf.Clamp(player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
