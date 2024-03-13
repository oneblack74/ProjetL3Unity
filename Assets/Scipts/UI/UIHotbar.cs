using UnityEngine;

public class UIHotbar : MonoBehaviour
{
    private int slot = 0;
    public void MoveSelect(int way)
    {
        slot += way;
        if (slot >= 10)
        {
            slot = 0;
        }
        if (slot < 0)
        {
            slot = 9;
        }
        transform.localPosition = new Vector3(-525 + (116 * slot), 0, 0);
    }

    public int GetSlot
    {
        get { return slot; }
    }
}
