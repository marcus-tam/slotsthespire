 using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _Scripts.MapManager;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using Newtonsoft.Json;

public class ProceduralGeneration : MonoBehaviour
{
    //TODO: Design ruleset for map generation
    [SerializeField] int width, levels;

    //Map Config
    [SerializeField] GameObject mapNode;
    [SerializeField] GameObject commonBattleNode, eliteBattleNode, bossBattleNode;
    [SerializeField] GameObject marketNode, restNode, eventNode;
    //Seed Config
    [SerializeField] private int mapSeed;
    
    //Variable Config
    private int[,] _arr, graphAdjacencyMatrix;

    private List<LineObject> LineObjects = new List<LineObject>();
    private static readonly List<List<Node>> nodes = new List<List<Node>>();
    /*
     * 0 0 1 0 0
     * 1 0 1 1 1
     * 0 1 1 0 0
     * 0 1 1 0 0
     * 0 0 1 0 0
     */
    
    void Start()
    {
        bool debug = false;
        //Check for predefined seed
        MapSeedConfig(mapSeed);
        
        //Initialize 2D array to represent map as matrix. 
        _arr = new int[width, levels];
        
        MapGeneration();

        CreatePathBetweenNodes();
        
        
        //Fetch all mapnodes of _arr. Graph Adjacency Matrix size is n x n where n = num of mapnodes
        int count = _arr.Cast<int>().Count(cell => cell == 1);
        
        //Initialize 2d array to represent map as graph adjacency matrix. Size should be num of nodes^^2
        graphAdjacencyMatrix = new int[count, count];

        
        //Debugging Station
        if (debug == true)
        {
            // DebugPrintMatrix(_arr, width, levels);

            foreach (LineObject lineObject in LineObjects)
            {
                Debug.Log(lineObject.lr.name);
            }  
        }

    }

    public void setBossLevel()
    {
        for (int i = 0; i < width; i++)
        {
            _arr[i, levels - 1] = 0;
        }
        
        _arr[width/2, levels - 1] = 1;
    }
    private static Node GetNode(Point p)
    {
        if (p.y >= nodes.Count) return null;
        if (p.x >= nodes[p.y].Count) return null;

        return nodes[p.y][p.x];
    }
    void SpawnGeneration()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < levels - 1; y++)
            {
                //TODO: Create dictionary to assign mapNode to specific type (battleNode, marketNode, eventNode, etc)
                if (_arr[x, y] == 1)
                {
                    Node node = new Node(mapNode, new Point(x, y));
                    SpawnObj(node.gameObject, "MapNode", new Point(x, y));
                }
                else
                {
                    continue;
                    // GameObject EmptyObj = new GameObject("emptyNode");

                }
            }
        }
        Node bossNode = new Node(bossBattleNode, new Point(width/2, levels - 1));
        SpawnObj(bossNode.gameObject, "bossBattleNode", new Point(width/2, levels - 1));

        setBossLevel();
    }
    void MapGeneration()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < levels; y++)
            {
                _arr[x, y] = Random.Range(0, 2);
            }
        }
        SpawnGeneration(); 
    }

    void SpawnObj(GameObject obj, string objName, Point p)
    {
        obj = Instantiate(obj, new Vector2(p.x, p.y), Quaternion.identity);
        obj.transform.SetParent(this.transform);
        //This is a dumb naming convention. This should change later
        obj.name = objName + "__" + p.x + "_" + p.y;
    }

    void CreatePathBetweenNodes()
    {
        // We might have to do something to read _arr and apply it the GAM
        for (int i = 1; i < levels; i++) //Loops through levels
        {
            for (int j = 0; j < width; j++) //Loops through target_node per level
            {
                int targetNode = _arr[j, i];
                for (int k = 0; k < width; k++) //Loops through origin_nodes per level - 1 
                {
                    int originNode= _arr[k, i - 1];
                    if (targetNode == 1 && originNode == 1)
                    {
                        //TODO: Implement GAM
                        // graphAdjacencyMatrix[i, j] = 1;
                        
                        //This will be generated somehow already
                        Node from = new Node(mapNode, new Point(k, i - 1));
                        from.AddOutgoing(new Point(j, i ));
                        
                        Node to = new Node(mapNode, new Point(j, i ));
                        to.AddIncoming(new Point(k, i-1));
                        
                        CreateLineBetweenNodes(from, to);
                    }
                    else
                    {
                        // graphAdjacencyMatrix[i, j] = 0;
                    }
                }
            }
        }
    }
    
    void DFSPathGeneration(int buffer)
    {
        //Perhaps we will accomplish this with a 2d matrix int version and translate to a 2d matrix Node version after 
        //Start at bottom layer.
        List<Node> nodeList = new List<Node>();
        for (int level = 0; level < levels; level++)
        {
            //First detect existing node.
            for (int col = 0; col < width; col++)
            {
                //Traverse upward starting at left buffer.
                continue;
            }   
            
        }
        
        //If success, repeat previous until failure:
        
        //If Failure, check to see if next node (left buffer node + 1) exists
        
        //If success, continue upwards; else:
        
        //If failure; goto 1st failure and increment by 1. If this fails while we have not reached battle boss node, mark this path as a failure.
        
        //Backtrack through path until we find a path that works.
        
        //If no path works, remove all nodes from matrix.
        
        
        
    }
    void CreateLineBetweenNodes(Node from, Node to)
    {

        string lineName = "Line__" + from.GetHashCode() + "_" + to.GetHashCode();
        LineRenderer lineRenderer = (new GameObject(lineName)).AddComponent<LineRenderer>();
        
        LineObject lo = new LineObject(lineRenderer, from, to);

        lo.lr.transform.SetParent(this.transform, true);
        lo.lr.startWidth = 0.05f;
        lo.lr.endWidth = 0.05f;
        
        var point1 = new Vector3(from.point.x, from.point.y);
        var point2 = new Vector3(to.point.x, to.point.y);
        
        lo.lr.SetPosition(0, point1);
        lo.lr.SetPosition(1, point2);
        
        LineObjects.Add(lo);
    }
    
    void RemoveRandomPathBetweenNode()
    {
        //TODO: Design algorithm to remove some paths between paths. 
        //TODO: Design validator to ensure paths don't have dead ends or cross over each other.

        
    }

    void GetDetailsOnInstantiatedObject(GameObject obj)
    {
        Debug.Log(obj.GetInstanceID());
    }

    void DebugPrintMatrix(int[,] matrix, int _width, int _length)
    {
        for (int i = 0; i < _length; i++)
        {
            string row = "";
            for (int j = 0; j < _width; j++)
            {
                row += matrix[j, i] + " ";
            }
            Debug.Log(row);
        }
    }

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
}
