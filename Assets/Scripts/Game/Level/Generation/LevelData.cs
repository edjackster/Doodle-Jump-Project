using System.Collections.Generic;

public class LevelData
{
    public List<PlatformGenerationData> PlatformsData {get;}
    public List<TrapGenerationData> TrapsData {get;}

    public LevelData(List<PlatformGenerationData> platformsData, List<TrapGenerationData> trapsData)
    {
        PlatformsData = platformsData;
        TrapsData = trapsData;
    }
}
