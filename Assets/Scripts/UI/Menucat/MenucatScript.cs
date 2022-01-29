using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenucatScript : MonoBehaviour
{
    Canvas canvas;
    RectTransform canvasRect;
    [SerializeField] GameObject testPrefab;
    float lastTeleport;

    Vector3 innerPoint, outerPoint;

    float activeTime = 4;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
        Teleport();
    }

    private void Start()
    {
        return;
    }

    void InstantiateTestCase()
    {
        var newTest = Instantiate(testPrefab, canvas.transform).GetComponent<RectTransform>();
        newTest.position = GetEdgePoint();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector3.Lerp(outerPoint, innerPoint, Mathf.PingPong(Time.time - lastTeleport, activeTime/2));
        if (Time.time - lastTeleport >= activeTime) Teleport();
    }

    void Teleport()
    {
        lastTeleport = Time.time;
        innerPoint = GetEdgePoint();
        outerPoint = ConvertToOuterPoint(innerPoint);
        LookAtCenter();
        activeTime = Random.Range(3, 6);
        //transform.up = innerPoint - transform.position;
    }

    void LookAtCenter()
    {
        Vector3 relative = -transform.position;
        float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    Vector3 GetEdgePoint()
    {
        var newVector = GetRandomPoint();
        if (Mathf.Abs(newVector.x) > Mathf.Abs(newVector.y)) { if (newVector.x < 0) { newVector.x = -Camera.main.orthographicSize * Screen.width / Screen.height; } else { newVector.x = Camera.main.orthographicSize * Screen.width / Screen.height; } } else { if (newVector.y < 0) { newVector.y = -Camera.main.orthographicSize; } else { newVector.y = Camera.main.orthographicSize; } }return newVector;
    }

    Vector3 ConvertToOuterPoint(Vector3 innerPoint)
    {
        return innerPoint + innerPoint.normalized * 2;
    }

    Vector3 GetRandomPoint()
    {
        //return new Vector3(Random.Range(-canvasRect.rect.width / 2, canvasRect.rect.width / 2), Random.Range(-canvasRect.rect.height / 2, canvasRect.rect.height / 2), 0);
        var horzExtent = Random.Range(-Camera.main.orthographicSize * Screen.width / Screen.height, Camera.main.orthographicSize * Screen.width / Screen.height);
        var vertExtent = Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize);
        return new Vector3(horzExtent, vertExtent, 0);
    }
}
