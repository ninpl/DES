//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// DeteccionDeSombraInspector.cs (00/00/0000)									\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:																	\\
// Fecha Mod:		00/00/0000													\\
// Ultima Mod:																	\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEditor;
#endregion

namespace MoonAntonio
{
	[CustomEditor(typeof(DeteccionDeSombra))]
	public class DeteccionDeSombraInspector : Editor
	{
		SerializedProperty shadowtargets, layers, lights, onChangeState, onEnterShadow, onExitShadow, onShadow, outShadow;
		DeteccionDeSombra sd;

		bool ShowHideGeneral = true;
		bool ShowHideTargets = true;
		bool ShowHideCommons = true;

		void OnEnable()
		{
			sd = (DeteccionDeSombra)target;
			shadowtargets = serializedObject.FindProperty("targets");
			layers = serializedObject.FindProperty("layers");
			lights = serializedObject.FindProperty("lights");
			onChangeState = serializedObject.FindProperty("onChangeState");
			onEnterShadow = serializedObject.FindProperty("onEnterShadow");
			onExitShadow = serializedObject.FindProperty("onExitShadow");
			onShadow = serializedObject.FindProperty("onShadow");
			outShadow = serializedObject.FindProperty("outShadow");
		}

		public override void OnInspectorGUI()
		{
			Rect bgRect = EditorGUILayout.GetControlRect();
			bgRect = new Rect(bgRect.x - 10, bgRect.y, bgRect.width + 10, bgRect.height);

			//=======================================================================================================
			//-----------------------------------------------//HEADER\\----------------------------------------------
			GUInspectorEx.DrawHeader(bgRect, "Shadow Detect");

			//=======================================================================================================
			//---------------------------------------------//OPTIONS GENERAL\\-------------------------------------------
			GUInspectorEx.OnCollapsibleVisible generalCollapsibleVisible = delegate ()
			{
				sd.detectMode = (MODODETECCION)EditorGUILayout.EnumPopup("Shadow Detect Mode:", sd.detectMode);
				sd.raycastinRate = EditorGUILayout.Slider(new GUIContent("Raycasting Rate:", "Rate to test if your targets are in or out a shadow"), sd.raycastinRate, 10.0f, 60.0f);
				EditorGUILayout.PropertyField(layers, new GUIContent("Layers:", "Layer that is used to selectively ignore Colliders when casting a ray"), true);
				GUILayout.BeginHorizontal();
				GUILayout.Label(new GUIContent("Automatic lights research?", "Defined if you want to research all lights of the scene or if you set them"));
				GUILayout.FlexibleSpace();
				sd.IsAuto = EditorGUILayout.Toggle(sd.IsAuto);
				GUILayout.EndHorizontal();

				if (!sd.IsAuto)
				{
					EditorGUI.indentLevel++;
					EditorGUILayout.PropertyField(lights, new GUIContent("Lights"), true);
					EditorGUI.indentLevel--;
				}

			};
			GUInspectorEx.DrawCollapsible(bgRect, "PARAMETERS", ref ShowHideGeneral, generalCollapsibleVisible);

			//=======================================================================================================
			//---------------------------------------------//OPTIONS GENERAL\\-------------------------------------------
			GUInspectorEx.OnCollapsibleVisible targetsCollapsibleVisible = delegate ()
			{
				EditorGUILayout.PropertyField(shadowtargets, new GUIContent("Targets", "Transforms of GameObjects to detect on shadow"), true);
			};
			GUInspectorEx.DrawCollapsible(bgRect, "TARGETS", ref ShowHideTargets, targetsCollapsibleVisible);

			//=======================================================================================================
			//---------------------------------------------//OPTIONS COMMON\\-------------------------------------------
			GUInspectorEx.OnCollapsibleVisible commonCollapsibleVisible = delegate ()
			{
				EditorGUILayout.PropertyField(onChangeState, new GUIContent("On Change State", "Event call on change of state, enter or exit shadow"), true);
				EditorGUILayout.PropertyField(onEnterShadow, new GUIContent("On Enter Shadow", "Event call if the GameObject enter in a shadow"), true);
				EditorGUILayout.PropertyField(onExitShadow, new GUIContent("On Exit Shadow", "Event call if the GameObject exit a shadow"), true);
				EditorGUILayout.PropertyField(onShadow, new GUIContent("On Shadow", "Event call if the GameObject is on a shadow and stay in"), true);
				EditorGUILayout.PropertyField(outShadow, new GUIContent("Out Shadow", "Event call if the GameObject is out of a shadow and stay out"), true);
			};
			GUInspectorEx.DrawCollapsible(bgRect, "COMMON EVENTS", ref ShowHideCommons, commonCollapsibleVisible);

			//=======================================================================================================
			//----------------------------------------------//UPDATE\\-----------------------------------------------
			serializedObject.ApplyModifiedProperties();
		}
	}
}
