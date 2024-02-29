using System;
using UnityEngine;

namespace _Scripts.MapManager
{
    public class PlayerTracking
    {
        private Point _currentPoint;
        private Point[] _previousPoints;
        private Point[] _availablePoints;
        private bool _isMoving; //Locking mechanism to prevent multiple moves at once.
        
        //TODO: I hate this. I want to move this somewhere else but let's just ship something.
        //Create a circle around the player that shows where they can move.
        private LineRenderer lineRenderer;
        private int numPoints = 50;
        private float radius = 5f;
        
        // private void GenerateCircle(Color color)
        // {
        //     lineRenderer.positionCount = numPoints;
        //
        //     for (int i = 0; i < numPoints; i++)
        //     {
        //         float angle = i * Mathf.PI * 2f / numPoints;
        //         float x = Mathf.Sin(angle) * radius;
        //         float y = Mathf.Cos(angle) * radius;
        //
        //         lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
        //     }
        //     Material redMaterial = new Material(Shader.Find("Unlit/Color"))
        //     {
        //         color = color
        //     };
        //     lineRenderer.material = redMaterial;
        // }
        

        public PlayerTracking(Point[] availablePoints)
        {
            this._currentPoint = null;
            this._previousPoints = null;
            this._availablePoints = availablePoints;
            this._isMoving = true;
        }
        
        public void UpdatePlayerLocation(Point newPoint, Point[] availablePoints)
        {
            this._currentPoint = newPoint;
            // this._previousPoints = new Point[this._previousPoints.Length + 1];
            // this._previousPoints[^1] = newPoint;
            
            // Debug.Log("Previous points: " + this._previousPoints.Length);

            this._availablePoints = availablePoints;


        }

        public bool canMove()
        {
            return this._isMoving;
        }

        public bool canMoveToNode(int x, int y)
        {
            Point candidate = new Point(x, y);
            foreach (Point point in _availablePoints)
            {
                if (candidate.Equals(point))
                {
                    return true;
                }
            }

            return false;
        }

        public Point GetPlayerPosition()
        {
            if(this._currentPoint == null)
                return new Point(-1, -1);
            return this._currentPoint;
        }

        public string GetPlayerTraveled()
        {
            return "";
        }
        public void EnterNode()
        {
            //Enter node logic here. Make sure to set _isMoving to true when done. This will lift the lock
            this._isMoving = true;
        }
        
        //TODO: Add a highlighter to see where the current player is.
    }
}