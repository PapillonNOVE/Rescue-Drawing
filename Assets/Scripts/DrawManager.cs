using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrawManager : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private Camera mainCam;

    [Header("Component")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private EdgeCollider2D edgeCollider2D;
    [SerializeField] private Rigidbody2D rigidbody2D;

    [Header("Value")]
    [Range(0f,1f)]
    [SerializeField] private float pointDistance = 1f;
    [SerializeField] private float minDrawLimit;
    [SerializeField] private float maxDrawLimit;
    [SerializeField] private float drawLenght;

    [SerializeField] private List<Vector2> points;

    float offSetZ;
    public bool canDraw;
    Vector3 drawStartPos;

	private void Start()
	{
        offSetZ = Mathf.Abs(transform.position.z - mainCam.transform.position.z);
        EventManager.Instance.SetDrawLimitBarMaxValue(maxDrawLimit);
        EventManager.Instance.UpdateDrawLimitBar(maxDrawLimit);
	}

	private void OnEnable()
	{
        GeneralManager.GameState = GameStates.GameStarted;
    }

    private void Update()
    {
		if (GeneralManager.GameState != GameStates.GameStarted)
		{
            return;
		}

        if (Input.GetMouseButtonDown(0))
        {
            StartDraw();
        }

        if (Input.GetMouseButton(0))
        {
            Draw();
        }

        if (Input.GetMouseButtonUp(0))
        {
            EndDraw();
        }
    }

	private void StartDraw() 
    {
        drawStartPos = GetMousePosition();
    }

    private void Draw()
    {
        if (drawLenght > maxDrawLimit)
        {
            return;
        }

        Vector3 currentPosition = GetMousePosition();

        if (points.Count > 0)
        {
            Vector3 lastPosition = lineRenderer.GetPosition(points.Count - 1);

            float distance = Vector3.Distance(lastPosition, currentPosition);

            if (distance < pointDistance)
            {
                return;
            }
        }

        points.Add(currentPosition);
        lineRenderer.positionCount = points.Count;

        lineRenderer.SetPosition(points.Count - 1, currentPosition);

		for (int i = 0; i < points.Count; i++)
		{
            drawLenght = i * pointDistance;
		}

        lineRenderer.enabled = true;

        EventManager.Instance.UpdateDrawLimitBar(maxDrawLimit - drawLenght);
    }

    private void EndDraw() 
    {
        if (drawLenght < minDrawLimit)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        canDraw = false;

        edgeCollider2D.points = points.ToArray();
        edgeCollider2D.enabled = true;
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        EventManager.Instance.SetFreeBalls?.Invoke();
        GeneralManager.GameState = GameStates.DeathTimer;
    }

    private float DrawDistanceCalculator()
    {
        Debug.Log(Vector3.Distance(drawStartPos, GetMousePosition()));
        return Vector3.Distance(drawStartPos, GetMousePosition());
    }

    private Vector3 GetMousePosition() 
    {
        Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = offSetZ;

		mousePosition = mainCam.ScreenToWorldPoint(mousePosition);

        return mousePosition;
    }
}
