[System.Serializable]
public class SkillData {
    public string Name;
    public float Power;
    public string Element;
    public float Cost;

    public SkillData Clone() {
        return new SkillData{
            Name = this.Name,
            Power = this.Power,
            Element = this.Element,
            Cost = this.Cost
        };
    }

    public override string ToString() {
        return $"{Name}, Power: {Power}, Cost: {Cost}, Element: [{Element}]";
    }
}