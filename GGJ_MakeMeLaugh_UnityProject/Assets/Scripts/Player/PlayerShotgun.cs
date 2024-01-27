using Autodesk.Fbx;
using GameJam.Audio;
using GameJam.Input;
using GameJam.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShotgun : MonoBehaviour
{
    private InputMaster inputManager;
    private Camera playerCamera;

    [SerializeField] private bool hasShotgun = false;
    [SerializeField] private GameObject shotgunInHand;
    [SerializeField] private LayerMask enemyLayer;

    private void Start()
    {
        inputManager = InputManager.INPUT;
        playerCamera = PlayerManager.Instance.PlayerCamera;
    }
    private void Update()
    {
        if(!hasShotgun) { return; }

        if(inputManager.Player.Shoot.IsPressed())
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit))
        {
            // enemy die
            Debug.Log(hit.transform.gameObject.name);
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * 100f);
            }
        }

        // particle effect
        shotgunInHand.GetComponent<Animator>().Play("Shoot");
        AudioManager.Instance.PlayAudio(FMODEvents.Instance.shotgunShoot);
        hasShotgun = false;

        //Invoke(nameof(DropShotgun), 3f);
    }

    private void DropShotgun()
    {
        shotgunInHand.transform.parent = null;
        shotgunInHand.GetComponent<Rigidbody>().isKinematic = false;
        Destroy(shotgunInHand.gameObject, 6f);
    }

    public void PlaceShotgun()
    {
        shotgunInHand.gameObject.SetActive(false);
    }

    public void TakeShotgun()
    {
        hasShotgun = true;
        shotgunInHand.SetActive(true);
        AudioManager.Instance.PlayAudio(FMODEvents.Instance.shotgunEquip);
    }
}
