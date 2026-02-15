[System.Serializable]
public class StatusEffectData {
    public float Duration;
    public int StackCount;
    public float Magnitude;//Generic power value of the status effect

    public StatusEffectData(int stackCount, float magnitude) {
        StackCount = stackCount;
        Magnitude = magnitude;
    }
}