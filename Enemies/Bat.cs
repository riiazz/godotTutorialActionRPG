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
    [Export]
    private int WANDER_TARGET_RANGE = 4;
    private Vector2 velocity = Vector2.Zero;
    private EnemyAction state = EnemyAction.CHASE;
    private AnimatedSprite sprite;
    private HurtBox hurtBox;
    private SoftCollision softCollision;
    private WanderController wanderController;

    public override void _Ready(){
        stats = GetNode<Stats>("Stats");
        playerDetectionZone = GetNode<PlayerDetectionZone>("PlayerDetectionZone");
        sprite = GetNode<AnimatedSprite>("AnimatedSprite");
        hurtBox = GetNode<HurtBox>("HurtBox");
        softCollision = GetNode<SoftCollision>("SoftCollision");
        wanderController = GetNode<WanderController>("WanderController");

        Godot.Collections.Array<EnemyAction> stateList = new Godot.Collections.Array<EnemyAction>();
        stateList.Add(EnemyAction.IDLE);
        stateList.Add(EnemyAction.WANDER);
        state = PickRandomState(stateList);
    }

    public override void _PhysicsProcess(float delta){
        knockback = knockback.MoveToward(Vector2.Zero, FRICTION * delta);
        knockback = MoveAndSlide(knockback);

        switch(state){
            case EnemyAction.IDLE:
                velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
                SeekPlayer();
                RandomState();
                break;

            case EnemyAction.WANDER:
                SeekPlayer();
                RandomState();
                AccelerateTowardPoint(wanderController.TargetPosition, delta);
                if (GlobalPosition.DistanceTo(wanderController.TargetPosition) <= WANDER_TARGET_RANGE)
                {
                    UpdateWander();
                }
                break;
                
            case EnemyAction.CHASE:
                Player player = playerDetectionZone.Player;
                if (player != null){
                    AccelerateTowardPoint(player.GlobalPosition, delta);
                } else{
                    state = EnemyAction.IDLE;
                }
                break;
        }
        velocity += softCollision.GetPushVector() * delta * 400;
        velocity = MoveAndSlide(velocity);
    }

    private void UpdateWander()
    {
        Godot.Collections.Array<EnemyAction> stateList = new Godot.Collections.Array<EnemyAction>();
        stateList.Add(EnemyAction.IDLE);
        stateList.Add(EnemyAction.WANDER);

        state = PickRandomState(stateList);
        wanderController.StartWanderTimer((float)GD.RandRange(1, 3));
    }

    private void AccelerateTowardPoint(Vector2 point, float delta){
        Vector2 direction = GlobalPosition.DirectionTo(point);
        velocity = velocity.MoveToward(direction * MAX_SPEED, ACCELERATION * delta);
        sprite.FlipH = velocity.x < 0;
    }

    private void RandomState()
    {
        if (wanderController.GetTimeLeft() == 0)
        {
            UpdateWander();
        }
    }

    public void SeekPlayer(){
        if (playerDetectionZone.CanSeePlayer()){
            state = EnemyAction.CHASE;
        }
    }
    
    public EnemyAction PickRandomState(Godot.Collections.Array<EnemyAction> stateList){
        stateList.Shuffle();
        return stateList[0];
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
