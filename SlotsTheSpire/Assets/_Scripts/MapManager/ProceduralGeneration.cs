using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Newtonsoft.Json;
using Unity.VisualScripting;

namespace _Scripts.MapManager 
 {
     
    public class ProceduralGeneration : MonoBehaviour
    {
        [Header("Map Config")]

        [SerializeField] private MapConfig config;
        
        [Header("Map Node Config")]
        [SerializeField] private GameObject mysteryNode;
        // [SerializeField] GameObject commonBattleNode, eliteBattleNode, bossBattleNode;
        
        // [SerializeField] private GameObject marketNode, restNode, eventNode, treasureNode;
        [SerializeField] private NodeBlueprint commonBattleNode, eliteBattleNode, bossBattleNode;
        [SerializeField] private NodeBlueprint marketNode, restNode, eventNode, treasureNode;
        
        //Seed Config
        [Header("Seed Config")]
        [SerializeField] private int mapSeed;
        
        //Variable Config
        private Map map;
        public GameEvent onNodeClick;
        
        //Local Variables
        IDictionary<NodeType, GameObject> nodeBlueprints = new Dictionary<NodeType, GameObject>();
        PlayerTracking playerTracking;
        SpriteRenderer m_SpriteRenderer;

        void Start()
        {
            //Local Variable
            nodeBlueprints = GetNodeBlueprints();
            
            //TODO: Change Seeding to be a global configuration 
            MapSeedConfig(mapSeed);
            Debug.Log(config.toString());
            
            //Create new Map Object
            Debug.Log("Starting Map Generation");
            map = new Map(config);
            map.SetMap();
            
            //Fetch bossNode
            Node bossNode = map.GetBossLevel(map.config);

            //Assign GameObjects to nodes using nodeBlueprints
            foreach (var node in map.nodes)
            {
                if(node.point.Equals(bossNode.point))
                    continue;
                
                node.setNewGameObject(node.getObscured() ? mysteryNode : nodeBlueprints[node.nodeType]);
                SpawnObj(node);
            }
            //Create LineRenderer between nodes
            foreach (var path in map.paths)
            {
                CreateLineBetweenNodes(map.GetNode(path.GetStartPoint()), map.GetNode(path.GetEndPoint()));
            }

            //Overwrite boss node with bossBattleNode
            bossNode.setNewGameObject(bossBattleNode.gameObject);
            SpawnObj(bossNode);

            Debug.Log("Finished Map Generation");

            //TODO: If playerprefs has a saved game, load it. Otherwise, instantiate player tracking
            Debug.Log("Instantiating Player Tracking");
            InstantiatePlayerTracking();

        }

        private void InstantiatePlayerTracking()
        {
            //Instantiate Player Tracking
            List<Node> firstLayer = map.FetchLayer(0);
            Point[] firstLayerPoints = { };
            //Append each point in firstLayer to firstLayerPoints
            foreach (var node in firstLayer)
            {
                Array.Resize(ref firstLayerPoints, firstLayerPoints.Length + 1);
                firstLayerPoints[^1] = node.point;
            }
            playerTracking = new PlayerTracking(
                firstLayerPoints
            );

        }

        private void SpawnObj(Node node)
        {
            GameObject obj = Instantiate(node.getGameObject(), 
                new Vector2(node.point.x, node.point.y), 
                Quaternion.identity);

            
            obj.GetComponent<NodeButton>().x = node.point.x;
            obj.GetComponent<NodeButton>().y = node.point.y;
            obj.GetComponent<NodeButton>().onNodeClick = onNodeClick;
            
            obj.transform.SetParent(this.transform);
            
            //This is a dumb naming convention. This should change later
            obj.name = node.gameObject.name + "__" + node.point.x + "_" + node.point.y;

            node.setNewGameObject(obj);

        }
        
        void CreateLineBetweenNodes(Node from, Node to)
        {
            string lineName = "Line__" + from.GetHashCode() + "_" + to.GetHashCode();
            LineRenderer lineRenderer = (new GameObject(lineName)).AddComponent<LineRenderer>();
            
            lineRenderer.useWorldSpace = false; //Set's the line to be in local space
            
            LineObject lo = new LineObject(lineRenderer, from, to)
            {
                lr =
                {
                    startWidth = 0.05f,
                    endWidth = 0.05f
                }
            };

            var point1 = new Vector3(from.point.x, from.point.y);
            var point2 = new Vector3(to.point.x, to.point.y);
            
            lo.lr.SetPosition(0, point1);
            lo.lr.SetPosition(1, point2);
            lo.lr.transform.SetParent(this.transform);
        }

        private IDictionary<NodeType, GameObject> GetNodeBlueprints()
        {
 
            nodeBlueprints.Add(commonBattleNode.nodetype, commonBattleNode.gameObject);
            nodeBlueprints.Add(eliteBattleNode.nodetype, eliteBattleNode.gameObject);
            nodeBlueprints.Add(bossBattleNode.nodetype, bossBattleNode.gameObject);
            nodeBlueprints.Add(marketNode.nodetype, marketNode.gameObject);
            nodeBlueprints.Add(restNode.nodetype, restNode.gameObject);
            nodeBlueprints.Add(eventNode.nodetype, eventNode.gameObject);
            nodeBlueprints.Add(treasureNode.nodetype, treasureNode.gameObject);

            return nodeBlueprints;
        }

        //TODO: This function should be global and should modify the entirety of the run instead of just map generation
        void MapSeedConfig(int seed)
        {
            if (seed != 0)
                Random.InitState(seed);
            else
                Random.InitState((int)DateTime.Now.Ticks);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        //TODO: Can this function be handled in PlayerTracking?
        public void NodeClicked(Component sender, object point)
        {            
            int x = ((int[])point)[0];
            int y = ((int[])point)[1];
            
            //Check to see if player is allowed to move
            if (playerTracking.canMove() == false)
            {
                Debug.Log("Player is not allowed to move");
            }
            
            //Check to see if player is allowed to move to node
            if (playerTracking.canMoveToNode(x, y) == false)
            {
                Debug.Log("Player is not allowed to move to node. Player is at [" + playerTracking.GetPlayerPosition().x + "," + playerTracking.GetPlayerPosition().y + "]");
                return;
            }
            Debug.Log(sender + " Clicked at [" + x + "," + y +"]");
            
            Node selectedNode = map.GetNode(new Point(x,y));
            
            //TODO: Check to see if we are on boss level. Because the next section will get a index out of bounds exception
            List<Point> connections = selectedNode.outgoing;
            Point[] newLayerPoints = { };
            foreach (var connection in connections)
            {
                Array.Resize(ref newLayerPoints, newLayerPoints.Length + 1);
                newLayerPoints[^1] = connection;
            }

            playerTracking.UpdatePlayerLocation(selectedNode.point, newLayerPoints);
            
            Debug.Log("Moved player to " + playerTracking.GetPlayerPosition());
            m_SpriteRenderer = selectedNode.getGameObject().GetComponent<SpriteRenderer>();
            m_SpriteRenderer.color = Color.red;
            Debug.Log("Player has traveled to" + playerTracking.GetPlayerTraveled());
            //TODO: Implement player tracking using x,y coordinates 
            
        }


    }

 }
