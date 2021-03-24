using UnityEngine;

namespace Policeman
{
	public class CopSpeechBehaviour : MonoBehaviour
	{
		public CopSpeechBehaviour(System.IntPtr ptr) : base(ptr) { }

		public AudioSource speechSource;
		public AudioClip[] speechClips;
		public int maxQueueCount = 5;

		private bool canSpeak;

		private float delayTime = 0f;

		private void Start()
		{
			speechSource.dopplerLevel = 0.5f;
		}

		private void Update()
		{
			if (!speechSource.isPlaying)
			{
				delayTime += Time.deltaTime;
				if (delayTime > 2.5f)
				{
					delayTime = 0f;
					float randomVal = Random.Range(0, 65535);

					if (randomVal % 2f == 0f)
					{
						speechSource.clip = speechClips[Random.Range(0, speechClips.Length)];
						speechSource.Play();
					}
				}
			}
		}

		/*private void QueueClip(AudioClip[] array, AudioClip clip)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == null)
				{
					array[i] = clip;
					break;
				}

				// If we queue a clip, and it reaches outside the bounds of the array
				// Simply replace the last element in the array with the clip we want
				if (i > array.Length)
				{
					array[array.Length] = clip;
				}
				else if (i < 0)
				{
					if (array[i] == null)
						array[i] = clip;
				}
			}
		}

		private void QueueClip(int index, AudioClip[] array, AudioClip clip)
		{
			if (array[index] == null)
			{
				array[index] = clip;
			}

			// If we queue a clip, and it reaches outside the bounds of the array
			// Simply replace the last element in the array with the clip we want
			if (index > array.Length)
			{
				array[array.Length] = clip;
			}
			else if (index < 0)
			{
				if (array[index] == null)
					array[index] = clip;
			}
			else return;
		}

		private void RemoveClip(int index, AudioClip[] array)
		{
			if (array[index] != null)
				array[index] = null;
		}

		private void RemoveClip(AudioClip clip, AudioClip[] array)
		{
			for(int i = 0; i < array.Length; i++)
			{
				if(array[i] == clip && array[i] != null)
				{
					array[i] = null;
					break;
				}
			}
		}*/
	}

}