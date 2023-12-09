using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace projetoCurricular_v002.Models
{
    public class Cliente
    {
        private string _nome;
        private string _contacto;
        private string _eMail;
        private DateTime _dataNascimento;
        private DateTime _dataCriacao;
        private int _numeroQuartos;
        private Decimal _precoPretendido;
        private string _observacoes;

        /*------GUID ------*/
        public Guid Uid { get; set; } //Primary Key

        public Guid UidLocalizacao { get; set; } //Localizacao foreign Key

        public Guid UidTipoCliente { get; set; } //TipoCliente foreign key

        public Guid UidTipologia { get; set; } //Tipologia foreign key


        
        public string Localizacao { get; set; }
        public string TipoCliente { get; set; }
        public string Tipologia { get; set; }
        public int Estado { get; set; }



        /*------ NOME ------*/
        [Required]
        public string Nome
        {
            get { return _nome; }
            set 
            {
                _nome = value;
                if (_nome == null)
                {
                    _nome = "n/a";
                }
            }
        }

        /*------ CONTACTO ------*/
        [Required]
        public string Contacto
        {
            get { return _contacto; }
            set
            {
                _contacto = value;
                if (_contacto == null || _contacto.Length != 9)
                {
                    _contacto = "n/a";
                }
            }
        }

        /*------ EMail ------*/
        [Required]
        public string EMail
        {
            get { return _eMail; }
            set
            {
                _eMail = value;
                if (_eMail == null)
                {
                    _eMail = "n/a";
                }
            }
        }

        /*------ DATA NASCIMENTO ------*/
        public DateTime DataNascimento
        {
            get { return _dataNascimento; }
            set 
            {
                _dataNascimento = value;
                if (_dataNascimento.Date < new DateTime(1900, 1, 1))
                {
                    _dataNascimento = new DateTime(1900, 1, 1);
                }
            }
        }

        /*------ DATA CRIAÇÃO ------*/
        public DateTime DataCriacao
        {
            get { return _dataCriacao; }
            set
            {
                _dataCriacao = value;
                if (_dataCriacao.Date < new DateTime(1900, 1, 1)) _dataCriacao = new DateTime(1900, 1, 1);
            }
        }

        /*------ NÚMERO QUARTOS------*/
        public int NumeroQuartos
        {
            get { return _numeroQuartos; }
            set 
            {
                _numeroQuartos = value;
                if (_numeroQuartos < 0) _numeroQuartos = 0;
            }
        }

        /*------ PREÇO PRETENDIDO------*/
        public Decimal PrecoPretendido
        {
            get { return _precoPretendido; }
            set 
            {
                _precoPretendido = value;
                if (_precoPretendido < 0M) _precoPretendido = 0M;
            }
        }

        /*------ OBSERVACOES ------*/
        public string Observacoes
        {
            get { return _observacoes; }
            set
            {
                _observacoes = value;
                if (_observacoes == null) _observacoes = "n/a";
            }
        }
    }
}
