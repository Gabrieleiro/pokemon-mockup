using Godot;
using System;

public class Player : Godot.KinematicBody2D
{
    Vector2 direction = new Vector2(0, 0);
    string spriteDirection = "down";
    int SPEED = 1;
    const int MAX_SPEED = 4000;
    bool moving = false;

    public override void _PhysicsProcess(float delta)
    {
        moving = Input.IsActionPressed("ui_up")
                || Input.IsActionPressed("ui_down")
                || Input.IsActionPressed("ui_left")
                || Input.IsActionPressed("ui_right");

        if (moving)
        {
            SPEED = MAX_SPEED;
            if (Input.IsActionPressed("ui_up"))
            {
                direction = new Vector2(0, -1);
                spriteDirection = "up";
            }
            else if (Input.IsActionPressed("ui_down"))
            {
                direction = new Vector2(0, 1);
                spriteDirection = "down";
            }
            else if (Input.IsActionPressed("ui_left"))
            {
                direction = new Vector2(-1, 0);
                spriteDirection = "left";
            }
            else if (Input.IsActionPressed("ui_right"))
            {
                direction = new Vector2(1, 0);
                spriteDirection = "right";
            }

            MoveAndSlide(direction * SPEED * delta, new Vector2(0, 0));

        }
        else
        {
            SPEED = 0;
            direction = new Vector2(0, 0);
        }

        if (direction == new Vector2(0, 0))
        {
            switchAnimation("idle");
            //DEBUG
            GD.Print("idle");
            GD.Print($"SPEED: {SPEED}");
            GD.Print($"X: {direction.x}");
            GD.Print($"Y: {direction.y}");
        }
        else
        {
            switchAnimation("walk");
            //DEBUG
            GD.Print("walk");
            GD.Print($"SPEED: {SPEED}");
            GD.Print($"X: {direction.x}");
            GD.Print($"Y: {direction.y}");
        }

    }

    public void switchAnimation(string stance)
    {
        string newAnimation = $"{stance}{spriteDirection}";
        if (GetNode("Anim").Get("current_animation").ToString() != newAnimation)
        {
            GetNode("Anim").Set("current_animation", newAnimation);
        }
    }
}
