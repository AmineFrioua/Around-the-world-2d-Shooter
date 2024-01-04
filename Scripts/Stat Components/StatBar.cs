using Godot;

public partial class StatBar : ProgressBar
{
	 public enum StatType { HEALTH, STAMINA }
	
	[Export]
	public StatType statType = StatType.HEALTH;
	public int CurrentValue { get; set; } = 100;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
			  // Initialize the progress bar
		this.MaxValue = MaxValue;
		this.Value = CurrentValue;
			  switch(statType)
		{
			case StatType.HEALTH:
			//add texture of health
			break;
			case StatType.STAMINA:
			// add texture of a stamania
			break;                                                         	
		}
	}
	
	 public void UpdateValue(int amount)
	{
		CurrentValue += amount;
		this.Value= CurrentValue ;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
