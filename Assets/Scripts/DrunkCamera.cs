/* Source: https://github.com/przemyslawzaworski/Unity3D-CG-programming/blob/master/drunk.cs

Unity shader programming (HLSL & GLSL).

Only for educational purposes.

License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
*/

using UnityEngine;
using System;
using UnityEngine.Rendering;

public class DrunkCamera : MonoBehaviour 
{
	public Material material;
	public Material hardMaterial;

	void OnRenderImage (RenderTexture source, RenderTexture destination) 
	{
		Graphics.Blit (source, destination, GetRightMaterial());
	}

    // Our Code
    private TimeSpan remainingTime = TimeSpan.Zero;
    private DateTime lastUpdate;
    private AudioSource song;
    [SerializeField]
    private AudioClip drunkSong;

    [SerializeField] private int fadeOutTime = 5;

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
        if (remainingTime > TimeSpan.Zero)
        {
            double volume = (double)remainingTime.Ticks / (fadeOutTime * TimeSpan.TicksPerSecond);
            if (song != null)
                song.volume = (volume > 1.0) ? 1.0f : (float)volume;
        }
        else
        {
            remainingTime = TimeSpan.Zero;
            if (song != null)
            {
                song.Stop();
                song.volume = 1.0f;
                song.Play();
            }
            enabled = false;
        }
    }

    private void OnEnable() {
        lastUpdate = DateTime.Now;
        if (song != null)
        {
            song.Stop();
            song.PlayOneShot(drunkSong);
        }
    }

    public void AddEfectTime(int additionalSeconds)
    {
        remainingTime += new TimeSpan( TimeSpan.TicksPerSecond * additionalSeconds );
    }

    private Material GetRightMaterial()
    {
        bool hardMode = PlayerPrefs.GetInt("hardDrunkMode") == 1;
        bool flipped = false;

        switch (SystemInfo.graphicsDeviceType)
        {
            case GraphicsDeviceType.Vulkan:
            case GraphicsDeviceType.Direct3D11:
            case GraphicsDeviceType.Direct3D12:
                flipped = true;
                break;
        }

        if (hardMode)
        {
            return flipped ? material : hardMaterial;
        }
        else
        {
            return flipped ? hardMaterial : material;
        }
    }
}
