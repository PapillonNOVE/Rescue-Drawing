using Constants;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[Header("GameObject")]
	[SerializeField] private GameObject miniBallLauncher;

	[Header("Component")]
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private CircleCollider2D circleCollider2D;
	[SerializeField] private Rigidbody2D rigidbody2D;

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
			SetActiveMiniBallLauncher();
		}
	}

	private void SetActiveMiniBallLauncher() 
	{
		miniBallLauncher.SetActive(true);

		miniBallLauncher.transform.SetParent(transform.parent);

		SelfDestruct();
	}

	private void SelfDestruct()
	{
		GeneralManager.QuittingControl(Unsubscribe);

		Destroy(gameObject);
	}
}
