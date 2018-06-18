using System.Collections;
using UnityEngine;

/// <summary>
/// This static class defines some useful extension methods for several existing classes (e.g. Vector3, float and others).
/// </summary>
public static class Extensions
{
    /// <summary>compares the squared magnitude of target - second to given float value</summary>
    public static bool AlmostEquals(this Vector3 target, Vector3 second, float sqrMagnitudePrecision)
    {
        return (target - second).sqrMagnitude < sqrMagnitudePrecision;  // TODO: inline vector methods to optimize?
    }

    /// <summary>compares the squared magnitude of target - second to given float value</summary>
    public static bool AlmostEquals(this Vector2 target, Vector2 second, float sqrMagnitudePrecision)
    {
        return (target - second).sqrMagnitude < sqrMagnitudePrecision;  // TODO: inline vector methods to optimize?
    }

    /// <summary>compares the angle between target and second to given float value</summary>
    public static bool AlmostEquals(this Quaternion target, Quaternion second, float maxAngle)
    {
        return Quaternion.Angle(target, second) < maxAngle;
    }

    /// <summary>compares two floats and returns true of their difference is less than floatDiff</summary>
    public static bool AlmostEquals(this float target, float second, float floatDiff)
    {
        return Mathf.Abs(target - second) < floatDiff;
    }
    /// <summary>
    /// TODO Doesn't work. Don't know why.
    /// </summary>
    public static float ClampAngle (this Mathf target, float angle, float min, float max) {
        float thisAngle = angle;
        if (thisAngle < -360) {
            thisAngle += 360;
        }
        if (thisAngle > 360) {
            thisAngle -= 360;
        }
        return Mathf.Clamp(thisAngle, min, max);
    }
    public static Color HSVToRGB (this Color target, float H, float S, float V) {
        Color white = Color.white;
        if (S == 0f)
        {
            white.r = V;
            white.g = V;
            white.b = V;
        }
        else
        {
            if (V == 0f)
            {
                white.r = 0f;
                white.g = 0f;
                white.b = 0f;
            }
            else
            {
                white.r = 0f;
                white.g = 0f;
                white.b = 0f;
                float num = H * 6f;
                int num2 = (int)Mathf.Floor(num);
                float num3 = num - (float)num2;
                float num4 = V * (1f - S);
                float num5 = V * (1f - S * num3);
                float num6 = V * (1f - S * (1f - num3));
                int num7 = num2;
                switch (num7 + 1) {
                    case 0:
                    white.r = V;
                    white.g = num4;
                    white.b = num5;
                    break;
                    case 1:
                    white.r = V;
                    white.g = num6;
                    white.b = num4;
                    break;
                    case 2:
                    white.r = num5;
                    white.g = V;
                    white.b = num4;
                    break;
                    case 3:
                    white.r = num4;
                    white.g = V;
                    white.b = num6;
                    break;
                    case 4:
                    white.r = num4;
                    white.g = num5;
                    white.b = V;
                    break;
                    case 5:
                    white.r = num6;
                    white.g = num4;
                    white.b = V;
                    break;
                    case 6:
                    white.r = V;
                    white.g = num4;
                    white.b = num5;
                    break;
                    case 7:
                    white.r = V;
                    white.g = num6;
                    white.b = num4;
                    break;
                }
            }
        }
        return white;
    }
    /// <summary>
    /// Checks if a particular integer value is in an int-array.
    /// </summary>
    /// <remarks>This might be useful to look up if a particular actorNumber is in the list of players of a room.</remarks>
    /// <param name="target">The array of ints to check.</param>
    /// <param name="nr">The number to lookup in target.</param>
    /// <returns>True if nr was found in target.</returns>
    public static bool Contains(this int[] target, int nr)
    {
        if (target == null)
        {
            return false;
        }
        for (int index = 0; index < target.Length; index++)
        {
            if (target[index] == nr)
            {
                return true;
            }
        }

        return false;
    }
}
