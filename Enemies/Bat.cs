using Godot;
using System;

public class Bat : KinematicBody2D
{
    private Vector2 knockback = Vector2.Zero;
    private Stats stats;

    public override void _Ready(){
        stats = GetNode<Stats>("Stats");
    }

    public override void _PhysicsProcess(float delta){
        knockback = knockback.MoveToward(Vector2.Zero, 200 * delta);
        knockback = MoveAndSlide(knockback);
        
    }
    public void OnHurtBoxAreaEntered(Area2D area){
        stats.Health -= (area as SwordHitbox).Damage;
        knockback = (area as SwordHitbox).knockbackVector * 120;
    }

    public void OnStatsNoHealthEventHandler(){
        QueueFree();
    }
}
