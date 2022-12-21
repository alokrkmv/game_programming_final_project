using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System; 

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
    public int currentQuestionStage = 1;



    //initialising all the UI elements -- questionText abd Buttons as answers

    public Text question1Text;
    public Text livesText;
    public Button answer1Option1;
    public Button answer1Option2;


    public GameObject question1Block;
    public GameObject question1Block1;
    public GameObject question1Block2;

    //all stoppers
    public GameObject question1Stopper;
    public GameObject question1Stopper1;
    public GameObject question1Stopper2;

    public GameObject question2Stopper;
    public GameObject question2Stopper1;
    public GameObject question2Stopper2;

    public GameObject question3Stopper;
    public GameObject question3Stopper1;
    public GameObject question3Stopper2;

    public GameObject question4Stopper;
    public GameObject question4Stopper1;
    public GameObject question4Stopper2;

    public GameObject question5Stopper;
    public GameObject question5Stopper1;
    public GameObject question5Stopper2;

    int questionindex;

    //question index, list of stopper game objects for that crossroad
    Dictionary<int, List<GameObject>> blockMapping =  new Dictionary<int, List<GameObject>>(); 

    Dictionary<int, List<string>> questionMapping =  new Dictionary<int, List<string>>(); 

    


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

        List<GameObject> list1 = new List<GameObject>();
        list1.Add(question1Stopper);
        list1.Add(question1Stopper1);
        list1.Add(question1Stopper2);
        blockMapping.Add(1, list1);
        List<GameObject> list2 = new List<GameObject>();
        list2.Add(question2Stopper);
        list2.Add(question2Stopper1);
        list2.Add(question2Stopper2);
        blockMapping.Add(2, list2);
        List<GameObject> list3 = new List<GameObject>();
        list3.Add(question3Stopper);
        list3.Add(question3Stopper1);
        list3.Add(question3Stopper2);
        blockMapping.Add(3, list3);
        List<GameObject> list4 = new List<GameObject>();
        list4.Add(question4Stopper);
        list4.Add(question4Stopper1);
        list4.Add(question4Stopper2);
        blockMapping.Add(4, list4);
        List<GameObject> list5 = new List<GameObject>();
        list5.Add(question5Stopper);
        list5.Add(question5Stopper1);
        list5.Add(question5Stopper2);
        blockMapping.Add(5, list5);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(TrollCollision);
        // Debug.Log(transform.position);
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
            Debug.Log("collided with Q1");
            questionindex = 1;
            Debug.Log(lastCorrectPlayerPosition);
            answer1Option1.gameObject.SetActive(true);
            answer1Option2.gameObject.SetActive(true);
            question1Text.enabled = true;
        }
        else if(col.tag == "question2Block")
        {
            lastCorrectPlayerPosition = transform.position;
            Debug.Log("collided with Q2");
            questionindex = 2;
            Debug.Log(lastCorrectPlayerPosition);
            answer1Option1.GetComponentInChildren<Text>().text =  "Indian";
            answer1Option2.GetComponentInChildren<Text>().text = "Atlantic";
            question1Text.text = "Question: In what ocean is the Bermuda Triangle located?";
            answer1Option1.gameObject.SetActive(true);
            answer1Option2.gameObject.SetActive(true);
            question1Text.enabled = true;

        }
        else if(col.tag == "question3Block")
        {
            lastCorrectPlayerPosition = transform.position;
            Debug.Log("collided with Q3");
            questionindex = 3;
            Debug.Log(lastCorrectPlayerPosition);
            answer1Option1.GetComponentInChildren<Text>().text =  "Missouri";
            answer1Option2.GetComponentInChildren<Text>().text = "Hudson";
            question1Text.text = "Question: What is the name of the longest river in USA?";
            answer1Option1.gameObject.SetActive(true);
            answer1Option2.gameObject.SetActive(true);
            question1Text.enabled = true;
        }
        else if(col.tag == "question4Block")
        {
            lastCorrectPlayerPosition = transform.position;
            Debug.Log("collided with Q4");
            questionindex = 3;
            Debug.Log(lastCorrectPlayerPosition);
            answer1Option1.GetComponentInChildren<Text>().text =  "opt1";
            answer1Option2.GetComponentInChildren<Text>().text = "opt2";
            question1Text.text = "Question: 4";
            answer1Option1.gameObject.SetActive(true);
            answer1Option2.gameObject.SetActive(true);
            question1Text.enabled = true;
        }
        else if(col.tag == "question5Block")
        {
            lastCorrectPlayerPosition = transform.position;
            Debug.Log("collided with Q5");
            questionindex = 3;
            Debug.Log(lastCorrectPlayerPosition);
            answer1Option1.GetComponentInChildren<Text>().text =  "opt 1";
            answer1Option2.GetComponentInChildren<Text>().text = "opt 2";
            question1Text.text = "Question: 5";
            answer1Option1.gameObject.SetActive(true);
            answer1Option2.gameObject.SetActive(true);
            question1Text.enabled = true;
        }
        else{
            Debug.Log("Not colliding");
        }

    }

    void TaskOnClick(string e){
        
        Debug.Log("Question index"+ questionindex);
        List<GameObject> list = blockMapping[questionindex];
	
        //Question window
        //question1Block.SetActive(false);
        //question1Stopper.SetActive(false);
        GameObject questionStopper = list[0];
        questionStopper.SetActive(false);

        //UI
        question1Text.enabled = false;
        answer1Option1.gameObject.SetActive(false);
        answer1Option2.gameObject.SetActive(false);

        if(e == "answer1Option1") //opt 1
        {
            Debug.Log("answer 1 has been selected");
            //question1Stopper1.SetActive(false);
            GameObject optionStopper = list[1];
            optionStopper.SetActive(false);
        }
        else if(e == "answer1Option2") //opt 2
        {
            Debug.Log("answer 2 has been selected");
            //question1Stopper2.SetActive(false);
            GameObject optionStopper = list[2];
            optionStopper.SetActive(false);
        
        }
	}
}



