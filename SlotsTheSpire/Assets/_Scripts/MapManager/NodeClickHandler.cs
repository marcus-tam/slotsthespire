using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.MapManager
{
    public class NodeClickHandler : MonoBehaviour
    {
        public Point point;
        
        public void OnMouseDown()
        {
            Debug.Log("Clicked on node: ");
        }
        
    }
}