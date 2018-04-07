//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// EditorTools.cs (08/04/2018)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Herramientas para el editor									\\
// Fecha Mod:		08/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
#endregion

namespace MoonAntonio
{
	public class EditorTools : MonoBehaviour 
	{
		#region Variables
		static List<string> layers;
		static string[] layerNames;
		#endregion

		#region Funcionalidades
		public static LayerMask LayerMaskField(string label, LayerMask selected)
		{

			if (layers == null)
			{
				layers = new List<string>();
				layerNames = new string[4];
			}
			else
			{
				layers.Clear();
			}

			int emptyLayers = 0;
			for (int i = 0; i < 32; i++)
			{
				string layerName = LayerMask.LayerToName(i);

				if (layerName != "")
				{

					for (; emptyLayers > 0; emptyLayers--) layers.Add("Layer " + (i - emptyLayers));
					layers.Add(layerName);
				}
				else
				{
					emptyLayers++;
				}
			}

			if (layerNames.Length != layers.Count)
			{
				layerNames = new string[layers.Count];
			}
			for (int i = 0; i < layerNames.Length; i++) layerNames[i] = layers[i];

			selected.value = EditorGUILayout.MaskField(label, selected.value, layerNames);

			return selected;
		}
		#endregion
	}
}
