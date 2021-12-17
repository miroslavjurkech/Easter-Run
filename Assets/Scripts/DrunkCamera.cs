/* Source: https://github.com/przemyslawzaworski/Unity3D-CG-programming/blob/master/drunk.cs

Unity shader programming (HLSL & GLSL).

Only for educational purposes.

License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
*/

using UnityEngine;
using System.Collections;

public class DrunkCamera : MonoBehaviour 
{
	public Material material;

	void OnRenderImage (RenderTexture source, RenderTexture destination) 
	{
		Graphics.Blit (source, destination, material);
	}
}
