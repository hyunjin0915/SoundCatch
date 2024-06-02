using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterial : MonoBehaviour
{
    public Material outline;
    
    public List<Material> materials = new List<Material>();
    public Renderer rdr;

    public Vector3 oriPos;
    public Vector3 selectedPos;
    
    public void OnOutline()
    {
        materials.Add(outline);
        rdr.sharedMaterials = materials.ToArray();
        transform.position = selectedPos;
    }

    public void OffOutline()
    {
        if (materials.Count == 2)
        {
            materials.RemoveAt(1);
        }
        rdr.sharedMaterials = materials.ToArray();
        transform.position = oriPos;
    }
}
