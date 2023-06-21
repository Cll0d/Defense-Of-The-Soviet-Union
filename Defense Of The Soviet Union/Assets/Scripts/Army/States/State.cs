using UnityEngine;

public abstract class State : MonoBehaviour
{
    [SerializeField] private Transition[] _transitions;

    public void Enter()
    {
        if(enabled == false)
        {
            enabled = true;
            foreach(var tranmstion in _transitions)
            {
                tranmstion.enabled = true;
            }
        }
    }

    public void Exit() 
    { 
        if(enabled == true)
        {
            foreach(var transition in _transitions)
            {
                transition.enabled = false;
            }
            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach(var transition in _transitions)
        {
            if(transition.NeedSwitch)
            {
                return transition.NextState;
            }
        }
        return null;
    }
}
