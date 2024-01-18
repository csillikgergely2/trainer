using System;
using System.Collections.Generic;
using System.IO;

class Főprogram
{
    static string fájlNév = "mozgas.txt";  // A fájl neve
    static List<string[]> adatok = new List<string[]>();  // Az adatok listája

    static void Main(string[] args)
    {
        AdatokBetöltése();  // Adatok betöltése a fájlból

        bool kilépés = false;
        while (!kilépés)
        {
            Console.WriteLine("Kérlek, válassz egy menüpontot:");
            Console.WriteLine("1. Új adat rögzítése");
            Console.WriteLine("2. Meglévő adatok kiíratása táblázatszerűen");
            Console.WriteLine("3. Meglévő adatok kiíratása adott edzéstípusra szűrve");
            Console.WriteLine("4. Adat törlése");
            Console.WriteLine("5. Kilépés");

            string választás = Console.ReadLine();
            Console.Clear();

            switch (választás)
            {
                case "1":
                    ÚjAdatRögzítése();
                    break;
                case "2":
                    AdatokKiíratása();
                    break;
                case "3":
                    AdatokSzűréseEdzéstípusSzerint();
                    break;
                case "4":
                    AdatTörlése();
                    break;
                case "5":
                    kilépés = true;
                    break;
                default:
                    Console.WriteLine("Érvénytelen választás!");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void AdatokBetöltése()
    {
        if (File.Exists(fájlNév))
        {
            string[] sorok = File.ReadAllLines(fájlNév);
            foreach (string sor in sorok)
            {
                string[] adat = sor.Split('\t');
                adatok.Add(adat);
            }
        }
    }

    static void AdatokMentése()
    {
        string[] sorok = new string[adatok.Count];
        for (int i = 0; i < adatok.Count; i++)
        {
            string sor = string.Join("\t", adatok[i]);
            sorok[i] = sor;
        }
        File.WriteAllLines(fájlNév, sorok);
    }

    static void ÚjAdatRögzítése()
    {
        Console.WriteLine("Kérem, adja meg a dátumot (éééé.hh.nn formátumban):");
        string dátum = Console.ReadLine();
        Console.WriteLine("Kérem, adja meg az edzés típusát:");
        string edzésTípus = Console.ReadLine();
        Console.WriteLine("Hány percig tartott az edzés?");
        string időtartam = Console.ReadLine();

        string[] újAdat = { dátum, edzésTípus, időtartam };
        adatok.Add(újAdat);

        AdatokMentése();
        Console.WriteLine("Az új adat sikeresen rögzítve!");
    }

    static void AdatokKiíratása()
    {
        Console.WriteLine("Dátum\t\tEdzés típusa\tIdőtartam");
        foreach (string[] sor in adatok)
        {
            Console.WriteLine($"{sor[0]}\t{sor[1]}\t\t{sor[2]}");
        }
    }

    static void AdatokSzűréseEdzéstípusSzerint()
    {
        Console.WriteLine("Kérem, adja meg az edzés típusát:");
        string edzésTípus = Console.ReadLine();

        Console.WriteLine("Dátum\t\tEdzés típusa\tIdőtartam");
        foreach (string[] sor in adatok)
        {
            if (sor[1] == edzésTípus)
            {
                Console.WriteLine($"{sor[0]}\t{sor[1]}\t\t{sor[2]}");
            }
        }
    }

    static void AdatTörlése()
    {
        Console.WriteLine("Kérem, adja meg a törlendő sor indexét:");
        if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < adatok.Count)
        {
            Console.WriteLine("A következő adat törlésre kerül:");
            Console.WriteLine($"{adatok[index][0]}\t{adatok[index][1]}\t{adatok[index][2]}");

            Console.WriteLine("Biztosan törölni szeretné? (Igen/Nem)");
            string válasz = Console.ReadLine().ToLower();

            if (válasz == "igen")
            {
                adatok.RemoveAt(index);
                AdatokMentése();
                Console.WriteLine("Az adat sikeresen törölve!");
            }
            else
            {
                Console.WriteLine("Adat nem lett törölve.");
            }
        }
        else
        {
            Console.WriteLine("Érvénytelen index!");
        }
    }
}
