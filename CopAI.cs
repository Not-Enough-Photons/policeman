using BoneworksModdingToolkit;
using UnityEngine;

namespace Policeman
{
	public class CopAI : MonoBehaviour
	{
		public CopAI(System.IntPtr ptr) : base(ptr) { }

		public Transform eyePos;
		public Transform eyeTarget;

		public LayerMask playerMask;

		public Animator animator;

		public float walkSpeed = 1f;

		private Rigidbody _rb;

		private float deltaX;
		private float deltaZ;
		private float lastDeltaX;
		private float lastDeltaZ;

		private void Start()
		{
			_rb = transform.Find("AI/Collider").GetComponent<Rigidbody>();
			eyeTarget = Player.FindPlayer().transform;
		}

		private void FixedUpdate()
		{
			UpdateLoop();
		}

		private void LateUpdate()
		{
			lastDeltaX = deltaX;
			lastDeltaZ = deltaZ;
		}

		private void UpdateLoop()
		{
			Vector3 lookAt = Quaternion.LookRotation(eyeTarget.position - _rb.transform.position).eulerAngles;
			_rb.transform.eulerAngles = Vector3.up * lookAt.y;

			if (Vector3.Distance(_rb.transform.position, eyeTarget.position) > 1f)
			{
				bool above = (eyeTarget.position.y - _rb.transform.position.y) > 2.5f;

				if (!above)
				{
					_rb.AddForce(_rb.transform.forward * walkSpeed, ForceMode.VelocityChange);
					_rb.velocity = Vector3.ClampMagnitude(_rb.velocity, 1f);
				}
				else
				{
					_rb.velocity = Vector3.zero;
				}


				if (_rb.velocity.magnitude > 0.05f && !above)
				{
					animator.Play("Running", 0);
				}
				else
				{
					animator.Play("Idle", 0);
				}
			}
			else
			{
				animator.Play("Punching");
			}
		}
	}

}