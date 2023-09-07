using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GridGenerator terr;
    public bool selectedEnemy,Flower,FlowerAttack;
    public GameObject attackedEnemy,ActiveMob;
    public int currentX,currentY,LeadCount=8,ActiveLead,JPGLead;
    public Text Name,Count,Player,DamageCounter,SpecialText;
    public GameObject[] mobJPG = new GameObject[4];
    public GameObject[] FlowerAttacked = new GameObject[20];
    public String[] mobNames = {"Knight","Archer","Skeleton","Zombie"};
    Mob ActiveMobScr;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("UpdateInfo",0.1f);
        //UpdateInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeLead(){
        ActiveMobScr = ActiveMob.GetComponent<Mob>();
         if(ActiveMobScr.reverse){ActiveMobScr.Player=1;ActiveMobScr.reverse=false;}
        ActiveMobScr.Active = false;
        ActiveMobScr.Attack = false;
        SpecialText.text="";
        DrawCircle(ActiveMobScr.posX,ActiveMobScr.posY,ActiveMobScr.MoveDist,Color.white);
        mobJPG[JPGLead].SetActive(false);
        if(ActiveLead<LeadCount)
        {ActiveLead+=1;}else{ActiveLead=0;}
        Invoke("UpdateInfo",0.1f);
    }
    public void UpdateInfo(){
        ActiveMobScr = ActiveMob.GetComponent<Mob>();
        if(ActiveMobScr.AllHP<=0){ChangeLead();}else{
        ActiveMobScr.Active = true;
        Name.text = ActiveMobScr.Name;
        Player.text = "Player "+ ActiveMobScr.Player;
        DamageCounter.text = "Damage "+ActiveMobScr.minDamage+"-"+ActiveMobScr.maxDamage+" hp";
        if(ActiveMobScr.Player == 2){ Player.color =  new Color(1.0f, 0.64f, 0.0f);}
        else{Player.color =  new Color( 0.764f , 0f , 1f );}//new Color(1.0f, 0.64f, 0.0f);}
       for(int i=0;i<4;i++){
        if(Name.text == mobNames[i]){
          JPGLead = i;
        } }
        Count.text = ""+ActiveMobScr.Count;
        mobJPG[JPGLead].SetActive(true);
         DrawCircle(ActiveMobScr.posX,ActiveMobScr.posY,ActiveMobScr.MoveDist,Color.green);
    }}

    public void Attack(){
         ActiveMobScr = ActiveMob.GetComponent<Mob>();
        ActiveMobScr.Attack = true;
    }
    public void SpecialVoid(){
        ActiveMobScr = ActiveMob.GetComponent<Mob>();
       ActiveMobScr.SpecialVoid();
       // ActiveMobScr.Attack = true;
    }
    public void DrawFlower(int x,int y,Color color){
       for(int j=y-1;j<=y+1;j++){
       for(int i=x-2;i<x+2;i++){
        if(0<=i && i < 15 && 0<=j &&j<9){
            if(terr.grids[i,j].GetComponent<Tile>().rend.color != Color.green)
            terr.grids[i,j].GetComponent<Tile>().rend.color = color;
         }
        }
       }
    }
    public void DrawCircle(int x, int y, float r, Color color)//int color)
{
      const double PI = 3.1415926535;
      double i, angle, x1, y1;
     for(float j = 1.0f; r > 1.0f; r-=j){
       
      for(i = 0; i < 360; i += 0.1)
      {
            angle = i;
            x1 = r * Math.Cos(angle * PI / 180);
            y1 = r * Math.Sin(angle * PI / 180);
           if(0<=(x+(int)x1) && (x+(int)x1) < 15 && 0<=(y+(int)y1) &&(y+(int)y1)<9){
            terr.grids[x+(int)x1,y+(int)y1].GetComponent<Tile>().rend.color = color;
            
            }
           
      }
     }
     
}
}
