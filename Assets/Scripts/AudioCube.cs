using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCube : Cube
{
    public AudioAnalyzer m_audioAnalyzer;
    public int m_index;
    protected override float GenerateNoise()
    {
        return m_audioAnalyzer.m_samples[m_index] * 100;
    }
}
