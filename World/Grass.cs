using Godot;
using System;

public class Grass : Node2D
{

    private void CreateGrassEffect(){
        PackedScene GrassEffect = GD.Load<PackedScene>("res://Effects/GrassEffect.tscn");
        Node2D grassEffect = GrassEffect.Instance<Node2D>();
        Node world = GetTree().CurrentScene;
        world.AddChild(grassEffect);
        grassEffect.GlobalPosition = GlobalPosition;
    }

    public void OnHurtBoxAreaEntered(Area2D area){
        CreateGrassEffect();
        QueueFree();
    }
}
