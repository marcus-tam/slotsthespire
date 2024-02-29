using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts.MapManager
{
    public class Map
    {
        public int[,] map_arr;
        public MapConfig config;
        public List<Node> nodes;
        public List<Path> paths;

        private NodeType nodeType;
        public Map(MapConfig conf)
        {
            this.config =  conf;
            this.paths = new List<Path>();
            this.nodes = new List<Node>();
        }

        // private void SetConfig(int width, int levels, int numOfStartingNodes)
        // {
        //     this.config.SetWidth(width);
        //     this.config.SetLevels(levels);
        //     this.config.SetNumOfStartingNodes(numOfStartingNodes);
        // }
        
        public void SetMap()
        {
            float obscurity = (float) this.config.GetMapObscurity() / 100;
            for (int randomPath = 0; randomPath < this.config.GetNumOfStartingNodes(); randomPath++)
            {
                int eliteSpawnLevel = this.config.GetLevels() / 3;
                List<NodeType> availableNodeTypes = new List<NodeType>();
                availableNodeTypes.Add(NodeType.CommonEnemy);
                availableNodeTypes.Add(NodeType.Market);
                availableNodeTypes.Add(NodeType.RestSite);
                availableNodeTypes.Add(NodeType.Treasure);
                availableNodeTypes.Add(NodeType.Event);
                
                int lowerbound = 0;
                int upperbound = this.config.GetWidth() - 1;
            
                int candidateX = Random.Range(lowerbound, upperbound);
                int candidateY = 0;

                Point prevPoint = new Point(candidateX, 0);
                Point currPoint = new Point(candidateX, 0); 

                //Generate random path
                while (candidateY < this.config.GetLevels() - 1)
                {
                    if(candidateY == eliteSpawnLevel)
                    {
                        availableNodeTypes.Add(NodeType.EliteEnemy);
                    }
                    int nodeTypeIndex = Random.Range(0, Math.Min(candidateY, availableNodeTypes.Count));
                    float obscurityRoll = Random.Range(0f, 1f);
                    if (candidateY != 0)
                    {
                        AddPath(new Path(prevPoint, currPoint));
                    }

                    prevPoint = currPoint;
                    
                    
                    if (GetNode(new Point(candidateX, candidateY)) == null)
                    {       
                        Debug.Log("CandidateX: "+ candidateX+ " CandidateY: "+ candidateY+ "");
                        Node node = new Node(
                            new Point(candidateX, candidateY), 
                            availableNodeTypes[nodeTypeIndex],
                            (obscurity > obscurityRoll)
                            );
                        AddNode(node);
                        Debug.Log("Adding new node at "+ node.pointToString()+ " to list");
                    }
                    else
                    {
                        Debug.Log("Node detected at "+ GetNode(new Point(candidateX, candidateY)).pointToString()+ ". Skipped adding to list");
                    }
                    
                    //Select value from -1 to 1 hopefully?
                    candidateX += Random.Range(-1, 2);

                    //Bound checker
                    if (candidateX < lowerbound)
                        candidateX = 0;
                    else if (candidateX > upperbound)
                        candidateX = upperbound;
                    candidateY += 1;
                    
                    currPoint = new Point(candidateX, candidateY);
                    
                }



            }

            foreach (Node node in FetchLayer(0))
            {
                node.setObscured(false);
            }
            
            Node bossNode = new Node(new Point(this.config.GetWidth() / 2, this.config.GetLevels() - 1));
            AddNode(bossNode);
            
            //Set Connections to boss level
            foreach (Node node in FetchLayer(this.config.GetLevels() - 2))
            {
                RemoveNode(node);
                Node restNode = new Node(node.point, NodeType.RestSite, false);
                AddNode(restNode);
                restNode.AddOutgoing(GetBossLevel(this.config).point);
                GetBossLevel(this.config).AddIncoming(restNode.point);
                AddPath(new Path(restNode.point, GetBossLevel(this.config).point));
            }
            SetConnections();
            
            
        }


        private void SetConnections()
        {
            Debug.Log("Setting connections...");
            foreach (var path in paths)
            {

                Node startNode = GetNode(path.GetStartPoint());
                Node endNode = GetNode(path.GetEndPoint());

                startNode.AddOutgoing(endNode.point);
                endNode.AddIncoming(startNode.point);

            }
        }

        public Node GetBossLevel(MapConfig mapConfig)
        {
            //Boss Level will always be the last level
            return GetNode(new Point(mapConfig.GetWidth() / 2, mapConfig.GetLevels() - 1));
        }

        private void AddNode(Node node)
        {
            nodes.Add(node);
        }
        private void RemoveNode(Node node)
        {
            nodes.Remove(node);
        }
        private void AddPath(Path path)
        {
            paths.Add(path);
        }
        
        private void RemovePath(Path path)
        {
            paths.Remove(path);
        }
        
        private void ClearPath()
        {
            paths.Clear();
        }
        
        private void ClearNodes()
        {
            nodes.Clear();
        }

        public List<Node> FetchLayer(int layer)
        {
            List<Node> layerNodes = new List<Node>();
            foreach (var node in nodes)
            {
                if (node.point.y == layer)
                {
                    layerNodes.Add(node);
                }
            }
            return layerNodes;
        }

        public Node GetNode(Point point)
        {
            foreach (var node in this.nodes)
            {
                if (node.point.Equals(point))
                {
                    return node;
                }
            }
            return null;   
        }
        
        public void PrintNodes()
        {
            foreach (var node in nodes)
            {
                Debug.Log(node.point.x + ", " + node.point.y);
            }
        }

        public void PrintPaths()
        {
            foreach (var path in paths)
            {
                Debug.Log(path.startPoint.x + ", " + path.startPoint.y + " -> " + path.endPoint.x + ", " + path.endPoint.y);
            }
        }
    }
}