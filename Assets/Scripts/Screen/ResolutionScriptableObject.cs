using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Tools/ResolutionTemplate", order = 2)]
public class ResolutionScriptableObject : ScriptableObject
{
    public int Width;
    public int Height;
    public int FrameRate;
}
