using System;
using System.Threading.Tasks;
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private Card _currentCard;
    private Card _lastCardTouched;
    private Vector2 _currentMousePosOffset;
    private Camera _camera;
    private bool _isHoldingCard;

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

        if (Input.GetMouseButton(0) && _currentCard != null)
        {
            Drag();
        }

        if (Input.GetMouseButtonUp(0) && _currentCard != null)
        {
            EndDrag();
        }

        CardDetected();
    }

    private Vector2 GetMousePositionWorld()
    {
        return _camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void CardDetected()
    {
        Vector2 currentPos = GetMousePositionWorld();
        RaycastHit2D hit = Physics2D.Raycast(currentPos, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Card card))
            {
                if (_isHoldingCard == false)
                {
                    // card.DoHoverCardScale();
                }

                _lastCardTouched = card;

                return;
            }
        }

        if (_lastCardTouched != null)
        {
            if (_isHoldingCard == false)
            {
                // _lastCardTouched.CancelCardScale();
            }
        }
    }

    private void InitDrag()
    {
        Vector2 currentPos = GetMousePositionWorld();

        RaycastHit2D hit = Physics2D.Raycast(currentPos, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Card card))
            {
                _currentMousePosOffset = (Vector2)hit.transform.position - hit.point;
                _currentCard = card;

                _isHoldingCard = true;

                // _currentCard.DoMoveCardScale();
            }
        }
    }

    private void Drag()
    {
        Vector2 currentPos = GetMousePositionWorld() + _currentMousePosOffset;

        if (_currentCard != null)
        {
            _currentCard.transform.position = currentPos;
        }
    }

    private void EndDrag()
    {
        _isHoldingCard = false;
        _currentCard.IsBeingDropped = true;

        // _currentCard.CancelCardScale();
        _currentCard = null;
    }
}