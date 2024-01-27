using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFloor : MonoBehaviour
{
    
    [SerializeField] private float speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        GameSequence.Instance.Die(3);
    }
}
