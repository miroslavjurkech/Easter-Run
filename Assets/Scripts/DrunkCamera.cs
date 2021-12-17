/* Source: https://github.com/przemyslawzaworski/Unity3D-CG-programming/blob/master/drunk.cs

Unity shader programming (HLSL & GLSL).

Only for educational purposes.

License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
*/

using UnityEngine;
using System.Collections;
using System;

public class DrunkCamera : MonoBehaviour 
{
	public Material material;

	void OnRenderImage (RenderTexture source, RenderTexture destination) 
	{
		Graphics.Blit (source, destination, material);
	}

    // Our Code
    private TimeSpan remainingTime = TimeSpan.Zero;
    private DateTime lastUpdate;
    private AudioSource song;

    private void Awake() {
        song = GetComponentInChildren<AudioSource>();
        if (song == null)
        {
            Debug.LogError("Drunk Song audio source not given as children");
        }
    }

    private void Update() {
        DateTime now = DateTime.Now;
        var updateTime = now - lastUpdate;
        lastUpdate = now;

        remainingTime -= updateTime;
        if (remainingTime <= TimeSpan.Zero)
        {
            remainingTime = TimeSpan.Zero;
            if (song != null)
                song.Stop();
            enabled = false;
        }
    }

    private void OnEnable() {
        lastUpdate = DateTime.Now;
        if (song != null)
            song.Play();
    }

    public void AddEfectTime(int additionalSeconds)
    {
        remainingTime += new TimeSpan( TimeSpan.TicksPerSecond * additionalSeconds );
    }
}
