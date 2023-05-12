using Godot;
using System;

public class HurtBox : Area2D
{
    private PackedScene HitEffect = ResourceLoader.Load<PackedScene>("res://Effects/HitEffect.tscn");
    private bool _invincible;
    public bool Invincible { get{return _invincible;} set{
        _invincible = value;
        if (_invincible){
            EmitSignal("InvincibilityStartedEventHandler");
        } else{
            EmitSignal("InvincibilityEndedEventHandler");
        }
    } }

    [Signal]
    public delegate void InvincibilityStartedEventHandler();
    [Signal]
    public delegate void InvincibilityEndedEventHandler();
    private Timer timer;
    private CollisionShape2D collisionShape2D;
    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        collisionShape2D = GetNode<CollisionShape2D>("CollisionShape2D");
    } 

    public void StartInvincibility(float duration){
        Invincible = true;
        timer.Start(duration);
    }
    public void CreateHitEffect(){        
        Node2D effect = HitEffect.Instance<Node2D>();
        Node main = GetTree().CurrentScene;
        main.AddChild(effect);
        effect.GlobalPosition = GlobalPosition;        
    }

    public void OnTimerTimeout(){
        Invincible = false;
    }

    public void OnHurtBoxInvincibilityStartedEventHandler(){
        SetDeferred("monitoring", false);
        //collisionShape2D.SetDeferred("disabled", true);
    }

    public void OnHurtBoxInvincibilityEndedEventHandler(){
        //collisionShape2D.Disabled = false;
        Monitoring = true;
    }
}
