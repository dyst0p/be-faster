using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;
    
    public void SwitchOnSprite(bool toggle)
    {
        if (toggle == true)
            gameObject.GetComponent<Image>().sprite = offSprite;
        else
            gameObject.GetComponent<Image>().sprite = onSprite;
    }
}
