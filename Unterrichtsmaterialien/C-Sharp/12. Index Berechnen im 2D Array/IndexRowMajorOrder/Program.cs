using System;

class Program
{

    //Literatur :
    // https://en.wikipedia.org/wiki/Row-_and_column-major_order
    // https://www.cyotek.com/blog/converting-2d-arrays-to-1d-and-accessing-as-either-2d-or-1d
   //  https://stackoverflow.com/questions/2151084/map-a-2d-array-onto-a-1d-array
    static void Main()
    {
        // 2D Array Dimensionen
        const int rows = 3;
        const int columns = 4;

        // Beispiel-2D-Array (Row-Major-Order)
        int[,] array2D = new int[rows, columns]
        {
            { 1, 2, 3, 4    },
            { 5, 6, 7, 8    },
            { 9, 10, 11, 12 }
        };

        // Zeile und Spalte des gewünschten Elements
        int row = 2;    // Dritte Zeile (Index 2)
        int column = 1; // Zweite Spalte (Index 1)

        // Berechnung des Index in Row-Major-Order
        // Formel : Index = y * width + x
        int index = (row * columns) + column;

        // Rückrechnung in Zeile und Spalte
        //Formel : y = index / width;
        //Formel : x = index % width;
        row = index / columns;      // Zeile: Ganzzahliges Ergebnis der Division
        column = index % columns;   // Spalte: Rest der Division

        // Ausgabe des berechneten Index
        Console.WriteLine($"Der berechnete Index in Row-Major-Order ist: {index}");

        // Zugriff auf das Element im 2D-Array
        Console.WriteLine($"Das Element an der Position ({row}, {column}) ist: {array2D[row, column]}");
    }
}
