using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Pattern), true)]
[CanEditMultipleObjects]
public class PatternEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ForEach((obj) =>
        {
            switch (obj.kind)
            {
                case Pattern.PatternKind.End:
                    break;

                case Pattern.PatternKind.Time:
                    obj.end = EditorGUILayout.FloatField("Time", obj.end);
                    break;
            }

            switch (obj.moveKind)
            {
                case Pattern.MoveKind.Stand:
                    break;

                case Pattern.MoveKind.Spin:
                    obj.speed = EditorGUILayout.FloatField("Speed", obj.speed);
                    break;

                case Pattern.MoveKind.Chase:
                    obj.speed = EditorGUILayout.Slider("Angle", obj.speed, 0.01f, 1.0f);
                    break;
            }
            if (GUI.changed)
            {
                EditorUtility.SetDirty(obj);
            }
        });
    }

    void ForEach(System.Action<Pattern> func)
    {
        foreach (var t in targets)
        {
            func(t as Pattern);
        }
    }
}
