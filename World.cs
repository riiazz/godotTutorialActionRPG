using Godot;
using System;

public class World : Node2D
{
    private Soundtrack soundtrack;
    public override void _Ready()
    {
        soundtrack = GetNode<Soundtrack>("/root/Soundtrack");
        soundtrack.LoopSoundtrack(true);
    }
}
