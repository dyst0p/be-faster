using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;
    
    public void SwitchOnSprite(bool toggle)
    {
        if (toggle == true)
            gameObject.GetComponent<Image>().sprite = onSprite;
        else
            gameObject.GetComponent<Image>().sprite = offSprite;
    }
}
