using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

   #region VAR SAUT
    private Rigidbody2D rb;
    public float speed;

    private float moveInput;
    public float jumpForce;
    private bool isGrounded;
    public Transform feetPos; //position des pied
    public float checkRadius; //cercle de validation
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    #endregion

    public bool isPrincessRange = false;
    public bool isCarryingPrincess = false;
    public bool isDead;
    public GameObject princess;

    public Sprite left_heart;
    public Sprite right_heart;
    public Sprite normal_eyes;

    GameObject[] list_trap_carrying;
    GameObject[] trap_appear;


    #region Profile & Caméra

    public Camera camera;

    public PostProcessingProfile allerProcessing;
    public PostProcessingProfile retourProcessing;

    #endregion

    private void Awake()
    {
        trap_appear = GameObject.FindGameObjectsWithTag("Trap_appear");
        foreach (GameObject go in trap_appear)
        {
            go.SetActive(false);
        }

        GameObject[] appear = GameObject.FindGameObjectsWithTag("Appear");
        foreach (GameObject go in appear)
        {
            go.GetComponent<TilemapRenderer>().enabled = false;
            go.GetComponent<TilemapCollider2D>().enabled = false;
        }

        list_trap_carrying = GameObject.FindGameObjectsWithTag("Trap_Carrying");
        rb = GetComponent<Rigidbody2D>();
    }

    /*public void restartSpin()
    {
        rb.DORotate(0f, 0f);
    }*/

    private void Update()
    {
        #region INPUT
        if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine("Dead");
            }
        if (Input.GetKeyDown(KeyCode.R) && isPrincessRange == true) // PORTE LA PRINCESSE
        {
            isCarryingPrincess = true;
            FindObjectOfType<AudioManager>().Play("Grab");
            princess.transform.position = new Vector3(gameObject.transform.position.x + 0.7f, gameObject.transform.position.y, gameObject.transform.position.z);
            princess.transform.parent = gameObject.transform;

            GameObject.Find("player_left_eye").GetComponent<SpriteRenderer>().sprite = left_heart;
            GameObject.Find("player_right_eye").GetComponent<SpriteRenderer>().sprite = right_heart;

            /*GameObject[] exit = GameObject.FindGameObjectsWithTag("Exit");
            foreach (GameObject go in exit)
            {
                go.GetComponent<BoxCollider2D>().enabled = true;
                go.GetComponent<SpriteRenderer>().enabled = true;
            }*/

            GameObject[] tile = GameObject.FindGameObjectsWithTag("Carrying");
            foreach (GameObject go in tile)
            {
                go.GetComponent<TilemapRenderer>().enabled = false;
                go.GetComponent<TilemapCollider2D>().enabled = false;
            }

            GameObject[] appear = GameObject.FindGameObjectsWithTag("Appear");
            foreach (GameObject go in appear)
            {
                go.GetComponent<TilemapRenderer>().enabled = true;
                go.GetComponent<TilemapCollider2D>().enabled = true;
            }

            foreach (GameObject go in list_trap_carrying)
            {
                go.SetActive(false);
            }

            foreach (GameObject go in trap_appear)
            {
                go.SetActive(true);
            }

            camera.GetComponent<PostProcessingBehaviour>().profile = retourProcessing;
        }
        if (Input.GetKeyDown(KeyCode.T) && isCarryingPrincess == true) // LACHE LA PRINCESSE
        {
            princess.transform.parent = null;
            FindObjectOfType<AudioManager>().Play("Drop");
            princess.transform.position = new Vector3(gameObject.transform.position.x - 0.2f, gameObject.transform.position.y, gameObject.transform.position.z);
            if (moveInput > 0)
                princess.transform.eulerAngles = new Vector3(0, 180, 0);
            else if (moveInput < 0)
                princess.transform.eulerAngles = new Vector3(0, 0, 0);
            Debug.Log("MOVE INPUT : " + moveInput);
            isCarryingPrincess = false;

            GameObject.Find("player_left_eye").GetComponent<SpriteRenderer>().sprite = normal_eyes;
            GameObject.Find("player_right_eye").GetComponent<SpriteRenderer>().sprite = normal_eyes;

            GameObject[] argo = GameObject.FindGameObjectsWithTag("Carrying");
            foreach (GameObject go in argo)
            {
                go.GetComponent<TilemapRenderer>().enabled = true;
                go.GetComponent<TilemapCollider2D>().enabled = true;
            }
            GameObject[] appear = GameObject.FindGameObjectsWithTag("Appear");
            foreach (GameObject go in appear)
            {
                go.GetComponent<TilemapRenderer>().enabled = false;
                go.GetComponent<TilemapCollider2D>().enabled = false;
            }

            /*GameObject[] exit = GameObject.FindGameObjectsWithTag("Exit");
            foreach (GameObject go in exit)
            {
                go.GetComponent<BoxCollider2D>().enabled = false;
                go.GetComponent<SpriteRenderer>().enabled = false;
            }*/
            
            foreach (GameObject go in list_trap_carrying)
            {
                go.SetActive(true);
            }

            foreach (GameObject go in trap_appear)
            {
                go.SetActive(false);
            }
            camera.GetComponent<PostProcessingBehaviour>().profile = allerProcessing;
        }
        #endregion
        #region SAUT

        moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput > 0)
            moveInput = 1;
        if (moveInput < 0)
            moveInput = -1;
        transform.position += new Vector3(moveInput * (speed * Time.deltaTime), 0.0f, 0);

        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (moveInput > 0)
            transform.eulerAngles = new Vector3(0, 0, 0);
        else if (moveInput < 0)
            transform.eulerAngles = new Vector3(0, 180, 0);

        if (isGrounded == true && (Input.GetKeyDown(KeyCode.Space)))  {
            //rb.DORotate(360f, 0.5f).OnComplete(restartSpin);
            isJumping = true;
            FindObjectOfType<AudioManager>().Play("jump");
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
        }
        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                isJumping = false;

        }
        if (Input.GetKeyUp(KeyCode.Space))
            isJumping = false;
        #endregion

        #region MORT
        if (gameObject.transform.position.y < -10)
        {
            isDead = true;
        }
        if (isDead == true && isCarryingPrincess == true)
        {
            FindObjectOfType<AudioManager>().Play("Death");
            StartCoroutine("Dead");
        }
        if (isDead == true && isCarryingPrincess == false)
        {
            FindObjectOfType<AudioManager>().Play("Death_alone");
            StartCoroutine("Dead");
        }
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Princess"))
        {
            isPrincessRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Princess"))
        {
            isPrincessRange = false;
        }
    }

    IEnumerator Dead()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        yield return null;
    }
}
