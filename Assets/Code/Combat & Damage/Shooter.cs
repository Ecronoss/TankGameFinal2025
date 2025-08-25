using UnityEngine;

public abstract class Shooter : MonoBehaviour
{

    public abstract void Start();
    public abstract void Shoot(Pawn shooterPawn);
    public abstract void ShotGun(Pawn shooterPawn);
    public abstract void DropMine(Pawn shooterPawn);
    public abstract void TryShoot(Pawn shooterPawn);
    public abstract void TryShotGun(Pawn shooterPawn);
    public abstract void TryDropMine(Pawn shooterPawn);
}
