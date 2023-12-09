using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace projetoCurricular_v002.Models
{
    public class TransacaoHelper
    {
        private string _ligacaoBD = "";

        public TransacaoHelper (string strConexao)
        {
            _ligacaoBD = strConexao;
        }

        /*=================================================== INSERÇÃO DE UM TRANSAÇÃO ===============================================*/
        public void inserir(Transacao transacao)
        {
            /*----------- Conexão com a BD -----------*/
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            comando.CommandType = CommandType.Text;

            /*----------- Comando INSERT para inserção de dados na tabela BD -----------*/
            comando.CommandText = "INSERT INTO tblTransacao (uid, titulo, descricao, dataCriacao, dataTransacao, preco, idCliente, idTipologia, idLocalizacao) " +
                        " VALUES (@uid, @titulo, @descricao, @dataCriacao, @dataTransacao, @preco, @idCliente, @idTipologia, @idLocalizacao)";
            transacao.Uid = Guid.NewGuid(); //Criação do Uid
            comando.Parameters.AddWithValue("@uid", transacao.Uid);
            comando.Parameters.AddWithValue("@titulo", transacao.Titulo);
            comando.Parameters.AddWithValue("@descricao", transacao.Descricao);
            comando.Parameters.AddWithValue("@dataCriacao", DateTime.Now);
            comando.Parameters.AddWithValue("@dataTransacao", transacao.DataTransacao);
            comando.Parameters.AddWithValue("@preco", transacao.Preco);
            comando.Parameters.AddWithValue("@idCliente", transacao.IdCliente);
            comando.Parameters.AddWithValue("@idTipologia", transacao.IdTipologia);
            comando.Parameters.AddWithValue("@idLocalizacao", transacao.IdLocalizacao);

            comando.Connection = conexao;
            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }
        /*============================================================================================================================*/


        /*==================================================== LISTAGEM DAS TRANSAÇÕES =================================================*/
        public List<Transacao> getList(Guid idClienteAVer)
        {
            /*----------- DataTable e Lista de Clientes -----------*/
            DataTable dtTransacao = new DataTable();
            List<Transacao> listaSaida = new List<Transacao>();
            /*-----------------------------------------------------*/

            /*------------------ Conexão com a BD -----------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;
            /*-----------------------------------------------------*/

            /*---------------------- Comando SELECT na BD ----------------------*/
            comando.CommandText = "SELECT * FROM tblTransacao" +
                " WHERE idCliente = @client";
            comando.Parameters.AddWithValue("@client", idClienteAVer);
            /*------------------------------------------------------------------*/

            /*--- Passagem de dados e conclusão da ligação ---*/
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtTransacao);
            conexao.Close();
            conexao.Dispose();
            /*------------------------------------------------*/

            //Para cada linha da DataTable
            foreach (DataRow linha in dtTransacao.Rows)
            {
                Transacao transacao = new Transacao();
                transacao.Uid = Guid.Parse(linha["uid"].ToString());
                transacao.Titulo = linha["titulo"].ToString();
                transacao.Descricao = linha["descricao"].ToString();
                transacao.DataCriacao = Convert.ToDateTime(linha["dataCriacao"]);
                transacao.DataTransacao = Convert.ToDateTime(linha["dataTransacao"]);
                transacao.Preco = Convert.ToDecimal(linha["preco"]);
                transacao.IdCliente = Guid.Parse(linha["idCliente"].ToString());
                transacao.IdLocalizacao = Guid.Parse(linha["idLocalizacao"].ToString());
                transacao.IdTipologia = Guid.Parse(linha["idTipologia"].ToString());

                listaSaida.Add(transacao);
            }
            return listaSaida;
        }
        /*============================================================================================================================*/


        /*==================================================== RETIRAR UMA TRANSAÇÃO =================================================*/
        public Transacao getTransacao(Guid idTransacaoAVer)
        {
            /*----------- DataTable e Lista de Clientes -----------*/
            DataTable dtTransacao = new DataTable();
            /*-----------------------------------------------------*/

            /*------------------ Conexão com a BD ------------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;
            /*------------------------------------------------------*/

            /*----------------------------- Comando SELECT na BD -----------------------------*/
            comando.CommandText = "SELECT * FROM tblTransacao" +
                " WHERE uid = @transaction";
            comando.Parameters.AddWithValue("@transaction", idTransacaoAVer);
            /*---------------------------------------------------------------------------------*/

            /*-- Passagem de dados e conclusão da ligação --*/
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtTransacao);
            conexao.Close();
            conexao.Dispose();
            /*----------------------------------------------*/

            //Criação do cliente
            Transacao t = new Transacao();
            if (dtTransacao.Rows.Count > 0)
            {
                DataRow linha = dtTransacao.Rows[0];

                t.Uid = Guid.Parse(linha["uid"].ToString());
                t.IdCliente = Guid.Parse(linha["idCliente"].ToString());
                t.IdLocalizacao = Guid.Parse(linha["idLocalizacao"].ToString());
                t.IdTipologia = Guid.Parse(linha["idTipologia"].ToString());
                t.Titulo = linha["titulo"].ToString();
                t.Descricao = linha["descricao"].ToString();
                t.DataCriacao = Convert.ToDateTime(linha["dataCriacao"]);
                t.DataTransacao = Convert.ToDateTime(linha["dataTransacao"]);
                t.Preco = Convert.ToDecimal(linha["preco"]);
            }
            return t;
        }
        /*============================================================================================================================*/


        /*=============================================== EDITAR TRANSAÇÃO =========================================================*/
        public void atualizar(Transacao t)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            comando.CommandType = CommandType.Text;

            comando.CommandText = "UPDATE tblTransacao SET titulo = @titulo, " +
                                    " descricao = @descricao, " +
                                    " dataTransacao = @dataTransacao, " +
                                    " preco = @preco, " +
                                    " idTipologia = @idTipologia, " +
                                    " idLocalizacao = @idLocalizacao " +
                                    " WHERE uid = @uid ";

            comando.Parameters.AddWithValue("@uid", t.Uid);
            comando.Parameters.AddWithValue("@titulo", t.Titulo);
            comando.Parameters.AddWithValue("@descricao", t.Descricao);
            comando.Parameters.AddWithValue("@dataTransacao", t.DataTransacao);
            comando.Parameters.AddWithValue("@preco", t.Preco);
            comando.Parameters.AddWithValue("@idTipologia", t.IdTipologia);
            comando.Parameters.AddWithValue("@idLocalizacao", t.IdLocalizacao);

            comando.Connection = conexao;
            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }
        /*==========================================================================================================================*/


        /*======================================== REMOVER TRANSAÇÃO ==============================================================*/
        public void remover(Guid uidTransacaoARemover)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            comando.CommandType = CommandType.Text;

            comando.CommandText = "DELETE FROM tblTransacao " +
                " WHERE uid = @uid";
            comando.Parameters.AddWithValue("@uid", uidTransacaoARemover);

            comando.Connection = conexao;
            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }
        /*=======================================================================================================================*/
    }
}
