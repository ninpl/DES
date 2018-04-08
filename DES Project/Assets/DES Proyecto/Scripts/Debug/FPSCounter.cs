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

namespace MoonAntonio
{
	[RequireComponent(typeof(Text))]
	public class FPSCounter : MonoBehaviour 
	{
		#region Variables Privadas
		const float medidaFPS = 0.5f;
		private int acumuladorFPS = 0;
		private float siguPeriodoFPS = 0;
		private int actualFPS;
		const string formato = "{0} FPS";
		private Text text;
		#endregion

		#region Inicializadores
		private void Start()
		{
			siguPeriodoFPS = Time.realtimeSinceStartup + medidaFPS;
			text = GetComponent<Text>();
		}
		#endregion

		#region Actualizadores
		private void Update()
		{
			acumuladorFPS++;
			if (Time.realtimeSinceStartup > siguPeriodoFPS)
			{
				actualFPS = (int)(acumuladorFPS / medidaFPS);
				acumuladorFPS = 0;
				siguPeriodoFPS += medidaFPS;
				text.text = string.Format(formato, actualFPS);
			}
		}
		#endregion
	}
}
