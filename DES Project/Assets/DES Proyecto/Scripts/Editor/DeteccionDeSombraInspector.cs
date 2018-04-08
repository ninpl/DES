//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// DeteccionDeSombraInspector.cs (08/04/2018)									\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Inspector de DeteccionDeSombra								\\
// Fecha Mod:		08/04/2018													\\
// Ultima Mod:		Version Inicial												\\
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
			GUInspectorEx.DrawHeader(bgRect, "Deteccion En Sombras");

			//=======================================================================================================
			//---------------------------------------------//OPCIONES GENERALES\\-------------------------------------------
			GUInspectorEx.OnCollapsibleVisible generalCollapsibleVisible = delegate ()
			{
				sd.detectMode = (MODODETECCION)EditorGUILayout.EnumPopup("Modo de deteccion:", sd.detectMode);
				sd.raycastinRate = EditorGUILayout.Slider(new GUIContent("Raycasting Rate:", "Puntuacion para probar si tus objetivos estan dentro o fuera de una sombra."), sd.raycastinRate, 10.0f, 60.0f);
				EditorGUILayout.PropertyField(layers, new GUIContent("Layers:", "Layer que se usa para ignorar selectivamente Colliders cuando se lanza un rayo."), true);
				GUILayout.BeginHorizontal();
				GUILayout.Label(new GUIContent("¿Investigacion automatica de luces?", "Definido si quieres investigar todas las luces de la escena o si las configuras."));
				GUILayout.FlexibleSpace();
				sd.IsAuto = EditorGUILayout.Toggle(sd.IsAuto);
				GUILayout.EndHorizontal();

				if (!sd.IsAuto)
				{
					EditorGUI.indentLevel++;
					EditorGUILayout.PropertyField(lights, new GUIContent("Luces"), true);
					EditorGUI.indentLevel--;
				}

			};
			GUInspectorEx.DrawCollapsible(bgRect, "PARAMETROS", ref ShowHideGeneral, generalCollapsibleVisible);

			//=======================================================================================================
			//---------------------------------------------//OPCIONES GENERALES\\-------------------------------------------
			GUInspectorEx.OnCollapsibleVisible targetsCollapsibleVisible = delegate ()
			{
				EditorGUILayout.PropertyField(shadowtargets, new GUIContent("Targets", "Transform de GameObjects para se detectadas en la sombra."), true);
			};
			GUInspectorEx.DrawCollapsible(bgRect, "OBJETIVOS", ref ShowHideTargets, targetsCollapsibleVisible);

			//=======================================================================================================
			//---------------------------------------------//OPCIONES COMUNES\\-------------------------------------------
			GUInspectorEx.OnCollapsibleVisible commonCollapsibleVisible = delegate ()
			{
				EditorGUILayout.PropertyField(onChangeState, new GUIContent("On Change State", "Evento llamado cuando se entra, sale o cambia de sombra."), true);
				EditorGUILayout.PropertyField(onEnterShadow, new GUIContent("On Enter Shadow", "Evento llamado cuando el gameobject entra en la sombra."), true);
				EditorGUILayout.PropertyField(onExitShadow, new GUIContent("On Exit Shadow", "Evento llamado cuando el gameobject sale de la sombra."), true);
				EditorGUILayout.PropertyField(onShadow, new GUIContent("On Shadow", "Evento llamado cuando se esta dentro de la sombra."), true);
				EditorGUILayout.PropertyField(outShadow, new GUIContent("Out Shadow", "Evento llamado cuano no se esta dentro de la sombra."), true);
			};
			GUInspectorEx.DrawCollapsible(bgRect, "EVENTOS COMUNES", ref ShowHideCommons, commonCollapsibleVisible);

			//=======================================================================================================
			//----------------------------------------------//UPDATE\\-----------------------------------------------
			serializedObject.ApplyModifiedProperties();
		}
	}
}
