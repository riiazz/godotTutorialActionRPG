using Godot;
using System;

public class Effect : AnimatedSprite
{
    public override void _Ready()
    {
        this.Connect("animation_finished", this, "_OnAnimatedSpriteAnimationFinished");
        Frame = 0; 
        Play("Animate");
    }

    public void _OnAnimatedSpriteAnimationFinished(){
        QueueFree();
    }
}
