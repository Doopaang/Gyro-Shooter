using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShooterAction), true)]
[CanEditMultipleObjects]
public class ShooterActionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ForEach((obj) =>
        {
            switch (obj.kind)
            {
                case ShooterAction.ShooterKind.Stand:
                    break;

                case ShooterAction.ShooterKind.Spin:
                case ShooterAction.ShooterKind.Rand:
                    obj.value = EditorGUILayout.FloatField("Speed", obj.value);
                    break;

                case ShooterAction.ShooterKind.Chase:
                    obj.value = EditorGUILayout.FloatField("Angle", obj.value);
                    break;
            }
            if (GUI.changed)
            {
                EditorUtility.SetDirty(obj);
            }
        });
    }

    void ForEach(System.Action<ShooterAction> func)
    {
        foreach (var t in targets)
        {
            func(t as ShooterAction);
        }
    }
}
