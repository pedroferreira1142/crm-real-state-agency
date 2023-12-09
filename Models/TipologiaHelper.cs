using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace projetoCurricular_v002.Models
{
    public class TipologiaHelper
    {
        private string _ligacaoBD = "";

        public TipologiaHelper(string strConexao)
        {
            _ligacaoBD = strConexao;
        }

        /*=============================================== LISTAGEM DE TIPOLOGIAS ==================================================*/
        public List<ItemLista> getList()
        {
            DataTable dtTipologia = new DataTable();
            List<ItemLista> listaSaida = new List<ItemLista>();

            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            
            telefone.SelectCommand = comando;
            comando.CommandText = "SELECT * FROM tblTipologia ORDER BY tipologia";
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtTipologia);
            conexao.Close();
            conexao.Dispose();

            //Para cada linha do datatable
            foreach (DataRow linha in dtTipologia.Rows)
            {
                ItemLista l = new ItemLista();
                l.Designacao = linha["tipologia"].ToString();
                l.Uid = Guid.Parse(linha["uid"].ToString());
                listaSaida.Add(l);
            }
            return listaSaida;
        }
        /*=========================================================================================================================*/



        /*=============================================== LISTAGEM DE TIPOLOGIAS ==================================================*/
        public List<ItemLista> getList(Guid idTipologiaAVer)
        {
            DataTable dtTipologia = new DataTable();
            List<ItemLista> listaSaida = new List<ItemLista>();

            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);

            telefone.SelectCommand = comando;
            comando.CommandText = "SELECT * FROM tblTipologia ORDER BY tipologia" +
                "WHERE uid = @type";
            comando.Parameters.AddWithValue("@type", idTipologiaAVer);
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtTipologia);
            conexao.Close();
            conexao.Dispose();

            //Para cada linha do datatable
            foreach (DataRow linha in dtTipologia.Rows)
            {
                ItemLista l = new ItemLista();
                l.Designacao = linha["tipologia"].ToString();
                listaSaida.Add(l);
            }
            return listaSaida;
        }
        /*=========================================================================================================================*/


        /*=============================================== EXTRAIR UMA TIPOLOGIA ID CLIENTE ==================================================*/
        public ItemLista getTipologiaCliente(Guid idClienteAVer)
        {
            DataTable dtTipologia = new DataTable();

            /*--------------------- Criada a conexão com a BD ---------------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            /*---------------------------------------------------------------------*/

            /*--------------------- Comando Select do TipoCliente ---------------------*/
            telefone.SelectCommand = comando;
            comando.CommandText = "SELECT tblTipologia.uid, tipologia FROM tblTipologia" +
                " INNER JOIN tblCliente ON tblTipologia.uid = tblCliente.idTipologia" +
                " WHERE tblCliente.uid = @client";
            comando.Parameters.AddWithValue("@client", idClienteAVer);
            /*-------------------------------------------------------------------------*/

            /*------------- Passagem de dados e conclusão da ligação -------------*/
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtTipologia);
            conexao.Close();
            conexao.Dispose();
            /*--------------------------------------------------------------------*/


            //Se a Tipologia for extraida transpor dados para um objeto tipo ItemLista
            ItemLista itemLista = new ItemLista();
            if (dtTipologia.Rows.Count > 0)
            {
                DataRow linha = dtTipologia.Rows[0];
                itemLista.Uid = Guid.Parse(linha["uid"].ToString()); //Passagem da info da BD para o ItemList
                itemLista.Designacao = linha["tipologia"].ToString(); 
            }
            return itemLista;
        }
        /*=========================================================================================================================*/

        


    }
}
