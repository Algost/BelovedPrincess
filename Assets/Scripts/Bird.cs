using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{

    private Vector3 posStart;
    public Vector3 posEnd;

    public Vector3 flipIN;
    public Vector3 flipOUT;

    public int timeLoop;

    // Use this for initialization
    void Start()
    {
        posStart = transform.position;
        aller();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine("Dead");
        }
    }

    public void aller()
    {
        transform.DOMove(posEnd, timeLoop).SetEase(Ease.Linear).OnComplete(retour);
        transform.eulerAngles = flipOUT;
    }

    public void retour()
    {
        transform.DOMove(posStart, timeLoop).SetEase(Ease.Linear).OnComplete(aller);
        transform.eulerAngles = flipIN;
    }

    IEnumerator Dead()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        yield return null;
    }
}
