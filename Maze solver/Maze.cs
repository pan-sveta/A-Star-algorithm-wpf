using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Maze_solver
{
    class Maze
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Field[,] Fields { get; private set; }
        List<Field> solution;

        public Maze(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            Fields = new Field[width,height];

            Create();
        }

        private void Create()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    Fields[x, y] = new Field(x,y);
                }
            }
        }
        public void Solve()
        {
            Field start = GetStartingField(Fields);
            Field destination = GetDestinationField(Fields);

            List<Field> openset = new List<Field>();
            openset.Add(start);
            List<Field> closeset = new List<Field>();

            start.GScore = 0;
            start.HScore = HeuristicScore(start,destination);
            start.CalculatFScore();


            while (openset.Capacity!=0)
            {
                Field x = GetLowestFScoreField(openset);

                if (x.FieldState==State.Destination)
                {
                    solution = ReconstructSolution(x, start);
                    foreach (var item in solution)
                    {
                        item.Rectangle.Fill = Brushes.Purple;
                    }
                    return;
                }

                openset.Remove(x);
                closeset.Add(x);

                foreach (Field y in GetAdjacentFields(x,Fields))
                {
                    if (closeset.Contains(y) || y.FieldState==State.Wall)
                    {
                        continue;
                    }

                    int currentGScore = x.GScore + FieldDistance(x,y);

                    bool recalculate;
                    if (!openset.Contains(y))
                    {
                        openset.Add(y);
                        recalculate = true;
                    }
                    else if (currentGScore<y.GScore)
                    {
                        recalculate = true;
                    }
                    else
                    {
                        recalculate = false;
                    }
                    if (recalculate)
                    {
                        y.CameFrom = x;
                        y.GScore = currentGScore;
                        y.HScore = HeuristicScore(y,destination);
                        y.CalculatFScore();
                    }
                    
                }
               
            }

        }

        private List<Field> ReconstructSolution(Field current, Field start)
        {
            List<Field> list = new List<Field>();
            while (current!=start)
            {
                list.Add(current);
                current = current.CameFrom;
            }
            list.Add(start);

            return list;
        }

        private int FieldDistance(Field a, Field b)
        {
            if (Math.Abs(a.X-b.X)==1&& Math.Abs(a.Y - b.Y) == 1)
            {
                return 10;
            }
            else
            {
                return 14;
            }
        }

        private Field GetLowestFScoreField(List<Field> fieldlist)
        {
            Field lowestField = fieldlist.First();

            foreach (var field in fieldlist)
            {
                if (lowestField.FScore>field.FScore)
                {
                    lowestField = field;
                }
            }

            return lowestField;
        }

        private int HeuristicScore(Field start, Field destination)
        {
            int dx = Math.Abs(start.X - destination.X);
            int dy = Math.Abs(start.Y - destination.Y);
            return 10 * (dx + dy);
        }

        private List<Field> GetAdjacentFields(Field candidate , Field[,] fields)
        {
            List<Field> fieldList = new List<Field>();

            if (candidate.X+1!=Width)
            {
                fieldList.Add(Fields[candidate.X + 1, candidate.Y]);
            }
            if (candidate.X != 0)
            {
                fieldList.Add(Fields[candidate.X - 1, candidate.Y]);
            }
            if (candidate.Y + 1 != Height)
            {
                fieldList.Add(Fields[candidate.X, candidate.Y + 1]);
            }
            if (candidate.Y != 0)
            {
                fieldList.Add(Fields[candidate.X, candidate.Y - 1]);
            }
            if (candidate.X + 1 != Width && candidate.Y + 1 != Height)
            {
                fieldList.Add(Fields[candidate.X + 1, candidate.Y + 1]);
            }
            if (candidate.X != 0 && candidate.Y != 0)
            {
                fieldList.Add(Fields[candidate.X - 1, candidate.Y - 1]);
            }
            if (candidate.X + 1 != Width&& candidate.Y != 0)
            {
                fieldList.Add(Fields[candidate.X + 1, candidate.Y - 1]);
            }
            if (candidate.X != 0&& candidate.Y + 1 != Height)
            {
                fieldList.Add(Fields[candidate.X - 1, candidate.Y + 1]);
            }

            return fieldList;
        }

        private Field GetStartingField(Field[,] fields)
        {
            foreach (Field field in fields)
            {
                if (field.FieldState==State.Start)
                {
                    return field;
                }
            }
            throw new Exception("No starting field.");
        }

        private Field GetDestinationField(Field[,] fields)
        {
            foreach (Field field in fields)
            {
                if (field.FieldState == State.Destination)
                {
                    return field;
                }
            }
            throw new Exception("No destination field.");
        }
    }
}
