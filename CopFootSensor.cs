using UnityEngine;

namespace Policeman
{
	public class CopFootSensor : MonoBehaviour
	{
		public CopFootSensor(System.IntPtr ptr) : base(ptr) { }

		public System.Action OnFootTouched;

		private void OnTriggerEnter(Collider other)
		{
			OnFootTouched?.Invoke();
		}
	}
}

