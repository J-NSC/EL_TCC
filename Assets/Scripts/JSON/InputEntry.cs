using System;

[Serializable]
public class InputEntry
{
    public string playerName;
    public int points;

    public InputEntry(string playerName, int points)
    {
        this.playerName = playerName;
        this.points = points;
    }
}
