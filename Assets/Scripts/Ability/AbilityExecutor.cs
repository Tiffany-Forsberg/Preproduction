using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityExecutor : MonoBehaviour
{
    [SerializeField] private AbilityData ability;
    [SerializeField] private GameObject target;

    public void Execute(GameObject target)
    {
        foreach (var effect in ability.Effects)
        {
            effect.Execute(gameObject, target);
        }
    }

    public void HandleBasicAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Execute(target);
        }
    }
}
