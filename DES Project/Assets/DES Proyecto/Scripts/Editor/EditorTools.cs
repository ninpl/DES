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
		private static List<string> layers;
		private static string[] nombresLayer;
		#endregion

		#region Funcionalidades
		public static LayerMask LayerMaskField(string nombre, LayerMask seleccion)
		{
			if (layers == null)
			{
				layers = new List<string>();
				nombresLayer = new string[4];
			}
			else
			{
				layers.Clear();
			}

		
			int layersVacias = 0;

			for (int i = 0; i < 32; i++)
			{
				string nomLayer = LayerMask.LayerToName(i);

				if (nomLayer != "")
				{

					for (; layersVacias > 0; layersVacias--) layers.Add("Layer " + (i - layersVacias));
					layers.Add(nomLayer);
				}
				else
				{
					layersVacias++;
				}
			}

			if (nombresLayer.Length != layers.Count)
			{
				nombresLayer = new string[layers.Count];
			}

			for (int i = 0; i < nombresLayer.Length; i++) nombresLayer[i] = layers[i];

			seleccion.value = EditorGUILayout.MaskField(nombre, seleccion.value, nombresLayer);

			return seleccion;
		}
		#endregion
	}
}
