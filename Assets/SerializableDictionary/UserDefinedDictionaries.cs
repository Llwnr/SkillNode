using System;
using System.Collections.Generic;

[Serializable]
public class StringFloatDictionary : SerializableDictionary<string, float> {
    public StringFloatDictionary() : base() { }
    public StringFloatDictionary(IDictionary<string, float> dict): base(dict) { }
}

[Serializable]
public class StringBoolDictionary : SerializableDictionary<string, bool> {
    public StringBoolDictionary() : base() { }
    public StringBoolDictionary(IDictionary<string, bool> dict) : base(dict) { }
}