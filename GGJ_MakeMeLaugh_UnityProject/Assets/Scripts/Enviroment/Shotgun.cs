using GameJam.Enviroment;
using GameJam.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public void OnPlayerTakeShotgun()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        PlayerManager.Instance.PlayerShotgun.TakeShotgun();
    }

    public void OnPlayerPlaceShotgun()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        PlayerManager.Instance.PlayerShotgun.PlaceShotgun();
    }

}
