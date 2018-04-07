//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// DragMouseOrbit.cs (00/00/0000)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:																	\\
// Fecha Mod:		00/00/0000													\\
// Ultima Mod:																	\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio
{
	[AddComponentMenu("Camera-Control/Mouse drag Orbit with zoom")]
	public class DragMouseOrbit : MonoBehaviour 
	{
		#region Variables Publicas
		public Transform target;
		public float distance = 5.0f;
		public float xSpeed = 120.0f;
		public float ySpeed = 120.0f;
		public float zSpeed = 5.0f;
		public float yMinLimit = -20f;
		public float yMaxLimit = 80f;
		public float distanceMin = .5f;
		public float distanceMax = 15f;
		public float smoothTime = 2f;
		#endregion

		#region Variables Privadas
		private bool isActive = true;
		private float rotationYAxis = 0.0f;
		private float rotationXAxis = 0.0f;
		private float velocityX = 0.0f;
		private float velocityY = 0.0f;
		#endregion
	}
}
