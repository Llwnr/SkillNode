using System;
using System.Collections.Generic;

public class SubscriptionManager {
    private readonly List<Action> _cleanupActions = new List<Action>();
    
    public void Bind<T>(Action<T> handler, Action<Action<T>> subscribe, Action<Action<T>> unsubscribe) {
        subscribe(handler);
        //Store unsubscription methods for when you need to dispose
        _cleanupActions.Add(() => unsubscribe(handler));
    }

    public void Dispose() {
        foreach (var cleanupAction in _cleanupActions) {
            cleanupAction.Invoke();
        }
        _cleanupActions.Clear();
    }
}