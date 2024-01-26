using UnityEngine;
using System.Collections;
using GameJam.Utilities.Attributes;

namespace GameJam.Utilities.Components
{
	public class AudioEmmiter : MonoBehaviour
	{
		[SerializeField, DisableInPlayMode] private bool emitOnParticleLoop;
		[SerializeField, DisableInPlayMode] private bool emitOnParticleCollision;
		[SerializeField, ConditionalField(ConditionType.NOR, nameof(emitOnParticleLoop), nameof(emitOnParticleCollision))] private bool randomBetweenTwoConstants;
		[SerializeField, ConditionalField(ConditionType.NOR, nameof(emitOnParticleLoop), nameof(emitOnParticleCollision))] private float timeConstant;
		[SerializeField, ConditionalField(ConditionType.NOR, nameof(emitOnParticleLoop), nameof(emitOnParticleCollision)), ShowField(nameof(randomBetweenTwoConstants))] private float secondTimeConstant;
		[Space]
		[SerializeField] private AudioClip audioClip;
		[SerializeField] private AudioSource audioSource;
		[SerializeField, ShowField(nameof(emitOnParticleLoop))] private ParticleSystem particles;

		private float particleDurationTime;

		void Awake()
		{
			if (!emitOnParticleLoop && !emitOnParticleCollision)
			{
				StartCoroutine(AudioRoutine());
			}
			else if (emitOnParticleLoop)
			{
				particleDurationTime = particles.main.duration;
			}
		}

		void Update()
		{
			if (emitOnParticleLoop)
			{
				if (particleDurationTime > 0f) particleDurationTime -= Time.deltaTime;

				if (particleDurationTime <= 0f)
				{
					audioSource.PlayOneShot(audioClip);
					particleDurationTime = particles.main.duration;
				}
			}
		}

		void OnParticleTrigger() => audioSource.PlayOneShot(audioClip);

		private IEnumerator AudioRoutine()
		{
			while (true)
			{
				audioSource.PlayOneShot(audioClip);

				if (randomBetweenTwoConstants)
				{
					yield return new WaitForSeconds(Random.Range(timeConstant, secondTimeConstant));
				}
				else
				{
					yield return new WaitForSeconds(timeConstant);
				}
			}
		}
	}
}
