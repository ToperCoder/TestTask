using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int x,y;
    public GameObject sprite;
    public GameObject[,] grids;
    public float xr=0.85f,yr=0.75f,sr=-0.21f;
    // Start is called before the first frame update
    void Start()
    {
        grids = new GameObject[x,y];
        for(int i =0;i<y;i++){
         //yr*=i+1;
         for(int j =0;j<x;j++){
          
         grids[j,i] = (GameObject)Instantiate(sprite, new Vector2(sr+transform.position.x+xr*(j+1),transform.position.y+yr*(i+1)), sprite.transform.rotation)  ;
           grids[j,i].GetComponent<Tile>().x=j;grids[j,i].GetComponent<Tile>().y=i;
         }
        sr *= -1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
