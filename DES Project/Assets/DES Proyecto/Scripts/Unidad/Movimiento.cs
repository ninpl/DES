//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// Movimiento.cs (07/04/2018)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Controlador de movimiento del personaje						\\
// Fecha Mod:		07/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio
{
	public class Movimiento : MonoBehaviour 
	{
		#region Variables Publicas
		public float vel = 1.0f;
		public Animator animator;
		#endregion

		#region Constantes
		private const string key_isRun = "IsRun";
		#endregion

		#region Actualizador
		private void Update()
		{
			if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.DownArrow)) || (Input.GetKey(KeyCode.LeftArrow)) || (Input.GetKey(KeyCode.RightArrow)) || (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.S)) || (Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.W)))
			{
				this.animator.SetBool(key_isRun, true);
			}
			else
			{
				this.animator.SetBool(key_isRun, false);
			}

			transform.Translate(vel * Input.GetAxis("Horizontal") * Time.deltaTime, 0F, vel * Input.GetAxis("Vertical") * Time.deltaTime);
		}
		#endregion
	}
}
