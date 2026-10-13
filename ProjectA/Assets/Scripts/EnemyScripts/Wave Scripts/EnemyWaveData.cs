using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWaveData : MonoBehaviour
{
    public UnityEvent OnWaveStart = new();
    public UnityEvent OnWaveComplete = new();


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        DrawEventInfluence(OnWaveStart, InfluenceLine.InfluenceType.OnStart);
        DrawEventInfluence(OnWaveComplete, InfluenceLine.InfluenceType.OnComplete);
    }

    private void DrawEventInfluence(UnityEvent unityEvent, InfluenceLine.InfluenceType type)
    {
        for(int i = 0; i < unityEvent.GetPersistentEventCount(); i++)
        {
            Object target = unityEvent.GetPersistentTarget(i);

            if(target is GameObject obj)
            {
                InfluenceLine.DrawLine(transform, obj.transform, type);
            }
        }
    }

#endif
}
