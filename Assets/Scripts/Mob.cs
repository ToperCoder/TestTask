using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mob : MonoBehaviour
{
    public string Name;
    public float reviveStage;
    public int spawnX, Player,AllHP,HP, MoveDist, lead, minDamage,maxDamage, Count,posX,posY;
    public float blockDamage = 0;
    GameManager manager;
    GridGenerator generator;
     Transform GridPos;
     public bool reverse,revive,Active,Special,Attack,selected;
     public Text textCount,damageText;
     int p=5;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        generator = GameObject.Find("GridGenerator").GetComponent<GridGenerator>();
       AllHP= Count*HP;
       textCount.text = ""+Count;
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.ActiveLead == lead){
            manager.ActiveMob = gameObject;
        }
        if(manager.Flower && GridPos.GetComponent<Tile>().rend.color == Color.cyan && manager.FlowerAttack){ //&& Input.GetMouseButtonDown(0)){
          manager.FlowerAttacked[lead]= gameObject;
        }
        GridPos = generator.grids[posX,posY].transform;
        transform.position = new Vector3(GridPos.position.x,GridPos.position.y,transform.position.z);
       if(Attack){
        if(Input.GetMouseButtonDown(0) && manager.selectedEnemy)
       {
        //Debug.Log("Clicked x:"+x+" y:"+y);
      //  GridPos.GetComponent<Tile>().rend.color =  Color.red;
        if(manager.attackedEnemy.GetComponent<Mob>().GridPos.GetComponent<Tile>().rend.color == Color.green)
        {AttackVoid();}
         //manager.DrawCircle(x,y,2.0f,2);
       }
       }
       if(Special && Name == "Archer"&&manager.Flower){
         if(Input.GetMouseButtonDown(0)){
            manager.FlowerAttack=true;
        Invoke("FlowerAttack",0.1f);
       }}
    }
    void FlowerAttack(){
for(int i=0;i<=manager.LeadCount;i++){
            if(manager.FlowerAttacked[i]!=null && manager.FlowerAttacked[i].GetComponent<Mob>().Player!=Player)
            manager.FlowerAttacked[i].GetComponent<Mob>().Damage(UnityEngine.Random.Range(minDamage*Count/2,maxDamage*Count/2));
        }
        manager.Flower=false;
        manager.DrawFlower(manager.currentX,manager.currentY,Color.white);
        Special=false;
        manager.FlowerAttack=false;
        for(int i=0;i<=manager.LeadCount;i++){manager.FlowerAttacked[i]=null;}
         manager.ChangeLead();   
    }
    public void Damage(int Damage){
        AllHP-= (int)Mathf.Ceil(Damage*(1-blockDamage));
        damageText.text = "-"+(int)Mathf.Ceil(Damage*(1-blockDamage));
        if(Name == "Knight"&& blockDamage>0){ blockDamage-=0.33f; }
        if(AllHP>=0){
            if(Name == "Skeleton" && revive && reviveStage>=0.1f){
                reviveStage-=0.3f;
                GameObject skeletik = (GameObject)Instantiate(gameObject);
                manager.LeadCount++;
                Mob SkeletScr = skeletik.GetComponent<Mob>();
                SkeletScr.revive= false;
                SkeletScr.Count = Count -(int)Mathf.Ceil(AllHP/HP);
                SkeletScr.lead = manager.LeadCount;
                SkeletScr.HP=(int)(SkeletScr.HP*reviveStage);
                SkeletScr.minDamage=(int)(SkeletScr.minDamage*reviveStage);
                SkeletScr.maxDamage=(int)(SkeletScr.maxDamage*reviveStage);
                SkeletScr.posX=spawnX; SkeletScr.posY=p;p--;
            }
        Count = (int)Mathf.Ceil((float)AllHP/HP);
         textCount.text = ""+Count;
         if(Damage==0){
            Player=2;
            reverse=true;
            manager.ActiveLead= lead-1;
            manager.ChangeLead();
         }}
         else{Count =0;textCount.text = ""+Count;}
    }
     public void AttackVoid(){
        Debug.Log("lol");
        manager.attackedEnemy.GetComponent<Mob>().Damage(UnityEngine.Random.Range(minDamage*Count,maxDamage*Count));
      if(reverse){Player=1;reverse=false;}
       if(Name!="Zombie") manager.ChangeLead();    
    }
   public void SpecialVoid(){
    if(Name == "Knight"){
    if(blockDamage==0){
    blockDamage+=0.99f;}
    //else{blockDamage-=0.33f;}
    manager.SpecialText.text = "Blocked "+blockDamage*100+"%";
    }
    else if(Name == "Zombie"){

    }
    else if(Name == "Archer"){
      manager.Flower = true;
      Special=true;
       manager.SpecialText.text = "Flower Shot";
    }
   }
    void OnMouseEnter(){
        selected = true;
        if(!Active && manager.ActiveMob.GetComponent<Mob>().Player != Player){
            manager.selectedEnemy = true;
            manager.attackedEnemy = gameObject;
        }
        //plate.SetActive(true);
        //rend.color =  Color.blue;
    }
    
    void OnMouseExit(){
       //rend.color =  Color.white;
       selected= false;
       if(!Active){
            manager.selectedEnemy = false;
        }
      // plate.SetActive(false);
    }
}
