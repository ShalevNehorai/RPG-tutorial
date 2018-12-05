using UnityEngine.Networking;

public interface IDamageable
{
    [Command]
     void CmdTakeDamage(float damage);
}