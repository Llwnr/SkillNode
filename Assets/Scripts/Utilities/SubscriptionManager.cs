using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

// Utility class to manage event cleanup.
// Problem Solved: It is difficult to unsubscribe anonymous functions (lambdas) from C# events 
// because you cannot reference them later. This class stores the unsubscribe action immediately when you bind.
public class SubscriptionManager
{
    private readonly List<Action> _cleanupActions = new List<Action>();

    public void Bind<T>(Func<T, UniTask> handler, Action<Func<T, UniTask>> subscribe, Action<Func<T, UniTask>> unsubscribe)
    {
        subscribe(handler); // Actually hook the event.
        // Store the specific unsubscribe call as a closure to be executed later.
        _cleanupActions.Add(() => unsubscribe(handler));
    }
    public void Bind(Func<UniTask> handler, Action<Func<UniTask>> subscribe, Action<Func<UniTask>> unsubscribe)
    {
        subscribe(handler);
        _cleanupActions.Add(() => unsubscribe(handler));
    }

    public void Dispose()
    {
        foreach (var cleanupAction in _cleanupActions)
        {
            cleanupAction.Invoke();
        }

        _cleanupActions.Clear();
    }
}