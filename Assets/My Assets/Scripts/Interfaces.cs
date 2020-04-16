using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaces 
{
    public interface ISelectable
    {
        void addToPlayersSelected(GameObject go);
    }

    public interface IAlive
    {
        Unit_Life life { get; set; }
        void TakeDamage(int amount);
        void TakeHealing(int amount);
    }
    

    public interface IUnitProducing
    {
        void createUnit(Unit unit);
    }

    public interface IAttacking
    {
        void attack(GameObject target);
        //void attack(Vector3 position);
        void stopAttack();
    }

    public interface IMovable
    {
        void move(Vector3 destination);
        void stopMoving();
    }
}
