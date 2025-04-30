using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Unity.VisualScripting;


public class PlayerController : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";

    CustomActions input;

    NavMeshAgent agent;
    Animator animator;

    [Header("Movement")]
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableGround;

    [Header("Action")]
    
    [SerializeField] LayerMask clickableObject;


    float lookRotationSpeed = 8f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        input = new CustomActions();
        AssignInputs();
    }

    void AssignInputs ()
    {
        input.Main.Controller.performed += ctx => ClickToMove();

    }

    void ClickToMove()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableObject))
                {
                    Debug.Log(hit.collider.gameObject.name);
                }
        else if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit, 100, clickableGround))
        {
            agent.destination = hit.point;
            if (clickEffect != null)
            {
                Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);        
            }
        }
        FaceTarget();
        
    }

   

    void OnEnable()
    {
        input.Enable();    
    }

    void OnDisable()
    {
        input.Disable();
    }
    
    void Update()
    {
       // FaceTarget();
        SetAnimations();
    }

    void FaceTarget()
    {
        Vector3 direction = (agent.destination - transform.position).normalized;    
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        //transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime * lookRotationSpeed);
        transform.rotation = lookRotation;  
    }

    void SetAnimations()
    {
        if (agent.velocity == Vector3.zero)
        {
            animator.Play(IDLE);
        }
        else
        {
            animator.Play(WALK);
        }
    }
}

public class Quest: MonoBehaviour
{
    
}
