using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGetCurrentVoltRatingsAffectingCurrentBehaviour
{
    public float GetComponentVoltRating()
    {
        return 0;
    }

    public float GetComponentCurrentRating()
    {
        return 0;
    }
}
