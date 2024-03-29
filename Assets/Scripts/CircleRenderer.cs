using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRenderer : MonoBehaviour{
    public float radius = 2f;
    public int segments = 24; // Чем больше сегментов, тем гладче окружность
    public float lineWidth = 0.2f;

    private LineRenderer lineRenderer;

    float deltaTheta;

    private void Start() {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.positionCount = segments + 1;
        deltaTheta = (2f * Mathf.PI) / segments;
    }

    private void Update() {
        DrawCircle();
    }

    void DrawCircle() {
        float theta = 0f;

        for (int i = 0; i < lineRenderer.positionCount; i++) {
            Vector3 pos = new Vector3(transform.position.x + radius * Mathf.Cos(theta), transform.position.y + radius * Mathf.Sin(theta), 0);
            lineRenderer.SetPosition(i, pos);
            theta += deltaTheta;
        }
    }
}
