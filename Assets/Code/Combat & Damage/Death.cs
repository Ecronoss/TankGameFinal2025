using UnityEngine;

public abstract class Death : MonoBehaviour
{
    public Pawn pawn;
    public void Awake()
    {
        pawn = GetComponent<Pawn>();
    }
    public abstract void Die(Pawn source);
}
