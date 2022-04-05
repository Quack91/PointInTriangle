using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableObject : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool IsDragged { get; set; }

    private RectTransform m_rectTransform;
    private RectTransform RectTransform => m_rectTransform ?? (m_rectTransform = GetComponent<RectTransform>());

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsDragged = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsDragged = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(IsDragged)
        {
            RectTransform.anchoredPosition = Input.mousePosition;
        }
    }
}
