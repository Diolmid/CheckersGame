using UnityEngine;

public class MouseOverBoard : MonoBehaviour
{
    public Vector2 mouseOverPosition;
    public LayerMask boarMask;
    
    private Camera _camera;
    private Vector2 _mousePositionMultiplier = new (4f, 4f);

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 25f, boarMask))
        {
            mouseOverPosition.x = (int)(hit.point.x + _mousePositionMultiplier.x);
            mouseOverPosition.y = (int)(hit.point.z + _mousePositionMultiplier.y);
        }
        else
        {
            mouseOverPosition.x = -1;
            mouseOverPosition.y = -1;
        }
    }
}
