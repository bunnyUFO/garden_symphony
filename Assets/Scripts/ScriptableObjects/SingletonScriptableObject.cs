using UnityEngine;


public abstract class SingletonScriptableObject<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance;

    public static T Instance {
        get
        {
            if (_instance == null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                if (results.Length == 0)
                {
                    Debug.LogError("SingletonSerializedScriptableObject count is 0 of " + typeof(T));
                    return null;
                }

                if (results.Length > 1)
                {
                    Debug.LogError("SingletonSerializedScriptableObject count more than 1 of" + typeof(T));
                    return null;
                }

                _instance = results[0];
                _instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
            }

            return _instance;
        }
    }
}
