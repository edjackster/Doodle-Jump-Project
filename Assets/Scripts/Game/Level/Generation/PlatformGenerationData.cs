public struct PlatformGenerationData
{
    public float X { get; } 
    public float Y { get; } 
    public PlatformType Type { get; } 
    public BoosterType BoosterType { get; } 

    public PlatformGenerationData(float y, float x, PlatformType type, BoosterType boosterType = BoosterType.None)
    {
        Y = y;
        X = x;
        Type = type;
        BoosterType = boosterType;
    }
}