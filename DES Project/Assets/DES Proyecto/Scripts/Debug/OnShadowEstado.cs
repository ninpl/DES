//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// OnShadowEstado.cs (07/04/2018)												\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Controlador de FPS											\\
// Fecha Mod:		07/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\


#region Librerias
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
#endregion

namespace MoonAntonio.Debug
{
	[RequireComponent(typeof(Image))]
	public class OnShadowEstado : MonoBehaviour 
	{
		#region Variables Publicas
		public Image icono;
		public Sprite spriteSinSombra;
		public Sprite spriteEnSombra;
		#endregion

		#region Variables Privadas
		[SerializeField] private bool onShadow = false;
		#endregion

	}
}
