using UnityEngine;
using System.Collections;

public class RobotBuilder : MonoBehaviour {

    /*
    
        Step 1: get array of parts
        Step 2: using torso, attach head
        Step 3: attach arms
        Step 4: attach legs
        Step 5: update prefab -- is this even possible???

                should i actually just work with a singleton?
        
        */

    public GameObject[] Parts; // probably the WORST WAY TO DO THIS


    void UpdateConfiguration() {

        // this is probably a massive hack but I simply do not care anymore

        // gets the list of instantiated parts
        var updatedTorsoPrefab       = Instantiate(Parts[0]);
        var updatedHeadPrefab        = Instantiate(Parts[1]);
        var updatedLeftArmPrefab     = Instantiate(Parts[2]);
        var updatedRightArmPrefab    = Instantiate(Parts[3]);
        var updatedLeftLegPrefab     = Instantiate(Parts[4]);
        var updatedRightLegPrefab    = Instantiate(Parts[5]);
        //var updatedExtraPrefab       = Instantiate(Parts[6]);

        //gameObject.SendMessage("Destroy");

        // Move torso
        updatedTorsoPrefab.transform.position = Vector3.zero; // Moves the torso to 0,0,0.

        // Move head
        updatedHeadPrefab.transform.position = updatedTorsoPrefab.GetComponent<DataContainer>().HeadNodeOffset.transform.position + updatedHeadPrefab.GetComponent<DataContainer>().HeadNodeOffset.transform.position;

        // Move arms
        updatedLeftArmPrefab.transform.position = updatedTorsoPrefab.GetComponent<DataContainer>().LeftArmNodeOffset.transform.position + updatedLeftArmPrefab.GetComponent<DataContainer>().LeftArmNodeOffset.transform.position;

        updatedRightArmPrefab.transform.position = updatedTorsoPrefab.GetComponent<DataContainer>().RightArmNodeOffset.transform.position + updatedRightArmPrefab.GetComponent<DataContainer>().RightArmNodeOffset.transform.position;

        // Move legs
        updatedLeftLegPrefab.transform.position = updatedTorsoPrefab.GetComponent<DataContainer>().LeftLegNodeOffset.transform.position + updatedLeftLegPrefab.GetComponent<DataContainer>().LeftLegNodeOffset.transform.position;

        updatedRightLegPrefab.transform.position = updatedTorsoPrefab.GetComponent<DataContainer>().RightLegNodeOffset.transform.position + updatedRightLegPrefab.GetComponent<DataContainer>().RightLegNodeOffset.transform.position;

        // Move extra component (not done)
        //updatedExtraPrefab.transform.position = updatedTorsoPrefab.GetComponent<DataContainer>().HeadNodeOffset.transform.position;

    }
}
