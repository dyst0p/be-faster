using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour
{
    [SerializeField]
    private Sprite _onSprite;
    [SerializeField]
    private Sprite _offSprite;
    
    public void SwitchSprite(bool toggle)
    {
        Image image = gameObject.GetComponent<Image>();

        if (toggle == true)
            image.sprite = _onSprite;
        else
            image.sprite = _offSprite;
    }
}
