// This is the main DLL file.

#include "stdafx.h"

#include "ClassLibraryMancala.h"

using namespace std;
using namespace ClassLibraryMancala;


/**
* creates a board with starting parameters according to the game mode
* default gameMode is 0 for the modern, North American 4 stone version
*/
Board::Board(int gameMode, int cupsPerRow) {
	this->whoseTurn = 0;//for player 1 goes 1st

	this->numOfCupsPerRow = cupsPerRow;
	P1Score = 0;
	P2Score = 0;
	this->gameMode = gameMode;
	gameOver = false;

	//allCups = new CupPointer[DEFAULTBOARDHEIGHT][numOfCupsPerRow + 1]; 
	this->allCups = new Cup**[DEFAULT_BOARD_HEIGHT];

	//initializes all the cups
	for (int row = 0; row < DEFAULT_BOARD_HEIGHT; row++) {
		allCups[row] = new Cup*[numOfCupsPerRow + 1];

		for (int column = 0; column < numOfCupsPerRow + 1; column++) 
		{
			if (!isCorner(row, column))
				allCups[row][column] = new Cup(DEFAULT_NUM_OF_STARTING_STONES, row, column);
			else
				allCups[row][column] = new Cup(0, row, column); 
		}//col iterations
	}//row iterations
}

// destructor, removes cup array from the heap
/*Board::~Board() {
	for (int i = DEFAULT_BOARD_HEIGHT - 1; i >= 0; i--) {
		delete[] allCups[i];
		allCups[i] = NULL;
	}
	delete[] allCups;
	allCups = NULL;
}*/

/**
Given the coordinates of a cup's row and column in a 0 based 2D array
checks if it belows to the top left corner or bottom right corner,
which would mean it is, or should be a mancala cup
*/
bool Board::isCorner(int row, int col) {
	if (row == 0 && col == 0)
		return true;
	else if (row == 1 && col == this->numOfCupsPerRow)
		return true;

	return false;
}

/** This method is to be run after the last stone is checked to
have landed into an empty NON-MANCALA cup, which is ooposite of an opponent cup
which contains stones

This method then provided the row and column of empty cup said stone landed into,
will proceed to steal all the stones from the non-empty enemy player cup opposite.
*/

void Board::stealFromOtherCup(int stealerRow, int stealerCol) {

	//calculate index of cup to steal from
	if (stealerRow == 1) {//p2 is stealin
		int stolenRow = 0;
		int stolenCol = stealerCol + 1;

		//!increment mancala cup, note the need to dereference pointers
		allCups[1][this->numOfCupsPerRow]->addStones(allCups[stolenRow][stolenCol]->getStones() + 1);
		allCups[stealerRow][stealerCol]->setStones(0);
		allCups[stolenRow][stolenCol]->setStones(0);
	}
	else { //p1 is stealing
		int stolenRow = 1;
		int stolenCol = stealerCol - 1;

		allCups[0][0]->addStones(allCups[stolenRow][stolenCol]->getStones() + 1);
		allCups[stealerRow][stealerCol]->setStones(0);
		allCups[stolenRow][stolenCol]->setStones(0);

	}
}

/*
*getCup returns the row and column of the cup the player has selected.
*/

Cup Board::getCup(int row, int col) {
	return *allCups[row][col];
}

int Board::getGameMode() {
	return this->gameMode;
}

int Board::getP1Score() {
	int score = this->allCups[0][0]->getStones() + this->allCups[0][1]->getStones() + this->allCups[0][2]->getStones() + this->allCups[0][3]->getStones() + this->allCups[0][4]->getStones() + this->allCups[0][5]->getStones() + this->allCups[0][6]->getStones();
	return score;
}

int Board::getP2Score() {
	int score = this->allCups[1][this->numOfCupsPerRow]->getStones() + this->allCups[1][0]->getStones() + this->allCups[1][1]->getStones() + this->allCups[1][2]->getStones() + this->allCups[1][3]->getStones() + this->allCups[1][4]->getStones() + this->allCups[1][5]->getStones();
	return score;
}


/**
This method is to be run after a move
when a player selects a cup, it checks if the cup is owned by the respective player

Next it checks if the cup is empty, and throws an error message if so

Otherwise it will attempt to
*/

bool Board::makeMove(int row, int col) {
	int stones = allCups[row][col]->take();
	for (int i = 0; i < stones; i++)
	{

		if (getTurn() == 0)
		{
			if (col == 5 && row == 1) {
				row--;
				col++;
			}
			else if (col == 0 && row == 0)
				row++;
			else if (row == 0)
				col--;
			else if (row == 1)
				col++;

			allCups[row][col]->increment();
		}

		if (getTurn() == 1)
		{
			if (col == 1 && row == 0) {
				row++;
				col--;
			}
			else if (col == 6 && row == 1)
				row--;
			else if (row == 1)
				col++;
			else if (row == 0)
				col--;

			allCups[row][col]->increment();
		}
	}
	
	bool stealing = checkForStealing(row, col);
	if (stealing)
		stealFromOtherCup(row, col);

	if (!checkForDoubleTurn(row, col))
		changeTurn();

	return stealing;
}

int Board::getTurn()
{
	return this->whoseTurn;
}


bool Board::isOwned(int row) {
	if (getTurn() == row)
		return true;
	else
		return false;
}

/** This method is called in the main gameflow while loop of the Mancala.cpp file

It is only called after a turn is made, and the checkForDoubleTurn fails.
*/
void Board::changeTurn()
{
	whoseTurn++;
	whoseTurn = whoseTurn % 2;
}
/**This method returns a boolean related to whether or not a the following
CupPointer to a cup being emptied will result in an additional turn for the current
player

passed in argument CupPointer toBeEmptied is the cup toBeEmptied theoritically
by the player's selection of a cup
*/
bool Board::checkForDoubleTurn(int row, int col)
{
	if (whoseTurn == row && (col == 0 || col == 6))
		return true;
	return false;
}

/** This method returns a boolean related to if emptying this cup
will land in a non-mancala cup owned by the player across from a non-empty,
non-mancala cup

passed in argument CupPointer toBeEmptied is the cup toBeEmptied theoritically
by the player's selection of a cup
*/
bool Board::checkForStealing(int row, int col) //changed from CupPoint to Cup, was having difficulty with them pointers
{
	if (whoseTurn == 0) // if its Player1's turn the stones will added to 
	{
		if (isCorner(row, col))  //check if this is a corner cup, (mancalaCup)
		{
			return false;
		}
		/* stealing is possible if:
		* (1) it is possible to steal from the cup landed in
		* (2) the cup belongs to p1, and
		* (3) the opposite cup isn’t empty.
		*/
		if (allCups[row][col]->stealPossible() && (row == 0) && !allCups[1][col - 1]->isEmpty())
		{
			return true;
		}
	}

	else if (whoseTurn == 1)
	{

		if (isCorner(row, col))  //check if this is a corner cup, (mancalaCup)
		{
			return false;
		}
		/* stealing is possible if:
		* (1) it is possible to steal from the cup landed in
		* (2) the cup belongs to p2, and
		* (3) the opposite cup isn’t empty.
		*/
		if (allCups[row][col]->stealPossible() && (row == 1) && !allCups[0][col + 1]->isEmpty())
		{
			return true;
		}

	}
	return false;
}

/**
* check if one side cup is empty

* check if one player has no more stones in any playable (non-manacala) cups
* Public board method checks if the game is over or not and returns
* true or false if the game is over or not, by checking for
* any empty cups
*
* this method currently does not scan for who wins, which
* should be done in a different method for modularity
*/
bool Board::checkForGameOver()
{
	//check both rows for any non empty non mancala cups
	bool p1CupsEmpty = true;
	bool p2CupsEmpty = true;

	//iterate top row indexes 1 to 6 inclusive
	for (int col = 1; col <= 6; col++) {
		if (!allCups[0][col]->isEmpty())
			p1CupsEmpty = false;
	}
	//iterate bottom row indexs 0 to 5 incluseive
	for (int col = 0; col < 5; col++) {
		if (!allCups[1][col]->isEmpty())
			p2CupsEmpty = false;
	}
	return (p1CupsEmpty || p2CupsEmpty);
}



int Board::evaluateWinner()
{
	int p1 = getP1Score();
	int p2 = getP2Score();

	if (p1>p2) return 0;              // 0 represents player 1 won
	else if (p2>p1) return 1;         // 1 represents player 2 won
	else return 2;                  // 2 represents a draw
}


/** This method is for use in testing.

The method currently prints the number of stones in each cup on the board to provide an updated status.
*/

void Board::printBoardStatus() {
	cout << "       \t Col:0 \t Col:1 \t Col:2 \t Col:3 \t Col:4 \t Col:5 \t  Col:6";
	for (int row = 0; row < DEFAULT_BOARD_HEIGHT; row++) {
		cout << "\nRow:" << row;
		for (int col = 0; col < DEFAULT_BOARD_WIDTH; col++) {
			cout << "    " << allCups[row][col]->getStones() << "    ";
		}
	}
	cout << "\n" << endl;
}


//---------------------------------------------------------------------------------------------------------

/**
* create a cup
* stone: number of stones
* r: row position in board (1->P1's cups, 2->P2's cups)
* c: column position in board
*/
Cup::Cup(int stone, int r, int c) : row(r), col(c) {
	this->numOfStones = stone;
}
/**
* accessor methods
*/
int Cup::getStones() {
	return this->numOfStones;
}
int Cup::getRow() {
	return this->row;
}
int Cup::getCol() {
	return this->col;
}


void Cup::addStones(int num) {
	for (int i = 0; i < num; i++) {
		increment();
	}
}

void Cup::setPosition(int r, int c) {
	this->row = r;
	this->col = c;
}

/**
* sets the number of stones in the cup
*/
void Cup::setStones(int stone) {
	this->numOfStones = stone;
}

/**
* add one stone in the cup
*/
void Cup::increment() {
	this->numOfStones++;
}

/**
* take stones in cup and return the number taken
*/
int Cup::take() {
	int stone = this->numOfStones;
	this->numOfStones = 0;
	return stone;
}

/**Public cup method that returns a boolean true/false
if this cup is empty or not

by geoff for board class
*/
bool Cup::isEmpty() {
	if (numOfStones == 0)
		return true;

	return false;
}

bool Cup::stealPossible() {
	if (numOfStones == 1)
		return true;
	return false;
}

void Cup::print() {
	cout << "Cup[" << getRow() << ", " << getCol() << "] has " << getStones() << " stones." << endl;
}

//---------------------------------------------------------------------------------------------------------------
/*
MancalaCup::MancalaCup(int r, int c) : Cup(0, r, c) {}

void MancalaCup::setPlayer(int p) {

	this->player = p;
}

int MancalaCup::getPlayer() {
	return this->player;
}*/

