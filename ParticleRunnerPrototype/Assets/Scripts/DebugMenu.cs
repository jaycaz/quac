using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SliderChangedEvent : UnityEvent<float> {}

public class DebugMenu : MonoBehaviour {

    [Header("Spawn parameters")]
    public float m_driftForceMin = 0f;
    public float m_driftForceMax = 5f;

    public float m_tuningForceMin = 0f;
    public float m_tuningForceMax = 5f;

    public float m_numParticlesMin = 0f;
    public float m_numParticlesMax = 5f;

    [Space]
    public SliderChangedEvent OnDriftForceChange;
    public SliderChangedEvent OnTuningForceChange;
    public SliderChangedEvent OnNumParticlesChange;
    public UnityEvent OnResetParticles;

    float m_driftForce = 1;
    float m_tuningForce = 1;
    float m_numParticles = 1;

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
                m_driftForce = LabelSlider(m_driftForce, m_driftForceMin, m_driftForceMax, "Drift Force", OnDriftForceChange);
                m_tuningForce = LabelSlider(m_tuningForce, m_tuningForceMin, m_tuningForceMax, "Tuning Force", OnTuningForceChange);
                m_numParticles = LabelSliderInt(m_numParticles, m_numParticlesMin, m_numParticlesMax, "Num Particles", OnNumParticlesChange);

                // "Respawn" button
                if(GUILayout.Button("Reset Particles"))
                {
                    OnResetParticles.Invoke();
                }

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

    float LabelSliderInt(float value, float min, float max, string label, UnityEvent<float> OnValueChanged)
    {
        // Display slider name and value
        GUILayout.Label(string.Format("{0} {1}", label, (int) value));
        float newVal = GUILayout.HorizontalSlider(value, min, max);

        // Call event when value has been changed
        if(GUI.changed)
        {
            if(OnValueChanged != null) OnValueChanged.Invoke((int)newVal);
        }

        return newVal;
    }

}
