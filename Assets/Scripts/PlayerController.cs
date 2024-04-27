using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Sprite spiderSprite;
    private Sprite greenGuy;
    public TextMeshProUGUI formText;
    Vector2 movement;
    private bool isSpiderForm = false;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    private Animator animator;
    private Vector2 lastMovementDirection = Vector2.zero; // Track last movement direction
    private InputAction interact;
    public PlayerControls playerControls;
    private void OnEnable()
    {
        interact = playerControls.Movement.Interact;
        interact.Enable();
        interact.performed += InteractHandler;
    }
    private void OnDisable()
    {
        interact.performed -= InteractHandler;
        interact.Disable();
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        greenGuy = GetComponent<SpriteRenderer>().sprite;
        formText = GameObject.Find("Form").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        MovementInput();
        UpdateFormText();
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }
    
    void MovementInput()
    {
        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");

        if (mx != 0 || my != 0)
        {
            // If there's movement input, update last movement direction
            lastMovementDirection = new Vector2(mx, my);
        }

        animator.SetFloat("moveX", mx);
        animator.SetFloat("moveY", my);
        movement = new Vector2(mx, my).normalized;
        animator.SetBool("isMoving", movement.magnitude > 0);


        if (movement.magnitude == 0)
        {
            // If no movement input, use last movement direction for idle animation
            animator.SetFloat("moveX", lastMovementDirection.x);
            animator.SetFloat("moveY", lastMovementDirection.y);
        }

        Vector2 targetPos = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        if (isWalkable(targetPos))
        {
            rb.position = targetPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpiderPowerUp"))
        {
            //GetComponent<SpriteRenderer>().sprite = spiderSprite; 
            isSpiderForm = true;
            animator.SetBool("isSpiderForm", true);
            other.gameObject.SetActive(false);

        }
    }

    public void ResetSprite()
    {
        //GetComponent<SpriteRenderer>().sprite = greenGuy;
        isSpiderForm = false;
        animator.SetBool("isSpiderForm", false);
    }

    void UpdateFormText()
    {
        if (isSpiderForm)
        {
            formText.text = "Spider";
        }
        else
        {
            formText.text = "Alien";
        }
    }

    private bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) != null)
        {
            return false;
        }
        return true;
    }
    private void InteractHandler(InputAction.CallbackContext context)
    {
        var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;
        //Debug.DrawLine(transform.position, interactPos, Color.red, 1f);
        var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
        if (collider != null)
        {
            Debug.Log("there is NPC here");
        }
    }
}
