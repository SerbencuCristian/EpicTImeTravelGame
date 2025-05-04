using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
public class FadeUI : Image
{
    public override Material materialForRendering //honestly no idea what this means, i stole it, but it works
    {
        get
        {
            Material mat = new Material(base.materialForRendering);
            mat.SetInt("_StencilComp",(int)CompareFunction.NotEqual);
            return mat;
        }
    }
}