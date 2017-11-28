using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.Media;
using Microsoft.VisualBasic;

using ClassLibraryMancala;

namespace MancalaGUI
{
    public partial class Form3 : Form
    {
        System.Media.SoundPlayer gameStartSound = new System.Media.SoundPlayer(MancalaGUI.Properties.Resources.GameStart);
        System.Media.SoundPlayer pointEarntSound = new System.Media.SoundPlayer(MancalaGUI.Properties.Resources.PointEarnt);
        System.Media.SoundPlayer cupSelectedSound = new System.Media.SoundPlayer(MancalaGUI.Properties.Resources.CupSelected);

        private int soundSetting = 1; //sound on initialized
        private int numClicksWrongSide = 0; //initialized
        private int lastPlayer = 0; //initialized

        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        public Form RefToMainForm { get; set; }

        public Board board;

        public Form3()
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            playGameSound(1);
            InitializeComponent();
            drawBoard();
            board = new Board(0, 6);
            getplayerNames();
        }

        //------BUTTON EVENTS------
        private void back_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to proceed? All game progress will be lost.", "Back", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
           
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            this.RefToMainForm.Show();
        }

        private void rules_Click(object sender, EventArgs e)
        {
            Form2 rulesForm = new Form2();
            rulesForm.Show();
            rulesForm.Location = new Point(this.Location.X, this.Location.Y);
        }

        //------ACCESSOR-TYPE METHODS------
        private CupObj getCupAcross(CupObj c)
        {
            if (c.Equals(cup1))
                return cup13;
            if (c.Equals(cup2))
                return cup12;
            if (c.Equals(cup3))
                return cup11;
            if (c.Equals(cup4))
                return cup10;
            if (c.Equals(cup5))
                return cup9;
            if (c.Equals(cup6))
                return cup8;
            if (c.Equals(cup13))
                return cup1;
            if (c.Equals(cup12))
                return cup2;
            if (c.Equals(cup11))
                return cup3;
            if (c.Equals(cup10))
                return cup4;
            if (c.Equals(cup9))
                return cup5;
            if (c.Equals(cup8))
                return cup6;
            return null;
        }

        private int nextCupTag(int original, int current)
        {
            if (original < 7) // if one of p1 cups
            {
                if (current == 13)
                    return 1;
                else
                    return current + 1;
            }
            else // if one of p2 cups
            {
                if (current == 6)
                    return 8;
                else if (current == 14)
                    return 1;
                else
                    return current + 1;
            }
        }

        private CupObj getCupWithTag(int tag)
        {
            foreach (var cup in Controls.OfType<CupObj>())
            {
                if ((int)cup.Tag == tag)
                    return cup;
            }
            return new CupObj();
        }

        private List<StoneObj> getStonesWithTag(int tag)
        {
            List<StoneObj> stones = new List<StoneObj>();
            foreach (var stone in Controls.OfType<StoneObj>())
            {
                if ((int)stone.Tag == tag)
                    stones.Add(stone);
            }
            return stones;
        }

        //------DRAW METHODS------
        private void drawBoard()
        {
            Random rnd = new Random();
            foreach (CupObj cup in Controls.OfType<CupObj>())
            {
                List<StoneObj> stonesToDraw = getStonesWithTag((int)cup.Tag);
                foreach (StoneObj stone in stonesToDraw)
                    drawStoneInCup(stone, cup, rnd);
            }
        }

        private void drawStoneInCup(StoneObj s, CupObj c, Random rnd)
        {
            int stoneCol = rnd.Next(1, 6);
            Bitmap stoneImg;
            if (stoneCol == 1)
                stoneImg = new Bitmap(MancalaGUI.Properties.Resources.rockBlue);
            else if (stoneCol == 2)
                stoneImg = new Bitmap(MancalaGUI.Properties.Resources.rockGreen);
            else if (stoneCol == 3)
                stoneImg = new Bitmap(MancalaGUI.Properties.Resources.rockPurple);
            else if (stoneCol == 4)
                stoneImg = new Bitmap(MancalaGUI.Properties.Resources.rockRed);
            else
                stoneImg = new Bitmap(MancalaGUI.Properties.Resources.rockYellow);
            s.BackgroundImage = stoneImg;
            s.BackgroundImageLayout = ImageLayout.Stretch;
            s.Enabled = false;
            s.Visible = true;
            s.Size = new Size(35, 25);
            s.BackColor = Color.Transparent;
            s.BorderStyle = BorderStyle.None;
            Point cupLoc = c.Location;
            Size cupSize = c.Size;
            int stoneX = rnd.Next(c.Location.X, c.Location.X + c.Width - s.Width);
            int stoneY = rnd.Next(c.Location.Y, c.Location.Y + c.Height - s.Height);
            s.Location = new Point(stoneX, stoneY);
        }

        //------UPDATE APPEARANCE METHODS------
        private void updateCounts()
        {
            count1.Text = board.getCup(1, 0).getStones().ToString();
            count2.Text = board.getCup(1, 1).getStones().ToString();
            count3.Text = board.getCup(1, 2).getStones().ToString();
            count4.Text = board.getCup(1, 3).getStones().ToString();
            count5.Text = board.getCup(1, 4).getStones().ToString();
            count6.Text = board.getCup(1, 5).getStones().ToString();
            count7.Text = board.getCup(1, 6).getStones().ToString();
            count8.Text = board.getCup(0, 6).getStones().ToString();
            count9.Text = board.getCup(0, 5).getStones().ToString();
            count10.Text = board.getCup(0, 4).getStones().ToString();
            count11.Text = board.getCup(0, 3).getStones().ToString();
            count12.Text = board.getCup(0, 2).getStones().ToString();
            count13.Text = board.getCup(0, 1).getStones().ToString();
            count14.Text = board.getCup(0, 0).getStones().ToString();
            if (board.getTurn() == 0)
            {
                label1.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label2.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            else
            {
                label1.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label2.Font = new System.Drawing.Font("Palatino Linotype", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }

        }

        private void updateInfoLabel()
        {
            int p1 = board.getP1Score();
            int p2 = board.getP2Score();
            int turn = board.getTurn();
            if (turn == 0)
                TurnInfo.Text = label1.Text + "'s Turn";
            else
                TurnInfo.Text = label2.Text + "'s Turn";
            p1StonesInfo.Text = p1.ToString();
            p2StonesInfo.Text = p2.ToString();
        }

        //------GRAPHICAL MOVEMENT METHODS------
        private CupObj moveStones(CupObj cupObj)
        {
            playGameSound(2);
            List<StoneObj> stonesToMove = getStonesWithTag((int)cupObj.Tag);
            int originalTag = (int)cupObj.Tag;
            int nextTag = nextCupTag(originalTag, originalTag);
            int landedTag = nextTag;
            foreach (StoneObj stone in stonesToMove)
            {
                if(nextTag == 7 || nextTag == 14)
                    playGameSound(3);
                moveStoneToCup(stone, getCupWithTag((int)cupObj.Tag), getCupWithTag(nextTag));
                landedTag = nextTag;
                nextTag = nextCupTag(originalTag, nextTag);
            }
            CupObj landedIn = getCupWithTag(landedTag);
            return landedIn;
        }

        private void stealStones(CupObj cupObj)
        {
            CupObj stealerCup = moveStones(cupObj);
            List<StoneObj> stealerStones = getStonesWithTag((int)stealerCup.Tag);
            CupObj stolenCup = getCupAcross(stealerCup);
            List<StoneObj> stolenStones = getStonesWithTag((int)stolenCup.Tag);
            //MessageBox.Show("Stealer cup = " + (int)stealerCup.Tag + " \nStolen cup = " + (int)stolenCup.Tag);
            foreach (StoneObj stone in stolenStones)
            {
                if (board.getTurn() == 0) //p2's turn just happened
                    moveStoneToCup(stone, stolenCup, cup7);
                else // p2's turn
                    moveStoneToCup(stone, stolenCup, cup14);
            }
            foreach (StoneObj stone in stealerStones)
            {
                if (board.getTurn() == 0) //p2's turn just happened
                    moveStoneToCup(stone, stealerCup, cup7);
                else // p2's turn
                    moveStoneToCup(stone, stealerCup, cup14);
            }

        }

        private void moveStoneToCup(StoneObj s, CupObj oldC, CupObj newC)
        {
            Random rnd = new Random();
            Point oldCupLoc = oldC.Location;
            Point newCupLoc = newC.Location;
            int stoneX = s.Location.X + (newCupLoc.X - oldCupLoc.X);
            int stoneY = s.Location.Y + (newCupLoc.Y - oldCupLoc.Y);
            int randomInt = rnd.Next(0, 2);
            //if moving to a mancala cup,  Y should occasionally be incremented by more to put it in the bottom half
            if (randomInt == 1 && ((int)newC.Tag == 7 || (int)newC.Tag == 14))
                stoneY += 80;
            s.Location = new Point(stoneX, stoneY);
            s.Tag = newC.Tag;
        }

        private void moveStonesLeft()
        {
            //move any stones left on P1's side to their mancala cup (cup14)
            for (int i = 8; i < 14; i++)
            {
                foreach (StoneObj stone in getStonesWithTag(i))
                    moveStoneToCup(stone, getCupWithTag((int)stone.Tag), cup14);
            }

            //move any stones left on P2's side to their mancala cup (cup7)
            for (int i = 1; i < 7; i++)
            {
                foreach (StoneObj stone in getStonesWithTag(i))
                    moveStoneToCup(stone, getCupWithTag((int)stone.Tag), cup14);
            }
        }
        
        //------HANDLE TURN METHOD------
        private void tryMove(int row, int col, CupObj cupObj)
        {
            Cup toBeEmptied = board.getCup(row, col);
            if (toBeEmptied.isEmpty())
            {
                MessageBox.Show("Please click on a cup that is not empty.");
                return;
            }
            if (!board.isOwned(row))
            {
                TurnInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
                turnExclamation.Visible = true;
                numClicksWrongSide++;
                if (numClicksWrongSide == 3)
                {
                    numClicksWrongSide = 0;
                    MessageBox.Show("This cup does not belong to you. Please click on a cup that belongs to you.");
                }
                return;
            }
            numClicksWrongSide = 0;
            bool stealOccured = board.makeMove(row, col);
            if (stealOccured)
            {
                stealStones(cupObj);
                bonusInfo.Visible = true;
                bonusInfo.Text = "Stolen Opponent's Stones!";
            }
            else
            {
                moveStones(cupObj);
                bonusInfo.Visible = false;
            }
            updateCounts();
            TurnInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            turnExclamation.Visible = false;

            if (board.getTurn() == 0 && lastPlayer == 0)
            {
                lastPlayer = 0;
                bonusInfo.Visible = true;
                bonusInfo.Text = "Double Turn!";
            }
            else if (board.getTurn() == 0 && lastPlayer != 0)
                lastPlayer = 0;
            else if (board.getTurn() == 1 && lastPlayer == 1)
            {
                lastPlayer = 1;
                bonusInfo.Visible = true;
                bonusInfo.Text = "Double Turn!";
            }
            else //(board.getTurn() == 1 && lastPlayer != 1)
                lastPlayer = 1;
        }

        //------SOUND METHODS------
        private void playGameSound(int soundCase)
        {
            if (soundSetting == 1)
            {
                if (soundCase == 1)
                    gameStartSound.Play();
                if (soundCase == 2)
                    cupSelectedSound.Play();
                if (soundCase == 3)
                    pointEarntSound.Play();
            }
        }

        private void sound_Click(object sender, EventArgs e)
        {
            if (soundSetting == 0)
            {
                sound.BackgroundImage = MancalaGUI.Properties.Resources.soundButton;
                soundSetting = 1;
            }
            else if (soundSetting == 1)
            {
                sound.BackgroundImage = MancalaGUI.Properties.Resources.soundOffButton;
                soundSetting = 0;
            }
        }

        //------UserInput METHODS------

        private void getplayerNames()
        {
            string player1Name = Interaction.InputBox("Please enter Player 1's name: \n Name entered must not exceed 7 characters.", "Player Information", "Player 1", 10, 10);
            while (player1Name.Length > 7)
            {
                MessageBox.Show("Name entered must not exceed 7 characters.", "Invalid Name Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                player1Name = Interaction.InputBox("Please enter Player 1's name: \nName entered must not exceed 7 characters.", "Player Information", "Player 1", 10, 10);
            }
            string player2Name = Interaction.InputBox("Please enter Player 2's name:\nName entered must not exceed 7 characters.", "Player Information", "Player 2", 10, 10);
            while (player2Name.Length > 7)
            {
                MessageBox.Show("Name entered must not exceed 7 characters.", "Invalid Name Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                player2Name = Interaction.InputBox("Please enter Player 2's name: \n Name entered must not exceed 7 characters.", "Player Information", "Player 2", 10, 10);
            }
            if (player1Name != "")
                label1.Text = player1Name;
            else
                label1.Text = "Player 1";
            if (player2Name != "")
                label2.Text = player2Name;
            else
                label2.Text = "Player 2";
            TurnInfo.Text = player1Name + "'s Turn";
            label3.Text = player1Name + "'s stones:";
            label4.Text = player2Name + "'s stones:";
        }

        //------END GAME METHODS------
        private void timer1_Tick(object sender, EventArgs e)
        {
            checkGameOver();
        }
        private void checkGameOver()
        {
            if ((count8.Text == "0" && count9.Text == "0" && count10.Text == "0" && count11.Text == "0" && count12.Text == "0" && count13.Text == "0")
               || (count1.Text == "0" && count2.Text == "0" && count3.Text == "0" && count4.Text == "0" && count5.Text == "0" && count6.Text == "0"))
            {
                timer1.Stop();

                //update GUI appearance
                moveStonesLeft();
                count1.Text = "0"; count2.Text = "0"; count3.Text = "0"; count4.Text = "0"; count5.Text = "0"; count6.Text = "0";
                count8.Text = "0"; count9.Text = "0"; count10.Text = "0"; count11.Text = "0"; count12.Text = "0"; count13.Text = "0";
                count7.Text = board.getP2Score().ToString();
                count14.Text = board.getP1Score().ToString();

                //declare winner
                int endGame = board.evaluateWinner();
                int difference = board.getP1Score() - board.getP2Score();
                string winner = "Player 1";
                if (endGame == 0)
                    winner = label1.Text;
                else if (endGame == 1)
                {
                    winner = label2.Text;
                    difference = difference * -1;
                }
                if (endGame != 2)
                { //if not tie
                    if (MessageBox.Show("Congratulations!! " + winner + " wins by " + difference + " points!", "Game Over", MessageBoxButtons.OK) == DialogResult.OK)
                        this.Close();
                }
                else
                { //tie
                    if (MessageBox.Show("The game was a tie!", "Game Over", MessageBoxButtons.OK) == DialogResult.OK)
                        this.Close();
                }
            } //end if   
        } //end checkGameOver 

        //------CUP CLICK EVENTS------
        private void cup1_Click(object sender, EventArgs e)
        {
            tryMove(1, 0, cup1);
            updateInfoLabel();
            //checkGameOver();

        }

        private void cup2_Click(object sender, EventArgs e)
        {
            tryMove(1, 1, cup2);
            updateInfoLabel();
            //checkGameOver();
        }

        private void cup3_Click(object sender, EventArgs e)
        {
            tryMove(1, 2, cup3);
            updateInfoLabel();
            //checkGameOver();
        }

        private void cup4_Click(object sender, EventArgs e)
        {
            tryMove(1, 3, cup4);
            updateInfoLabel();
            //checkGameOver();
        }

        private void cup5_Click(object sender, EventArgs e)
        {
            tryMove(1, 4, cup5);
            updateInfoLabel();
            //checkGameOver();
        }

        private void cup6_Click(object sender, EventArgs e)
        {
            tryMove(1, 5, cup6);
            updateInfoLabel();
            //checkGameOver();
        }

        private void cup8_Click(object sender, EventArgs e)
        {
            tryMove(0, 6, cup8);
            updateInfoLabel();
            //checkGameOver();
        }

        private void cup9_Click(object sender, EventArgs e)
        {
            tryMove(0, 5, cup9);
            updateInfoLabel();
            //checkGameOver();
        }

        private void cup10_Click(object sender, EventArgs e)
        {
            tryMove(0, 4, cup10);
            updateInfoLabel();
            //checkGameOver();
        }

        private void cup11_Click(object sender, EventArgs e)
        {
            tryMove(0, 3, cup11);
            updateInfoLabel();
            //checkGameOver();
        }

        private void cup12_Click(object sender, EventArgs e)
        {
            tryMove(0, 2, cup12);
            updateInfoLabel();
            //checkGameOver();
        }

        private void cup13_Click(object sender, EventArgs e)
        {
            tryMove(0, 1, cup13);
            updateInfoLabel();
            //checkGameOver();
        }
    }

    //------STONE AND CUP FORM OBJECT CLASSES------
    public class StoneObj : PictureBox
    {
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            using (var gp = new GraphicsPath())
            {
                gp.AddEllipse(new Rectangle(0, 0, this.Width - 1, this.Height - 1));
                this.Region = new Region(gp);
            }
        }
    }

    public class CupObj : PictureBox { }
}
