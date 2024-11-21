using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InverseMask : Image
{
    public override Material materialForRendering
    {
        get
        {
            Material mat = new Material(base.materialForRendering);
            mat.SetFloat("_StencilComp", (float)CompareFunction.NotEqual);
            return mat;
        }
    }
}
