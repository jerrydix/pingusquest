using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class SelectionArrow : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private RectTransform[] positions;
    private int currentPos;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            ChangePos(-1);
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            ChangePos(1);

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            Execute();
    }

    private void ChangePos(int newPosChange)
    {
        currentPos += newPosChange;

        if (currentPos < 0)
            currentPos = positions.Length - 1;
        else if (currentPos > positions.Length - 1)
            currentPos = 0;
        
        rect.position = new Vector3(rect.position.x, positions[currentPos].position.y, 0);
    }

    private void Execute()
    {
        positions[currentPos].GetComponent<Button>().onClick.Invoke();
    }
}
