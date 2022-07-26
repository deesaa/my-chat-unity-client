using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create ColorBase", fileName = "ColorBase", order = 0)]
public class ColorBase : ScriptableObject, IColorBase
{
    [SerializeField] private List<ColorElement> colors;
    public Color GetColor(string color)
    {
        if (colors.Exists(element => element.Name == color))
            return colors.Find(element => element.Name == color).Color;
        Debug.LogWarning($"Color {color} does not exist in ColorBase");
        return Color.magenta;
    }
}