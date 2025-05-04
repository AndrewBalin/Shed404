using System;
using System.Collections.Generic;


[Serializable]
public class DialogOption
{
    public string text;
    public string next;
}


[Serializable]
public class DialogNode
{
    public string speaker;
    public string text;
    public List<DialogOption> options;
    public string next;
    public string @event; // Под вопросом, пока пробую перенести в next, как `event:`
}


[Serializable]
public class DialogNodeWithId
{
    public string id;
    public string speaker;
    public string text;
    public List<DialogOption> options;
    public string next;
}


[Serializable]
public class DialogWrapper
{
    public List<DialogNodeWithId> nodes;

    public Dictionary<string, DialogNodeWithId> ToDictionary()
    {
        var dict = new Dictionary<string, DialogNodeWithId>();
        foreach (var entry in nodes)
            dict[entry.id] = entry;
        return dict;
    }
}