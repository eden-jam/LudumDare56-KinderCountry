using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
	[ContextMenu("UpdateMat")]
	public void UpdateMat()
	{
		foreach (var material in Resources.FindObjectsOfTypeAll<Material>())
		{
			if (material.shader.name.StartsWith("Universal Render Pipeline"))
			{
				material.shader = Shader.Find("Standard");
			}
		}
	}
}