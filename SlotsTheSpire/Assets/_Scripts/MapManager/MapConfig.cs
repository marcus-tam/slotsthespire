using UnityEditor.UI;
using UnityEngine;

namespace _Scripts.MapManager
{
    [CreateAssetMenu]
    public class MapConfig : ScriptableObject
    {
        // private int Width, Levels, NumOfStartingNodes;

            // this.Width = width;
            // this.Levels = levels;
            // this.NumOfStartingNodes = numOfStartingNodes;
        public int Width;
        public int Levels;
        [Tooltip("Number of randomly generated paths")]
        public int NumOfStartingNodes;
        [Tooltip("0 - 100; 0 - no obscurity, 100 - full obscurity; Used to hide nodes on map")]
        public int mapObscurity;

            // public void SetWidth(int width) { this.Width = width; }
        public int GetWidth() { return Width; }

        // public void SetLevels(int levels) { this.Levels = levels; }
        public int GetLevels() { return Levels; }
        
        public int GetNumOfStartingNodes() { return NumOfStartingNodes; }
        // public int SetNumOfStartingNodes(int numOfStartingNodes) { return this.NumOfStartingNodes = numOfStartingNodes; }
        public int GetMapObscurity() { return mapObscurity; }
        public string toString()
        {
            return $"Width: {Width}, Levels: {Levels}, NumOfStartingNodes: {NumOfStartingNodes}";
        }
    }
}