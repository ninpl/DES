//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// DeteccionDeSombra.cs (00/00/0000)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:																	\\
// Fecha Mod:		00/00/0000													\\
// Ultima Mod:																	\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace MoonAntonio
{
	public class DeteccionDeSombra : MonoBehaviour 
	{

	}

	#region Enums
	public enum ShadowEstado { NODEFINIDO, DENTRO, FUERA }
	[System.Serializable]
	public enum MODODETECCION
	{
		UNO = 0,
		ALL = 1
	}
	#endregion

	#region Clases Eventos
	[System.Serializable] public class UnityEventBool : UnityEvent<bool> { }
	#endregion

	#region Clases
	[System.Serializable]
	public class ShadowTarget
	{
		#region Constructor
		public ShadowTarget()
		{
			nombre = "";
		}
		#endregion

		#region Variables
		[SerializeField]public string nombre;
		[SerializeField]public Transform Target;
		[SerializeField]private UnityEventBool onChangeState;
		[SerializeField]private UnityEvent onEnterShadow;
		[SerializeField]private UnityEvent onExitShadow;
		[SerializeField]private UnityEvent onShadow;
		[SerializeField]private UnityEvent outShadow;
		[HideInInspector]public ShadowEstado IsOnShadowEstado;
		[HideInInspector]public ShadowEstado LastIsOnShadowEstado;
		#endregion

		#region Propiedades
		public UnityEventBool OnChangeState
		{
			get
			{
				return onChangeState;
			}

			set
			{
				onChangeState = value;
			}
		}

		public UnityEvent OnEnterShadow
		{
			get
			{
				return onEnterShadow;
			}

			set
			{
				onEnterShadow = value;
			}
		}

		public UnityEvent OnExitShadow
		{
			get
			{
				return onExitShadow;
			}

			set
			{
				onExitShadow = value;
			}
		}

		public UnityEvent OnShadow
		{
			get
			{
				return onShadow;
			}

			set
			{
				onShadow = value;
			}
		}

		public UnityEvent OutShadow
		{
			get
			{
				return outShadow;
			}

			set
			{
				outShadow = value;
			}
		}

		public bool IsOnShadow
		{
			get
			{
				return (IsOnShadowEstado == ShadowEstado.DENTRO);
			}

			set
			{
				IsOnShadowEstado = (value) ? ShadowEstado.DENTRO : ShadowEstado.FUERA;
			}
		}

		public bool Last_IsOnShadow
		{
			get
			{
				return (LastIsOnShadowEstado == ShadowEstado.DENTRO);
			}

			set
			{
				LastIsOnShadowEstado = (value) ? ShadowEstado.DENTRO : ShadowEstado.FUERA;
			}
		}
		#endregion

	}
	#endregion
}
