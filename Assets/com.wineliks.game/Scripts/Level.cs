using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour
{
    public static Level Instance { get => FindObjectOfType<Level>(); }

    private bool IsSet;

    private static float smoothTime = 0.1f;
    private Vector2 velocity = Vector2.zero;

    private Camera _camera;
    private GameObject elementRef;

    [Space(10)]
    [SerializeField] BallAI ballAi;

    private List<Transform> emptyCells;
    [SerializeField] Transform cells;
    [SerializeField] Transform elementParent;

    [Space(10)]
    [SerializeField] Sprite startSprite;
    [SerializeField] SpriteRenderer[] startPoints;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        Transform startCell = startPoints[Random.Range(0, startPoints.Length)].transform;
        emptyCells = new List<Transform>();
        for(int i = 0; i < cells.childCount; i++)
        {
            Transform cell = cells.GetChild(i);
            if(cell == startCell)
            {
                continue;
            }

            emptyCells.Add(cell);
        }

        startCell.gameObject.AddComponent<StartCell>();
        startCell.GetComponent<SpriteRenderer>().sprite = startSprite;

        ballAi.SetBallPosition(startCell.position);

        transform.position += Vector3.down * 15.0f;
    }

    private void Update()
    {
        transform.position = Vector2.SmoothDamp(transform.position, Vector2.zero, ref velocity, smoothTime);
    }

    private Vector2 GetNearestCellPosition(Vector2 mousePosition)
    {
        foreach(Transform t in emptyCells)
        {
            float dist = Vector2.Distance(t.position, mousePosition);
            if(dist < 0.25f)
            {
                IsSet = true;
                return t.position;
            }
        }

        IsSet = false;
        return mousePosition;
    }

    private bool IsEmptyCell()
    {
        foreach(Transform t in elementParent)
        {
            float dist = Vector2.Distance(t.position, elementRef.transform.position);
            if(dist < 0.5f && t.gameObject != elementRef)
            {
                return false;
            }
        }

        return true;
    }

    public void InstElement(GameObject elementPrefab)
    {
        elementRef = Instantiate(elementPrefab, elementParent);
    }

    public void UpdateElementPosition(Vector2 position)
    {
        if(!elementRef)
        {
            return;
        }

        Vector2 mousePosition = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
        elementRef.transform.localPosition = GetNearestCellPosition(mousePosition);
    }

    public void SetElement()
    {
        if (!elementRef)
        {
            return;
        }

        if(!IsSet || !IsEmptyCell())
        {
            Destroy(elementRef);
        }

        elementRef = null;
    }
}
