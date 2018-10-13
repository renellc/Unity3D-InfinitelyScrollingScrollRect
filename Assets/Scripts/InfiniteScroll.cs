using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField]
    protected ScrollContent scrollContent;
    [SerializeField]
    protected float outOfBoundsThreshold;

    private ScrollRect scrollRect;
    private Vector2 lastTouchPosition;
    private bool positiveDrag;


    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.vertical = scrollContent.Vertical;
        scrollRect.horizontal = scrollContent.Horizontal;
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastTouchPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (scrollContent.Vertical)
        {
            positiveDrag = eventData.position.y > lastTouchPosition.y;
        }
        else if (scrollContent.Horizontal)
        {
            positiveDrag = eventData.position.x > lastTouchPosition.x;
        }

        lastTouchPosition = eventData.position;
    }

    public void OnViewScroll()
    {
        if (scrollRect.vertical)
        {
            HandleVerticalScroll();
        }
        else
        {
            HandleHorizontalScroll();
        }
    }

    private void HandleVerticalScroll()
    {
        int currItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var currItem = scrollRect.content.GetChild(currItemIndex);

        if (!ReachedThreshold(currItem))
        {
            return;
        }

        int endItemIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (positiveDrag)
        {
            newPos.y = endItem.position.y - scrollContent.ChildHeight * 1.5f + scrollContent.ItemSpacing;
        }
        else
        {
            newPos.y = endItem.position.y + scrollContent.ChildHeight * 1.5f - scrollContent.ItemSpacing;
        }

        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
    }

    private void HandleHorizontalScroll()
    {
        int currItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var currItem = scrollRect.content.GetChild(currItemIndex);
        if (!ReachedThreshold(currItem))
        {
            return;
        }

        int endItemIndex = positiveDrag ? 0 : scrollRect.content.childCount - 1;
        Transform endItem = scrollRect.content.GetChild(endItemIndex);
        Vector2 newPos = endItem.position;

        if (positiveDrag)
        {
            newPos.x = endItem.position.x - scrollContent.ChildWidth * 1.5f + scrollContent.ItemSpacing;
        }
        else
        {
            newPos.x = endItem.position.x + scrollContent.ChildWidth * 1.5f - scrollContent.ItemSpacing;
        }

        currItem.position = newPos;
        currItem.SetSiblingIndex(endItemIndex);
    }

    private bool ReachedThreshold(Transform item)
    {
        if (scrollContent.Vertical)
        {
            float posYThreshold = transform.position.y + scrollContent.ParentHeight * 0.5f + outOfBoundsThreshold;
            float negYThreshold = transform.position.y - scrollContent.ParentHeight * 0.5f - outOfBoundsThreshold;
            return positiveDrag ? item.position.y - scrollContent.ChildWidth * 0.5f > posYThreshold :
                item.position.y + scrollContent.ChildWidth * 0.5f < negYThreshold;
        }
        else
        {
            float posXThreshold = transform.position.x + scrollContent.ParentWidth * 0.5f + outOfBoundsThreshold;
            float negXThreshold = transform.position.x - scrollContent.ParentWidth * 0.5f - outOfBoundsThreshold;
            return positiveDrag ? item.position.x - scrollContent.ChildWidth * 0.5f > posXThreshold :
                item.position.x + scrollContent.ChildWidth * 0.5f < negXThreshold;
        }
    }
}
