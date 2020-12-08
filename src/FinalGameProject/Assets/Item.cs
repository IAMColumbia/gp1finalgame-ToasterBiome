using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{

    public string description;





    public string Name;
    public Sprite sprite;

    [SerializeField] string id;
    public string ID { get { return id; } }



    [Range(1,999)]
    public int maxStack = 999;
    public int researchValue;




    private void OnValidate()
    {
    #if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    #endif
        Name = name;
    }

    public Item GetCopy()
    {
        return this;
    }

    public void Destroy()
    {
        
    }
}
