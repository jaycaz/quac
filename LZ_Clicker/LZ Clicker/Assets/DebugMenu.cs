using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SliderChangedEvent : UnityEvent<float> {}

public class DebugMenu : MonoBehaviour {

    [Header("Spawn parameters")]
    public float m_spawnRateMin = 0f;
    public float m_spawnRateMax = 5f;

    public float m_spawnSpeedMin = 0f;
    public float m_spawnSpeedMax = 5f;

    public float m_spawnSizeMin = 0f;
    public float m_spawnSizeMax = 5f;

    [Space]
    public SliderChangedEvent OnSpawnRateChange;
    public SliderChangedEvent OnSpawnSpeedChange;
    public SliderChangedEvent OnSpawnSizeChange;

    float m_spawnRateValue = 1;
    float m_spawnSpeedValue = 1;
    float m_spawnSizeValue = 1;

    private bool m_expanded = true;

    void OnGUI()
    {
        using (var verticalScope = new GUILayout.VerticalScope("box"))
        {
            // "Expand" button
            if(!m_expanded)
            {
                if(GUILayout.Button("Show Menu"))
                {
                    m_expanded = true;
                }
            }

            // Expanded content
            if(m_expanded)
            {
                m_spawnRateValue = LabelSlider(m_spawnRateValue, m_spawnRateMin, m_spawnRateMax, "Spawn Rate", OnSpawnRateChange);
                m_spawnSpeedValue = LabelSlider(m_spawnSpeedValue, m_spawnSpeedMin, m_spawnSpeedMax, "Spawn Speed", OnSpawnSpeedChange);
                m_spawnSizeValue = LabelSlider(m_spawnSizeValue, m_spawnSizeMin, m_spawnSizeMax, "Spawn Size", OnSpawnSizeChange);

                // // "Clear Score" button
                // if(GUILayout.Button("Clear Score"))
                // {
                //     // TODO: Clear score
                // }

                // "Collapse" button
                if(GUILayout.Button("Hide Menu"))
                {
                    m_expanded = false;
                }
            }
        }
    }

    float LabelSlider(float value, float min, float max, string label, UnityEvent<float> OnValueChanged)
    {
        // Display slider name and value
        GUILayout.Label(string.Format("{0} {1:0.00}", label, value));
        float newVal = GUILayout.HorizontalSlider(value, min, max);

        // Call event when value has been changed
        if(GUI.changed)
        {
            if(OnValueChanged != null) OnValueChanged.Invoke(newVal);
        }

        return newVal;
    }

}
