﻿using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public Sprite doorClosed, doorOpened;
	public GameObject doorShine, winnerPanel; 
	public bool canOpenIt;
	public AudioSource cannotSound, openDoorSound;

	[HideInInspector]
	public SpriteRenderer spriteRender;

	private Player player;
	private float sec = 3f;

	// Use this for initialization
	void Start () {

		player = GameObject.Find("Oki").GetComponent<Player>();

		spriteRender = GetComponent<SpriteRenderer>();

		if(spriteRender == null)
			spriteRender.sprite = doorClosed;
	}
	
	// Update is called once per frame
	void Update () {

		if(player.transform.position.x == -6.09f && player.playerKeys.activeSelf) {
			canOpenIt = true;
		} else {
			canOpenIt = false;
		}
	}


	void ChangeSprite() {

		if(spriteRender.sprite == doorClosed) {
			
			spriteRender.sprite = doorOpened;
			StartCoroutine(Winner());
		}
			
	
	}

	void OnMouseExit() {
		doorShine.SetActive(false);
	}

	void OnMouseOver() {

		if(spriteRender.sprite == doorClosed)
			doorShine.SetActive(true);

		if(Input.GetMouseButtonDown(0) && !canOpenIt) {

			cannotSound.Play();
			
		} else if(Input.GetMouseButtonDown(0) && canOpenIt) {
			
			openDoorSound.Play();

			ChangeSprite();
			player.actions++;

		}
	}

	IEnumerator Winner() {

		yield return new WaitForSeconds(sec);

		winnerPanel.SetActive(true);

	}
}
