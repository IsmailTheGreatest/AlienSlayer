using UnityEngine;

public class EnvironmentObjectTrigger : MonoBehaviour
{


    public EnvironmentObject[] objectsAbovePlayer;
    public EnvironmentObject[] objectsLeftOfPlayer;
    public EnvironmentObject[] objectsRightOfPlayer;
    public EnvironmentObject[] objectsBelowPlayer;

    public void SetActiveObjects()
    {
        for (int i = 0; i < objectsAbovePlayer.Length; i++)
        {
            if (objectsAbovePlayer[i] != null)
                objectsAbovePlayer[i].SetActiveObject(EnvironmentObject.WhenPlayerIsOnPosition.Below);
        }

        for (int i = 0; i < objectsLeftOfPlayer.Length; i++)
        {
            if (objectsLeftOfPlayer[i] != null)
                objectsLeftOfPlayer[i].SetActiveObject(EnvironmentObject.WhenPlayerIsOnPosition.Right);
        }

        for (int i = 0; i < objectsRightOfPlayer.Length; i++)
        {
            if (objectsRightOfPlayer[i] != null)
                objectsRightOfPlayer[i].SetActiveObject(EnvironmentObject.WhenPlayerIsOnPosition.Left);
        }

        for (int i = 0; i < objectsBelowPlayer.Length; i++)
        {
            if (objectsBelowPlayer[i] != null)
                objectsBelowPlayer[i].SetActiveObject(EnvironmentObject.WhenPlayerIsOnPosition.Above);
        }
    }
}
