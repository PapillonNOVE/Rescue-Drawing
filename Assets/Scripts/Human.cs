using Constants;
using UnityEngine;

public class Human : MonoBehaviour
{
	[Header("Component")]
	[SerializeField] private Animator animator;
	[SerializeField] private BoxCollider2D boxCollider2D;
	[SerializeField] private Rigidbody2D rigidbody2D;
	
	[SerializeField] private float deathTimer;

	[SerializeField] private bool _isHumanAlive;
	public bool IsHumanAlive { get => _isHumanAlive; set => _isHumanAlive = value; }

	private void Update()
	{
		if (GeneralManager.GameState == GameStates.DeathTimer)
		{
			deathTimer += Time.deltaTime;

			if (deathTimer >= GeneralManager.deathTimerLimit && IsHumanAlive)
			{
				CompleteLevel();
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		int layerNumber = collision.gameObject.layer;

		if ((layerNumber == LayerContants.BALL || layerNumber == LayerContants.MINI_BALL) && IsHumanAlive && GeneralManager.GameState == GameStates.DeathTimer)
		{
			IsHumanAlive = false;

			Die();
			Debug.Log("Çarpan obje " + collision.gameObject.name);
		}
	}

	private void CompleteLevel()
	{
		deathTimer = 0;

		EventManager.Instance.SaveLevelID();
		GeneralManager.GameState = GameStates.LevelCompleted;
	}

	private void Die() 
	{
		boxCollider2D.enabled = false;
		rigidbody2D.bodyType = RigidbodyType2D.Kinematic;

		animator.SetBool("isDead", true);
		EventManager.Instance.GameOver();

		GeneralManager.GameState = GameStates.GameOver;
	}
}
