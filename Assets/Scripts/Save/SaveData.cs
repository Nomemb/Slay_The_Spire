using System.Collections.Generic;
using System.IO;

[System.Serializable]
public class SaveData
{
    public List<string> testDataA = new List<string>();
    public List<int> testDataB = new List<int>();

    public int gold;
    public int power;
}
