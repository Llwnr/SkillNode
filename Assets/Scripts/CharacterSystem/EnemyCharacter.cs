using System;
using System.Threading.Tasks;

public class EnemyCharacter : Character {
    public int waitDelay;
    private async void Update() {
        if (TurnManager.Instance.CurrActiveCharacter == this) {
            await Task.Delay(waitDelay);
            TurnManager.Instance.EndTurn(this);
        }
    }
}