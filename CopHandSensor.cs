using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Policeman
{
	public class CopHandSensor : MonoBehaviour
	{
		public CopHandSensor(System.IntPtr ptr) : base(ptr) { }

		public System.Action<Collider> OnHitAction;

		private void OnTriggerEnter(Collider other)
		{
			OnHitAction.Invoke(other);
		}
	}
}

