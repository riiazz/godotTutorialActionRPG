using Godot;
using System;

public class WanderController : Node2D
{
    [Export]
    private int wanderRange = 32;
    private Vector2 startPosition = Vector2.Zero;
    private Vector2 targetPosition = Vector2.Zero;
    public Vector2 TargetPosition { get{return targetPosition;} }
    private Timer timer;
    public override void _Ready()
    {
        startPosition = GlobalPosition;
        UpdateTargetPosition();
        timer = GetNode<Timer>("Timer");
    }

    public void UpdateTargetPosition(){
        Vector2 targetVector = new Vector2((int)GD.RandRange(-wanderRange, wanderRange), (int) GD.RandRange(-wanderRange, wanderRange));
        targetPosition = startPosition + targetVector;
    }

    public float GetTimeLeft(){
        return timer.TimeLeft;
    }

    public void StartWanderTimer(float duration){
        timer.Start(duration);
    }
    public void OnTimerTimeout(){
        UpdateTargetPosition();
    }
}
