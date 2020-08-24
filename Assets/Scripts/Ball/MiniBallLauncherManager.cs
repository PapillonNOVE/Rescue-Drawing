using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBallLauncherManager : MonoBehaviour
{
	[Header("Component")]
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private ParticleSystem lightParticle;
	[SerializeField] private ParticleSystem sparkParticle;

	[Header("Value")]
	[Range(0f, 1f)]
	[SerializeField] private float explosionRadius;
	[SerializeField] private float explosionForce;

	[Space(10)]
	[SerializeField] private LayerMask layerMask;

	[Space(10)]
	[SerializeField] private List<Rigidbody2D> miniBallRigidbodies;

	void Start()
    {
		SpawnMiniBalls();
	}

	private void SpawnMiniBalls()
	{
		StartCoroutine(SelfDestruct());

		foreach (Rigidbody2D miniBallRigidbody in miniBallRigidbodies)
		{
			miniBallRigidbody.transform.parent = null;

			Vector2 direction = miniBallRigidbody.transform.position - transform.position;

			miniBallRigidbody.gameObject.SetActive(true);

			miniBallRigidbody.AddForce(direction * Vector3.up * explosionForce);
		}
	}

	private void PlaySFX()
	{
		audioSource.Play();
	}

	private void PlayParticleSystem()
	{
		if (lightParticle.isPlaying)
		{
			lightParticle.Stop();
		}

		lightParticle.Play();


		if (sparkParticle.isPlaying)
		{
			sparkParticle.Stop();
		}

		sparkParticle.Play();
	}

	private IEnumerator SelfDestruct()
	{
		PlaySFX();
		PlayParticleSystem();

		yield return new WaitWhile(() => audioSource.isPlaying && lightParticle.isPlaying && sparkParticle.isPlaying);

		Destroy(gameObject);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
