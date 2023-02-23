using Godot;
using System;

public class Player : KinematicBody2D
{
	private const int ACCELERATION = 500;
	private const int MAX_SPEED = 80;
	private const int FRICTION = 500;
	private Vector2 velocity = Vector2.Zero;
	private AnimationPlayer animationPlayer;
	private AnimationTree animationTree;
	private AnimationNodeStateMachinePlayback animationState;
	public override void _Ready(){
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		animationState = (AnimationNodeStateMachinePlayback) animationTree.Get("parameters/playback");
	}
	
	public override void _PhysicsProcess(float delta){
		Vector2 inputVector = Vector2.Zero;
		inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		inputVector = inputVector.Normalized();
		if (inputVector != Vector2.Zero){
			animationTree.Set("parameters/Idle/blend_position", inputVector);
			animationTree.Set("parameters/Run/blend_position", inputVector);
			animationState.Travel("Run");
			velocity = velocity.MoveToward(inputVector * MAX_SPEED, ACCELERATION * delta);
		} else {
			animationState.Travel("Idle");
			velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
		}
		velocity = MoveAndSlide(velocity);
	}
}
