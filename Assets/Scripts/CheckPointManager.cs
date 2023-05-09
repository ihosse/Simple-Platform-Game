using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    private CheckPoint[] checkpoints;

    private void Start()
    {
        checkpoints = GetComponentsInChildren<CheckPoint>();
    }

    public CheckPoint GetLastCheckpointthatWasPassed() 
    {
        return checkpoints.Last(t => t.Passed == true);
    }
}
