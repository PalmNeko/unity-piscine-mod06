using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RestartEffects : MonoBehaviour
{
    PostProcessVolume m_Volume;
    LensDistortion m_LensDistortion;
    float startTime;
    float velocity;

    void Start()
    {
        GameManager ins = GameManager.instance;
        if (ins.continueCount == 0 || ins.preLifeIsGameOvered == false)   
        {
            Destroy(this);
            return;
        }
        // Create an instance of a vignette
        m_LensDistortion = ScriptableObject.CreateInstance<LensDistortion>();
        m_LensDistortion.enabled.Override(true);
        m_LensDistortion.intensity.Override(1f);

        // Use the QuickVolume method to create a volume with a priority of 100, and assign the vignette to this volume
        m_Volume = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, m_LensDistortion);
        startTime = Time.realtimeSinceStartup;
        m_LensDistortion.intensity.value = -100;
        velocity = 0.0f;
    }

    void Update()
    {
        if (m_Volume == null)
            return ;
        // Change vignette intensity using a sinus curve
        if (m_LensDistortion.intensity.value >= 13f)
        {
            Destroy(this);
        }
        m_LensDistortion.intensity.value = Mathf.SmoothDamp(m_LensDistortion.intensity.value, 13, ref velocity, 1f);
    }

    void OnDestroy()
    {
        if (m_Volume != null)
        {
            RuntimeUtilities.DestroyVolume(m_Volume, true, true);
            m_Volume = null;
        }
    }
}