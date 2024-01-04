using System.Collections.Specialized;
using Godot;
using Valossy.Loggers;

namespace AroundTheWorldShooter.Scripts.Stat_Components;

[Tool]
[GlobalClass]
public partial class StatList : Node
{
    public ListDictionary Stats { get; }

    public StatList()
    {
        Stats = new ListDictionary();
    }

    public override void _Ready()
    {
        // foreach (Node child in GetChildren())
        // {
        //     AddStat(child);
        // }
    }

    public void OnChildEnteredTree(Node node)
    {
        AddStat(node);
    }

    public void OnChildExitingTree(Node node)
    {
        if (node is IStat stat)
        {
            Stats.Remove(stat.StatName);
        }
        else
        {
            Logger.Error($"{nameof(IStat)} does not have proper interface");
        }
    }

    private void AddStat(Node node)
    {
        if (node is IStat stat)
        {
            Stats.Add(stat.StatName, stat);
        }
        else
        {
            Logger.Error($"{nameof(IStat)} does not have proper interface");
        }
    }
}