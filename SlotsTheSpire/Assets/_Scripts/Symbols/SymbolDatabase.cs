using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "DataBase/Symbols")]
public class SymbolDatabase : ScriptableObject
{
    [SerializeField] private List<SymbolData> _symbolDataBase;

    [ContextMenu("Set IDs")]
    public void SetSymbolIDs()
    {
        _symbolDataBase = new List<SymbolData>();

        var foundSymbols = Resources.LoadAll<SymbolData>("SymbolData").OrderBy(i=> i.ID).ToList();

        var hasIDInRange = foundSymbols.Where(i => i.ID != -1 && i.ID < foundSymbols.Count).OrderBy(i=> i.ID).ToList();
        var hasIDNotInRange = foundSymbols.Where(i=> i.ID != 1 && i.ID > foundSymbols.Count).OrderBy(i=> i.ID).ToList();
        var noID = foundSymbols.Where(i=> i.ID <= 1).ToList();

        var index = 0;
        for (int i = 0 ; i < foundSymbols.Count; i++)
        {
            SymbolData symbolToAdd;
            symbolToAdd = hasIDInRange.Find(d=> d.ID == i);

            if(symbolToAdd != null)
            {
                _symbolDataBase.Add(symbolToAdd);
            }
            else if (index < noID.Count)
            {
                noID[index].ID = i;
                symbolToAdd = noID[index];
                index++;
                _symbolDataBase.Add(symbolToAdd);
            }

        }

        foreach(var symbol in hasIDNotInRange)
        {
            _symbolDataBase.Add(symbol);
        }

    }

    public SymbolData GetItem(int id)
    {
        return _symbolDataBase.Find(i=> i.ID == id);
    }

    public SymbolData GenerateRandom(){
        int random = Random.Range(0, _symbolDataBase.Count);
        return _symbolDataBase.Find(i=> i.ID == random);
    }
}
