using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.MapManager
{
    public class Node
    {
        public readonly Point point;
        public readonly List<Point> incoming = new List<Point>();
        public readonly List<Point> outgoing = new List<Point>();
        public NodeType nodeType;
        
        [JsonConverter(typeof(StringEnumConverter))]
        public GameObject gameObject;
        public Vector2 position;
        private bool obscured;

        public Node(Point point)
        {
            this.point = point;
            this.nodeType = NodeType.Event;
            this.obscured = false;
        }
        public Node(Point point, NodeType nodeType, bool obscured)
        {
            this.point = point;
            this.nodeType = nodeType;
            this.obscured = obscured; 
        }
        
        public bool getObscured()
        {
            return obscured;
        }
        public void setObscured(bool _obscured)
        {
            this.obscured = _obscured;
        }

        public void setNodeType(NodeType _nodeType)
        {
            this.nodeType = _nodeType;
        }
        public NodeType getNodeType()
        {
            return this.nodeType;
        }
        
        public void setNewGameObject(GameObject gameObjectOverride)
        {
            this.gameObject = gameObjectOverride;
        }
        public GameObject getGameObject()
        {
            return this.gameObject;
        }
        public void AddIncoming(Point p)
        {
            if (incoming.Any(element => element.Equals(p)))
                return;

            incoming.Add(p);
        }

        public void AddOutgoing(Point p)
        {
            if (outgoing.Any(element => element.Equals(p)))
                return;

            outgoing.Add(p);
        }

        public void RemoveIncoming(Point p)
        {
            incoming.RemoveAll(element => element.Equals(p));
        }

        public void RemoveOutgoing(Point p)
        {
            outgoing.RemoveAll(element => element.Equals(p));
        }
        
        public bool HasNoConnections()
        {
            return incoming.Count == 0 && outgoing.Count == 0;
        }

        public string pointToString()
        {
            string ret = ("Point: " + point.x + ", " + point.y + "");
            return ret;
        }
    }
}