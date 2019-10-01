using UnityEngine;

public abstract class DevilDeal : ScriptableObject
{
    [TextArea(10, 100)]
    public string dealDescriptionEnglish;

    [TextArea(10, 100)]
    public string dealDescriptionDanish;

    public virtual void ApplyDevilDeal()
    {
        // override in child class
    }
}
