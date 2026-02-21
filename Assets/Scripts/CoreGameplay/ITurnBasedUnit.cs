using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public interface ITurnBasedUnit
{
    UniTask BattleStart();
    UniTask TurnStart();
    UniTask TurnEnd();
}