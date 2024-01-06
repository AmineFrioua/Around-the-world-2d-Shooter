using System.Collections;
using System.Collections.Generic;
using Godot;
using Valossy.Loggers;

namespace AroundTheWorldShooter.Scripts.Stat_Components;

[Tool]
[GlobalClass]
public partial class StatList : Node, IReadOnlyDictionary<string, IStat>
{
	public IStat this[string key] => stats[key];

	public IEnumerable<string> Keys
	{
		get => stats.Keys;
	}

	public IEnumerable<IStat> Values
	{
		get => stats.Values;
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
	
	private readonly Dictionary<string, IStat> stats;

	public StatList()
	{
		stats = new Dictionary<string, IStat>();
	}

	public override void _Ready()
	{
		foreach (Node node in GetChildren())
		{
			AddStat(node);
		}
	}

	public void OnChildEnteredTree(Node node)
	{
		AddStat(node);
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

	public bool ContainsKey(string key)
	{
		return stats.ContainsKey(key);
	}

	public bool TryGetValue(string key, out IStat value)
	{
		return stats.TryGetValue(key, out value);
	}

	private void AddStat(Node node)
	{
		if (node is IStat stat)
		{
			if(ContainsKey(stat.StatName) == true) return;
			
			stats.Add(stat.StatName, stat);
		}
		else
		{
			Logger.Error($"{nameof(IStat)} does not have proper interface");
		}
	}
}
