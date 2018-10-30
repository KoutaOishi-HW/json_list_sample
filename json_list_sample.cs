using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class json_list_sample : MonoBehaviour
{
    void Start()
    {
        SetJson();
        //Call();
    }

    [Serializable]
    public class Player
    {
        public int id;
        public int count;
    }
    public void SetJson()
    {

        PlayerData p = new PlayerData();

        Player player = new Player();

        player.id = 1;
        player.count = 12;

        p.playerData.Add("勇者", player);


        Player player2 = new Player();

        player2.id = 2;
        player2.count = 8;

        p.playerData.Add("戦士", player2);

        SavePlayerDate(p);
    }

    public void SavePlayerDate(PlayerData p)
    {

        StreamWriter writer;

        string json = JsonUtility.ToJson(p);

        writer = new StreamWriter(Application.dataPath + "/savedata.json", false);
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }

    public PlayerData LoadPlayerData()
    {

        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(Application.dataPath + "/savedata.json");
        datastr = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<PlayerData>(datastr);
    }

    public void Call()
    {

        PlayerData playerData = LoadPlayerData();
        string json = JsonUtility.ToJson(playerData);
        Debug.Log(json);
    }

    public class PlayerData : ISerializationCallbackReceiver
    {
        public Dictionary<string, Player> playerData = new Dictionary<string, Player>();

        public List<string> playerKeys = new List<string>();
        public List<Player> playerVals = new List<Player>();

        public void OnBeforeSerialize()
        {
            playerKeys.Clear();
            playerVals.Clear();
            foreach (var kvp in playerData)
            {
                playerKeys.Add(kvp.Key);
                playerVals.Add(kvp.Value);
            }
        }
        public void OnAfterDeserialize()
        {
            playerData = new Dictionary<string, Player>();
            for (int i = 0; i < Math.Min(playerKeys.Count, playerVals.Count); i++)
            {
                playerData.Add(playerKeys[i], playerVals[i]);
            }
        }
    }
}
