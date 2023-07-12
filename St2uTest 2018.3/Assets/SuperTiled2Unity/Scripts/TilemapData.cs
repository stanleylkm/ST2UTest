using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SuperTiled2Unity.Dictionaries;
using UnityEngine.Tilemaps;

namespace SuperTiled2Unity
{
    public class TilemapData : MonoBehaviour
    {
        public int Height;
        public int Priority;
        public int DefaultSortingOrder;
        public List<int> IndexesOfContainedCom2dPaths;

        [SerializeField]
        private Dictionary_Vector3Int_FlipFlags m_TilePositionFlipFlags = new Dictionary_Vector3Int_FlipFlags();

        int shaderId_Height = Shader.PropertyToID("_Height");

        TilemapRenderer _tilemapRenderer;
        TilemapRenderer tilemapRenderer
        {
            get
            {
                if (_tilemapRenderer == null)
                    _tilemapRenderer = GetComponent<TilemapRenderer>();
                return _tilemapRenderer;
            }
        }

        CompositeCollider2D _compositeCollider2D;
        CompositeCollider2D compositeCollider2D
        {
            get
            {
                if (_compositeCollider2D == null)
                    _compositeCollider2D = GetComponent<CompositeCollider2D>();
                return _compositeCollider2D;
            }
        }

        private void Awake()
        {
            var strings = name.Split('_');
            var heightLayerNameString = strings[0];
            var heightValueString = heightLayerNameString.Replace("Height", "");
            Height = int.Parse(heightValueString);
        }

        public void SetFlipFlags(Vector3Int pos3, FlipFlags flags)
        {
            if (flags != 0)
            {
                m_TilePositionFlipFlags.Add(pos3, flags);
            }
        }

        public FlipFlags GetFlipFlags(Vector3Int pos3)
        {
            FlipFlags flags;
            if (m_TilePositionFlipFlags.TryGetValue(pos3, out flags))
            {
                return flags;
            }

            return 0;
        }

    }
}
