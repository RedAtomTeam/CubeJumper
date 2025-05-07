using UnityEngine;

[System.Serializable]
public class UpgradeTrack
{
    public string name;
    public UpgradeLevel[] levels;
}

[System.Serializable]
public class UpgradeLevel
{
    public int cost;
    public int valueModifier; 
}

[CreateAssetMenu(menuName = "Configs/UpgradesConfig")]
public class UpgradesConfig : ScriptableObject
{
    public UpgradeTrack speedUpgrade;
    public UpgradeTrack jumpUpgrade;
    public UpgradeTrack slideUpgrade;
}
