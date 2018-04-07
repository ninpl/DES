//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// FPSCounter.cs (07/04/2018)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Controlador de FPS											\\
// Fecha Mod:		07/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace MoonAntonio.Debug
{
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

		#region Inicializadores
		private void Start()
		{
			m_FpsNextPeriod = Time.realtimeSinceStartup + fpsMeasurePeriod;
			m_Text = GetComponent<Text>();
		}
		#endregion

		#region Actualizadores
		private void Update()
		{
			m_FpsAccumulator++;
			if (Time.realtimeSinceStartup > m_FpsNextPeriod)
			{
				m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);
				m_FpsAccumulator = 0;
				m_FpsNextPeriod += fpsMeasurePeriod;
				m_Text.text = string.Format(display, m_CurrentFps);
			}
		}
		#endregion
	}
}
