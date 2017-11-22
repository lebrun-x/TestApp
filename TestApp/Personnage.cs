using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.App
{
    public class Personnage
    {
        public int _life { get; set; }
        public int _chakra { get; set; }
        public int _bonusDeg { get; set; }
        public Arme _arme { get; set; }
        public String _name { get; set ;}
        public List<Sort> _listSort { get; set; }
        public int _action { get; set; }
        public int _dammage { get; set; }

        public Personnage(int life, int chakra, String name, Arme arme, List<Sort> listSort)
        {
            this._life = life;
            this._name = name;
            this._chakra = chakra;
            this._arme = arme;
            this._listSort = listSort;
            this._bonusDeg = 1;
        }

        public void Attack()
        {
            Console.WriteLine(_name + " attaque avec " + _arme.getName());
            _dammage = _arme.attaque() * _bonusDeg;
            if (_dammage == 0)
                Console.WriteLine(_name + " échoue lamentablement son attaque !");
            _bonusDeg = 1;
        }

        public int ThrowSort(Sort sort)
        {
            Console.WriteLine(_name + " lance " + sort.getName());
            return sort.GetDammage();
        }
    }
}
