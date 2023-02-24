using Godot;
using System;

public class Grass : Node2D
{
    
    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("attack")){
            QueueFree();
        }
    }
}
