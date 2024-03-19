using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "TE/Recipe", order = 0)]
public class Recipe : ScriptableObject
{
    [Header("Liste des composants")]
    [SerializeField] private StructItem item0;
    [SerializeField] private StructItem item1;
    [SerializeField] private StructItem item2;

    [Header("Résultat")]
    [SerializeField] private StructItem result;

    public StructItem GetItem0
    {
        get { return item0; }
    }

    public StructItem GetItem1
    {
        get { return item1; }
    }

    public StructItem GetItem2
    {
        get { return item2; }
    }

    public StructItem GetResult
    {
        get { return result; }
    }
}

[System.Serializable]
public class StructItem
{
    public int id;
    public int quantity;
}
