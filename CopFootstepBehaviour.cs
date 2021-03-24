using UnityEngine;

namespace Policeman
{
	public class CopFootstepBehaviour : MonoBehaviour
	{
		public CopFootstepBehaviour(System.IntPtr ptr) : base(ptr) { }

		public Transform[] feet;

		public AudioClip[] sounds;

		public AudioSource[] sources;

		private CopFootSensor[] sensors;

		private void Start()
		{
			if (feet != null)
			{
				sources = new AudioSource[2];
				sensors = new CopFootSensor[2];
				for (int i = 0; i < 2; i++)
				{
					if (feet[i].GetComponent<AudioSource>())
					{
						sources[i] = feet[i].GetComponent<AudioSource>();
						sources[i].spatialBlend = 0.5f;
					}

					if (feet[i].GetComponent<CopFootSensor>())
					{
						sensors[i] = feet[i].GetComponent<CopFootSensor>();
					}

					sensors[i].OnFootTouched = FootTouched;
				}
			}
		}

		private void FootTouched()
		{
			for (int i = 0; i < 2; i++)
			{
				sources[i].clip = sounds[Random.Range(0, sounds.Length)];
				sources[i].Play();
			}
		}
	}

}