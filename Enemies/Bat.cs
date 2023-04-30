using Godot;
using System;

public enum EnemyAction{
    IDLE,
    WANDER,
    CHASE
}

public class Bat : KinematicBody2D
{
    private PackedScene EnemyDeathEffect = ResourceLoader.Load<PackedScene>("res://Effects/EnemyDeathEffect.tscn");
    private Vector2 knockback = Vector2.Zero;
    private Stats stats;
    private PlayerDetectionZone playerDetectionZone;
    [Export]
    private int ACCELERATION = 300;
    [Export]
    private int MAX_SPEED = 50;
    [Export]
    private int FRICTION = 200;
    private Vector2 velocity = Vector2.Zero;
    private EnemyAction state = EnemyAction.CHASE;
    private AnimatedSprite sprite;
    private HurtBox hurtBox;

    public override void _Ready(){
        stats = GetNode<Stats>("Stats");
        playerDetectionZone = GetNode<PlayerDetectionZone>("PlayerDetectionZone");
        sprite = GetNode<AnimatedSprite>("AnimatedSprite");
        hurtBox = GetNode<HurtBox>("HurtBox");
    }

    public override void _PhysicsProcess(float delta){
        knockback = knockback.MoveToward(Vector2.Zero, FRICTION * delta);
        knockback = MoveAndSlide(knockback);

        switch(state){
            case EnemyAction.IDLE:
                velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
                SeekPlayer();
                break;
            case EnemyAction.WANDER:
                break;
            case EnemyAction.CHASE:
                Player player = playerDetectionZone.Player;
                if (player != null){
                    Vector2 direction = (player.GlobalPosition - GlobalPosition).Normalized();
                    velocity = velocity.MoveToward(direction * MAX_SPEED, ACCELERATION * delta);
                } else{
                    state = EnemyAction.IDLE;
                }
                sprite.FlipH = velocity.x < 0;
                break;
        }
        velocity = MoveAndSlide(velocity);
    }

    public void SeekPlayer(){
        if (playerDetectionZone.CanSeePlayer()){
            state = EnemyAction.CHASE;
        }
    }

    public void OnHurtBoxAreaEntered(Area2D area){
        stats.Health -= (area as SwordHitbox).Damage;
        knockback = (area as SwordHitbox).knockbackVector * 120;
        hurtBox.CreateHitEffect();
    }

    public void OnStatsNoHealthEventHandler(){
        QueueFree();
        Node2D enemyDeathEffect = EnemyDeathEffect.Instance<Node2D>();
        GetParent().AddChild(enemyDeathEffect);
        enemyDeathEffect.GlobalPosition = GlobalPosition;
    }
}
