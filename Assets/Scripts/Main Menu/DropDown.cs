using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DropDown : MonoBehaviour
{
    private TMP_Dropdown dropDown;
    // Start is called before the first frame update
    void Awake()
    {
        dropDown = GetComponent<TMP_Dropdown>();
        //dropDown.value = 6;
    }
    
}
