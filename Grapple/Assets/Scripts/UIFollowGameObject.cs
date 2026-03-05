using UnityEngine;
using UnityEngine.UI;

public class UIFollowGameObject : MonoBehaviour
{
    public GameObject targetObject; 
    private RectTransform rectTransform;
    private Camera mainCamera;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (targetObject != null && mainCamera != null)
        {
            
            Vector3 screenPoint = mainCamera.WorldToScreenPoint(targetObject.transform.position);
            
            if (screenPoint.z > 0 && targetObject.gameObject.activeInHierarchy )
            {
                GetComponent<Graphic>().enabled = true; 
                rectTransform.position = screenPoint;
            }
            else
            {
                
                GetComponent<Graphic>().enabled = false; 
            }
        }
    }
}