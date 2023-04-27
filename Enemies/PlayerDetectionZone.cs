using Godot;
using System;

public class PlayerDetectionZone : Area2D
{
    public Player Player { get; set; } = null;
    public void OnPlayerDetectionZoneBodyEntered(Node body){
        Player = body as Player;
    }
    public void OnPlayerDetectionZoneBodyExited(Node body){
        Player = null;
    }
    public bool CanSeePlayer(){
        return Player != null;
    }
}
