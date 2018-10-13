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
            positiveDrag = eventData.position.y > lastTouchPosition.y;
        else if (scrollContent.Horizontal)
            positiveDrag = eventData.position.x > lastTouchPosition.x;

        lastTouchPosition = eventData.position;
    }

    public void OnViewScroll()
    {
        if (scrollRect.vertical)
            HandleVerticalScroll();
        else
            HandleHorizontalScroll();
    }

    private void HandleVerticalScroll()
    {
        
    }

    private void HandleHorizontalScroll()
    {
        int currItemIndex = positiveDrag ? scrollRect.content.childCount - 1 : 0;
        var currItem = scrollRect.content.GetChild(currItemIndex);
        float itemThreshold = transform.position.x + scrollContent.ParentWidth * 0.5f + outOfBoundsThreshold;

        if (positiveDrag && currItem.position.x - scrollContent.ChildWidth * 0.5f > itemThreshold)
        {
            var itemToBeShifted = scrollRect.content.GetChild(0);
            var newPos = new Vector2
            {
                x = itemToBeShifted.position.x - scrollContent.ChildWidth * 1.5f + scrollContent.ItemSpacing,
                y = itemToBeShifted.position.y
            };
            currItem.position = newPos;
            currItem.SetSiblingIndex(0);
        }
    }
}
