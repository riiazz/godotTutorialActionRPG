using Godot;
using System;
public class SoftCollision : Area2D
{
    private (bool isCollide, Godot.Collections.Array areas) IsColliding(){
        Godot.Collections.Array areas = GetOverlappingAreas();
        return (areas.Count > 0, areas);
    }

    public Vector2 GetPushVector(){
        var isColliding = IsColliding();
        Vector2 pushVector = Vector2.Zero;
        if(isColliding.isCollide){
            Area2D area = isColliding.areas[0] as Area2D;
            pushVector = area.GlobalPosition.DirectionTo(GlobalPosition);
        }
        return pushVector;
    }
}
