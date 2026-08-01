using UnityEngine;


[CreateAssetMenu(menuName = "Audio/Sound Library")]
public class SoundLibrarySO : ScriptableObject
{
    public SoundSO[] smallScore;
    public SoundSO[] bigScore;
    public SoundSO[] takeItem;
    public SoundSO[] heal;
    public SoundSO[] craft;
    public SoundSO[] hurt;
}
