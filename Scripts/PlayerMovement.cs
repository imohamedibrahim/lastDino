using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float jumpDistance;
    [SerializeField]
    private float smoothnessInRun;
    private bool canJump;
    private Rigidbody2D rb;
    private float moveVelocity;
    private Vector3 m_Velocity = Vector3.zero;
    public bool isInvisible = false;
    private Vector3 faceRight = new Vector3(0, 0, 0);
    private Vector3 faceLeft = new Vector3(0, 180, 0);
    private float score;
    private Animator anim;
    private string moveDirection = "";
    private string WALK_ANIM = "CanWalk";
    private float limitXDir;
    //managers
    private UIGamePlayScreenManager uIGamePlayScreenController;
    private SoundManager soundManager;
    private GameObject expressionText;
    private GameObject expressionCanvas;
    private int expressionTextTimmer;
    void Start()
    {
        if (movementSpeed == 0) movementSpeed = 0.5f;
        if (jumpDistance == 0) jumpDistance = 3f;
        if (smoothnessInRun == 0) smoothnessInRun = 0.1f;
        canJump = true;
        rb = transform.GetComponent<Rigidbody2D>();
        uIGamePlayScreenController = UIGamePlayScreenManager.Instance();
        isInvisible = false;
        score = 0f;
        anim = GetComponent<Animator>();
        limitXDir = Camera.main.ViewportToWorldPoint(Vector3.one).x - 0.2f;
        soundManager = SoundManager.Instance();
        expressionText = UtilFunctions.GetGameObjectWithTagRecursive(gameObject,TagHolder.PLAYER_EXPRESSION_TEXT);
        expressionCanvas = UtilFunctions.GetChildGameObjectWithTag(gameObject, TagHolder.PLAYER_TEXT_CANVAS);
        SetExpression();
    }

    public void Update()
    {
        score = score + Time.deltaTime;
        expressionTextTimmer--;
        if(expressionTextTimmer < 0)
        {
            expressionText.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        float screenWidth = Screen.currentResolution.width;
        if(Input.touchCount !=0 && Input.touches[0].position.x > screenWidth/2)
        {
            moveDirection = "right";
        }
        else if(Input.touchCount != 0 && Input.touches[0].position.x < screenWidth / 2)
        {
            moveDirection = "left";
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveDirection = "left";
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveDirection = "right";
        }
        MovePlayer();
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            if (canJump)
                rb.velocity = new Vector2(0, jumpDistance * Time.deltaTime);
            canJump = false;
        }
        ApplyVelocity();
        uIGamePlayScreenController.SetText(Mathf.Floor(score).ToString());
    }

    private void SetExpression()
    {
        expressionText.SetActive(true);
        expressionTextTimmer = 150;
    }

    private void MovePlayer()
    {
        if (moveDirection == "left")
        {
            moveVelocity = -movementSpeed;
            transform.eulerAngles = faceRight;
            anim.SetBool(WALK_ANIM, true);
        }
        else if(moveDirection == "right")
        {
            moveVelocity = movementSpeed;
            transform.eulerAngles = faceLeft;
            anim.SetBool(WALK_ANIM, true);
        }else{
            anim.SetBool(WALK_ANIM, false);
        }
        moveDirection = "";
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag.Equals(TagHolder.GROUND) || collision.transform.tag.ToLower().Contains("jumpable"))
        {
            canJump = true;
        } else if (collision.transform.tag.Equals(TagHolder.METEO))
        {
            soundManager.StopAudio(SoundManager.AUDIO_LIST.MAIN_THEME_AUDIO);
            StartCoroutine(PlayerDied());
        }
    }

    private IEnumerator PlayerDied()
    {
        soundManager.PlayAudio(SoundManager.AUDIO_LIST.METEO_HIT_AUTIO);
        transform.GetComponent<SpriteRenderer>().enabled = false;
        transform.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        transform.GetComponent<BoxCollider2D>().enabled = false;
        
        expressionCanvas.SetActive(false);
        GameObject deathParticle = UtilFunctions.GetChildGameObjectWithTag(transform.parent.gameObject, TagHolder.DEATH_PARTICLE_EFFECT);
        deathParticle.transform.position = new Vector3(transform.position.x,transform.position.y,deathParticle.transform.position.z);
        deathParticle.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
        uIGamePlayScreenController.GameOver(Mathf.Floor(score));
    }

    private void ApplyVelocity()
    {
        Vector3 targetVelocity = new Vector2(moveVelocity * 10f, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref m_Velocity, smoothnessInRun * Time.deltaTime);
        moveVelocity = 0;
        Vector3 t = transform.localPosition;
        if(transform.localPosition.x > limitXDir || transform.localPosition.x < -limitXDir)
            transform.position = new Vector2(Mathf.Clamp(transform.position.x,-limitXDir,limitXDir),transform.position.y);
    }

}
