using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MacroMethod
{
    public static Color DeleteAlpha(this Color color)
	{
		return new Color(color.r, color.g, color.b);
	}

	public static Color AddAlpha(this Color color, float alpha)
	{
		return new Color(color.r, color.g, color.b, alpha);
	}

	public static Vector3 YZero(this Vector3 position)
	{
		return new Vector3(position.x, 0, position.z);
	}

	public static bool IsObjectVisible(this Camera camera, Renderer renderer)
	{
		return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), renderer.bounds);
	}

	public static bool IsEnemy(this int target1, int target2)
	{
		return target1 % 2 != target2 % 2;
	}

	public static float CalculateDamage(this float attack, float defence)
	{
		return 100f * attack / (100f + defence);
	}
}
