using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Sprite spiderSprite;
    private Sprite greenGuy;
    public TextMeshProUGUI formText;
    Vector2 movement;
    public bool isSpiderForm = false;
    public LayerMask solidObjectsLayer;
    public LayerMask interactableLayer;
    private Animator animator;
    private Vector2 lastMovementDirection = Vector2.zero; // Track last movement direction
    private InputAction interact;
    public PlayerControls playerControls;
    public PlayerHealth playerHealth;


private void OnEnable()
{
    playerControls.Enable();
}
private void OnDisable()
{
    playerControls.Disable();
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
    playerControls.Movement.Move.performed += OnMovePerformed;
    playerControls.Movement.Move.canceled += OnMoveCanceled;
    playerHealth = GetComponent<PlayerHealth>();
}
private void OnMovePerformed(InputAction.CallbackContext context)
{
    movement = context.ReadValue<Vector2>();

    // Update last movement direction
    if (movement.magnitude > 0)
    {
        lastMovementDirection = movement.normalized;
    }
    // Update animator parameters
    animator.SetFloat("moveX", movement.x);
    animator.SetFloat("moveY", movement.y);
    animator.SetBool("isMoving", movement.magnitude > 0);
}
private void OnMoveCanceled(InputAction.CallbackContext context)
{
    // When movement input is released, stop the player
    movement = Vector2.zero;
    rb.velocity = Vector2.zero;
}
public void HandleUpdate()
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
    if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Z))
        Interact();
    if (Input.GetKeyDown(KeyCode.Escape))
        FindObjectOfType<StateManager>().ChangeSceneByName("Menu");

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
    formText.text = isSpiderForm ? "SPIDER" : "ALIEN";
}

private bool isWalkable(Vector3 targetPos)
{
    return Physics2D.OverlapCircle(targetPos, 0.2f, solidObjectsLayer | interactableLayer) == null;
}
void Interact()
{

    var facingDir = new Vector3(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
    var interactPos = transform.position + facingDir;
    //Debug.DrawLine(transform.position, interactPos, Color.red, 1f);
    var collider = Physics2D.OverlapCircle(interactPos, 0.2f, interactableLayer);
    if (collider != null)
    {
        Debug.Log("there is an NPC here!");
        collider.GetComponent<Interactable>()?.Interact();
    }

}
}