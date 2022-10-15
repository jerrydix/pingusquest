using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;
    private Image currentImage;
    void Awake()
    {
        currentImage = GetComponent<Image>();
        currentImage.sprite = fullHeart;
    }

    public void SetState(bool full)
    {
        currentImage.sprite = full ? fullHeart : emptyHeart;
    }
}
