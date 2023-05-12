using Godot;
using System;

public class Camera2D : Godot.Camera2D
{
    private Position2D topLeft;
    private Position2D bottomRight;
    public override void _Ready()
    {
        topLeft = GetNode<Position2D>("Limits/TopLeft");
        bottomRight = GetNode<Position2D>("Limits/BottomRight");
        LimitTop = (int) topLeft.Position.y;
        LimitLeft = (int) topLeft.Position.x;
        LimitBottom = (int) bottomRight.Position.y;
        LimitRight = (int) bottomRight.Position.x;
    }

}
