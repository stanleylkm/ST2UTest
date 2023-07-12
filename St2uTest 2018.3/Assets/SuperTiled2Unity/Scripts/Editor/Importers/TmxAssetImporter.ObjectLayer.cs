using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace SuperTiled2Unity.Editor
{
    partial class TmxAssetImporter
    {
        private GameObject ProcessObjectLayer(GameObject goParent, XElement xObjectLayer)
        {
            List<string> layerNamesToSkip = new List<string>() 
            {
                "VirusArea"
            };

            var layerName = xObjectLayer.GetAttributeAs("name", "layer");
            if (layerNamesToSkip.Contains(layerName))
                return null;

            // Have our super object layer loader take care of things
            var loader = new SuperObjectLayerLoader(xObjectLayer);
            loader.AnimationFramerate = SuperImportContext.Settings.AnimationFramerate;
            loader.ColliderFactory = CreateColliderFactory();
            loader.SuperMap = m_MapComponent;
            loader.Importer = this;
            loader.GlobalTileDatabase = m_GlobalTileDatabase;

            // Create our layer and objects
            var objectLayer = goParent.AddSuperLayerGameObject<SuperObjectLayer>(loader, SuperImportContext);
            AddSuperCustomProperties(objectLayer.gameObject, xObjectLayer.Element("properties"));

            RendererSorter.BeginObjectLayer(objectLayer);
            loader.CreateObjects();
            RendererSorter.EndObjectLayer(objectLayer);
            //var parentLayer = objectLayer.gameObject.layer;
            //var parentLayerName = LayerMask.LayerToName(parentLayer);
            //parentLayerName = parentLayerName.Replace("Height", "");
            //int parentLayerHeight = int.Parse(parentLayerName.Substring(0, 1));

            int indexOfFirstCharAfterHeightString = 6;
            int indexOfLastNumberChar;
            if (objectLayer.gameObject.name.Contains("_"))
                indexOfLastNumberChar = objectLayer.gameObject.name.IndexOf("_") - 1;
            else
                indexOfLastNumberChar = objectLayer.gameObject.name.Length - 1;
            string heightString = objectLayer.gameObject.name.Substring(indexOfFirstCharAfterHeightString, indexOfLastNumberChar - indexOfFirstCharAfterHeightString + 1);

            return objectLayer.gameObject;
        }

        private ColliderFactory CreateColliderFactory()
        {
            if (m_MapComponent.m_Orientation == MapOrientation.Isometric)
            {
                return new ColliderFactoryIsometric(m_MapComponent.m_TileWidth, m_MapComponent.m_TileHeight, SuperImportContext);
            }
            else
            {
                return new ColliderFactoryOrthogonal(SuperImportContext);
            }
        }
    }
}
