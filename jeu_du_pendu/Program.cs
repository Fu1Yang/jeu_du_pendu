using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace jeu_du_pendu
{
    internal class Program
    {
        static void Afficher(string mot, List<char> lettres)
        {
            int nb_index = mot.Count();
            Console.WriteLine(nb_index);
            
            for (int i = 0; i < nb_index; i++)
            {
                char lettre = mot[i];
                if (lettres.Contains(lettre))
                {
                    Console.Write(lettre+" ");
                }
                else
                {
                    Console.Write("_ ");
                }
            }
            Console.WriteLine();
            
        }

        static bool ToutesLettresDevinees(string mot, List<char> lettres)
        {
            List<string> deleteMot = new List<string>();
            //Console.WriteLine(lettres.Count());
            //Console.WriteLine(mot.Length);
            for (int i=0 ;i < lettres.Count(); i++)
            {
                mot = mot.Replace(lettres[i].ToString(), "");
           
                if (mot.Length == 0)
                {
                    return true;
                }
          
            }
            Console.Write(mot);
            
            return false;
        }

        static char DemanderUnelettre()
        {
            string lettre = "";
            while (true) 
            {
                Console.WriteLine("Donne une lettre ");
                lettre = Console.ReadLine();
                if (lettre.Length >= 2 || (int.TryParse(lettre, out int resultat) && resultat >= 0))
                {
                    Console.WriteLine("ERREUR: donne que une lettre");
                }
                else
                {
                    lettre = lettre.ToUpper();
                    return lettre[0];
                }
            }
     
           
        }
        static void DevinerMot(string er)
        {
            List<char> devinerLettre = new List<char>();
            const int NB_VIES = 6;
            int pointdeVie = NB_VIES;
        
            while (pointdeVie > 0)
            {
                Console.WriteLine(Ascii.PENDU[NB_VIES - pointdeVie]); 
                Afficher(er, devinerLettre);
                Console.WriteLine();
                var lettre = DemanderUnelettre();
                Console.Clear();
                if(er.Contains(lettre))
                {
                    Console.WriteLine("Correcte");
                    devinerLettre.Add(lettre);
                    var isCorrect = ToutesLettresDevinees(er, devinerLettre);
                    if (isCorrect)
                    {
                        Console.Write("Gagner !!! ");
                        break;
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("cette lettre n'est pas dans le mot");
                    pointdeVie--;
                    Console.WriteLine("Point de vie restant "+pointdeVie);
                    if (pointdeVie == 0)
                    {
                        Console.WriteLine("perdu dommage: " + er);
                        break;
                    }
                }
                Console.WriteLine();
            }

         
            Console.WriteLine(er+ " :Etais le mot a trouvé");
        }

        static string[] ChargerLesMots(string nomFichier)
        {
            try
            {
                return File.ReadAllLines(nomFichier);
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Erreur de lecture du fichier");
            }
           return null;
        
        }
        static void Main(string[] args)
        {
            var mots = ChargerLesMots("mots.txt");
            if ((mots == null) || (mots.Length == 0))
            {
                Console.WriteLine("La liste de mots est vide");
            }
            else
            {
                Random rand = new Random();

                int text_index = rand.Next(0, mots.Length);
                Console.WriteLine("Le fichier text contient " + mots.Length + " mots");
                Console.WriteLine("le numero aléatoire " + text_index);
                string mot = mots[text_index].Trim().ToUpper();
                DevinerMot(mot);
            }

            Console.WriteLine("Voulez-vous rejouer o/n");
            string reponse = Console.ReadLine();
            if (reponse == null || reponse == "n")
            {
                Console.WriteLine("Fini");
            }
            else
            {
                Main(args);
            }
        }
    }
}
