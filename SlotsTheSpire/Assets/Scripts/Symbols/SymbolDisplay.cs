using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolDisplay : MonoBehaviour
{

    public SymbolData symbol;
    public Image artworkImage;
    // Start is called before the first frame update
    void Start()
    {
        artworkImage.sprite = symbol.artwork;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
