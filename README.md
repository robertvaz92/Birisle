# Birisle

GameManager.cs 
	1. Will handle most of the logic of managing the User flow
	2. Save and Loads the Game data
	3. Handles Audio preferences save
	

UI Screens
1. Splash Screen: a dummy splash screen, will be a first screen when game loads

2. Main Menu Screen: 
	1. Handles the User input for selecting the grid and Turning on/off of the audio. 
	2. Also handles Resume game or Start a new game
	
3. Game Play Screen: 
	1. Handles Game play initialisation and Game over screens. 
	2. Auto saves the game on manual close if there is a progress in the curremt game.
	3. Displays all the stats like Score, Moves etc..
	4. Manages the card and grid logic manager

Card Grid Manager
	1. Responsible to Create the grid and populate the cards and scale the cards accrodingly
	2. Hnadles the Odd cell by moving one of the card from the list to stale state for making all the other cards to pair up
	3. Manages the Score Manager

Card
	1. Its a entity representing a card
	2. Has states like 
		a. Close : For Hiding the card from user view, 
		b. Open : Showing the cards and matching logic execution
		c. Stale : Non interactable, when matched or for odd card, so that it can be moved out from the game play
	3. Card Updater: its a helper class which will be managing the card states and events trigger and other card releated updations
	4. The matching logic will be handled inside the card so that user is not blocked to interact with any other cards