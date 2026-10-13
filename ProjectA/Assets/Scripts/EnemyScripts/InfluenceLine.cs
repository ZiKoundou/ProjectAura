using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

//Utility class used for drawing "lines of influence" between different objects in the editor
public class InfluenceLine : MonoBehaviour
{
    public enum InfluenceType
    {
        General,
        OnStart,
        OnComplete,
    }


#if UNITY_EDITOR

    static float arrowScale = 0.2f;
    public static void DrawLine(Transform transform1, Transform transform2, InfluenceType type = InfluenceType.General)
    {
        Vector3 pos1 = transform1.position;
        Vector3 pos2 = transform2.position;
        Vector3 dir = (pos2 - pos1).normalized;
        Vector3 midPos = Vector3.Lerp(pos1, pos2, 0.5f);

        Vector3 q1 = Vector3.Lerp(pos1, pos2, 0.25f);
        Vector3 q2 = Vector3.Lerp(pos1, pos2, 0.75f);

        //Math to draw arrows
        Vector3 perpendicular = Vector3.Cross(SceneView.lastActiveSceneView.camera.transform.forward, dir).normalized;
        

        Gizmos.DrawLine(pos1, pos2);
        
        //Arrow 1
        Gizmos.DrawLine(q1 + ((perpendicular - dir) * arrowScale), q1);
        Gizmos.DrawLine(q1 - ((perpendicular + dir) * arrowScale), q1);

        //Arrow 2
        Gizmos.DrawLine(q2 + ((perpendicular - dir) * arrowScale), q2);
        Gizmos.DrawLine(q2 - ((perpendicular + dir) * arrowScale), q2);

        if(type != InfluenceType.General)
        {
            Gizmos.DrawIcon(midPos, "Gubby" + type.ToString());    
        }
    }
#endif
}
