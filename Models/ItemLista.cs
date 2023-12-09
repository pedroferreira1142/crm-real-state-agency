using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetoCurricular_v002.Models
{
    public class ItemLista
    {
        private string _designacao;

        public Guid Uid { get; set; }

        public string Designacao
        {
            get { return _designacao; }
            set 
            {
                _designacao = value;
                if (_designacao == null) _designacao = "n/a";
            }
        }
    }
}
