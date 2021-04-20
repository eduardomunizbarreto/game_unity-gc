using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePlayer : MonoBehaviour
{

    public Rigidbody2D rb;
    public int upForce = 12;
    public int moveSpeed;
    private float direction;
    private bool facingRight = true;

    public Animator animator;

    private int pulosExtras = 1;
    public bool taNoChao;
    public Transform detectaChao;
    public LayerMask oQueEhChao;

    public GameObject tiro;
    public GameObject zBuster;
    public AudioClip audioTiro;
    public AudioClip audioSaber;
    public AudioClip audioMoeda;

    private int count = 0;
    public Text textCount;

    private string attackAnim = "shooting";

    public Text armaText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (attackAnim == "shooting")
            {
                attackAnim = "attacking";
                armaText.text = "Arma: ZSaber";
            }
            else
            {
                attackAnim = "shooting";
                armaText.text = "Arma: ZBuster";
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool(attackAnim, true);
            ControladorAudio.instancia.playAudio(attackAnim == "shooting" ? audioTiro : audioSaber);

            if (attackAnim == "shooting")
                Instantiate(tiro, new Vector3(zBuster.transform.position.x, zBuster.transform.position.y, zBuster.transform.position.z), zBuster.transform.rotation);
        }

        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool(attackAnim, false);
        }


        taNoChao = Physics2D.OverlapCircle(detectaChao.position, 0.2f, oQueEhChao);

        if (Input.GetButtonDown("Jump") && taNoChao)
        {
            rb.velocity = Vector2.up * upForce;
            animator.SetBool("jumping", true);
            animator.SetBool("falling", false);

        }

        if (Input.GetButtonDown("Jump") && !taNoChao && pulosExtras > 0)
        {
            rb.velocity = Vector2.up * upForce;
            animator.SetBool("jumping", true);
            animator.SetBool("falling", false);
            pulosExtras--;
        }

        if (rb.velocity.y < 0)
        {
            animator.SetBool("jumping", false);
            animator.SetBool("falling", true);
        }

        if (taNoChao && rb.velocity.y == 0)
        {
            pulosExtras = 1;
            animator.SetBool("jumping", false);
            animator.SetBool("falling", false);
        }

        direction = Input.GetAxisRaw("Horizontal");

        if (direction != 0)
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }

        if ((direction > 0 && !facingRight) || (direction < 0 && facingRight))
        {
            transform.Rotate(0f, 180f, 0f);
            facingRight = !facingRight;
        }

        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D body)
    {
        if (body.gameObject.CompareTag("coin"))
        {
            Destroy(body.gameObject);
            ControladorAudio.instancia.playAudio(audioMoeda);
            count++;
            textCount.text = "Moedas: " + count;
        }
    }

}
