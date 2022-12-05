using Data.Player;
using UnityEngine;

public static class DataExtentions
{
    public static Vector3Data AsVectorData(this Vector3 vector) => new Vector3Data(vector.x, vector.y, vector.z);
    public static Vector3 AsUnityVector(this Vector3Data vector3Data) => 
        new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);
    public static Vector3 AddHeight(this Vector3 currentVector, float height) =>
        new Vector3(currentVector.x, currentVector.y + height, currentVector.z);

    public static T ToDeserialized<T>(this string json) => JsonUtility.FromJson<T>(json);

    public static string ToJson(this object obj) => JsonUtility.ToJson(obj);
}
