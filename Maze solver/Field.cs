using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Maze_solver
{
    class Field
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Rectangle Rectangle { get; private set; }

        public int GScore { get; set; }
        public int HScore { get; set; }
        public int FScore { get; set; }
        public Field CameFrom { get; set; }   

        private State fieldState;

        public State FieldState
        {
            get { return fieldState; }
            set
            {
                ChangeFieldFill(value);
                fieldState = value;
            }
        }



        public Field(int x, int y)
        {
            this.X = x;
            this.Y = y;

            CreateRectangle();
        }

        private void CreateRectangle()
        {
            Rectangle = new Rectangle
            {
                Width = 25,
                Height = 25,
                Fill = Brushes.White,
                Stroke = Brushes.Black,
                StrokeThickness = 2

            };
            Rectangle.MouseDown += FieldClickEvent;
            Canvas.SetLeft(Rectangle, X * 25);
            Canvas.SetTop(Rectangle, Y * 25);
        }

        internal void CalculatFScore()
        {
            FScore = GScore + HScore;
            this.Rectangle.Fill = Brushes.Blue;
        }

        private void ChangeFieldFill(State toState)
        {
            switch (toState)
            {
                case State.Empty:
                    Rectangle.Fill = Brushes.White;
                    break;
                case State.Wall:
                    Rectangle.Fill = Brushes.Black;
                    break;
                case State.Start:
                    Rectangle.Fill = Brushes.Green;
                    break;
                case State.Destination:
                    Rectangle.Fill = Brushes.Red;
                    break;
                default:
                    break;
            }
        }

        private void FieldClickEvent(object sender, MouseEventArgs e)
        {
            switch (FieldState)
            {
                case State.Empty:
                    FieldState = State.Wall;
                    break;
                case State.Wall:
                    FieldState = State.Start;
                    break;
                case State.Start:
                    FieldState = State.Destination;
                    break;
                case State.Destination:
                    FieldState = State.Empty;
                    break;
                default:
                    break;
            }
        }
    }

    enum State
    {
        Empty,
        Wall,
        Start,
        Destination
    }
}
