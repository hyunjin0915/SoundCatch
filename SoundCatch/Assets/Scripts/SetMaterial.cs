using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetMaterial : MonoBehaviour
{
    public Material outline;
    
    public List<Material> materials = new List<Material>();
    public Renderer rdr;

    public Vector3 oriPos;
    public Vector3 selectedPos;
    
    public void OnOutline()
    {
        // 현재 씬이 활성화된 씬일 때만 작동하도록 처리
        if (SceneManager.GetActiveScene().name == gameObject.scene.name)
        {
            materials.Add(outline);
            rdr.sharedMaterials = materials.ToArray();
            transform.position = selectedPos;
        }
    }

    public void OffOutline()
    {
        // 현재 씬이 활성화된 씬일 때만 작동하도록 처리
        if (SceneManager.GetActiveScene().name == gameObject.scene.name)
        {
            if (materials.Count == 2)
            {
                materials.RemoveAt(1);
            }
            rdr.sharedMaterials = materials.ToArray();
            transform.position = oriPos;
        }
    }
}
