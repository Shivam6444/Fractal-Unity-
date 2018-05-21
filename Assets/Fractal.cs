using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour {
    //Material and mesh are like Classes.MESH//Contains shapes like triangles.Material//Contains Colors and stuff.

    public Mesh[] meshes;
    public Material material;
    public int maxDepth;
    private int depth;
    public float maxTwist;
    public float childScale;
    public float spawnProbability;
    private Material[,] materials;
    public float maxRotationSpeed;
    private float rotationSpeed;


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
    private void Update(){
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
    private void Start(){//invoked by unity once its component is enabled.
        rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f);
        if (materials == null){//If materails have nothing inside initialize it(by using that method).
            InitializeMaterials();
        }
        gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range(0, meshes.Length)];
        gameObject.AddComponent<MeshRenderer>().material = materials[depth, Random.Range(0,2)];
        //GetComponent<MeshRenderer>().material.color = Color.Lerp(Color.white, Color.yellow, (float)depth / maxDepth);
        if (depth < maxDepth){
            StartCoroutine(CreateChildren());   
        }// HERE THIS MEANS the current struct in which "this" is present.

    }

    private void Initialize(Fractal parent, int childIndex){//This method will invoke before Start.
        meshes = parent.meshes;
        maxTwist = parent.maxTwist;
        maxRotationSpeed = parent.maxRotationSpeed;
        spawnProbability = parent.spawnProbability;
        materials = parent.materials;
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
            if (Random.value < spawnProbability){
                yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));//<-Giving time and after that time, it is performing task.
                new GameObject("Fractal Child").AddComponent<Fractal>().Initialize(this, i);
            }
        }
    }

    private void InitializeMaterials(){
        materials = new Material[maxDepth + 1, 2];
        for (int i = 0; i <= maxDepth; i++)
        {
            float t = i / (maxDepth - 1f);
            t *= t;
            materials[i, 0] = new Material(material);
            materials[i, 0].color = Color.Lerp(Color.white, Color.yellow, t);
            materials[i, 1] = new Material(material);
            materials[i, 1].color = Color.Lerp(Color.white, Color.cyan, t);
        }
        materials[maxDepth, 0].color = Color.magenta;
        materials[maxDepth, 1].color = Color.red;
    }


}
