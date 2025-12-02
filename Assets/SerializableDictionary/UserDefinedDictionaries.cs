using System;
using System.Collections.Generic;

[Serializable]
public class StringFloatDictionary : SerializableDictionary<string, float>{}

[Serializable]
public class StringListStringDictionary : SerializableDictionary<string, List<string>> {}