using UnityEngine;

namespace AztechGames
{
public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var objs = FindObjectsOfType(typeof(T)) as T[];
                if (objs != null && objs.Length > 0) 
                    _instance = objs[0];
                if (_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    //obj.hideFlags = HideFlags.HideAndDontSave;
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}
public class SingletonPersistent<T> : MonoBehaviour where T : Component
{
    public static T Instance { get; private set; }
    public virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
    }
}
}