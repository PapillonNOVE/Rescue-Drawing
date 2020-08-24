using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[Header("Particle")]
	[SerializeField] private GameObject explosionParticlePrefab;

	[Header("Component")]
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private CircleCollider2D circleCollider2D;
	[SerializeField] private Rigidbody2D rigidbody2D;
	[SerializeField] private AudioSource audioSource;

	[Header("Value")]
	[Range(0f,1f)]
	[SerializeField] private float explosionRadius;
	[SerializeField] private float explosionForce;

	[SerializeField] private LayerMask layerMask;

	[Header("List")]
	[SerializeField] private List<GameObject> miniBalls;

	private void OnEnable()
	{
		rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
		Subscribe();
	}

	private void OnDisable()
	{
		GeneralManager.QuittingControl(Unsubscribe);
	}

	#region Event Subscribe

	private void Subscribe()
	{
		EventManager.Instance.SetFreeBalls += SetFreeBalls;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.SetFreeBalls -= SetFreeBalls;
	}

	#endregion

	private void SetFreeBalls() 
	{
		rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		int layerNumber = collision.gameObject.layer;
	
		if (layerNumber == LayerContants.OBSTACLE_LINE || layerNumber == LayerContants.HUMAN)
		{
			SpawnMiniBalls();
		}
	}

	private void SpawnMiniBalls() 
	{
		PrepareSelfDestruct();

		Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);

		foreach (GameObject miniBall in miniBalls)
		{
			miniBall.transform.parent = null;

			Vector2 direction = miniBall.transform.position - transform.position;

			miniBall.SetActive(true);

			miniBall.GetComponent<Rigidbody2D>().AddForce(direction * Vector3.up * explosionForce);
		}
	}

	private void PrepareSelfDestruct() 
	{
		meshRenderer.enabled = false;
		Destroy(rigidbody2D);
		circleCollider2D.enabled = false;

		StartCoroutine(SelfDestruct());
	}

	private void PlaySFX() 
	{
		audioSource.Play();
	}

	private IEnumerator SelfDestruct() 
	{
		PlaySFX();

		yield return new WaitWhile(() => audioSource.isPlaying);

		Destroy(gameObject);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
