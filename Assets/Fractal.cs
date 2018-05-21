using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {
//Material and mesh are like Classes.
    public Mesh mesh;//Contains shapes like triangles.
    public Material material;//Contains Colors and stuff.
    public int maxDepth;
    private int depth;
    public float childScale;
    private void Start(){//invoked by unity once its component is enabled.
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        if(depth < maxDepth){
            new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.up);
            new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, Vector3.right);
        }// HERE THIS MEANS the current struct in which "this" is present.

    }
    private void Initialize(Fractal parent, Vector3 direction){//This method will invoke before Start.
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = direction * (0.5f + 0.5f * childScale);
    }
}
