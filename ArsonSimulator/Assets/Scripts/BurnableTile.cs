using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BurnableTile : MonoBehaviour
{
    #region Tile States
    [SerializeField]
    public bool isBurning = false;
    bool hasSpread = false; //lets us skip checking for spreading behaviour
    public bool hasPetrol = false;
    #endregion
    #region Tile variables
    float maxHealth = 5f;
    float health;
    float burnRate = 5f;
    float spreadThreshold = 2f; //threshold, in health points, that must be met before fire will spread to neighbouring tiles
    float petrolMultiplier = 2f; //multiplier to burn damage if the tile has petrol on it
    #endregion
    #region component/object references
    Transform[] connections;
    Renderer[] rend;
    ParticleSystem particleSystem;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        Transform ignore = transform.Find("Connections").GetComponent<Transform>();
        connections = transform.Find("Connections").GetComponentsInChildren<Transform>().Where(t => t.transform != ignore).ToArray();
        rend = transform.GetComponentsInChildren<Renderer>();
        particleSystem = transform.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        BurnTile();
        SpreadFire();
    }
    void BurnTile()
        {
        if (!isBurning) return; //skip everything if tile isnt currently burning

        //calc burn damage and remove it from tile health
        float dmg = burnRate * Time.deltaTime;
        if (hasPetrol) dmg *= petrolMultiplier;
        health -= dmg;

        //change tile colors based on hp
        float c = health / maxHealth;
        Color color = new Color(c, c, c, 1);
        foreach (var item in rend)
            {
            item.material.SetColor("_Color", color);
            }

        //enable fire particles
        if(!particleSystem.isPlaying) particleSystem.Play();

        if (health <= maxHealth / 2) transform.Find("Petrol").gameObject.SetActive(false); //hide petrol graphic when health is <= half
        if (health <= 0f)
            {
            if (GameObject.Find("GameController"))
                GameObject.Find("GameController").GetComponent<GameController>().burnableTiles.Remove(transform.gameObject);
            isBurning = false; //stop burning when hp runs out
            particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            }
        }
    void SpreadFire()
        {
        if (hasSpread) return; //skip spreading behaviour if tile has already spread fire
        float threshold = spreadThreshold;
        if (hasPetrol) threshold = maxHealth * 0.8f; //fire instantly spreads if the tile has petrol
        if (health <= threshold)
            {
            foreach (var item in connections)
                {
                //check for neighbouring tiles
                LayerMask lm = LayerMask.GetMask("Tiles");
                Collider[] hitcolliders = Physics.OverlapBox(item.position, item.localScale / 10, Quaternion.identity, lm);

                //attempt to burn neighbouring tiles if theyre burnable
                int i = 0;
                while (i < hitcolliders.Length)
                    {
                    BurnableTile b = hitcolliders[i].transform.parent.GetComponent<BurnableTile>();
                    if (b) b.TryIgnite(item.name);
                    i++;
                    }
                }

            hasSpread = true;
            }
        }
    void TryIgnite(string srcDir) //checks that fire is coming from a valid direction before igniting the tile
        {
        foreach (var item in connections)
            {
            string connectionName = item.name;
            bool igniteSuccessful = false;
            switch (srcDir)
                {
                case "+x": if (connectionName == "-x") igniteSuccessful = true; break;
                case "-x": if (connectionName == "+x") igniteSuccessful = true; break;
                case "+z": if (connectionName == "-z") igniteSuccessful = true; break;
                case "-z": if (connectionName == "+z") igniteSuccessful = true; break;
                }
            if (igniteSuccessful) isBurning = true;
            }
        }
}
