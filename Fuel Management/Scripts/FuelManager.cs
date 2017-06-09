//This script handles fuel costs and recipies for crafting. it should be called when a nanobot is asking if it can make something an asking to destroy something.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using System.Linq;

public class FuelManager : MonoBehaviour
{
    public List<string> transformFuelTags = new List<string>();
    public GameObject fuelMeterParent;

    ItemDataBaseList itemDataBase;
    List<int> currentFuelLevels = new List<int>();
    LootManager lootmanger;
    bool update = false;
    bool negativeFuelChange = false;
    int[] fuelChange;
    //debug
    //GameObject ball;

    void Start()
    {
        //debug
        //Instantiate(GameObject.Find("Ball"));
 
        //assignes transform fuel tags if not assigned
        if (transformFuelTags.Count == 0)
        {
            transformFuelTags.Add("Tree");
            transformFuelTags.Add("Machine");
        }
        itemDataBase = (ItemDataBaseList)Resources.Load("ItemDatabase");
        lootmanger = GameObject.FindObjectOfType<LootManager>();

        //Gets current fuel levels from UI text components
        Text[] fuelLevels = GameObject.FindGameObjectWithTag("Fuel").GetComponentsInChildren<Text>();
        foreach (Text fuelLevel in fuelLevels)
        {
            currentFuelLevels.Add(int.Parse(fuelLevel.text));
        }
    }
    void Update()
    {
        /*
        debug
        if (Input.GetKeyUp(KeyCode.O))
           OnObjectDestroy(ball);
        if (Input.GetKeyUp(KeyCode.P))
           negativeFuelChange = OnObjectCreate(ball);
        */

        if (update)
        {
            //update the text display of fuel levels
            for (int i = 0; i < currentFuelLevels.Count; i++)
            {
                if (negativeFuelChange)
                    currentFuelLevels[i] -= fuelChange[i];
                else
                    currentFuelLevels[i] += fuelChange[i];
            }
            negativeFuelChange = false;
            Text[] fuelMeters = fuelMeterParent.GetComponentsInChildren<Text>();
            int fuel = 0;
            foreach (Text fuelMeter in fuelMeters)
            {

                fuelMeter.text = currentFuelLevels[fuel].ToString();
                fuel++;
            }

            StartCoroutine(FlashFuelHUD(fuelChange));
            update = false;

        }
    }

    //Call this when an object wants to be destroyed
    public bool OnObjectDestroy(GameObject gameobject)
    {
        bool permissionToDestroy = false;

        //cost to destroy an object is 45% of the matter fuel required to create it
        int matterFuelCost = Mathf.RoundToInt(0.45f * BuildMatterFuel(gameobject));
        
        //check to see if player can afford to destroy object, checks matter fuel only [2]
        if (matterFuelCost <= currentFuelLevels[2])
        {
            //builds recipe for item creation, uses 100% of the recipe value
            fuelChange = new int[] {
            BuildAestheticFuel(gameobject),
            BuildPhysicsFuel(gameobject),
            BuildMatterFuel(gameobject),
            BuildTransformFuel(gameobject)
            };

            //add item as an Inventory Master Item, including its fuel recipe
            lootmanger.CreateItem(gameobject,fuelChange);

            //lower the recipe by 10%. The remaining 90% is considered profit for harvesting this item
            fuelChange = new int[] {
                Mathf.RoundToInt(0.9f * fuelChange[0]),
                Mathf.RoundToInt(0.9f * fuelChange[1]),
                Mathf.RoundToInt(0.9f * fuelChange[2] - matterFuelCost),
                Mathf.RoundToInt(0.9f * fuelChange[3])
            };

            update = true; 
            permissionToDestroy = true;
        }
        else
            NotEnoughFuel();
        return permissionToDestroy;
    }

    //Game object call option
    public bool OnObjectCreate(GameObject gameobject)
    {
        bool createBool = OnObjectCreate(itemDataBase.getItemByName(gameobject.name));
        return createBool;
    }

    //call when an object wants to be born
    public bool OnObjectCreate(Item item)
    {
        bool permissionToCreate = false;
        if (item != null)
        {
            fuelChange = item.fuelRecipe;
            //check if player can afford to create
            bool canMake = true;
            for (int i = 0; i < currentFuelLevels.Count; i++)
            {
                if (currentFuelLevels[i] < item.fuelRecipe[i])
                {
                    NotEnoughFuel();
                    canMake = false;
                    break;
                }
            }
            if (canMake)
            {
                permissionToCreate = true;
                update = true;
            }
            else
            {
                NotEnoughFuel();
            }
        }
        return permissionToCreate;
    }

    //calculates amount of Aesthetic fuel a given gameObject has based on its material name
    public int BuildAestheticFuel(GameObject gameobject)
    {
        List<string> colors = new List<string>();
        if (gameobject.GetComponent<MeshRenderer>().material != null)
        {
            if (gameobject.GetComponent<MeshRenderer>().material.name.Contains(","))
            {
                colors = (gameobject.GetComponent<MeshRenderer>().material.name.Split(',')).ToList();
                string[] lastColor = colors.Last().Split(' ');
                colors.RemoveAt(colors.Count - 1);
                colors.Add(lastColor[0]);
            }
        }
        return 10 * colors.Count;
    }

    //caluculates amount of physics fuel based on if object has a rigidbody and how complex the mesh is
    public int BuildPhysicsFuel(GameObject gameobject)
    {
        int triangleCount = 0;
        if (gameobject.GetComponent<Rigidbody>() != null && gameobject.GetComponent<MeshFilter>() != null)
        {
            triangleCount = gameobject.GetComponent<MeshFilter>().mesh.triangles.Length / 3;
        }
        int fuel =10 + Mathf.RoundToInt( triangleCount / 20);

        return fuel;
    }

    //caluculates amount of Matter Fuel based on volume of gameobject
    public int BuildMatterFuel(GameObject gameobject)
    {
        float volume = 0;
        if (gameobject.GetComponent<MeshFilter>() != null)
            volume = FindVolumeOfGameObject(gameobject);
        return 10 + Mathf.RoundToInt(volume);
    }

    //calculates transform fuel based on if it has an accepted tag and on its volume
    public int BuildTransformFuel(GameObject gameobject)
    {
        float volume = 0;
        if (transformFuelTags.Contains(gameobject.tag))
            volume = 15 + Mathf.RoundToInt(FindVolumeOfGameObject(gameobject));
        return Mathf.RoundToInt(volume);
    }

    //utility for finding volume of gameobjects, used for matter fuel and transform
    float FindVolumeOfGameObject(GameObject gameobject)
    {
        float volume = 0;
        Mesh mesh = gameobject.GetComponent<MeshFilter>().sharedMesh;
        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            Vector3 p1 = vertices[triangles[i + 0]];
            Vector3 p2 = vertices[triangles[i + 1]];
            Vector3 p3 = vertices[triangles[i + 2]];
            volume += SignedVolumeOfTriangle(p1, p2, p3);
        }
        return volume;
    } 

    //utility used to calculate MatterFuel and transform fuel
    float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float v321 = p3.x * p2.y * p1.z;
        float v231 = p2.x * p3.y * p1.z;
        float v312 = p3.x * p1.y * p2.z;
        float v132 = p1.x * p3.y * p2.z;
        float v213 = p2.x * p1.y * p3.z;
        float v123 = p1.x * p2.y * p3.z;
        return (1.0f / 6.0f) * (-v321 + v231 + v312 - v132 - v213 + v123);
    }

    //flashes the fuel meter when it is updated
    IEnumerator FlashFuelHUD(int[] fuelChange)
    {
        Color[] fuelFlashScreenColors = new Color[] {
            new Color ( 1,0,0,1 ),//Red
            new Color ( 0,0,1,1 ),//Blue
            new Color ( 1,0.5f,0,1 ),//Orange
            new Color ( 0,1,0,1 )//Green
        };
                
        for(int i = 0; i < fuelChange.Length; i++)
        {
            if (fuelChange[i] != 0)
                fuelMeterParent.transform.GetChild(i).GetComponentInChildren<Image>().color = fuelFlashScreenColors[i];
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < fuelChange.Length; i++)
        {
            fuelMeterParent.transform.GetChild(i).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1); //White
        }
    }

    //display text informing player they don't have the fuel to do that action
    void NotEnoughFuel()
    {
        Debug.Log("You're don't have enough fuel");
        int[] noChange = new int[] { 0, 0, 0, 0 };
        FlashFuelHUD(noChange);
    }
}
