//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// OnShadowEstado.cs (07/04/2018)												\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Controlador del sprite del debug							\\
// Fecha Mod:		07/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEngine.UI;
#endregion

namespace MoonAntonio
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

		#region Inicializadores
		private void Awake()
		{
			if (icono == null) icono = GetComponent<Image>();

			Init(onShadow);
		}
		#endregion

		#region Metodos
		public void SetOnShadow(bool value)
		{
			if (spriteSinSombra == null || spriteEnSombra == null) return;

			icono.sprite = (value) ? spriteEnSombra : spriteSinSombra;

			onShadow = value;
		}

		private void Init(bool value)
		{
			if (spriteSinSombra == null || spriteEnSombra == null) return;

			icono.sprite = (value) ? spriteEnSombra : spriteSinSombra;
		}
		#endregion
	}
}
