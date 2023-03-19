using Godot;
using System;

public class Stats : Node
{
    [Export]
    public double MaxHealth { get; set; } = 1;
    private double _health;
    public double Health { get{return _health;}
        set{
            _health = value;
            if (Health <= 0){
                EmitSignal("NoHealthEventHandler");
            }
        } 
    }
    public override void _Ready(){
        this.Health = this.MaxHealth;
    }

    [Signal]
    public delegate void NoHealthEventHandler();
}
