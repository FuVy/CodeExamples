using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;

public static class SaveLoader
{
    public static string Path { get; private set; } = @$"{Application.persistentDataPath}/Slot0.Guards";

    public static bool Exists() => Exists(Path);
    public static bool Exists(string path) => File.Exists(path);

    public static SaveData Load() => Load(Path);
    public static SaveData Load(string path)
    {
        if (Exists(path))
        {
            var json = File.ReadAllText(path);
            SaveData data = StringSerialization.Deserialize(typeof(SaveData), json) as SaveData;
            
            return data;
        }
        else
        {
            return CreateFile(path);
        }
    }

    public static SaveData CreateFile() => CreateFile(Path);
    public static SaveData CreateFile(string path)
    {

        MonoBehaviour.print(path);
        File.Create(path).Close();
        SaveData save = new SaveData();
        save.Init();
        var json = StringSerialization.Serialize(typeof(SaveData), save);
        File.WriteAllText(path, json);
        return save;
    }

    public static void Save(SaveData save)
    {
        Save(save, Path);
    }

    public static void Save(SaveData save, string path)
    {
        if (Exists(path))
        {
            var tempPath = path + "_temp";
            File.Copy(path, tempPath, true);
            File.Delete(path);
            var json = StringSerialization.Serialize(typeof(SaveData), save);
            File.WriteAllText(path, json);
        }
        else
        {
            CreateFile(path);
        }
    }

    public static void Delete()
    {
        if (Exists())
        {
            File.Delete(Path);
        }
    }

    public static void SetSlot(int index) => Path = GetSlotPath(index);
    public static string GetSlotPath(int index) => @$"{Application.persistentDataPath}/Slot{index}.Guards";
}
