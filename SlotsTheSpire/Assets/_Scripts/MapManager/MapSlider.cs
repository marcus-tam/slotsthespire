using UnityEngine;
using UnityEngine.UI;

public class MapSlider : MonoBehaviour
{
    public Scrollbar scrollbar;
    public ScrollRect scrollRect;
    
    private float minY;
    private float maxY;
    
    private void Start()
    {
        minY = 0f;
        maxY = scrollRect.content.rect.height - scrollRect.viewport.rect.height;
    }
    
    public void ScrollMap()
    {
        float contentY = Mathf.Lerp(minY, maxY, scrollbar.value);
        Vector2 contentPosition = scrollRect.content.anchoredPosition;
        contentPosition.y = contentY;
        scrollRect.content.anchoredPosition = contentPosition;
    }
}