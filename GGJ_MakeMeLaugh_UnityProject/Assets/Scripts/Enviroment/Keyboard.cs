using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keyboard : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTyping()
    {
        inputField.Select();
    }
}
