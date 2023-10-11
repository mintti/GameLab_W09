using System;
using UnityEngine;
public class ResourceManager : MonoBehaviour
{
    #region Singleton
    public static ResourceManager I;

    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public void Init()
    {
        InitClassData();
        InitEnvData();
        InitMonsterData();
        InitBossData();
    }

    void InitClassData()
    {
        
    }

    void InitEnvData()
    {
        
    }
    void InitMonsterData()
    {
        
    }

    void InitBossData()
    {
        
    }
}