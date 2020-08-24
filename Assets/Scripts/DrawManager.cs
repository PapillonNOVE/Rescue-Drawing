using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private float drawLimit;
    [SerializeField] private float drawLenght;

    [SerializeField] private List<Vector2> points;

    float offSetZ;
    public bool canDraw;

    

	private void Start()
	{
        offSetZ = Mathf.Abs(transform.position.z - mainCam.transform.position.z);
        EventManager.Instance.SetDrawLimitBarMaxValue(drawLimit);
        EventManager.Instance.UpdateDrawLimitBar(drawLimit);
	}

	private void OnEnable()
	{
        GeneralManager.isGameStarted = true;
    }

	private void Update()
    {
        if (GeneralManager.isGameStarted)
        {
            if (drawLenght >= drawLimit)
            {
                canDraw = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                StartDraw();
            }

            if (Input.GetMouseButton(0) && canDraw)
            {
                Draw();
            }

            if (Input.GetMouseButtonUp(0))
            {
                EndDraw();
            }
        }
    }

	private void StartDraw() 
    {
        //canDraw = true;
    }

    private void Draw()
    {
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

        EventManager.Instance.UpdateDrawLimitBar(drawLimit - drawLenght);

        lineRenderer.enabled = true;
    }

    private void EndDraw() 
    {
        canDraw = false;

        edgeCollider2D.points = points.ToArray();
        edgeCollider2D.enabled = true;
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        EventManager.Instance.SetFreeBalls();
        Human.isCountdownStarted = true;
    }

    private Vector3 GetMousePosition() 
    {
        Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = offSetZ;

		mousePosition = mainCam.ScreenToWorldPoint(mousePosition);

        return mousePosition;
    }
}
