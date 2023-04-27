using Godot;
using System;

public class HurtBox : Area2D
{
    private PackedScene HitEffect = ResourceLoader.Load<PackedScene>("res://Effects/HitEffect.tscn");
    [Export]
    private bool showHit = true;
    private void OnHurtBoxAreaEntered(Area2D area){
        if(showHit){
            Node2D effect = HitEffect.Instance<Node2D>();
            Node main = GetTree().CurrentScene;
            main.AddChild(effect);
            effect.GlobalPosition = GlobalPosition;
        }
    }
}
