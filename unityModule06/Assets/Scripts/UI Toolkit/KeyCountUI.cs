using UnityEngine;
using UnityEngine.UIElements;

public class KeyCountUI : MonoBehaviour
{
    public PlayerController player;
    Label keyCountLabel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        var uidocument = GetComponent<UIDocument>();
        keyCountLabel = uidocument.rootVisualElement.Q<Label>("KeyCount");
    }

    void Update()
    {
        keyCountLabel.text = player.keyCount.ToString();
    }
}
