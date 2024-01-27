using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cowboy : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Jukebox jukebox;

    public float walkDistance;
    public bool isWalking = false;

    public bool isDead = false;


    // Start is called before the first frame update
    void Start()
    {
        EnterBar();
    }

    // Update is called once per frame
    void Update()
    {
        if(isWalking)
        {
            transform.position += new Vector3(0f,0f, 2.2f) * Time.deltaTime;
        }   
    }

    void EnterBar()
    {
        isWalking = true;
        Invoke(nameof(Shoot), walkDistance);
    }

    void Shoot()
    {
        if(isWalking)
        {
            isWalking = false;
            // transform.rotation = new Quaternion(0f, 1f, 0f, 1f);
            transform.Rotate(new Vector3(0f,90f,0f));
            animator.SetTrigger("isShooting");
            Invoke(nameof(KillPlayer), 2.8f);
        }
    }

    void KillPlayer()
    {
        if(!isDead)
        {
            GameSequence.Instance.Die(2);
        }
    }

    public void Die()
    {
        isWalking = false;
        isDead = true;
        animator.SetTrigger("die");
        jukebox.Invoke(nameof(jukebox.StartPlaying), 2f);
    }
}
