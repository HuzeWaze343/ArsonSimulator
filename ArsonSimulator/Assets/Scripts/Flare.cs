using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flare : MonoBehaviour
    {
    [SerializeField]
    float speed = 5f;

    [SerializeField]
    ParticleSystem particleSystem;
    Rigidbody rb;


    //FLARE FUNCTIONALITY
    //if the flare flies over a tile that has petrol on it, light the tile on fire
    //when the flare hits an object, cease all movement and ignite the hit object if it is ignitable

    void Start()
        {
        rb = GetComponent<Rigidbody>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        rb.AddForce(transform.forward * speed, ForceMode.Force);
        }
    void Update()
        {
        RaycastHit hit;
        if(Physics.Raycast(rb.position, -rb.transform.up, out hit, LayerMask.GetMask("Tiles")))
            {
            BurnableTile b = hit.collider.transform.GetComponentInParent<BurnableTile>();
            if (b != null && b.hasPetrol == true) b.isBurning = true;
            }
        }
    private void OnCollisionEnter(Collision collision)
        {
        RaycastHit hit;
        if (Physics.Raycast(rb.position, -rb.transform.up, out hit, LayerMask.GetMask("Tiles")))
            {
            BurnableTile b = hit.collider.transform.GetComponentInParent<BurnableTile>();
            if (b != null) b.isBurning = true;
            }

        particleSystem.gameObject.SetActive(false);
        Destroy(rb);
        Destroy(this);
        }
    }
