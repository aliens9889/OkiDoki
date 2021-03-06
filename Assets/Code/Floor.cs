﻿using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {


	public Juice juice;

	public GameObject[] floors;

	[HideInInspector]
	public int countClicks = 0;

	private Color hoverColor, normalColor;
	private Player player;
	private Dog dog;
	private bool canClickFloor, clickOnce;
	private Door door;


	// Use this for initialization
	void Start () {

		hoverColor = new Color32(111, 159, 99, 255);
		normalColor = new Color32(255, 255, 255, 255);

		player = GameObject.Find("Oki").GetComponent<Player>();
		dog = GameObject.Find("Doki").GetComponent<Dog>();
		door = GameObject.Find("Door").GetComponent<Door>();

	}
	
	// Update is called once per frame
	void Update () {

		if(juice.lvlNumber < 1) {
			
			player.canMove = true;

		}else if(juice.lvlNumber <= 5 && player.currentState != Player.PlayerState.Sleepy) {
			
			player.canMove = true;

		} else if ( juice.lvlNumber <= 0 
			|| player.currentState == Player.PlayerState.Sleepy || player.playerBalloon.activeSelf) {

			player.canMove = false;

		}

		if(dog.currentState == Dog.DogState.Walking 
			|| dog.currentState == Dog.DogState.Finding 
			|| player.playerBalloon.activeSelf ) {

			player.currentPosition = player.transform.position;

			foreach(GameObject floor in floors) {
				floor.GetComponent<PolygonCollider2D>().enabled = false;
			}
		}

		if(dog.currentState == Dog.DogState.Eating 
			|| dog.currentState == Dog.DogState.HasBalloon 
			|| dog.currentState == Dog.DogState.HasKeys 
			|| dog.currentState == Dog.DogState.Surprise) {

			foreach(GameObject floor in floors) {
				floor.GetComponent<PolygonCollider2D>().enabled = true;
			}
		}

		if(player.playerBalloon.activeSelf) {
			player.currentPosition = player.transform.position;

			foreach(GameObject floor in floors) {
				floor.GetComponent<PolygonCollider2D>().enabled = false;
			}
		}

		if(door.spriteRender.sprite == door.doorOpened || player.gameOverPanel.activeSelf) {

			foreach(GameObject floor in floors) {
				floor.GetComponent<PolygonCollider2D>().enabled = false;
			}
		}

		if(player.currentPosition == transform.position || player.currentPosition == player.transform.position) {
			canClickFloor = true;

		} else if(player.currentPosition != transform.position 
			|| player.currentPosition != player.transform.position || dog.currentState == Dog.DogState.Finding) {

			canClickFloor = false;
		}

	}

	void MovePlayer(){

	}



	void OnMouseOver(){
		this.GetComponent<SpriteRenderer>().color = hoverColor;

		if(canClickFloor) {

			if(Input.GetMouseButtonDown(0) && player.canMove && player.transform.position.x == transform.position.x) {
				juice.lvlNumber = juice.currentLvl;

			} else if(Input.GetMouseButtonDown(0) 
				&& player.canMove 
				&& player.transform.position.x != transform.position.x 
				&& player.currentState != Player.PlayerState.Sleepy) {

				countClicks++;

				juice.lvlNumber--;

//				player.GetComponent<Animator>().SetTrigger("Walk Trigger");

				if (juice.lvlNumber < 1)
					StartCoroutine(player.GameOver());

				if(transform.position.x < player.transform.position.x) {
					player.direction = -1;

				}else if (transform.position.x >= player.transform.position.x){
					player.direction = 1;
				}

				if(player.isMoving) {
					player.actions = player.currentActions;
					juice.lvlNumber = juice.currentLvl;
				} else {
					player.actions++;
				}
					
				player.currentPosition = transform.position;

				if(dog.currentState == Dog.DogState.Eating 
					|| dog.currentState == Dog.DogState.HasBalloon 
					|| dog.currentState == Dog.DogState.WatchTV || dog.currentState == Dog.DogState.HasToy) {

					dog.dogCount++;

				}


			} else if (Input.GetMouseButtonDown(0) 
				&& !player.canMove && player.currentState == Player.PlayerState.Sleepy) {

				player.currentPosition = player.transform.position;

			}
				
		} else {
			return;
		}
	}

	void OnMouseExit(){
		this.GetComponent<SpriteRenderer>().color = normalColor;
	}
}
