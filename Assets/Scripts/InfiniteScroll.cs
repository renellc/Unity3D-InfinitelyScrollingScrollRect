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
        if (scrollRect.vertical)
            positiveDrag = lastTouchPosition.y > eventData.position.y;
        else
            positiveDrag = lastTouchPosition.x > eventData.position.x;

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

    }
}
