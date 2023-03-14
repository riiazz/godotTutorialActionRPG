using Godot;
using System;

public class Bat : KinematicBody2D
{
    private Vector2 knockback = Vector2.Zero;

    public override void _PhysicsProcess(float delta){
        knockback = knockback.MoveToward(Vector2.Zero, 200 * delta);
        knockback = MoveAndSlide(knockback);
    }
    public void OnHurtBoxAreaEntered(Area2D area){
        knockback = (area as SwordHitbox).knockbackVector * 120;
    }
}
