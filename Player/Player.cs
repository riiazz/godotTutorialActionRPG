using Godot;
using System;

public enum Action {
	MOVE, ROLL, ATTACK
}

public class Player : KinematicBody2D
{
	private const int ACCELERATION = 500;
	private const int MAX_SPEED = 80;
	private const int ROLL_SPEED = 100;
	private const int FRICTION = 500;
	private Action state = Action.MOVE;
	private Vector2 velocity = Vector2.Zero;
	private Vector2 rollVector = Vector2.Left;
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
				RollState(delta);
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
		if (inputVector != Vector2.Zero){
			rollVector = inputVector;
			animationTree.Set("parameters/Idle/blend_position", inputVector);
			animationTree.Set("parameters/Run/blend_position", inputVector);
			animationTree.Set("parameters/Attack/blend_position", inputVector);
			animationTree.Set("parameters/Roll/blend_position", inputVector);
			animationState.Travel("Run");
			velocity = velocity.MoveToward(inputVector * MAX_SPEED, ACCELERATION * delta);
		} else {
			animationState.Travel("Idle");
			velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
		}
		
		Move();

		if (Input.IsActionJustPressed("roll")){
			state = Action.ROLL;
		}

		if (Input.IsActionJustPressed("attack")){
			state = Action.ATTACK;
		}
	}

	private void RollState(float delta){
		velocity = rollVector * ROLL_SPEED;
		animationState.Travel("Roll");
		Move();
	}

	private void AttackState(float delta){
		velocity = Vector2.Zero;
		animationState.Travel("Attack");
	}

	private void Move(){
		velocity = MoveAndSlide(velocity);
	}

	private void AttackAnimationFinished(){
		state = Action.MOVE;
	}

	private void RollAnimationFinished(){
		state = Action.MOVE;
	}
}
