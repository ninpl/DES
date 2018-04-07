//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// DragMouseOrbit.cs (00/00/0000)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:																	\\
// Fecha Mod:		00/00/0000													\\
// Ultima Mod:																	\\
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
		public float distance = 5.0f;
		public float xSpeed = 120.0f;
		public float ySpeed = 120.0f;
		public float zSpeed = 5.0f;
		public float yMinLimit = -20f;
		public float yMaxLimit = 80f;
		public float distanceMin = .5f;
		public float distanceMax = 15f;
		public float smoothTime = 2f;
		#endregion

		#region Variables Privadas
		private bool isActive = true;
		private float rotationYAxis = 0.0f;
		private float rotationXAxis = 0.0f;
		private float velocityX = 0.0f;
		private float velocityY = 0.0f;
		#endregion

		#region Inicializacion
		private void Start()
		{
			Vector3 angles = transform.eulerAngles;
			rotationYAxis = angles.y;
			rotationXAxis = angles.x;

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
					velocityX += xSpeed * Input.GetAxis("Mouse X") * distance * 0.02f;
					velocityY += ySpeed * Input.GetAxis("Mouse Y") * 0.02f;
				}
				rotationYAxis += velocityX;
				rotationXAxis -= velocityY;
				rotationXAxis = ClampAngle(rotationXAxis, yMinLimit, yMaxLimit);
				Quaternion toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
				Quaternion rotation = toRotation;

				distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
				Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
				Vector3 position = rotation * negDistance + target.position;

				transform.rotation = rotation;
				transform.position = position;

				velocityX = Mathf.Lerp(velocityX, 0, Time.deltaTime * smoothTime);
				velocityY = Mathf.Lerp(velocityY, 0, Time.deltaTime * smoothTime);
			}
		}
		#endregion

		#region Metodos y Funciones
		public static float ClampAngle(float angle, float min, float max)
		{
			if (angle < -360F)
				angle += 360F;
			if (angle > 360F)
				angle -= 360F;
			return Mathf.Clamp(angle, min, max);
		}

		public void SetActive(bool value)
		{
			isActive = value;
		}
		#endregion
	}
}
