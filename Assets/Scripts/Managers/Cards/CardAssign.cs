using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardAssign : MonoBehaviour
{
    public CardID CardID;

    [SerializeField] TextMeshProUGUI _textComponent;
    [SerializeField] SpriteRenderer _spriteRendererColor;
    [SerializeField] SpriteRenderer _spriteRendererSprite;

    private void Start()
    {
        if (_textComponent != null)
            _textComponent.text = CardID.NameCard;

        if (_spriteRendererColor != null)
            _spriteRendererColor.color = CardID.ColorCard;

        if (_spriteRendererSprite != null && CardID.SpriteCard != null)
            _spriteRendererSprite.sprite = CardID.SpriteCard;
    }
}