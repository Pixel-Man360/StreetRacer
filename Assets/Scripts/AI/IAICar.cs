using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAICar
{
    public CheckPoint GetCurrentCheckPoint();
    public AiCarController GetAI();
}
