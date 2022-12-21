using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TrolControl1 : MonoBehaviour {
	
	private Animator anim;
	private CharacterController controller;
	private bool battle_state;
	public float speed = 2.0f;
	public float runSpeed = 5.0f;
	public float turnSpeed = 60.0f;
	public float gravity = 20.0f;
	private Vector3 moveDirection = Vector3.zero;
	public level2Logic script;
	Transform player;
	public Vector3 trollInitialPosition;
	
	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator>();
		controller = GetComponent<CharacterController> ();
		player = GameObject.FindGameObjectWithTag("Almond").transform;
		trollInitialPosition = transform.position;

	}
	
	// Update is called once per frame
	void Update () {

		if(script.hasCollidedWithBlock1 == true)
		{
			//make the troll move towards the almond;
			anim.SetBool("walking", true);
			anim.transform.LookAt(player);
			Vector3 dir = (script.playerPosition-transform.position);
            dir  = dir/dir.magnitude;
            dir.y=0;
            transform.position+=20*dir*speed*Time.deltaTime;
		}
		else{
			anim.SetBool("walking", false);
		}
	
		// if (Input.GetKey("2")) //battle_idle
		// {
		// 	anim.SetInteger("battle", 1);
		// 	battle_state = true;
			
		// }
		// if (Input.GetKey("1")) 			//idle
		// {
		// 	anim.SetInteger("battle", 0);
		// 	battle_state = false;
		// }
		// if (Input.GetKey ("up")) {		 //moving
		// 	if (battle_state == false)
		// 	{
		// 		anim.SetInteger ("moving", 1);//walk
		// 		runSpeed = 1.0f;
		// 	} else 
		// 	{
		// 		anim.SetInteger ("moving", 2);//run
		// 		runSpeed = 2f;
		// 	}
			
			
		// } else {
		// 	anim.SetInteger ("moving", 0);
		// }

		// if (Input.GetKeyDown("v")) //victory
		// {
		// 	anim.SetInteger("moving",6);
		// }


		// if (Input.GetKeyUp("k")) // death 1
		// {
		// 	anim.SetInteger("moving", 9);
		// } 
		// if (Input.GetKeyDown("l")) //death 2
		// {
		// 	anim.SetInteger("moving", 12);
		// }


		// if (Input.GetKeyDown("b")) //attack4
		// {
		// 	anim.SetInteger("moving", 8);
		// }
		// if (Input.GetMouseButtonDown (0)) //attack1
		// {
		// 	anim.SetInteger("moving", 3);
		// }
		// if (Input.GetMouseButtonDown (1)) //attack2
		// {
		// 	anim.SetInteger("moving", 4);
		// }
		// if (Input.GetMouseButtonDown (2)) //attack3
		// {
		// 	anim.SetInteger("moving", 5);
		// }


		// if (Input.GetKeyDown("space")) //jump
		// {
		// 	anim.SetInteger("moving", 7);
		// }


		// if (Input.GetKeyDown("i")) //hit1
		// {
		// 	anim.SetInteger("moving", 13);
		// }
		// if (Input.GetKeyDown("o")) //hit2
		// {
		// 	anim.SetInteger("moving", 14);
		// }
		// if (Input.GetKeyDown("p")) //cast
		// {
		// 	anim.SetInteger("moving", 15);
		// }

		// if (Input.GetKeyDown("u")) //defence
		// {
		// 	anim.SetInteger("moving", 16);
		// }


		// if(controller.isGrounded)
		// {
		// 	moveDirection=transform.forward * Input.GetAxis ("Vertical") * speed * runSpeed;
			
		// }
		// float turn = Input.GetAxis("Horizontal");
		// transform.Rotate(0, turn * turnSpeed * Time.deltaTime, 0);
		// controller.Move(moveDirection * Time.deltaTime);
		// moveDirection.y -= gravity * Time.deltaTime;


	}



	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.name == "AlmondH"){
		Debug.Log("entered here after collision 1");
		transform.position = trollInitialPosition;
		script.TrollCollision1 = true;
		}
		
	}
}
