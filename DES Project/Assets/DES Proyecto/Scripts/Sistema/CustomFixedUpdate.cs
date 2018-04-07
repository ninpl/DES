//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// CustomFixedUpdate.cs (07/04/2018)											\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Clase que realiza un custom Fixed Update optimizado			\\
// Fecha Mod:		07/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio
{
	public class CustomFixedUpdate
	{
		#region Variables Privadas
		private float fixedDeltaTime;
		private float referenceTime = 0;
		private float fixedTime = 0;
		private float maxAllowedTimestep = 0.3f;
		private System.Action fixedUpdate;
		private System.Diagnostics.Stopwatch timeout = new System.Diagnostics.Stopwatch();
		#endregion

		#region Propiedades
		public float FixedDeltaTime
		{
			get { return fixedDeltaTime; }
			set { fixedDeltaTime = value; }
		}
		public float MaxAllowedTimestep
		{
			get { return maxAllowedTimestep; }
			set { maxAllowedTimestep = value; }
		}
		public float ReferenceTime
		{
			get { return referenceTime; }
		}
		public float FixedTime
		{
			get { return fixedTime; }
		}
		#endregion

		#region Actualizadores
		public CustomFixedUpdate(float aFixedDeltaTime, System.Action aFixecUpdateCallback)
		{
			fixedDeltaTime = aFixedDeltaTime;
			fixedUpdate = aFixecUpdateCallback;
		}

		public bool Update(float aDeltaTime)
		{
			timeout.Reset();
			timeout.Start();

			referenceTime += aDeltaTime;
			while (fixedTime < referenceTime)
			{
				fixedTime += fixedDeltaTime;
				if (fixedUpdate != null) fixedUpdate();
				if ((timeout.ElapsedMilliseconds / 1000.0f) > maxAllowedTimestep) return false;
			}
			return true;
		}
		#endregion
	}
}
