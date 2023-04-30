using Godot;
using System;

public class HealthUI : Control
{
    public float Hearts { get; set; } = 4;
    public float MaxHearts { get; set; } = 4;
    private Label label;

    public override void _Ready()
    {
        label = GetNode<Label>("Label");
        //this.MaxHearts = PlayerSt
    }

    public void SetHeart(float value){
        Hearts = Mathf.Clamp(value, 0, MaxHearts);
    }

    public void SetMaxHearts(float value){
        MaxHearts = Mathf.Max(value, 1);
    }
}
