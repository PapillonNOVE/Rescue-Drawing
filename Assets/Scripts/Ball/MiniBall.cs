using System.Collections;
using UnityEngine;

public class MiniBall : MonoBehaviour
{
	[Header("Particle System")]
    [SerializeField] private GameObject particlePrefab;
	
	[Header("Component")]
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private CircleCollider2D circleCollider2D;
	[SerializeField] private Rigidbody2D rigidbody2D;
	[SerializeField] private AudioSource audioSource;

	private void OnEnable()
	{
		Subscribe();
	}

	private void OnDisable()
	{
		GeneralManager.QuittingControl(Unsubscribe);
	}

	#region Event Subscribe

	private void Subscribe()
	{
		EventManager.Instance.DestroyBalls += PrepareSelfDestruct;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.DestroyBalls -= PrepareSelfDestruct;
	}

	#endregion

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

		Instantiate(particlePrefab, transform.position, Quaternion.identity);

		yield return new WaitWhile(() => audioSource.isPlaying);

		Destroy(gameObject);
	}
}
