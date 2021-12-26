using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Tools/ItemTemplate", order = 1)]
public class ItemScriptableObject : ScriptableObject
{
    public string Name;
    public Sprite Image;
}
