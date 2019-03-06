using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player_death : MonoBehaviour {

    public bool isDead;


	// Use this for initialization
	void Start () {
        isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.y < -10)
        {
            isDead = true;
        }
        if (isDead == true)
        {
            StartCoroutine("Dead");
        }
	}

    IEnumerator Dead()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        yield return null;
    }
}
