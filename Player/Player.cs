using Godot;
using System;

public enum Action {
	MOVE, ROLL, ATTACK
}

public class Player : KinematicBody2D
{
	private const int ACCELERATION = 500;
	private const int MAX_SPEED = 80;
	private const int FRICTION = 500;
	private Action state = Action.MOVE;
	private Vector2 velocity = Vector2.Zero;
	private AnimationPlayer animationPlayer;
	private AnimationTree animationTree;
	private AnimationNodeStateMachinePlayback animationState;
	public override void _Ready(){
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationTree = GetNode<AnimationTree>("AnimationTree");
		animationTree.Active = true;
		animationState = (AnimationNodeStateMachinePlayback) animationTree.Get("parameters/playback");
	}
	
	public override void _PhysicsProcess(float delta){
		switch (state){
			case Action.MOVE:
				MoveState(delta);
				break;
			case Action.ROLL:
				break;
			case Action.ATTACK:
				AttackState(delta);
				break;
		}
	}

	private void MoveState(float delta){
		Vector2 inputVector = Vector2.Zero;
		inputVector.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
		inputVector.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
		inputVector = inputVector.Normalized();
		GD.Print(inputVector);
		if (inputVector != Vector2.Zero){
			animationTree.Set("parameters/Idle/blend_position", inputVector);
			animationTree.Set("parameters/Run/blend_position", inputVector);
			animationTree.Set("parameters/Attack/blend_position", inputVector);
			animationState.Travel("Run");
			velocity = velocity.MoveToward(inputVector * MAX_SPEED, ACCELERATION * delta);
		} else {
			animationState.Travel("Idle");
			velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
		}
		velocity = MoveAndSlide(velocity);
		if (Input.IsActionJustPressed("attack")){
			state = Action.ATTACK;
		}
	}

	private void AttackState(float delta){
		velocity = Vector2.Zero;
		animationState.Travel("Attack");
	}

	private void AttackAnimationFinished(){
		state = Action.MOVE;
	}
}
