using System;
using System.Collections.Generic;

public class Rectangle
{
    int X, Y, Width, Height;
    public Rectangle(int x, int y, int width, int height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }
    public int Area() => Width * Height;

    public bool Overlaps(Rectangle other)
    {
        return !(X + Width <= other.X || other.X + other.Width <= X
            || Y + Height <= other.Y || other.Y + other.Height <= Y);
    }
    public override string ToString()
    {
        return $"({X},{Y}), {Width} + {Height}";
    }
}

class Program
{
    public static void Main()
    {
        int cWidth = 10;
        int cHeight = 5;
        Console.WriteLine("uneti zeljeni broj iteracija:");
        int iterations = Convert.ToInt32(Console.ReadLine());

        List<(int, int)> smallRectangles = new List<(int, int)> {
            (2,4), (3,2),(3,1), (1,1),(2,2),(3,1),(1,2)
        };
        Random rand = new Random();
        List<Rectangle> bestPlacement = new List<Rectangle>();
        int bestArea = 0;

        for (int i = 0; i < iterations;i++)
        {
            List<Rectangle> placed = new List<Rectangle> ();
            foreach (var (w, h) in smallRectangles)
            {
                bool placedSuc = false;
                for (int a = 0; a <= iterations; a++)
                {
                    int max_x = cWidth - w;
                    int max_y = cHeight - h;
                    int x = rand.Next(0, max_x + 1);
                    int y = rand.Next(0, max_y + 1);
                    Rectangle newRect = new Rectangle (x, y, w, h);
                    bool overlaps = false;

                    foreach(var other in placed)
                    {
                        if(newRect.Overlaps(other))
                        {
                            overlaps = true;
                            break;
                        }
                    }
                    if(!overlaps)
                    {
                        placed.Add(newRect);
                        placedSuc = true;
                        break;
                    }
                }
            }
            int totalArea = 0;
            foreach(var p in placed)
            {
                totalArea += p.Area();
            }
            if (totalArea > bestArea)
            {
                bestArea = totalArea;
                bestPlacement = new List<Rectangle>(placed);
            }
        }
        Console.WriteLine($"\nNajbolje popunjenost: {bestArea} jedinica površine");
        Console.WriteLine("Najbolji raspored pravougaonika:");
        foreach (var r in bestPlacement)
            Console.WriteLine(r);
    }
}
