using UnityEngine;

namespace _Scripts.MapManager
{
    public class LineObject
    {
        public LineRenderer lr;
        private Node from ;
        private Node to;

        public LineObject(LineRenderer lr, Node from, Node to)
        {
            this.lr = lr;
            this.from = from;
            this.to = to;
        }
    }
}