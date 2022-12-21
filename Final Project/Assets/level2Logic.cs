using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System; 
using Random=UnityEngine.Random;

public class level2Logic : MonoBehaviour
{
    private Animator animation_controller;
    private CharacterController character_controller;
    public Vector3 movement_direction;
    public float velocity;
    public bool hasCollidedWithBlock1 = false;
    public bool hasCollidedWithBlock2 = false;
    public bool hasCollidedWithBlock3 = false;
    public bool hasCollidedWithBlock4 = false;
    public bool hasCollidedWithBlock5 = false;
    public Vector3 playerPosition;
    public int numberOfLives;
    public Vector3 lastCorrectPlayerPosition;
    public bool TrollCollision1 = false;
    public bool TrollCollision2 = false;
    public bool TrollCollision3 = false;
    public bool TrollCollision4 = false;
    public bool TrollCollision5 = false;
    public bool playerLost = false;

    // Audio elements
    public AudioSource audioSource_UI;
    public AudioSource audioSource_Button;
    public AudioSource audioSource_Win;
    public AudioSource audioSource_Ouch;
    //public AudioClip UIClick;
    // public AudioClip buttonClick;
    // public AudioClip win;
    // public AudioClip ouch;


    //initialising all the UI elements -- questionText abd Buttons as answers

    public Text question1Text;
    public Text livesText;
    public Button answer1Option1;
    public Button answer1Option2;
    public Text correctAnswerText;
    public Text wrongAnswerText;


    public GameObject question1Block2;

    
    //all blockers
    public GameObject question1Blocker;
    public GameObject question2Blocker;
    public GameObject question3Blocker;
    public GameObject question4Blocker;
    public GameObject question5Blocker;
    
    
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
    int RandomPick;
    bool won;

  

    //question index, list of stopper game objects for that crossroad
    Dictionary<int, List<GameObject>> blockMapping =  new Dictionary<int, List<GameObject>>(); 

    // Dictionary<int, List<string>> questionMapping =  new Dictionary<int, List<string>>(); 

    Dictionary<int , List<string>> randomQuestions = new Dictionary<int, List<string>>();
    List<int> questionsSelected = new List<int>();
    


    // Start is called before the first frame update
    void Start()
    {
        //adding questions to dictionary
        List<string> ques1= new List<string>(){"Which is the largest country in the world","Russia","Japan"};
        List<string> ques2= new List<string>(){"Which is the hottest continent on Earth","Africa","Asia"};
        List<string> ques3= new List<string>(){"In which American city is the Golden Gate Bridge located","San Francisco","New York"};
        List<string> ques4= new List<string>(){"What is the capital city of Spain","Madrid","Barcelona"};
        List<string> ques5= new List<string>(){"Ceylon is the former name of which country","Sri Lanka","India"};
        List<string> ques6= new List<string>(){"What is the official language of Brazil","Portuguese","English"};
        List<string> ques7= new List<string>(){"What is the capital of Thailand","Bangkok","Dhaka"};
        List<string> ques8= new List<string>(){"Which European country shares its border with the most neighbors","Germany","France"};
        List<string> ques9= new List<string>(){"Which country faces the threat of drowning due to global warming","Maldives","Japan"};
        List<string> ques10= new List<string>(){"Which city is also known as The Eternal City","Rome","Lyon"};
        randomQuestions.Add(1, ques1);
        randomQuestions.Add(2, ques2);
        randomQuestions.Add(3, ques3);
        randomQuestions.Add(4, ques4);
        randomQuestions.Add(5, ques5);
        randomQuestions.Add(6, ques6);
        randomQuestions.Add(7, ques7 );
        randomQuestions.Add(8, ques8);
        randomQuestions.Add(9, ques9);
        randomQuestions.Add(10, ques10);


        correctAnswerText.enabled = false;
        wrongAnswerText.enabled = false;
        // TrollCollision1 = false;
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
        
        // Debug.Log(hasCollidedWithBlock2);
        // Debug.Log(transform.position);
        livesText.enabled = true;
        livesText.text = "Lives Left: " + numberOfLives;

        //condition if player 2 is dead
        //condition if player has won
        animation_controller.SetBool("NoKey",true);

        if(won == true)
        {
            audioSource_Win.Play();
            correctAnswerText.text = "Congratulations!!! You successfully finished Level 2";
            correctAnswerText.enabled = true;
        }
        if(playerLost == true)
        {
            wrongAnswerText.text = "SORRY YOU LOST!!";
            wrongAnswerText.enabled = true;
            animation_controller.SetTrigger("dead");
        }


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
            transform.Rotate(new Vector3(0.0f,-3f,0.0f));
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            transform.Rotate(new Vector3(0.0f,3f,0.0f));
        }

        float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        movement_direction = new Vector3(2*xdirection, 0.0f, 2*zdirection);

        if (transform.position.y > 50f) // if the character starts "climbing" the terrain, drop her down
        {
            Vector3 lower_character = movement_direction * velocity * Time.deltaTime;
            lower_character.y = -100f; // hack to force her down
            character_controller.Move(lower_character);
        }


        if(TrollCollision1 == true)
        {
            audioSource_Ouch.Play();
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
            TrollCollision1 = false;
            hasCollidedWithBlock1 = false;
            question1Stopper.SetActive(true);
            question1Stopper2.SetActive(true);
            question1Blocker.SetActive(true);
        }
        
        else if(TrollCollision2 == true)
        {
            audioSource_Ouch.Play();
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
            TrollCollision2 = false;
            hasCollidedWithBlock2 = false;
            question2Stopper.SetActive(true);
            question2Stopper2.SetActive(true);
            question2Blocker.SetActive(true);
        }

        else if(TrollCollision3 == true)
        {
            audioSource_Ouch.Play();
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
            TrollCollision3 = false;
            hasCollidedWithBlock3 = false;
            question3Stopper.SetActive(true);
            question3Stopper2.SetActive(true);
            question3Blocker.SetActive(true);
        }

        else if(TrollCollision4 == true)
        {
            audioSource_Ouch.Play();
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
            TrollCollision4 = false;
            hasCollidedWithBlock4 = false;
            question4Stopper.SetActive(true);
            question4Stopper2.SetActive(true);
            question4Blocker.SetActive(true);
        }

        else if(TrollCollision5 == true)
        {
            audioSource_Ouch.Play();
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
            TrollCollision5 = false;
            hasCollidedWithBlock5 = false;
            question5Stopper.SetActive(true);
            question5Stopper2.SetActive(true);
            question5Blocker.SetActive(true);
        }
        


            // else if(questionindex == 2)
            // {
            // question2Stopper.SetActive(true);
            // question2Stopper2.SetActive(true);
            // }
            // if(questionindex == 3)
            // {
            // question3Stopper.SetActive(true);
            // question3Stopper2.SetActive(true);
            // }
            // if(questionindex == 4)
            // {
            // question4Stopper.SetActive(true);
            // question4Stopper2.SetActive(true);
            // }
            // if(questionindex == 5)
            // {
            // question5Stopper.SetActive(true);
            // question5Stopper2.SetActive(true);
            // }
            

            
        // }

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
        audioSource_UI.Play();

        if(col.tag == "question1Block"||col.tag == "question2Block" ||col.tag =="question3Block"||col.tag =="question4Block"||col.tag =="question5Block")
        {
            wrongAnswerText.enabled = false;
            correctAnswerText.enabled = false;
            lastCorrectPlayerPosition = transform.position;
            Debug.Log("collided with");

            if(col.tag== "question1Block")
            questionindex = 1;
            if(col.tag== "question2Block")
            questionindex = 2;
            if(col.tag== "question3Block")
            questionindex = 3;
            if(col.tag== "question4Block")
            questionindex = 4;
            if(col.tag== "question5Block")
            questionindex = 5;

            Debug.Log(questionindex);
            Debug.Log(lastCorrectPlayerPosition);

            for(int i = 0; i<20 ;i++ )
            {
            RandomPick = Random.Range(1,11);
            if(!questionsSelected.Contains(RandomPick))
            {
                questionsSelected.Add(RandomPick);
                break;
            }
            else
            {
                continue;
            }
            }
            int ansIndex1 = RandomPick%2 + 1;
            int ansIndex2;
            if(ansIndex1==1)
            ansIndex2 = 2;
            else
            ansIndex2 = 1;

            answer1Option1.GetComponentInChildren<Text>().text =  randomQuestions[RandomPick][ansIndex1];
            answer1Option2.GetComponentInChildren<Text>().text = randomQuestions[RandomPick][ansIndex2];
            question1Text.text = randomQuestions[RandomPick][0];
            answer1Option1.gameObject.SetActive(true);
            answer1Option2.gameObject.SetActive(true);
            question1Text.enabled = true;
        }

        else if(col.tag == "blocker1")
        {
            hasCollidedWithBlock1 = true;
        }
        else if(col.tag == "blocker2")
        {
            hasCollidedWithBlock2 = true;
        }
        else if(col.tag == "blocker3")
        {
            hasCollidedWithBlock3 = true;
        }
        else if(col.tag == "blocker4")
        {
            hasCollidedWithBlock4 = true;
        }
        else if(col.tag == "blocker5")
        {
            hasCollidedWithBlock5 = true;
        }
        
        else if(col.tag == "winning")
        {
            won = true;
        }

        else{
            Debug.Log("Not colliding");
        }


    }

    void TaskOnClick(string e){

        audioSource_Button.Play();

        if (questionindex == 1){
            question1Blocker.SetActive(false);
        }

        if (questionindex == 2){
            question2Blocker.SetActive(false);
        }

        if (questionindex == 3){
            question3Blocker.SetActive(false);
        }

        if (questionindex == 4){
            question4Blocker.SetActive(false);
        }

        if (questionindex == 5){
            question5Blocker.SetActive(false);
        }

        var correctAnswers = new List<string>()
                {
                    "Russia",
                    "Africa",
                    "San Francisco",
                    "Madrid",
                    "Sri Lanka",
                    "Portuguese",
                    "Bangkok",
                    "Germany",
                    "Maldives",
                    "Rome"         
                };
        
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


        string answerSelectedText;

        if(e == "answer1Option1") //opt 1
        {
            answerSelectedText = answer1Option1.GetComponentInChildren<Text>().text;
        }
        else{
            answerSelectedText = answer1Option2.GetComponentInChildren<Text>().text;
        }


        
        if(correctAnswers.Contains(answerSelectedText))
        {
            wrongAnswerText.enabled = false;
            correctAnswerText.enabled = true;
            GameObject optionStopper = list[1];
            optionStopper.SetActive(false);
        }
        else
        {
            correctAnswerText.enabled = false;
            wrongAnswerText.enabled = true;
            GameObject optionStopper = list[2];
            optionStopper.SetActive(false);
        }
            // Debug.Log("answer 1 has been selected");
            // //question1Stopper1.SetActive(false);
            // GameObject optionStopper = list[1];
            // optionStopper.SetActive(false);

        // }
        // else if(e == "answer1Option2") //opt 2
        // {
        //     //get answer text here;
        //     if(correctAnswers.Contains(answer1Option2.GetComponentInChildren<Text>().text))
        //     {
        //         correctAnswerText.enabled = true;
        //         GameObject optionStopper = list[1];
        //         optionStopper.SetActive(false);
        //     }
        //     else
        //     {
        //         wrongAnswerText.enabled = true;
        //         GameObject optionStopper = list[2];
        //         optionStopper.SetActive(false);
        //     }
        // }

        //implement disabling the text in sometime.
	}
}



