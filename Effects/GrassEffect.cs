using Godot;
using System;

public class GrassEffect : Node2D
{
    private AnimatedSprite animatedSprite;
    public override void _Ready()
    {
        animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        animatedSprite.Frame = 0; 
        animatedSprite.Play("Animate");
    }

    public void _OnAnimatedSpriteAnimationFinished(){
        QueueFree();
    }
}
