using UnityEngine;

namespace _Scripts.MapManager
{
    public enum NodeType
    {
        CommonEnemy,
        EliteEnemy,
        BossEnemy,
        RestSite,
        Market,
        Treasure,
        Event,
        Mystery,
    }
}

namespace _Scripts.MapManager
{
    [CreateAssetMenu]
    public class NodeBlueprint : ScriptableObject
    {
        public GameObject gameObject;
        public NodeType nodetype;
    }
    
    
}