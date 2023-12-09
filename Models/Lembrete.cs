using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace projetoCurricular_v002.Models
{
    public class Lembrete
    {
        private string _titulo;
        private string _descricao;
        private DateTime _dataLembrete;

        public Guid Uid { get; set; }
        public Guid IdTipoLembrete { get; set; }
        public Guid IdCliente { get; set; }

        public int Estado { get; set; }

        /*----- Titulo -----*/
        public string Titulo
        {
            get { return _titulo; }
            set 
            {
                _titulo = value;
                if (_titulo == null) _titulo = "n/a";
            }
        }

        /*----- Descricao -----*/
        public string Descricao
        {
            get { return _descricao; }
            set
            {
                _descricao = value;
                if (_descricao == null) _descricao = "n/a";
            }
        }

        /*------ DATA LEMBRETE ------*/
        public DateTime DataLembrete
        {
            get { return _dataLembrete; }
            set
            {
                _dataLembrete  = value;
                if (_dataLembrete.Date < new DateTime(1900, 1, 1)) _dataLembrete = new DateTime(1900, 1, 1);
            }
        }

    }
}
