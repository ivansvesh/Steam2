using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script : MonoBehaviour
{
    public bool isdead = false;
    public float timeRemaining = 0.1f;
    private IEnumerator coroutine;
    [SerializeField] private GameObject kol;
    [SerializeField] private GameObject Target; 
    GameObject objectTarget; 
    MeshRenderer redMesh;    
    [SerializeField] Material redMaterial, greenMaterial;
    Material kolMaterial;
    [SerializeField] private GameObject[] itemsArray;
    [SerializeField] private GameObject itemsParent;
 
    private int _totalItems = 0;
    
    void Start()
    {
        _totalItems = itemsParent.transform.childCount;
 
        itemsArray = new GameObject[_totalItems];
 
        for(int i = 0; i < _totalItems; i++)
        {
            itemsArray[i] = itemsParent.transform.GetChild(i).gameObject;
        }
         coroutine = СhangeСolorsTrafficLight();
         StartCoroutine(coroutine);
    }
    private IEnumerator СhangeСolorsTrafficLight()
	{  
	    redMesh.material = redMaterial;
	    yield return new WaitForSeconds(1f);
	}

    void OnCollisionEnter(Collision collision)
    {	if(collision.relativeVelocity.magnitude >= 3)
	    {
 	        Destroy(gameObject);

            objectTarget = collision.gameObject;
            if (objectTarget == Target) 
            {
                kolMaterial = greenMaterial;
            }
            else
            {
                kolMaterial = redMaterial;
            }

	        GetComponent<Rigidbody>().isKinematic = false;
	            foreach(GameObject i in itemsArray)
	            {
	                i.GetComponent<MeshRenderer>().material = kolMaterial; 
  	                i.GetComponent<Rigidbody>().isKinematic = false;
	            }

              Vector3 pos = gameObject.transform.position;
              isdead = true;
              Instantiate(kol, pos, Quaternion.identity);
        }
    }

    void Update()
    {
	Debug.Log(Resources.Load<GameObject>("kol") as GameObject);
	if(isdead)
        {
	        timeRemaining -= Time.deltaTime;
	        if(timeRemaining < 0)
	        {
	            Destroy(gameObject); 
	        }
        }
    }
}