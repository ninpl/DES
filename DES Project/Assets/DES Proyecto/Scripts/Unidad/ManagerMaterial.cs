//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// ManagerMaterial.cs (07/04/2018)												\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:		Controlador del cambio de material							\\
// Fecha Mod:		07/04/2018													\\
// Ultima Mod:		Version Inicial												\\
//******************************************************************************\\

#region Librerias
using UnityEngine;
#endregion

namespace MoonAntonio
{
	public class ManagerMaterial : MonoBehaviour 
	{
		#region Variables Publicas
		public MeshRenderer meshRenderer;
		public SkinnedMeshRenderer skinnedMeshRenderer;
		public Material[] materiales;
		#endregion

		#region Variables Privadas
		private int id = 0;
		#endregion

		#region Inicializadores
		private void Start()
		{
			if (meshRenderer == null) meshRenderer = gameObject.GetComponent<MeshRenderer>();
			if (meshRenderer == null && skinnedMeshRenderer == null) skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
		}
		#endregion

		#region Metodos Publicos
		public void Next()
		{
			SetMaterial(id + 1);
		}

		public void Prev()
		{
			SetMaterial(id - 1);
		}

		public void SetMaterial(int id)
		{
			if (materiales == null || meshRenderer == null || materiales.Length == 0) return;

			id = (id < 0) ? materiales.Length - 1 : (id > materiales.Length - 1) ? 0 : id;

			if (meshRenderer != null) meshRenderer.material = materiales[id];
			else skinnedMeshRenderer.material = materiales[id];
		}
		#endregion
	}
}
