using System.Collections.Generic;

public static class Utils
{
	public static T Random<T>(this T[] array) => array.Length != 0 ? array[UnityEngine.Random.Range(0, array.Length)] : default;
	public static T Random<T>(this List<T> list) => list.Count != 0 ? list[UnityEngine.Random.Range(0, list.Count)] : default;
}