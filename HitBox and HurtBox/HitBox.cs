using Godot;
using System;

public class HitBox : Area2D
{
    [Export]
    public double Damage { get; set; } = 1;
}
