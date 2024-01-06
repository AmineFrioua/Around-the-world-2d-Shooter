using System.Collections;
using System.Collections.Generic;
using Godot;
using Valossy.Loggers;

namespace AroundTheWorldShooter.Scripts.Stat_Components;

[Tool]
[GlobalClass]
public partial class StatList : Node, IReadOnlyDictionary<string, IStat>
{
    private readonly Dictionary<string, IStat> stats;

    public StatList()
    {
        stats = new Dictionary<string, IStat>();
    }

    public void OnChildEnteredTree(Node node)
    {
        if (node is IStat stat)
        {
            stats.Add(stat.StatName, stat);
        }
        else
        {
            Logger.Error($"{nameof(IStat)} does not have proper interface");
        }
    }

    public void OnChildExitingTree(Node node)
    {
        if (node is IStat stat)
        {
            stats.Remove(stat.StatName);
        }
        else
        {
            Logger.Error($"{nameof(IStat)} does not have proper interface");
        }
    }

    public IEnumerator<KeyValuePair<string, IStat>> GetEnumerator()
    {
        return stats.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count
    {
        get => stats.Count;
    }

    public bool ContainsKey(string key)
    {
        return stats.ContainsKey(key);
    }

    public bool TryGetValue(string key, out IStat value)
    {
        return stats.TryGetValue(key, out value);
    }

    public IStat this[string key] => stats[key];

    public IEnumerable<string> Keys
    {
        get => stats.Keys;
    }

    public IEnumerable<IStat> Values
    {
        get => stats.Values;
    }
}