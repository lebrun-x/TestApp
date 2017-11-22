using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.App
{
    public class Sort
    {
        private String _name; 
        private String _type;
        private int _dammage;
        private int _cost;
        private int _timeCast;
        private int _sortReady;

        public Sort(String name, String type, int dammage, int cost, int timecast)
        {
            this._name = name;
            this._type = type;
            this._dammage = dammage;
            this._cost = cost;
            this._timeCast = timecast;
            this._sortReady = timecast;
        }

        public String getName()
        {
            return _name;
        }

        public int GetCost()
        {
            return _cost;
        }

        public int GetDammage()
        {
            return _dammage;
        }

        public void DescreaseSortReady()
        {
            --_sortReady;
        }

        public void ResetSortReady()
        {
            this._sortReady = _timeCast;
        }
    }
}
