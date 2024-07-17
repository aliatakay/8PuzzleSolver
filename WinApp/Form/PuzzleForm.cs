using Puzzle.Common.Algorithm;
using Puzzle.Common.Event;
using Puzzle.Common.Extension;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace App
{
    public partial class PuzzleForm : Form
    {
        private bool _isDrawn;
        private int[] _gameField;
        private int[] _targetPuzzle;
        private int _puzzleSize = 3;
        private int[] _finalStatue = { 1, 2, 3, 4, 5, 6, 7, 8, 0 };
        private List<int> moves = new List<int>();

        // Constructor
        public PuzzleForm()
        {
            InitializeComponent();

            PuzzleController();

            _isDrawn = false;

            Get_NewPuzzle_Start(cb_last.Checked, Convert.ToInt32("3"));

            cbSpeed.Text = "0.50";

            Control.CheckForIllegalCrossThreadCalls = false;
        }

        //   Events
        public event EventHandler<PieceChangedEventArgs> Get_ChangedPieceValue = null;

        public event EventHandler<PuzzleRenewedEventArgs> Get_NewPuzzle = null;

        public event EventHandler Start_AStar = null;
        
        private void Run_AStar_Start()
        {
            EventHandler handler = Start_AStar;
            if (handler != null)
            {
                Start_AStar(this, new EventArgs());
            }
        }
        
        private void Get_ChangedPieceValue_Start(int index, int value)
        {
            EventHandler<PieceChangedEventArgs> handler = Get_ChangedPieceValue;
            if (handler != null)
            {
                Get_ChangedPieceValue(this, new PieceChangedEventArgs(index, value));
            }
        }
        
        private void Get_NewPuzzle_Start(bool lastPieceEmpty, int puzzleSize)
        {
            lblMove.Text = "";
            lblTime.Text = "";

            EventHandler<PuzzleRenewedEventArgs> handler = Get_NewPuzzle;
            if (handler != null)
            {
                Get_NewPuzzle(this, new PuzzleRenewedEventArgs(puzzleSize, lastPieceEmpty));
            }
        }
        
        public void DrawPuzzle(int[,] puzzle, int puzzleSize)
        {
            if (!_isDrawn)
            {
                DrawPuzzleButtons(puzzle, puzzleSize);
                _isDrawn = true;
            }
            else UpdateGame(puzzle, puzzleSize);
        }
        
        private void UpdateGame(int[,] puzzle, int puzzleSize)
        {
            int butonSize = 80;

            for (int x = 0; x < puzzleSize; x++)
            {
                for (int y = 0; y < puzzleSize; y++)
                {
                    Button button = (Button)panel1.GetChildAtPoint(new Point(x * butonSize + 150, y * butonSize + 45));
                    button.BackColor = Color.Yellow;

                    string val = puzzle[y, x].ToString();
                    if (val == "0")
                    {
                        val = "";
                        button.BackColor = Color.Gray;
                    }

                    button.Text = val;

                    button.Font = new Font("Microsoft Sans Serif", 25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    button.ForeColor = Color.Black;
                    button.Margin = new Padding(10, 10, 10, 10);
                    button.FlatStyle = FlatStyle.Standard;
                    button.Width = butonSize;
                    button.Height = butonSize;
                }
            }
        }
        
        private void DrawPuzzleButtons(int[,] puzzle, int puzzleSize)
        {
            panel1.Controls.Clear();
            int buttonSize = 80;

            for (int x = 0; x < puzzleSize; x++)
            {
                for (int y = 0; y < puzzleSize; y++)
                {
                    Button button = new Button();
                    button.Click += Button_Click;
                    string val = puzzle[y, x].ToString();
                    if (val == "0") val = "";
                    button.Text = val;
                    button.Location = new Point(x * buttonSize + 150, y * buttonSize + 45);

                    button.BackColor = Color.Yellow;
                    button.Font = new Font("Microsoft Sans Serif", 25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
                    button.ForeColor = Color.Black;
                    button.Margin = new Padding(10, 10, 10, 10);
                    button.FlatStyle = FlatStyle.Standard;
                    button.Width = buttonSize;
                    button.Height = buttonSize;
                    button.Text = val;

                    if (button.Text == "")
                        button.BackColor = Color.Gray;

                    panel1.Controls.Add(button);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (!_gameField.SequenceEqual(_finalStatue))
            {
                Button filledBtn = (Button)sender;
                if (filledBtn.Text == "")
                {
                    return;
                }

                Button emptyBtn = new Button();

                Button leftBtn = new Button
                {
                    Text = ""
                };
                Button rightBtn = new Button
                {
                    Text = ""
                };
                Button downBtn = new Button
                {
                    Text = ""
                };
                Button upBtn = new Button
                {
                    Text = ""
                };

                int buttonSize = 80;
                for (int x = 0; x < _puzzleSize; x++)
                {
                    for (int y = 0; y < _puzzleSize; y++)
                    {
                        Button button = (Button)panel1.GetChildAtPoint(new Point(x * buttonSize + 150, y * buttonSize + 45));
                        if (button.Text == "")
                        {
                            emptyBtn = button;

                            if (x > 0)
                            {
                                leftBtn = (Button)panel1.GetChildAtPoint(new Point((x - 1) * buttonSize + 150, y * buttonSize + 45));
                            }

                            if (x < (_puzzleSize - 1))
                            {
                                rightBtn = (Button)panel1.GetChildAtPoint(new Point((x + 1) * buttonSize + 150, y * buttonSize + 45));
                            }

                            if (y > 0)
                            {
                                upBtn = (Button)panel1.GetChildAtPoint(new Point(x * buttonSize + 150, (y - 1) * buttonSize + 45));
                            }

                            if (y < (_puzzleSize - 1))
                            {
                                downBtn = (Button)panel1.GetChildAtPoint(new Point(x * buttonSize + 150, (y + 1) * buttonSize + 45));
                            }

                            break;
                        }
                    }
                }

                int i1 = -1;
                int i2 = -1;

                int filled = Convert.ToInt32(filledBtn.Text);
                for (int i = 0; i < _gameField.Length; i++)
                {
                    if (_gameField[i] == 0)
                    {
                        i1 = i;
                    }

                    if (_gameField[i] == filled)
                    {
                        i2 = i;
                    }
                }

                if (filledBtn.Text == downBtn.Text)
                {
                    emptyBtn.Text = filledBtn.Text;
                    filledBtn.Text = "";

                    filledBtn.BackColor = Color.Gray;
                    emptyBtn.BackColor = Color.Yellow;

                    _gameField[i1] = filled;
                    _gameField[i2] = 0;
                }
                else if (filledBtn.Text == upBtn.Text)
                {
                    emptyBtn.Text = filledBtn.Text;
                    filledBtn.Text = "";

                    filledBtn.BackColor = Color.Gray;
                    emptyBtn.BackColor = Color.Yellow;

                    _gameField[i1] = filled;
                    _gameField[i2] = 0;
                }
                else if (filledBtn.Text == leftBtn.Text)
                {
                    emptyBtn.Text = filledBtn.Text;
                    filledBtn.Text = "";

                    filledBtn.BackColor = Color.Gray;
                    emptyBtn.BackColor = Color.Yellow;

                    _gameField[i1] = filled;
                    _gameField[i2] = 0;
                }
                else if (filledBtn.Text == rightBtn.Text)
                {
                    emptyBtn.Text = filledBtn.Text;
                    filledBtn.Text = "";

                    filledBtn.BackColor = Color.Gray;
                    emptyBtn.BackColor = Color.Yellow;

                    _gameField[i1] = filled;
                    _gameField[i2] = 0;
                }
            }
            else
            {
                btnNew.Enabled = true;
                panel1.Enabled = false;
                return;
            }
        }
        
        private void btnNew_Click(object sender, EventArgs e)
        {
            panel1.Enabled = true;
            btnSolve.Enabled = true;
            lblMoves.Items.Clear();
            moves.Clear();
            _isDrawn = false;
            Get_NewPuzzle_Start(cb_last.Checked, Convert.ToInt32(_puzzleSize.ToString()));
        }
        
        private void btnSolve_Click(object sender, EventArgs e)
        {
            btnSolve.Enabled = false;
            btnNew.Enabled = false;
            panel1.Enabled = false;

            int size = Convert.ToInt32(_puzzleSize.ToString());
            int[,] controlPuzzle = new int[size, size];
            int i = 0;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    controlPuzzle[x, y] = _gameField[i];
                    i++;
                }
            }

            lblMoves.Items.Clear();
            moves.Clear();
            Run_AStar_Start();
        }

        public void Call_AStar_Start(bool success, List<int[,]> puzzleStatuses, string time, int amount)
        {
            lblTime.Text = time;
            lblMove.Text = (amount - 1).ToString();

            Thread thread = new Thread(delegate () { Draw_All_Statuses(puzzleStatuses); });
            thread.Start();
        }

        private void Draw_All_Statuses(List<int[,]> puzzleStatuses)
        {
            lblMoves.Items.Clear();
            moves.RemoveAt(0);
            foreach (var move in moves)
            {
                lblMoves.Items.Add(move);
            }

            if (!cbSolution.Checked)
            {
                btnNew.Enabled = true;
                panel1.Enabled = true;
                return;
            }

            foreach (int[,] state in puzzleStatuses)
            {
                if (cbSpeed.SelectedItem.ToString() == "0.05") Thread.Sleep(50);
                else if (cbSpeed.SelectedItem.ToString() == "0.10") Thread.Sleep(100);
                else if (cbSpeed.SelectedItem.ToString() == "0.25") Thread.Sleep(250);
                else if (cbSpeed.SelectedItem.ToString() == "0.50") Thread.Sleep(500);
                else Thread.Sleep(1000);

                DrawPuzzle(state, state.GetLength(0));
            }

            btnNew.Enabled = true;
            panel1.Enabled = false;
        }

        public void PuzzleController()
        {
            Get_NewPuzzle += Get_NewPuzzle_Gui;
            Start_AStar += Start_AStar_Gui;

            _gameField = null;
            _targetPuzzle = null;
            _puzzleSize = 0;
        }

        private void Start_AStar_Gui(object sender, EventArgs e)
        {
            Thread thread = new Thread(delegate () { AStar_Start_Method(); });
            thread.Start();
        }
        
        private void AStar_Start_Method()
        {
            int index = 0;
            AStar algorithm = new AStar(_targetPuzzle, _puzzleSize);
            bool isSolved = algorithm.Solve(_gameField);

            List<int[]> puzzleStatuses = algorithm.GetPuzzleCases();
            List<int[,]> newPuzzleStatuses = new List<int[,]>();

            foreach (int[] statue in puzzleStatuses)
            {
                newPuzzleStatuses.Add(PuzzleExtension.ConvertToDimension2(statue, _puzzleSize));
                moves.Add(statue[index]);

                for (int i = 0; i < statue.Length; i++)
                {
                    if (statue[i] == 0) index = i;
                }
            }

            Call_AStar_Start(isSolved, newPuzzleStatuses, algorithm.Time, algorithm.NumberOfNodes);
        }
        
        private void Get_NewPuzzle_Gui(object sender, PuzzleRenewedEventArgs e)
        {
            _puzzleSize = e.PuzzleSize;
            GenerateNewPuzzle(e.PuzzleSize, e.LastPieceEmpty);
            this.DrawPuzzle(PuzzleExtension.ConvertToDimension2(_gameField, _puzzleSize), e.PuzzleSize);
        }
        
        private void GenerateNewPuzzle(int gameFieldSize, bool lastPieceEmpty)
        {
            int[,] puzzle = new int[gameFieldSize, gameFieldSize];
            List<int> numbers = new List<int>();

            FillNumbers(numbers, gameFieldSize);

            FillTargetPuzzle(numbers, gameFieldSize);

            if (lastPieceEmpty) numbers.RemoveAt(0);

            MixNumbers(numbers);

            FillPuzzle(puzzle, gameFieldSize, numbers, lastPieceEmpty);

            if (IsPuzzleSolvable(puzzle, gameFieldSize))
            {
                _gameField = PuzzleExtension.ConvertToDimension1(puzzle, gameFieldSize);
            }
            else
            {
                GenerateNewPuzzle(gameFieldSize, lastPieceEmpty);
                return;
            }
        }
        
        private void FillTargetPuzzle(List<int> puzzleNumbers, int puzzleSize)
        {
            int[,] puzzle = new int[puzzleSize, puzzleSize];
            int i = 1;
            for (int x = 0; x < puzzleSize; x++)
            {
                for (int y = 0; y < puzzleSize; y++)
                {
                    if (x == puzzleSize - 1 && y == puzzleSize - 1) break;
                    puzzle[x, y] = puzzleNumbers[i];
                    ++i;
                }
            }

            _targetPuzzle = PuzzleExtension.ConvertToDimension1(puzzle, puzzleSize);
        }
        
        private bool IsPuzzleSolvable(int[,] puzzle, int puzzleSize)
        {
            int[] _1DPuzzleArray = Make1Dimensional(puzzle, puzzleSize);

            int parity = 0;
            int gridWidth = (int)Math.Sqrt(_1DPuzzleArray.Length);
            int row = 0;
            int blankRow = 0;

            for (int i = 0; i < _1DPuzzleArray.Length; i++)
            {
                if (i % gridWidth == 0) row++;
                if (_1DPuzzleArray[i] == 0)
                {
                    blankRow = row;
                    continue;
                }
                for (int j = i + 1; j < _1DPuzzleArray.Length; j++)
                {
                    if (_1DPuzzleArray[i] > _1DPuzzleArray[j] && _1DPuzzleArray[j] != 0) parity++;
                }
            }

            if (gridWidth % 2 == 0) return (blankRow % 2 == 0) ? (parity % 2 == 0) : (parity % 2 != 0);
            else return parity % 2 == 0;
        }
        
        public static int[] Make1Dimensional(int[,] puzzle, int puzzleSize)
        {
            int[] _1DPuzzleArray = new int[puzzleSize * puzzleSize];
            int index = 0;

            for (int x = 0; x < puzzleSize; x++)
            {
                for (int y = 0; y < puzzleSize; y++)
                {
                    _1DPuzzleArray[index] = puzzle[x, y];
                    ++index;
                }
            }

            return _1DPuzzleArray;
        }
        
        private void FillNumbers(List<int> puzzleNumbers, int puzzleSize)
        {
            for (int i = 0; i < puzzleSize * puzzleSize; i++)
            {
                puzzleNumbers.Add(i);
            }
        }

        private void FillPuzzle(int[,] puzzle, int puzzleSize, List<int> puzzleNumbers, bool lastPieceEmpty)
        {
            for (int x = 0; x < puzzleSize; x++)
            {
                for (int y = 0; y < puzzleSize; y++)
                {
                    if (lastPieceEmpty && x == puzzleSize - 1 && y == puzzleSize - 1) break;
                    Random rnd = new Random();
                    int index = rnd.Next(0, puzzleNumbers.Count - 1);
                    puzzle[x, y] = puzzleNumbers[index];
                    puzzleNumbers.RemoveAt(index);
                }
            }
        }

        private void MixNumbers(List<int> list)
        {
            int n = list.Count;
            Random rnd = new Random();

            while (n > 1)
            {
                int k = (rnd.Next(0, n) % n);
                n--;
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}