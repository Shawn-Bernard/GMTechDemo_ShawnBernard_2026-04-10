using UnityEngine;

[CreateAssetMenu(menuName = "Health/Health Data")]
public class HealthSystemData : ScriptableObject
{
    [Range(0, 100)]
    public int maxHealth;
    [Range(0f, 1f)]
    [Tooltip("How long it will stay on the color before swapping, I personally like .1f")]
    public float flashTime;

    [Tooltip("This is for unity events on death, it'll wait and then disable object self")]
    public float waitForDeathTime;

    [Header("Material Settings")]
    public Material flashMaterial;
    [Tooltip("This color will go first than wait, goes middle color and then this color again")]
    public Color flashColorStartAndEnd;
    [Tooltip("This color will only show once, in the middle before going to the last color ")]
    public Color flashColorMiddle;
}
