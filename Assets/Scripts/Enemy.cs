using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;
using Pathfinding;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour
{
    private static bool isAlarmPlaying = false;
    public AIPath aiPath;
    public float speed = 3f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    private float canAttack;
    public Rigidbody2D rb;
    private Transform target;
    public float rotationSpeed = 5.0f;
    public List<Transform> waypoints;
    int waypointNum = 0;
    Transform nextWaypoint;
    public float waypointReachedDistance = 0.1f;
    public Light2D globalLight;
    public AudioSource alertAudioSource;

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
        aiPath.enabled = false;
    }
    private void FixedUpdate()
    {

        if (target != null)
        {
            aiPath.enabled = true;
            aiPath.destination = target.position;
            /*
            // Calculate direction to the target
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            // Rotate towards the target
            float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Move towards the target
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            */
        }
        else
        {
            aiPath.enabled = false;
            Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToWaypoint.y, directionToWaypoint.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, nextWaypoint.position, step);
            float distance = Vector2.Distance(nextWaypoint.position, transform.position);
            transform.position = Vector2.MoveTowards(transform.position, nextWaypoint.position, step);
            if (distance <= waypointReachedDistance)
            {
                waypointNum++;
                if (waypointNum >= waypoints.Count)
                {
                    waypointNum = 0;
                }
                nextWaypoint = waypoints[waypointNum];
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            AttackPlayer(other.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            AttackPlayer(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
            StopCoroutine("AlertMode");
            StartCoroutine("AlertMode");
        }
    }
    /*
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
        }
    }
    */
    IEnumerator PlayHaptics(float seconds)
    {
        //    Gamepad.current.SetMotorSpeeds(.25f, .25f);
        yield return new WaitForSeconds(seconds);
        InputSystem.ResetHaptics();
    }
    private void AttackPlayer(GameObject player)
    {
        if (attackSpeed < canAttack)
        {
            player.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
            StartCoroutine(PlayHaptics(.25f));
            canAttack = 0f;
        }
        else
        {
            canAttack += Time.deltaTime;
        }
    }
    IEnumerator AlertMode()
    {
        aiPath.enabled = true;
        aiPath.destination = target.position;
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        audioManager.StopMusic();
        if (!isAlarmPlaying)
        {
            alertAudioSource.Play();
            globalLight.color = Color.red;
            isAlarmPlaying = true;
        }
        yield return new WaitForSeconds(10);
        globalLight.color = Color.white;
        alertAudioSource.Stop();
        isAlarmPlaying = false;
        audioManager.Play("Green Guy L2");
        target = null;
        aiPath.enabled = false;
    }
}