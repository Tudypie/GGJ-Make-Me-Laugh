using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AngryAIEvent : MonoBehaviour
{
    public Color color = Color.red;
    public Light roomLight;
    public TMP_Text monitorText;

    public void TriggerEvent()
    {
        roomLight.color = color;
        monitorText.color = color;
    }

}
