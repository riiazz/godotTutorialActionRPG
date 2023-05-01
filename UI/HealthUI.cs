using Godot;
using System;

public class HealthUI : Control
{
    public float Hearts { get; set; }
    public float MaxHearts { get; set; }
    private Stats playerStats;
    private TextureRect heartUIFull;
    private TextureRect heartUIEmpty;
    public override void _Ready()
    {
        playerStats = GetNode<Stats>("/root/PlayerStats");
        heartUIEmpty = GetNode<TextureRect>("HeartUIEmpty");
        heartUIFull = GetNode<TextureRect>("HeartUIFull");
        this.MaxHearts = (float) playerStats.MaxHealth;
        this.Hearts = (float) playerStats.Health;
        playerStats.Connect("HealthChangeEventHandler", this, "SetHeart");
        playerStats.Connect("MaxHealthChangedEventHandler", this, "SetMaxHearts");
        SetHeart(Hearts);
        SetMaxHearts(MaxHearts);
    }

    public void SetHeart(float value){
        Hearts = Mathf.Clamp(value, 0, MaxHearts);
        if (heartUIFull != null){
            heartUIFull.RectSize = new Vector2(Hearts * 15, 11);
        }
    }

    public void SetMaxHearts(float value){
        MaxHearts = Mathf.Max(value, 1);
        Hearts = Mathf.Min((int) Hearts, (int) MaxHearts);
        if (heartUIEmpty != null){
            heartUIEmpty.RectSize = new Vector2(MaxHearts * 15, 11);
        }
    }
}
