using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class level2Logic : MonoBehaviour
{
    private Animator animation_controller;
    private CharacterController character_controller;
    public Vector3 movement_direction;
    public float velocity;
    public bool hasCollidedWithBlock = false;
    public Vector3 playerPosition;
    public int numberOfLives;
    public Vector3 lastCorrectPlayerPosition;
    public bool TrollCollision = false;
    public bool playerLost = false;



    //initialising all the UI elements -- questionText abd Buttons as answers

    public Text question1Text;
    public Text livesText;
    public Button answer1Option1;
    public Button answer1Option2;
    public GameObject question1Block;
    public GameObject question1Block1;
    public GameObject question1Block2;
    public GameObject question1Stopper;
    public GameObject question1Stopper1;
    public GameObject question1Stopper2;



    // Start is called before the first frame update
    void Start()
    {
        numberOfLives = 5;
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        answer1Option1.gameObject.SetActive(false);
        answer1Option2.gameObject.SetActive(false);
        question1Text.enabled = false;
        answer1Option1.onClick.AddListener(()=> TaskOnClick(answer1Option1.gameObject.tag));
        answer1Option2.onClick.AddListener(()=> TaskOnClick(answer1Option2.gameObject.tag));
    }

    // Update is called once per frame
    void Update()
    {
        livesText.enabled = true;
        livesText.text = "Lives Left: " + numberOfLives;

        //condition if player 2 is dead
        //condition if player has won
        animation_controller.SetBool("NoKey",true);
        if(Input.GetKey(KeyCode.UpArrow))
        {
            if(Input.GetKey(KeyCode.LeftShift))
            {
                animation_controller.SetBool("running",true);
                animation_controller.SetBool("walking",false);
                animation_controller.SetBool("NoKey",false);

                if(velocity<0.0f){
                    velocity=0.0f;
                }
                velocity+=1f;

            }

            else
            {
                // Debug.Log("Yahan pe change hua?");
                animation_controller.SetBool("walking",true);
                animation_controller.SetBool("running",false);
                animation_controller.SetBool("NoKey",false);

                if(velocity<0.0f){
                        velocity=0.0f;
                    }
                velocity+=1f;
                }

            }

        else
        {
        velocity=0.0f;
        animation_controller.SetBool("walking",false);
        animation_controller.SetBool("running",false);
        animation_controller.SetBool("NoKey",true);

        }

        if(Input.GetKey(KeyCode.LeftArrow)){
            transform.Rotate(new Vector3(0.0f,-1f,0.0f));
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Rotate(new Vector3(0.0f,1f,0.0f));
        }

        float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        movement_direction = new Vector3(xdirection, 0.0f, zdirection);

        if (transform.position.y > 50f) // if the character starts "climbing" the terrain, drop her down
        {
            Vector3 lower_character = movement_direction * velocity * Time.deltaTime;
            lower_character.y = -100f; // hack to force her down
            character_controller.Move(lower_character);
        }


        if(TrollCollision == true)
        {
            Debug.Log("we have successfully come here now");
            numberOfLives = numberOfLives - 1;
            if(numberOfLives <= 0)
            {
                playerLost = true;
            }
            character_controller.enabled = false;
            transform.position = lastCorrectPlayerPosition;
            character_controller.enabled = true;
            Debug.Log("resets to");
            Debug.Log(transform.position);
            //activate the blocks again
            question1Block.SetActive(true);
            question1Stopper.SetActive(true);
            question1Stopper2.SetActive(true);
            question1Block2.SetActive(true);
            TrollCollision = false;
            hasCollidedWithBlock = false;
        }

        character_controller.Move(movement_direction * velocity * Time.deltaTime);
        playerPosition = transform.position;
    }

    // void OnTriggerEnter(Collision collide )
    // {
    //         if(collide.gameObject)
    //         Debug.Log("COLLIDED HERE");
    //         answer1Option1.SetActive(true);
    //         answer1Option2.SetActive(true);
    //         question1Text.enabled = true;  
    // }

    void OnTriggerEnter(Collider col)
    {
        // new Vector3 stoppingPositionOfPlayer  = transform.position;

        if(col.tag == "question1Block")
        {
            lastCorrectPlayerPosition = transform.position;

            Debug.Log("collided");
            Debug.Log(lastCorrectPlayerPosition);
            answer1Option1.gameObject.SetActive(true);
            answer1Option2.gameObject.SetActive(true);
            question1Text.enabled = true;
        }

        if(col.tag == "question1Block2")
        {
            //enemy to start attacking;
            Debug.Log("here we are");
            hasCollidedWithBlock = true;
        }
        
        // if(answer1Option1.is_clicked)
        // {
        //     Debug.Log("Answer 1 clicked");
        // }
        // else if(answer1Option2.is_clicked)
        // {
        //     Debug.Log("Answer 2 clicked");
        // }
        //write code here now!!!!!

        //if button pressed answer 1
        // {
        //     display on screen -> correct direction
        //     mesh collider block deactivate
        // }
        //tests
        //else {
        //     display wrong direction
        //     check if collision happens
        //     activate the npc to move towards the player and the player looses a life
        //     reset the life and position.
        // }

    }

    void TaskOnClick(string e){
        
		Debug.Log ("You have clicked the button!");
        Debug.Log(e);

        question1Block.SetActive(false);
        question1Stopper.SetActive(false);
        question1Text.enabled = false;
        answer1Option1.gameObject.SetActive(false);
        answer1Option2.gameObject.SetActive(false);
        // question1Stopper.gameObject.SetActive(false);


        if(e == "answer1Option1")
        {
            Debug.Log("answer 1 has been selected");
            question1Stopper1.SetActive(false);
            //Block the wrong path and let it go towards the right path.
        }
        else if(e == "answer1Option2")
        {
            Debug.Log("answer 2 has been selected");
            question1Stopper2.SetActive(false);
            //attack of npc
            //take away life
            //reset player position
        }
        //make this.UI disable
        //make this.stopper disable

        //if option 1 then let it pass through stopper 1 (disable)
        //else let it pass through stopper 2 (enable)
	}

    // void OnCollisionEnter(Collision col)
    // {
    //     if(col.gameObject.name == "q1")
    //     {
    //         Debug.Log("Collided");
    //     }
    // }
}
// public Button yourButton;
// void Start () {
// 		Button btn = yourButton.GetComponent<Button>();
// 		btn.onClick.AddListener(TaskOnClick);
// 	}



