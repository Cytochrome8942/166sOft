using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Bolt;

public class CommonObject : EntityEventListener<IMinionState>
{
    public bool targetable = true;
}
