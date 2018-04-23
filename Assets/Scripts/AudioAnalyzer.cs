using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioAnalyzer : MonoBehaviour
{
    [SerializeField]
    private GameObject cubeObject;

    private AudioSource m_audioSource;
    public float[] m_samples = new float[512];

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < m_samples.Length; ++i)
        {
            GameObject go = Instantiate(cubeObject, transform.position, Quaternion.Euler(new Vector3(0, -0.703125f * i, 0)), transform);
            go.transform.position = Vector3.forward * 100;
            go.GetComponent<AudioCube>().m_audioAnalyzer = this;
            go.GetComponent<AudioCube>().m_index = i;
        }

        transform.position = new Vector3(22.8f, 0, -78.3f);
    }

    private void Update()
    {
        GetSpectrumAudioSource();
    }

    void GetSpectrumAudioSource()
    {
        m_audioSource.GetSpectrumData(m_samples, 0, FFTWindow.Blackman);
    }
}
