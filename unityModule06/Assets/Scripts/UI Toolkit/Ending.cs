using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public enum ImageType {
    GameOverImage,
    GameClearImage,
}

[RequireComponent(typeof(UIDocument))]
public class Ending : MonoBehaviour
{
    public Sprite gameOver;
    public Sprite gameClear;
    public bool canRestart;
    VisualElement image;
    VisualElement overlay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        var uidocument = GetComponent<UIDocument>();
        image = uidocument.rootVisualElement.Q<VisualElement>("Image");
        Color newColor = Color.white;
        newColor.a = 0;
        image.style.unityBackgroundImageTintColor = newColor;
        overlay = uidocument.rootVisualElement.Q<VisualElement>("Overlay");
        overlay.RegisterCallback<ClickEvent>(OnClick);
        canRestart = false;
    }

    void OnDisable()
    {
        overlay.UnregisterCallback<ClickEvent>(OnClick);
    }

    public void SetImage(ImageType type)
    {
        if (type == ImageType.GameOverImage)
        {
            image.style.backgroundImage = new StyleBackground(gameOver);
        }
        else if (type == ImageType.GameClearImage)
        {
            image.style.backgroundImage = new StyleBackground(gameClear);
        }
    }

    public async Awaitable FadeIn()
    {
        await Fade(0, 1, 1);
    }

    public async Awaitable Fadeout()
    {
        await Fade(1, 0, 1);
    }

    async Awaitable Fade(float from, float to, float duration)
    {
        var startFade = Time.time;
        var endFade = startFade + duration;

        Color newColor = Color.white;
        while (Time.time < endFade)
        {
            newColor.a = Mathf.Lerp(from, to, (Time.time - startFade) / duration);
            image.style.unityBackgroundImageTintColor = newColor;
            await Awaitable.NextFrameAsync();
        }
        newColor.a = to;
        image.style.unityBackgroundImageTintColor = newColor;
    }

    void OnClick(ClickEvent evt)
    {
        if (canRestart)
            _ = Restart();
    }

    async Awaitable Restart()
    {
        await Fadeout();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
