[System.Serializable]
public class StatusEffectData {
    public float Duration;
    public int StackCount;
    public float Magnitude;//Generic power value of the status effect

    public StatusEffectData(float duration, int stackCount, float magnitude) {
        Duration = duration;
        StackCount = stackCount;
        Magnitude = magnitude;
    }
}