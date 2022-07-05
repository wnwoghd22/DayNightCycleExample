using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

public class SkyBoxWithMoon : ShaderGUI
{
    private enum DiskMode
    {
        None,
        Simple,
        HighQuality
    }

    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
    {
        MaterialProperty sunDiskModeProp = FindProperty("_SunDisk", props);
        DiskMode sunDiskMode = (DiskMode)sunDiskModeProp.floatValue;

        float labelWidth = EditorGUIUtility.labelWidth;

        for (var i = 0; i < props.Length; i++)
        {
            // dropdowns should have full width
            if (props[i].type == MaterialProperty.PropType.Float)
                EditorGUIUtility.labelWidth = labelWidth;
            else
                materialEditor.SetDefaultGUIWidths();

            if ((props[i].flags & MaterialProperty.PropFlags.HideInInspector) != 0)
                continue;

            //_SunSizeConvergence is only used with the HighQuality sun disk.
            if ((props[i].name == "_SunSizeConvergence") && (sunDiskMode != DiskMode.HighQuality))
                continue;
            //_MoonSizeConvergence too.
            if ((props[i].name == "_MoonSizeConvergence") && (sunDiskMode != DiskMode.HighQuality))
                continue;

            float h = materialEditor.GetPropertyHeight(props[i], props[i].displayName);
            Rect r = EditorGUILayout.GetControlRect(true, h, EditorStyles.layerMaskField);

            materialEditor.ShaderProperty(r, props[i], props[i].displayName);
        }
    }
}
