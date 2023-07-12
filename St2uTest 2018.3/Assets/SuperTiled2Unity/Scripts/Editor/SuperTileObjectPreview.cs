using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPreview(typeof(SuperTiled2Unity.SuperTile))]
public class SuperTileObjectPreview : ObjectPreview
{
    public override bool HasPreviewGUI()
    {
        return true;
    }

    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        var tileSprite = ((SuperTiled2Unity.SuperTile)target).m_Sprite;
        GUI.DrawTexture(r, AssetPreview.GetAssetPreview(tileSprite), ScaleMode.ScaleToFit);
    }
}
