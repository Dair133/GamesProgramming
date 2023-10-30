using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<GameObject> allPrefabs = new List<GameObject>();
    public GameObject playerPrefab;
    public GameObject testGunPrefab;
    public GameObject testObjectPrefab;
    public GameObject defaultItem;

    private GameObject playerInstance;
    private GameObject itemSelected;
    private GameObject previousItemSelected;
    // Start is called before the first frame update
    void Start()
    {
        //spawns player
       playerInstance =  GameObject.Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        previousItemSelected = defaultItem;
        LoadPrefabs();//loads in all objects into the list from the folder specified
     
    }

    // Update is called once per frame
    void Update()
    {
       if(previousItemSelected != itemSelected)
        {
           
            loadSelectedItem();
        }
    }

    void LoadPrefabs()
    {
        // Load all prefabs from the folder into the dictionary
        GameObject[] loadedPrefabs = Resources.LoadAll<GameObject>("AllObjects");

        for (int i = 0; i < loadedPrefabs.Length; i++)
        {
            string key = "string" + i.ToString();
            allPrefabs.Add(loadedPrefabs[i]);
        }
    }

    void loadSelectedItem()
    {
        Vector3 originalObjectScale;
        //gun selected from inventory
        itemSelected = pullItemFromList(testGunPrefab);
        previousItemSelected = itemSelected;
        //now add child to pplayer of the selected item
        // Instantiate new selected item
        //itemSelected.name = "heldItem"; Dont need this line so long as GetChild(3) (helditem is the third prefab)


        originalObjectScale = itemSelected.transform.localScale;
        GameObject instantiatedItem = Instantiate(itemSelected, playerInstance.transform.position, Quaternion.identity);

        // Set the player as the parent of the instantiated item
        instantiatedItem.transform.SetParent(playerInstance.transform);
        instantiatedItem.transform.localPosition = new Vector3(0, 0, 0);
        //Debug.Log("original scale" + originalObjectScale);
        instantiatedItem.transform.localScale = originalObjectScale/5;//we divide by 5 because its scaled by 5 cause player is, so divide to get original scale
        //position of the individual different items say for large weapons can be set in large weapon script
        // so in the large weapon script we set it at the begining accoridngly because all large weapons will 
        //need to have the same position

    }

    GameObject pullItemFromList(GameObject itemSelected)
    {//pulls the prefab selected in the inventroy from the global list of items

        if(allPrefabs.Find(allPrefabs => allPrefabs.name == itemSelected.name)) 
            return allPrefabs.Find(allPrefabs => allPrefabs.name == itemSelected.name);

        else
            Debug.LogWarning("Item not found in list"); return defaultItem; //returns null and prints warning
    }


}
