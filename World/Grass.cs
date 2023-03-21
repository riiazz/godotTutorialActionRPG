using Godot;
using System;

public class Grass : Node2D
{
    private PackedScene GrassEffect = ResourceLoader.Load<PackedScene>("res://Effects/GrassEffect.tscn");
    private void CreateGrassEffect(){
        Node2D grassEffect = GrassEffect.Instance<Node2D>();
        //Node world = GetTree().CurrentScene;
        //world.AddChild(grassEffect);
        GetParent().AddChild(grassEffect);
        grassEffect.GlobalPosition = GlobalPosition;
    }

    public void OnHurtBoxAreaEntered(Area2D area){
        CreateGrassEffect();
        QueueFree();
    }
}
