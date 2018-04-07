//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// EditorTools.cs (08/04/2018)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Extension para el inspector - GUI							\\
// Fecha Mod:		08/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEditor;
using UnityEngine;
#endregion

namespace MoonAntonio.Extensiones
{
	public static class GUInspectorEx
	{
		#region Variables
		public delegate void OnCollapsibleVisible();
		private static string companyName = "MoonAntonio";
		private static Color backgroundColor = Color.black;
		private static float logoSize = 64.0f;
		#endregion

		#region API
		public static void DrawHeader(Rect rect, string name)
		{
			Texture2D logo = AssetDatabase.LoadAssetAtPath("Assets/DES Proyecto/Source/Texturas/mini_logo_official.png", typeof(Texture2D)) as Texture2D;
			GUISkin skin = AssetDatabase.LoadAssetAtPath("Assets/DES Proyecto/Source/Skins/Editor.guiskin", typeof(GUISkin)) as GUISkin;
			GUIStyle headerStyle_project = new GUIStyle(skin.customStyles[0]);
			GUIStyle headerStyle_title = new GUIStyle(skin.customStyles[1]);

			EditorGUI.DrawRect(new Rect(rect.x, rect.y, rect.width, 64), backgroundColor);

			GUI.DrawTexture(new Rect(rect.x, rect.y, logoSize, logoSize), logo, ScaleMode.ScaleToFit);

			EditorGUI.LabelField(new Rect(rect.x + logoSize, rect.y, rect.width - logoSize, logoSize), companyName, headerStyle_project);
			EditorGUI.LabelField(new Rect(rect.x + logoSize, rect.y, rect.width - logoSize, logoSize), name, headerStyle_title);

			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
			EditorGUILayout.Space();
		}

		public static void DrawCollapsible(Rect rect, string name, ref bool collapsibleContentVisible, OnCollapsibleVisible collapsibleContent)
		{
			Texture2D hideCollapsibleTexture = AssetDatabase.LoadAssetAtPath("Assets/DES Proyecto/Source/Texturas/arrow-point-to-bottom.png", typeof(Texture2D)) as Texture2D;
			Texture2D showCollapsibleTexture = AssetDatabase.LoadAssetAtPath("Assets/DES Proyecto/Source/Texturas/arrow-point-to-right.png", typeof(Texture2D)) as Texture2D;
			GUISkin skin = AssetDatabase.LoadAssetAtPath("Assets/DES Proyecto/Source/Skins/Editor.guiskin", typeof(GUISkin)) as GUISkin;
			GUIStyle collapseStyle = new GUIStyle(skin.customStyles[2]);

			EditorGUI.DrawRect(new Rect(rect.x, GUILayoutUtility.GetRect(rect.width, 2).y, rect.width, 24), backgroundColor);
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(name.ToUpper(), collapseStyle);

			collapseStyle.alignment = TextAnchor.MiddleRight;
			collapsibleContentVisible = EditorGUILayout.Toggle(collapsibleContentVisible, collapseStyle, GUILayout.Width(24), GUILayout.Height(24));
			EditorGUILayout.EndHorizontal();

			GUI.DrawTexture(new Rect(rect.width - 14, GUILayoutUtility.GetRect(rect.width, -2).y - 26, 16, 16), ((collapsibleContentVisible) ? hideCollapsibleTexture : showCollapsibleTexture));

			if (collapsibleContentVisible) collapsibleContent();
		}
		#endregion
	}
}
