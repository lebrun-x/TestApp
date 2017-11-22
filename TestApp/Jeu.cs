using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.App
{
    public class Jeu
    {
        private Personnage _J1;
        private Personnage _J2;
        private List<Sort> _listSort;
        private List<Arme> _listArme;

        public Jeu()
        {
        }

        public List<Sort> Get_ListSort()
        {
            return _listSort;
        }

        public void SetPersonnageJ1(Personnage personnage)
        {
            this._J1 = personnage;
        }

        public void SetPersonnageJ2(Personnage personnage)
        {
            this._J2 = personnage;
        }

        public void LaunchGame()
        {
            LaunchArme();
            LaunchSort();
            Console.WriteLine("Bienvenu dans Transformoule !");
            Console.WriteLine("Passons à la créaton de la Moule de combat du joueur 1");
            _J1 = CreateCharacter();
            Console.Clear();
            Console.WriteLine("Au tour de joueur 2 de créer son personnage");
            _J2 = CreateCharacter();
            Console.Clear();
            Console.WriteLine("Place au combat !");
            Round();
        }

        public void Round()
        {
            while (_J1._life > 0 && _J2._life > 0)
            { 
                _J1._dammage = 0;
                _J2._dammage = 0;
                DisplayStats(_J1);
                Console.WriteLine("");
                DisplayStats(_J2);
                Console.WriteLine("");
                PlayerPhase(_J1);
                Console.WriteLine("");
                PlayerPhase(_J2);
                Console.Clear();
                FightPhase();
            }
            DisplayStats(_J1);
            Console.WriteLine("");
            DisplayStats(_J2);
            Console.WriteLine("");
            Victory();
        }

        public void Victory()
        {
            if (_J1._life <= 0 && _J2._life <= 0)
                Console.WriteLine("Vous avez tous les deux perdus bande de moules !");
            else if (_J1._life <= 0)
                Console.WriteLine(_J2._name + " a remporté le combat !");
            else
                Console.WriteLine(_J1._name + " a remporté le combat !");
            Console.ReadKey(true);
        }

        public void DisplayStats(Personnage personnage)
        {
            String sentence = "* " + personnage._name + " . " + " points de vie restants : " + personnage._life + " - points de chamoule restants " + personnage._chakra + " *";
            DisplayLine(sentence);
            Console.WriteLine(sentence);
            DisplayLine(sentence);
            Console.WriteLine("");
        }

        public void DisplayLine(String str)
        {
            for (int i = 0; i < str.Length + 2; i++)
                Console.Write("*");
            Console.WriteLine("");
        }

        public void PlayerPhase(Personnage personnage)
        {
            String choice;
            Console.WriteLine("C'est au tour de " + personnage._name + " de jouer !");
            Console.WriteLine("Quelle action " + personnage._name + " va faire ?");
            Console.WriteLine("Choix de l'action (Tapez le numéro de l'action choisie :)");
            personnage._action = 0;
            while (personnage._action == 0)
            {
                Console.WriteLine("1) Coup de " + personnage._arme.getName());
                for (int i = 0; i < personnage._listSort.Count(); i++)
                    Console.WriteLine(i + 2 + ") Lancez " + personnage._listSort.ElementAt(i).getName());
                choice = Console.ReadLine();
                int c;
                int.TryParse(choice, out c);
                personnage._action = c;
                if(personnage._action < 1 || personnage._action > personnage._listSort.Count() + 1)
                {
                    Console.WriteLine("Erreur dans le choix de l'action, veuillez rentrer une action valide");
                    personnage._action = 0;
                }
                else if (personnage._action > 1 && personnage._listSort.ElementAt(personnage._action - 2).GetCost() > personnage._chakra)
                {
                    Console.WriteLine("Vous n'avez pas assez de chamoule pour lancer ce sort. Veuillez choisir une autre action.");
                    personnage._action = 0;
                }
            }
        }

        public void FightPhase()
        {
            TestArme();
            TestSort(_J2._action - 2, ref _J1, ref _J2);
            TestSort(_J1._action - 2, ref _J2, ref _J1);
            _J1._chakra++;
            _J2._chakra++;
        }

        public void TestArme()
        {
            if (_J1._action == 1)
                _J1.Attack();
            if (_J2._action == 1)
                _J2.Attack();
        }

        public void TestSort(int action, ref Personnage p1, ref Personnage p2)
        {
            if (action <= -1)
            {
                if (p2._dammage != 0)
                {
                    Console.WriteLine(p1._name + " perd " + p2._dammage + " points de vie");
                    p1._life -= p2._dammage;
                }
            }
            else
            {
                Sort sort = p1._listSort.ElementAt(action);
                p1.ThrowSort(sort);
                p1._chakra -= sort.GetCost();
                switch (sort.getName())
                {
                    case "moulage":
                        if (p2._dammage == 0)
                        {
                            if (p2._listSort.ElementAt(p2._action - 2).getName().CompareTo("vampiMoule") == 0)
                                Console.WriteLine(p1._name + " Interrompt vampiMoule de " + p2._name);
                            else
                                Console.WriteLine(p1._name + " se protège, mais on ne sait pas trop pourquoi ...");
                        }
                        else
                            Console.WriteLine(p1._name + " pare facilement l'attaque de " + p2._name);
                        break;
                    case "dansTaMoule":
                        if (p2._dammage == 0)
                        {
                            Console.WriteLine(p1._name + " tente de renvoyer une attaque ... mais rien ne se passe.");
                        }
                        else
                        {
                            Console.WriteLine(p1._name + " renvoie l'attaque de " + p2._name + " ! ça pique !");
                            Console.WriteLine(p2._name + " prend " + p2._dammage + " points de dégats");
                            p2._life -= p2._dammage;
                        }
                        break;
                    case "bandeDeMoule":
                        {
                            Console.WriteLine(p1._name + " se booste pour sa prochaine attaque ! (Dégats x2)");
                            p1._bonusDeg = 2;
                        }
                        break;
                    case "vampiMoule":
                        {
                            if (p2._listSort.ElementAt(p2._action - 2).getName().CompareTo("moulage") == 0)
                                Console.WriteLine(p1._name + " tente d'aspirer la vie de son adversaire mais ... rien ne se passe");
                            else
                            {
                                Console.WriteLine(p1._name + " aspire la vie de son adversaire tel une moule vampire !");
                                Console.WriteLine(p1._name + " vole " + sort.GetDammage() + " points de vie à " + p2._name);
                                p1._life += sort.GetDammage();
                                p2._life -= sort.GetDammage();
                            }
                        }
                        break;
                }
            }
        }

        public Personnage CreateCharacter()
        {
            List<Sort> listSort = new List<Sort>();
            Arme arme = new Arme();

            Console.WriteLine("Veuillez entrer le nom de votre Moule de combat");
            String name = "";
            while (name == "")
                name = Console.ReadLine();
            Console.WriteLine("Veuillez choisir l'arme de votre Moule (Tapez le numéro correspondant à l'arme)");
            arme = ChooseArme();
            Console.WriteLine("Veuillez choisir le premier sort de votre Moule (Tapez le numéro correspondant au sort)"); 
            ChooseSort(ref listSort);
            Console.WriteLine("Veuillez choisir le deuxième sort de votre Moule (Tapez le numéro correspondant au sort)");
            ChooseSort(ref listSort);
            _listSort.AddRange(listSort);
            return new Personnage(40, 10, name, arme, listSort);
        }

        public Arme ChooseArme()
        {
            for (int i = 0; i < _listArme.Count(); i++)
            {
                Console.WriteLine(i + 1 + ") " + _listArme.ElementAt(i).getName());
            }
            String choice = Console.ReadLine();
            int c;
            int.TryParse(choice, out c);
            if (c < 1 || c > _listArme.Count())
            {
                Console.WriteLine("Erreur dans le choix de l'arme, veuillez rentrer un choix valide");
                return ChooseArme();
            }
            else
                return _listArme.ElementAt(c - 1);
        }

        public void ChooseSort(ref List<Sort> listSort)
        {
            for (int i = 0; i < _listSort.Count(); i++)
            {
                Console.WriteLine(i + 1 + ") " + _listSort.ElementAt(i).getName());
            }
            String choice = Console.ReadLine();
            int c;
            int.TryParse(choice, out c);
            if (c < 1 || c > _listSort.Count())
            {
                Console.WriteLine("Erreur dans le choix du sort, veuillez rentrer un choix valide");
                ChooseSort(ref listSort);
            }
            else
            {
                listSort.Add(_listSort.ElementAt(c - 1));
                _listSort.RemoveAt(c - 1);
            }
        }

        public void LaunchSort()
        {
            _listSort = new List<Sort>();
            Sort moulage = new Sort("moulage", "défense", 0, 5, 1);
            Sort dansTaMoule = new Sort("dansTaMoule", "défense", 0, 6, 1);
            Sort bandeDeMoule = new Sort("bandeDeMoule", "boost", 0, 4, 1);
            Sort vampiMoule = new Sort("vampiMoule", "attaque", 5, 6, 1);
            CreateList<Sort>(_listSort, moulage, dansTaMoule, bandeDeMoule, vampiMoule);
        }

        public void LaunchArme()
        {
            _listArme = new List<Arme>();
            Arme arc = new Arme("ArcMoule", 3, 9, 3);
            Arme moulaGauffre = new Arme("Moulagauffre", 7, 2, 2);
            Arme moulaFritte = new Arme("Moulafritte", 6, 5, 8);
            Arme laMoumoule = new Arme("LaMoumoule", 10, 1, 10);
            CreateList(_listArme, arc, moulaFritte, moulaGauffre, laMoumoule);
        }

        public void CreateList<T>(List<T> list, params T[] objects)
        {
            for(int i = 0; i < objects.Length; i++)
            {
                list.Add((T)objects.GetValue(i));
            }
        }
    }
}
