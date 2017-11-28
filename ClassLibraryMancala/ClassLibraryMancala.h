// ClassLibraryMancala.h

#pragma once
#include <string>
#include <stdio.h>
#include <iostream>
#include <typeinfo>

using namespace System;
using namespace std;

#define DEFAULT_BOARD_WIDTH 7 //this includes mancala cup
#define DEFAULT_BOARD_HEIGHT 2 
#define DEFAULT_NUM_OF_STARTING_STONES 4


namespace ClassLibraryMancala {


	public value class Cup {   
	private:
		int numOfStones;
		int row, col;
	public:
		Cup(int stone, int r, int c);

		void addStones(int num);
		int getStones();
		int getRow();
		int getCol();
		void setStones(int stone);
		void setPosition(int r, int c);
		void increment();
		bool stealPossible();
		int take();

						   //	friend ostream& operator<<(ostream& out, const Cup& c);
		bool isEmpty();
		void print();
	};

	//-------------------------------------------------------------------------
/*
	public value class MancalaCup {  ////DLL: Should it be public value/ref class...?

											//the row of the mancala cup can represent the player, where the team can decide if 0 = player 1, and 1 =player 2
											//or vice versa......
	private:
		int player;
		int numOfStones;
		int row, col;
	public:
		MancalaCup(int r, int c);
		void setPlayer(int p);
		int getPlayer();

	};*/

	//--------------------------------------------------------------------------------------------------------------



	public value class Board   ////DLL: Should it be public value/ref class...?
	{
		//typedef Cup** Row;//an array of cup pointers is  arow      

		//b/c 2DCupArray is illegal name
		//typedef Row* Cup***;// a table is composed of multiple rows

		//typedef Cup* CupPointer;

	private:
		int gameMode;
		int P1Score, P2Score;
		bool gameOver;

		//Cup allcups[7][2];



		int numOfCupsPerRow; //NOT INCLUDING MANCALA CUPS

	public:
		Board(int gameMode, int cupsPerRow); //constructor
		//~Board(); //destructor
		Cup*** allCups;//[DEFAULTBOARDHEIGHT][DEFAULTBOARDWIDTH];
					   //void initBoard(int gameModem ,int numOfNonMancCups);
		int whoseTurn; //1 for P1, 2 for P2
		int getGameMode();
		int getP1Score();
		int getP2Score();
		int getTurn();

		void stealFromOtherCup(int stealerRow, int stealerCol);

		Cup getCup(int row, int col);
		bool makeMove(int row, int col);

		void changeTurn();

		bool checkForDoubleTurn(int row, int col);
		bool checkForStealing(int row, int col);
		int evaluateWinner();
		//bool distribute(int row, int col);
		bool isOwned(int row);//Checks whether the current cup is owned by the player whose turn it currently is 
		bool checkForGameOver();

								//string save();
								//void load(string info);
		bool isCorner(int row, int col);

		void printBoardStatus(); //added for testing - prints the status(number of stones) of each cup.
	};

}
