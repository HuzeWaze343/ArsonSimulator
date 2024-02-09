using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullObjects : MonoBehaviour
{
    List<GameObject> culledObjects = new List<GameObject>();
    Material wall;
    Material wallTransparent;
    private void Start()
        {
        wall = Resources.Load<Material>("Materials/WallMaterial");
        wallTransparent = Resources.Load<Material>("Materials/WallMaterialTransparent");
        }
    private void OnTriggerEnter(Collider other)
        {
        if (other.gameObject.layer == LayerMask.NameToLayer("Cullable"))
            {
            culledObjects.Add(other.gameObject); 
            Renderer renderer = other.gameObject.GetComponent<Renderer>();
            Material otherMat = renderer.material;
            renderer.material = wallTransparent;
            renderer.material.color = new Color(otherMat.color.r, otherMat.color.g, otherMat.color.b, wallTransparent.color.a);
            }
        }
    private void OnTriggerExit(Collider other)
        {
        if (culledObjects.Contains(other.gameObject))
            {
            Renderer renderer = other.gameObject.GetComponent<Renderer>();
            Material otherMat = renderer.material;
            renderer.material = wall;
            renderer.material.color = new Color(otherMat.color.r, otherMat.color.g, otherMat.color.b, wall.color.a);
            culledObjects.Remove(other.gameObject);
            }
        }
    }
