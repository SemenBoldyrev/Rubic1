using Godot;
using Rubic1.Script.EntitieScript.Interfaces;
using Rubic1.Script.Sets.KeySets;
using System;

[GlobalClass]
public partial class CharacterController : CharacterBody2D, ICharacterController
{
    public bool IsMoving => Velocity != Vector2.Zero;

    public Vector2 GetNovementVectorByInput()
    {
        return Input.GetVector(MainKeyNames.LEFT, MainKeyNames.RIGHT, MainKeyNames.UP, MainKeyNames.DOWN);
    }

    public void Movement(Vector2 dir, float speed = 0)
    {
        Velocity = dir * speed;
        MoveAndSlide();
    }
}
