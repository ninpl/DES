//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// DeteccionDeSombra.cs (08/04/2018)											\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Sistema de deteccion en la sombra							\\
// Fecha Mod:		08/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace MoonAntonio
{
	public class DeteccionDeSombra : MonoBehaviour 
	{
		#region Variables
		[SerializeField]private bool isAuto = true;
		[SerializeField]private List<Light> lights;
		[SerializeField]public int minimumNbTargetOnShadow = 1;
		public MODODETECCION detectMode = MODODETECCION.ALL;
		[SerializeField]public float raycastinRate = 60f;
		private CustomFixedUpdate customFixedUpdate;
		[SerializeField]private List<ShadowTarget> targets;
		[SerializeField]private LayerMask layers;
		[SerializeField]private UnityEventBool onChangeState;
		[SerializeField]private UnityEvent onEnterShadow;
		[SerializeField]private UnityEvent onExitShadow;
		[SerializeField]private UnityEvent onShadow;
		[SerializeField]private UnityEvent outShadow;
		[HideInInspector]public ShadowEstado IsOnShadowEstado = ShadowEstado.NODEFINIDO;
		[HideInInspector]public ShadowEstado LastIsOnShadowEstado = ShadowEstado.NODEFINIDO;
		#endregion

		#region Propiedades
		public List<Light> Lights
		{
			get
			{
				return lights;
			}

			set
			{
				lights = value;
			}
		}

		public UnityEventBool OnChangeState
		{
			get
			{
				return onChangeState;
			}

			private set
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

			private set
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

			private set
			{
				onExitShadow = value;
			}
		}

		public bool IsAuto
		{
			get
			{
				return isAuto;
			}

			set
			{
				isAuto = value;
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

		#region Inicializadores
		private void Awake()
		{
			customFixedUpdate = new CustomFixedUpdate(1.0f / raycastinRate, _FixedUpdate);
			minimumNbTargetOnShadow = (detectMode == MODODETECCION.UNO) ? 1 : targets.Count;
			if (isAuto)
			{
				Lights = new List<Light>();
				Lights = FindObjectsOfType<Light>().ToList();
			}
		}
		#endregion

		#region Actualizadores
		private void Update()
		{
			customFixedUpdate.Update(Time.deltaTime);
		}

		private void _FixedUpdate()
		{
			if (Lights == null || targets == null || targets.Count == 0)
				return;
			ShadowTarget current_st;
			int nbTargetOnShadow = 0;
			//Browse the list of target
			for (int i = 0; i < targets.Count; ++i)
			{
				current_st = targets[i];
				current_st.IsOnShadow = !IsOnLight(Lights, current_st.Target.position);

				//Call Events
				if (current_st.LastIsOnShadowEstado != current_st.IsOnShadowEstado)
				{
					current_st.OnChangeState.Invoke(current_st.IsOnShadow);
					if (current_st.IsOnShadow)
						current_st.OnEnterShadow.Invoke();
					else
						current_st.OnExitShadow.Invoke();
				}
				if (current_st.IsOnShadow)
					current_st.OnShadow.Invoke();
				else
					current_st.OutShadow.Invoke();

				current_st.Last_IsOnShadow = current_st.IsOnShadow;

				if (current_st.IsOnShadow)
					nbTargetOnShadow++;
			}

			IsOnShadow = (nbTargetOnShadow >= minimumNbTargetOnShadow);
			//Call Events
			if (LastIsOnShadowEstado != IsOnShadowEstado)
			{
				OnChangeState.Invoke(IsOnShadow);
				if (IsOnShadow)
					OnEnterShadow.Invoke();
				else
					OnExitShadow.Invoke();
			}
			if (IsOnShadow)
				OnShadow.Invoke();
			else
				OutShadow.Invoke();

			Last_IsOnShadow = IsOnShadow;
		}
		#endregion

		#region Funcionalidad
		/// <summary>
		/// Detect if your character is on Light
		/// </summary>
		/// <param name="light"></param>
		/// <returns></returns>
		public bool IsOnLight(List<Light> light, Vector3 target_position)
		{
			//Browse the list of lights
			for (int i = 0; i < light.Count; ++i)
			{
				var onshadow = !IsOnLight(light[i], target_position);
				if (!onshadow)
					return true;
			}
			return false;
		}
		/// <summary>
		/// Detect if your character is on Light
		/// </summary>
		/// <param name="light"></param>
		/// <returns></returns>
		public bool IsOnLight(Light light, Vector3 target_position)
		{
			if (!light.isActiveAndEnabled || light.intensity == 0)
				return false;

			//0: Spot - 1: Directional - 2: Point
			switch ((int)light.type)
			{
				case 0:
					return IsOnSpotlLight(light, target_position);
				case 1:
					return IsOnDirectionalLight(light, target_position);
				case 2:
					return IsOnPointLight(light, target_position);
				default:
					return false;
			}
		}

		private bool IsOnDirectionalLight(Light light, Vector3 target_position)
		{
			RaycastHit hit;
			Ray ray = new Ray(target_position, -light.transform.forward);
#if UNITY_EDITOR
			UnityEngine.Debug.DrawRay(ray.origin, ray.direction, Color.red);
#endif
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, ~layers))
			{
				// Do Stuff if you want
				return false;
			}

			return true;
		}

		private bool IsOnSpotlLight(Light light, Vector3 target_position)
		{
			Vector3 FromLightToCharacter = target_position - light.transform.position;

			if (FromLightToCharacter.magnitude > light.range * (light.intensity / 10.0f) || Vector3.Angle(light.transform.forward, FromLightToCharacter) > light.spotAngle / 2.0f)
				return false;

			RaycastHit hit;
			Ray ray = new Ray(light.transform.position, FromLightToCharacter);

#if UNITY_EDITOR
			UnityEngine.Debug.DrawRay(ray.origin, ray.direction, Color.blue);
#endif

			if (Physics.Raycast(ray, out hit, FromLightToCharacter.magnitude, ~layers))
			{
				// Do Stuff if you want
				return false;
			}

			return true;
		}

		private bool IsOnPointLight(Light light, Vector3 target_position)
		{
			Vector3 FromCharacterToLight = light.transform.position - target_position;

			if (FromCharacterToLight.magnitude >= light.range * (light.intensity / 10.0f))
				return false;

			RaycastHit hit;
			Ray ray = new Ray(target_position, FromCharacterToLight);

#if UNITY_EDITOR
			UnityEngine.Debug.DrawRay(ray.origin, ray.direction, Color.blue);
#endif

			if (Physics.Raycast(ray, out hit, FromCharacterToLight.magnitude, ~layers))
			{
				// Do Stuff if you want
				return false;
			}

			return true;
		}
		#endregion

		#region Metodos
		private void OnDrawGizmos()
		{
			if (Lights == null || targets == null || targets.Count == 0)
				return;

			ShadowTarget current_st;
			for (int i = 0; i < targets.Count; ++i)
			{
				current_st = targets[i];
				if (current_st.IsOnShadow)
					Gizmos.color = Color.red;
				else
					Gizmos.color = Color.blue;
				Gizmos.DrawWireSphere(current_st.Target.position, 0.1f);
			}
		}
		#endregion
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
