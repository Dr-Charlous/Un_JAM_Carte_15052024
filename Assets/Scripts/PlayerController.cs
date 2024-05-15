using System;
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Card _currentCard;
    private Vector2 _currentMousePosWorld;
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InitDrag();
        }

        if (Input.GetMouseButton(0))
        {
            Drag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndDrag();
        }
    }
    
    private void InitDrag()
    {
        _currentMousePosWorld = _camera.ScreenToWorldPoint(Input.mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(_currentMousePosWorld, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Card card))
            {
                _currentCard = card;
            }
        }
    }

    private void Drag()
    {
        _currentMousePosWorld = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        if (_currentCard != null)
        {
            _currentCard.transform.position = _currentMousePosWorld;
        }
    }
    
    private void EndDrag()
    {
        GameManager.Instance.CardManager.EndMovePunch(_currentCard);
        _currentCard = null;
    }
}