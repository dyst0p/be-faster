using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;
    
    public void SwitchSprite(bool toggle)
    {
        Image image = gameObject.GetComponent<Image>();

        if (toggle == true)
            image.sprite = onSprite;
        else
            image.sprite = offSprite;
    }
}
