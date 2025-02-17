using UnityEngine;

public class RopeRenderer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private LineRenderer lineRenderer;
    [SerializeField]
    private Transform startPosition;
    
    private float lineWidth = 0.05f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.enabled = false;
    }
    void Start()
    {
        
    }

    public void RenderLine(Vector3 endPosition, bool enableRenderer)
    {
        if (enableRenderer)
        {
            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
            }
            lineRenderer.positionCount = 2;
        }
        else
        {
            lineRenderer.positionCount = 0;
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
        }

        if (lineRenderer.enabled)
        {
            Vector3 temp = startPosition.position;
            temp.z = 0f;
            
            startPosition.position = temp;
            temp = endPosition;
            temp.z = 0f;
            
            endPosition = temp;
            
            lineRenderer.SetPosition(0, startPosition.position);
            lineRenderer.SetPosition(1, endPosition);
            
        }
    }
}
