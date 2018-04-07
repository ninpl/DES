//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// DragMouseOrbit.cs (07/04/2018)												\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Controlador de la camara del personaje						\\
// Fecha Mod:		07/04/2018													\\
// Ultima Mod:		Version Inicial												\\
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
		public float distancia = 5.0f;
		public float xVel = 120.0f;
		public float yVel = 120.0f;
		public float yMinLimit = -20f;
		public float yMaxLimit = 80f;
		public float distanciaMin = .5f;
		public float distanciaMax = 15f;
		public float smoothTime = 2f;
		#endregion

		#region Variables Privadas
		private bool isActive = true;
		private float rotacionYAxis = 0.0f;
		private float rotacionXAxis = 0.0f;
		private float velX = 0.0f;
		private float velY = 0.0f;
		#endregion

		#region Inicializacion
		private void Start()
		{
			Vector3 angulos = transform.eulerAngles;
			rotacionYAxis = angulos.y;
			rotacionXAxis = angulos.x;

			if (GetComponent<Rigidbody>()) GetComponent<Rigidbody>().freezeRotation = true;
		}
		#endregion

		#region Actualizaciones
		private void LateUpdate()
		{
			if (target)
			{
				if (Input.GetMouseButton(0) && isActive)
				{
					velX += xVel * Input.GetAxis("Mouse X") * distancia * 0.02f;
					velY += yVel * Input.GetAxis("Mouse Y") * 0.02f;
				}
				rotacionYAxis += velX;
				rotacionXAxis -= velY;
				rotacionXAxis = ClampAngle(rotacionXAxis, yMinLimit, yMaxLimit);
				Quaternion toRotacion = Quaternion.Euler(rotacionXAxis, rotacionYAxis, 0);
				Quaternion rotacion = toRotacion;

				distancia = Mathf.Clamp(distancia - Input.GetAxis("Mouse ScrollWheel") * 5, distanciaMin, distanciaMax);
				Vector3 negDistancia = new Vector3(0.0f, 0.0f, -distancia);
				Vector3 posicion = rotacion * negDistancia + target.position;

				transform.rotation = rotacion;
				transform.position = posicion;

				velX = Mathf.Lerp(velX, 0, Time.deltaTime * smoothTime);
				velY = Mathf.Lerp(velY, 0, Time.deltaTime * smoothTime);
			}
		}
		#endregion

		#region Metodos y Funciones
		public static float ClampAngle(float angulo, float min, float max)
		{
			if (angulo < -360F) angulo += 360F;
			if (angulo > 360F) angulo -= 360F;

			return Mathf.Clamp(angulo, min, max);
		}

		public void SetActive(bool value)
		{
			isActive = value;
		}
		#endregion
	}
}
