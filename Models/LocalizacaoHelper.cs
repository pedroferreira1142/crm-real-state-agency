using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace projetoCurricular_v002.Models
{
    public class LocalizacaoHelper
    {
        private string _ligacaoBD = "";

        public LocalizacaoHelper(string strConexao)
        {
            _ligacaoBD = strConexao;
        }

        /*=============================================== LISTAGEM DE LOCALIZAÇÕES ==================================================*/
        public List<ItemLista> getList()
        {
            DataTable dtLocalizacao = new DataTable();
            List<ItemLista> listaSaida = new List<ItemLista>();

            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);

            telefone.SelectCommand = comando;
            comando.CommandText = "SELECT * FROM tblLocalizacao ORDER BY localizacao";
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtLocalizacao);
            conexao.Close();
            conexao.Dispose();

            //Para cada linha do datatable
            foreach (DataRow linha in dtLocalizacao.Rows)
            {
                ItemLista l = new ItemLista();
                l.Designacao = linha["localizacao"].ToString();
                l.Uid = Guid.Parse(linha["uid"].ToString());
                listaSaida.Add(l); //Adicionamos à lista do tipo ItemLista
            }
            return listaSaida;
        }
        /*===========================================================================================================================*/


        /*=============================================== EXTRAIR UMA LOCALIZACAO ==================================================*/
        public ItemLista getLocalizacao(Guid idClienteAVer)
        {
            DataTable dtLocalizacao = new DataTable();

            /*--------------------- Criada a conexão com a BD ---------------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            /*---------------------------------------------------------------------*/

            /*--------------------- Comando Select do TipoCliente ---------------------*/
            telefone.SelectCommand = comando;
            comando.CommandText = "SELECT localizacao FROM tblLocalizacao" +
                " INNER JOIN tblCliente ON tblLocalizacao.uid = tblCliente.idLocalizacao" +
                " WHERE tblCliente.uid = @client";
            comando.Parameters.AddWithValue("@client", idClienteAVer);
            /*-------------------------------------------------------------------------*/

            /*------------- Passagem de dados e conclusão da ligação -------------*/
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtLocalizacao);
            conexao.Close();
            conexao.Dispose();
            /*--------------------------------------------------------------------*/


            //Se a Tipologia for extraida transpor dados para um objeto tipo ItemLista
            ItemLista itemLista = new ItemLista();
            if (dtLocalizacao.Rows.Count > 0)
            {
                DataRow linha = dtLocalizacao.Rows[0];
                itemLista.Designacao = linha["localizacao"].ToString(); //Passagem da info da BD para o ItemList
            }
            return itemLista;
        }
        /*=========================================================================================================================*/
    }
}
