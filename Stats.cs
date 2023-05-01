using Godot;
using System;

public class Stats : Node
{
    private double _maxHealth;
    [Export]
    public double MaxHealth { get{return _maxHealth;} 
    set{
        _maxHealth = value;
        Health = Mathf.Min((int) Health, (int) _maxHealth);
        EmitSignal("MaxHealthChangedEventHandler", _maxHealth);
    } }
    private double _health;
    [Export]
    public double Health { get{return _health;}
        set{
            _health = value;
            EmitSignal("HealthChangeEventHandler", _health);
            if (_health <= 0){
                EmitSignal("NoHealthEventHandler");
            }
        } 
    }
    public override void _Ready(){
        Health = MaxHealth;
    }

    [Signal]
    public delegate void NoHealthEventHandler();
    [Signal]
    public delegate void HealthChangeEventHandler(float value);
    [Signal]
    public delegate void MaxHealthChangedEventHandler(float value);
}
