[System.Serializable]
public abstract class Powerup
{
    public bool permanent;
    public float duration;
    public abstract void Apply(PowerupManager target);
    public abstract void Remove(PowerupManager target);
}
