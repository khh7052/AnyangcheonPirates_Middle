using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    [System.Serializable]
    public class ObjectData
    {
        public Vector3 pos;
        public string patrolPathName;
        public float maxSpeed;
    }

    [System.Serializable]
    public class ObjectList
    {
        public List<ObjectData> list = new List<ObjectData>();
    }

    [SerializeField]
    string filePath;

    enum State
    {
        SAVE,
        LOAD
    }

    [SerializeField]
    State state;

    [SerializeField]
    GameObject top;

    [SerializeField]
    GameObject prefab;



    // Start is called before the first frame update
    void Start()
    {
        filePath = Path.Combine(Application.streamingAssetsPath, "objData.json");

        if (state == State.SAVE)
        {
            SaveData();
        }
        else
        {
            LoadData();
        }
    }

    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            // 파일에서 JSON 읽고 객체로 변환
            string json = File.ReadAllText(filePath);
            ObjectList objectList = JsonUtility.FromJson<ObjectList>(json);

            foreach (var i in objectList.list)
            {
                var enemy = Instantiate(prefab, top.transform);

                enemy.transform.position = i.pos;


            }

            Debug.Log("\nData loaded: " + json);
        }
        else
        {
            Debug.Log("No save file found.");
        }
    }

    public void SaveData()
    {
        var list = GameObject.FindGameObjectsWithTag("Enemy");

        ObjectList objectList = new ObjectList();

        foreach (var obj in list)
        {
            ObjectData data = new ObjectData();

            data.pos = obj.transform.position;


            objectList.list.Add(data);
        }

        // JSON 형식으로 변환하고 파일로 저장
        string json = JsonUtility.ToJson(objectList, true);

        // 경로상 폴더 없으면 폴더 생성
        var path = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        File.WriteAllText(filePath, json);
        Debug.Log("Data saved: " + json);
    }
}
