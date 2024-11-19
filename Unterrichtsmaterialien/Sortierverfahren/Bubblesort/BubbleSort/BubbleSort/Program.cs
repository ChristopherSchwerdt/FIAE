using System;

public class Bubble_Sort
{
    public static void Main(string[] args)
    {
        int[] a = { 5, 1, 4, 2, 8 }; // Initialisierung des Arrays mit den entsprechenden Anfangswerten.
        int t; // Bublesort benötigt eine temporäre Variable zum Sortieren

        Console.WriteLine("Ursprüngliches Array :");
        foreach (int aa in a) // Schleife um das ursprüngliche Array anzuzeigen                
            Console.Write(aa + " "); // Ausgabe jedes Element des Arrays

        for (int p = 0; p <= a.Length - 2; p++) // Äußere Schleife für die Durchgänge
        {
            for (int i = 0; i <= a.Length - 2; i++) // Innere Schleife für den vergleich und das tauschen.
            {
                if (a[i] > a[i + 1]) // Prüfen ob das aktuelle Element größer als das nächste Element ist
                {
                    t = a[i + 1]; // Tauschen der Elemente falls diese in der falschen Reihenfolge sind
                    a[i + 1] = a[i];
                    a[i] = t;
                }
            }
        }

        Console.WriteLine("\n" + "Sortiertes Array :");
        foreach (int aa in a) // Schleife um das sortierte Array anzuzeigen                    
            Console.Write(aa + " "); // Ausgabe jedes Element des Arrays

        Console.Write("\n"); // Neue Zeile am Ende hinzufügen.
    }
}