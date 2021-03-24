using UnityEngine;

namespace Policeman
{
	public class CopPunchBehaviour : MonoBehaviour
	{
		public CopPunchBehaviour(System.IntPtr ptr) : base(ptr) { }

		public float punchDamage = 0.25f;
		public CopHandSensor[] sensors;

		private void Start()
		{
			for (int i = 0; i < 2; i++)
				sensors[i].OnHitAction = DealDamage;
		}

		private void DealDamage(Collider other)
		{
			if (other.GetComponentInParent<Player_Health>())
			{
				other.GetComponentInParent<Player_Health>().TAKEDAMAGE(punchDamage);
			}
		}
	}

}
