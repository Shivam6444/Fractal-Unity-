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
    private static Vector3[] childDirections = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };

    private static Quaternion[] childOrientations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(90f, 0f, 0f),
        Quaternion.Euler(-90f, 0f, 0f)
    };

    private void Start(){//invoked by unity once its component is enabled.
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;
        if(depth < maxDepth){
            StartCoroutine(CreateChildren());   
        }// HERE THIS MEANS the current struct in which "this" is present.

    }
    private void Initialize(Fractal parent, int childIndex){//This method will invoke before Start.
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
        //Above, Lets say childIndex is 0. it will get the 0 entity from that array.
        transform.localRotation = childOrientations[childIndex];
      
    }

    //This Method will make the process slower.
    private IEnumerator CreateChildren(){
        //for loop will itterate through the number of direction childDirections struct have
        for (int i = 0; i < childDirections.Length; i++){
            yield return new WaitForSeconds(0.5f);//<-Giving time and after that time, it is performing task.
            new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, i);
        }
    }
}
