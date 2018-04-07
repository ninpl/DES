//                                  ┌∩┐(◣_◢)┌∩┐
//																				\\
// ManagerMaterial.cs (00/00/0000)													\\
// Autor: Antonio Mateo (.\Moon Antonio) 	antoniomt.moon@gmail.com			\\
// Descripcion:																	\\
// Fecha Mod:		00/00/0000													\\
// Ultima Mod:																	\\
//******************************************************************************\\

#region Librerias
using System.Collections;
using System.Collections.Generic;
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
	}
}
