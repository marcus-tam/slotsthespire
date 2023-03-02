using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace _Scripts.MapManager
{
    public class Node
    {
        public readonly Point point;
        public readonly List<Point> incoming = new List<Point>();
        public readonly List<Point> outgoing = new List<Point>();
        [JsonConverter(typeof(StringEnumConverter))]
        public readonly GameObject gameObject;
        public Vector2 position;

        public Node(GameObject gameObject, Point point)
        {
            this.gameObject = gameObject;
            this.point = point;
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
        
    }
}