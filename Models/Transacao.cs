using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetoCurricular_v002.Models
{
    public class Transacao
    {
        private string _titulo;
        private string _descricao;
        private DateTime _dataCriacao;
        private DateTime _dataTransacao;
        private Decimal _preco;

        public Guid Uid { get; set; }
        public Guid IdCliente { get; set; }
        public Guid IdTipologia { get; set; }
        public Guid IdLocalizacao { get; set; }

        /*-------- Titulo --------*/
        public string Titulo
        {
            get { return _titulo; }
            set
            {
                _titulo = value;
                if (_titulo == null) _titulo = "n/a";
            }
        }

        /*-------- Descricao --------*/
        public string Descricao
        {
            get { return _descricao; }
            set
            {
                _descricao = value;
                if (_descricao == null) _descricao = "n/a";
            }
        }

        /*-------- Data de Criacao --------*/
        public DateTime DataCriacao
        {
            get { return _dataCriacao; }
            set 
            { 
                _dataCriacao = value;
                if (_dataCriacao.Date < new DateTime(1900, 1, 1) || _dataCriacao == null) _dataCriacao = new DateTime(1900, 1, 1);
            }
        }


        /*-------- Data de Transacao --------*/
        public DateTime DataTransacao
        {
            get { return _dataTransacao; }
            set
            {
                _dataTransacao = value;
                if (_dataTransacao.Date < new DateTime(1900, 1, 1) || _dataTransacao == null) _dataTransacao = new DateTime(1900, 1, 1);
            }
        }

        /*-------- Preço --------*/
        public decimal Preco
        {
            get { return _preco; }
            set 
            {
                _preco = value;
                if (_preco < 0M) _preco = 0M;
            }
        }
    }
}
