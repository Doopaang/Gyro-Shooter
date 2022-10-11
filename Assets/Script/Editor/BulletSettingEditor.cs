using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BulletSetting), true)]
[CanEditMultipleObjects]
public class BulletSettingEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ForEach((obj) =>
        {
            switch (obj.kind)
            {
                case BulletFactory.BulletKind.Classic:
                    break;

                case BulletFactory.BulletKind.Chase:
                    obj.ParamFloat = EditorGUILayout.Slider("Bullet Rotate Speed", obj.ParamFloat, 0.1f, 1.0f);
                    break;

                case BulletFactory.BulletKind.Laser:
                    obj.ParamFloat = EditorGUILayout.FloatField("Length", obj.ParamFloat);
                    break;
            }
            if(!obj.shotAwake)
            {
                obj.waitTime = EditorGUILayout.FloatField("Wait", obj.waitTime);
            }
            if (GUI.changed)
            {
                EditorUtility.SetDirty(obj);
            }
        });
    }

    void ForEach(System.Action<BulletSetting> func)
    {
        foreach (var t in targets)
        {
            func(t as BulletSetting);
        }
    }
}
