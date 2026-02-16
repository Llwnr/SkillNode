using System;
using System.Collections.Generic;

public class TraitInstance
{
    private Trait _trait;
    public Type TraitType => _trait.GetType();

    private UnitInstance _owner;
    public UnitInstance Owner => _owner;
    private TraitData _traitData;
    public TraitData TraitData => _traitData;

    public readonly SubscriptionManager Subscriptions = new SubscriptionManager();

    public TraitInstance(Trait trait, UnitInstance owner, TraitData traitData)
    {
        _trait = trait;
        _owner = owner;
        _traitData = new TraitData
        {
            StackCount = traitData.StackCount,
        };
    }

    public void AddStacks(int amount)
    {
        _traitData.StackCount += amount;
    }

    public void Apply()
    {
        _trait?.ApplyTo(this);
    }

    public void Cleanup()
    {
        Subscriptions.Dispose();
    }
}