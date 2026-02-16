using System;

public interface ITurnBasedUnit
{
    void BattleStart();
    void TurnStart(Action<UnitController> onTurnComplete);
    void TurnEnd();
}