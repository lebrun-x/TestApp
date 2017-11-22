using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.App
{
    public class Arme
    {
        private String _name;
        private int _dammage;
        private int _touch;
        private int _critique;

        public Arme()
        {

        }

        public Arme(String name, int dammage, int touch, int critique)
        {
            this._name = name;
            this._dammage = dammage;
            this._touch = touch;
            this._critique = critique;
        }

        public String getName()
        {
            return _name;
        }

        public void setName(String name)
        {
            _name = name;
        }

        public int attaque()
        {
            Random rnd = new Random();
            int randCritique = rnd.Next(1, 10);
            int randTouch = rnd.Next(1, 10);
            if (randTouch > _touch)
                return 0;
            else
            {
                if (randCritique > _critique)
                    return _dammage;
                else
                {
                    Console.WriteLine("Coup Critique !!!");
                    return _dammage * 2;
                }
            }
        }
    }
}
