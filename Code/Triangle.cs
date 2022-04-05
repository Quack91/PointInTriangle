using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Triangle : MonoBehaviour
{
    [SerializeField]
    private DraggableObject[] vertices;

    [SerializeField]
    private LineRenderer[] lineRenderers;

    [SerializeField]
    private DraggableObject targetObject;

    private Image m_targetObjectImage;
    private Image TargetObjectImage => m_targetObjectImage ?? (m_targetObjectImage = targetObject.GetComponent<Image>());

    [SerializeField]
    private LineRenderer w1Line;

    [SerializeField]
    private LineRenderer w2Line;

    [SerializeField]
    private TextMeshProUGUI w1Label;

    [SerializeField]
    private TextMeshProUGUI w2Label;

    // Update is called once per frame
    void Update()
    {
        Vector3 a = vertices[0].transform.position;
        Vector3 b = vertices[1].transform.position;
        Vector3 c = vertices[2].transform.position;
        Vector3 p = targetObject.transform.position;

        lineRenderers[0].SetPositions(new Vector3[] { a, b });
        lineRenderers[1].SetPositions(new Vector3[] { b, c });
        lineRenderers[2].SetPositions(new Vector3[] { c, a });

        Vector3 abDir = b - a;
        Vector3 acDir = c - a;

        bool isPointInTriangle = EstimateIsTargetPointInTriangle(a, b, c, p, out float w1, out float w2);
        TargetObjectImage.color = isPointInTriangle ? Color.green : Color.red;

        w1Label.text = $"W1: {w1.ToString("f2")}";
        w2Label.text = $"W2: {w2.ToString("f2")}";

        w1Line.SetPositions(new Vector3[] { a, a + abDir * w1 });
        w2Line.SetPositions(new Vector3[] { a, a + acDir * w2 });
    }

    private bool EstimateIsTargetPointInTriangle(Vector3 a, Vector3 b, Vector3 c, Vector3 p, out float w1, out float w2)
    {
        w1 = a.x * (c.y - a.y) + (p.y - a.y) * (c.x - a.x) - p.x * (c.y - a.y);
        w1 /= (b.y - a.y) * (c.x - a.x) - (b.x - a.x)*(c.y - a.y);

        w2 = p.y - a.y - w1 * (b.y - a.y);
        w2 /= c.y - a.y;

        return w1 >= 0 && w2 >= 0 && (w1 + w2) <= 1;
    }
}
