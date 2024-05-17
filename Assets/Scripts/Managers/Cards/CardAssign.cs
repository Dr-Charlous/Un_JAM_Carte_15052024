using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardAssign : MonoBehaviour
{
    public Card CardComponent { get; set; }
    public CardData CardData;
    
    [SerializeField] TextMeshProUGUI _textComponent;
    [SerializeField] SpriteRenderer _spriteRendererColor;
    [SerializeField] SpriteRenderer _spriteRendererSprite;

    private void Start()
    {
        CardComponent = GetComponent<Card>();
        
        if (_textComponent != null)
            _textComponent.text = CardData.NameCard;

        if (_spriteRendererColor != null)
            _spriteRendererColor.color = CardData.ColorCard;

        if (_spriteRendererSprite != null && CardData.SpriteCard != null)
            _spriteRendererSprite.sprite = CardData.SpriteCard;
    }
}