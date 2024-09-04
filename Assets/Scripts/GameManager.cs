using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform camPos;

    public Transform name;

    static GameManager instance;

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject();
            go.name = "GameManager";
            go.AddComponent<GameManager>();
        }
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
    }

    private void Update()
    {
        Vector3 dir =  camPos.position - name.position;
        name.forward = -dir;
    }

}
