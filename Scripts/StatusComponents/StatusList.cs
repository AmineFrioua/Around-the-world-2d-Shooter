using System.Collections;
using System.Collections.Generic;
using AroundTheWorldShooter.Scripts.StatusComponents.Interfaces;
using Godot;
using Valossy.Loggers;

namespace AroundTheWorldShooter.Scripts.StatusComponents;

[Tool]
[GlobalClass]
public partial class StatusList : Node, IReadOnlyDictionary<string, IStatus>
{
    public IStatus this[string key] => statuses[key];

    public IEnumerable<string> Keys
    {
        get => statuses.Keys;
    }

    public IEnumerable<IStatus> Values
    {
        get => statuses.Values;
    }

    public IEnumerator<KeyValuePair<string, IStatus>> GetEnumerator()
    {
        return statuses.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count
    {
        get => statuses.Count;
    }

    private readonly Dictionary<string, IStatus> statuses;

    public StatusList()
    {
        statuses = new Dictionary<string, IStatus>();
    }

    public override void _Ready()
    {
    }

    public void OnChildEnteredTree(Node node)
    {
        AddStat(node);
    }

    public void OnChildExitingTree(Node node)
    {
        if (node is IStatus stat)
        {
            statuses.Remove(stat.StatName);
        }
        else
        {
            Logger.Error($"{nameof(IStatus)} does not have proper interface");
        }
    }

    public bool ContainsKey(string key)
    {
        return statuses.ContainsKey(key);
    }

    public bool TryGetValue(string key, out IStatus value)
    {
        return statuses.TryGetValue(key, out value);
    }

    private void AddStat(Node node)
    {
        if (node is IStatus stat)
        {
            if (ContainsKey(stat.StatName) == true) return;

            statuses.Add(stat.StatName, stat);
        }
        else
        {
            Logger.Error($"{node.Name}: {nameof(IStatus)} does not have proper interface");
        }
    }
}