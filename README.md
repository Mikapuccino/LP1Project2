# Tragic The Reckoning

## Authors

Miguel Feliciano - 22002782

- Implemented all of the View and IView:
- Display of the menu
- Added action to ask for action
- Added display of the action
- Added display of the fight
- Added display of the result of the fight
- Added display of HP after each fight
- Added display of the winner
- Added comments
- Added all methods to the interface
- Updated portions of the Controller for the View to work correctly

Nelson Milheiro - 21904365

- Implemented Controller, Card, Model, Player and Program:
- Every method in Model
- Every method in Controller
- Program Main method
- Card class constructor
- Player class constructor

---

## Solution

After displaying the instructions and rules of the game, the game starts with
player one being the first to play, starting the spell phase. It displays the current player's turn hand and asks the player to choose a card to play, unless they can't play any card due to their costs being higher then their current
mana. Every card player goes to the respective player's field.

After the players play their chosen cards and can't play anymore or decide to
pass their turn, then the attack phase begins. Going by the order they were
played, each card from each player fights the cards of their opponent until at
least one field is empty.

After this, we check which field is empty, or if both are, and the cards deal
damage to the opposing player based on their attack power, or no player takes
damage if both field are empty.

This all repeats until a player has 0 health or they give up, declaring the
other player the winner and ending the program.

The Model class is responsible for setting up the start of the game, creating
the players with their respective decks and hands, decks being stacks and the
hands being lists, using methods to create the decks and shuffling them
properly.

The Controller class is responsible for the game logic itself, with each phase
of the game being a method that deals with everything that phase necessitates.
It also uses methods to set up the MP for each player based on the turn to
draw more cards and to check if the game has reached an end.

The View class is used to properly display all the information a player needs
to play the game during each phase, showing their hand, showing if cards have
been successfully played or not, and giving the reason why, and showing the
fights during the attack phase so that every player knows exactly what happened.

---

## UML Diagram

![Alt text](UML.png)