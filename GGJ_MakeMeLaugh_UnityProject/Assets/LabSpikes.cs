using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabSpikes : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float timeTillKill;
    private bool isInTrigger;


    // Start is called before the first frame update
    void Start()
    {
        isInTrigger = false;
        animator.Play("Idle");
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            animator.Play("Jump");
            Invoke(nameof(KillPlayer), timeTillKill);
            isInTrigger = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Player"))
        {
            isInTrigger = false;
        }
    }

    void KillPlayer()
    {
        if(isInTrigger)
        {
            Debug.Log("DIE");
        }
    }
}
