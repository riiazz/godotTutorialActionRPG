using Godot;
using System;

public class Grass : Node2D
{
    
    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("attack")){
            PackedScene GrassEffect = GD.Load<PackedScene>("res://Effects/GrassEffect.tscn");
            Node2D grassEffect = GrassEffect.Instance<Node2D>();
            Node world = GetTree().CurrentScene;
            world.AddChild(grassEffect);
            grassEffect.GlobalPosition = GlobalPosition;
            QueueFree();
        }
    }
}
