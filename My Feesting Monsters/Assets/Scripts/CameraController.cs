using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f; // �������� ���������������
    public float zoomSpeed = 5f; // �������� ����
    public float minZoom = 2f; // ����������� ���
    public float maxZoom = 10f; // ������������ ���

    public Vector2 baseMinBounds; // ������� ����������� ������� (������ ����� ����)
    public Vector2 baseMaxBounds; // ������� ������������ ������� (������� ������ ����)

    private Vector3 dragOrigin;

    void Update()
    {
        HandleMouseDrag();
        HandleScrollZoom();
    }

    void HandleMouseDrag()
    {
        // ������ ��������������
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        // ��������������
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(-difference.x * panSpeed, -difference.y * panSpeed, 0);
            transform.Translate(move, Space.World);

            // ����������� ������� ������ � �������� �������� ������
            Vector3 pos = transform.position;
            float zoomFactor = Camera.main.orthographicSize / minZoom;
            Vector2 zoomAdjustedMinBounds = baseMinBounds / zoomFactor;
            Vector2 zoomAdjustedMaxBounds = baseMaxBounds / zoomFactor;
            pos.x = Mathf.Clamp(pos.x, zoomAdjustedMinBounds.x, zoomAdjustedMaxBounds.x);
            pos.y = Mathf.Clamp(pos.y, zoomAdjustedMinBounds.y, zoomAdjustedMaxBounds.y);
            transform.position = pos;

            dragOrigin = Input.mousePosition;
        }
    }

    void HandleScrollZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float size = Camera.main.orthographicSize - scroll * zoomSpeed;
        Camera.main.orthographicSize = Mathf.Clamp(size, minZoom, maxZoom);
    }
}





