//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// FPSCounter.cs (00/00/0000)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:																	\\
// Fecha Mod:		00/00/0000													\\
// Ultima Mod:																	\\
//******************************************************************************\\

#region Librerias
using System;
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace MoonAntonio.Debug
{
	/// <summary>
	/// <para></para>
	/// </summary>
	[RequireComponent(typeof(Text))]
	public class FPSCounter : MonoBehaviour 
	{
		#region Variables Privadas
		const float fpsMeasurePeriod = 0.5f;
		private int m_FpsAccumulator = 0;
		private float m_FpsNextPeriod = 0;
		private int m_CurrentFps;
		const string display = "{0} FPS";
		private Text m_Text;
		#endregion
	}
}
