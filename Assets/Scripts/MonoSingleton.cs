using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindAnyObjectByType<T>() ?? throw new System.Exception(
                        $"Didn't find an instance of {typeof(T).Name} in the scene."
                    );

            return _instance;
        }
    }
}