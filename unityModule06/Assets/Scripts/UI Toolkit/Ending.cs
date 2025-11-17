using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class Ending : MonoBehaviour
{
    public Sprite gameOver;
    public Sprite gameClear;
    VisualElement image;
    VisualElement overlay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        var uidocument = GetComponent<UIDocument>();
        image = uidocument.rootVisualElement.Q<VisualElement>("Image");
        overlay = uidocument.rootVisualElement.Q<VisualElement>("Overlay");
        overlay.RegisterCallback<ClickEvent>(OnClick);
    }

    void OnDisable()
    {
        overlay.UnregisterCallback<ClickEvent>(OnClick);
    }

    void OnClick(ClickEvent evt)
    {
        Debug.Log("oihwepijsd");
    }
}
