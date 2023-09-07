using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public SpriteRenderer rend;
    bool selected;
    public GameObject plate;
    public int x,y;
     GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
   void Update(){
      if(Input.GetMouseButtonDown(1) && selected && rend.color==Color.green)
       {
        Debug.Log("Clicked x:"+x+" y:"+y);
        Mob ActiveMobScr =  manager.ActiveMob.GetComponent<Mob>();
        manager.DrawCircle(ActiveMobScr.posX,ActiveMobScr.posY,ActiveMobScr.MoveDist,Color.white);
        ActiveMobScr.posX=x;
        ActiveMobScr.posY=y;
       // rend.color =  Color.red;
       //  manager.DrawCircle(x,y,2.0f,2);
       }
   }
    void OnMouseEnter(){
        selected = true;
        plate.SetActive(true);
        manager.currentX=x;
        manager.currentY=y;
        if(manager.Flower){
            manager.DrawFlower(x,y,Color.cyan);
            //rend.color = Color.cyan;
        }
        //rend.color =  Color.blue;
    }
    
    void OnMouseExit(){
       //rend.color =  Color.white;
       selected= false;
       plate.SetActive(false);
       if(manager.Flower){
        manager.DrawFlower(x,y,Color.white);
           // rend.color = Color.white;
        }
    }
}
