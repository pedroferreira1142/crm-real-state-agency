using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace projetoCurricular_v002.Models
{
    public class TipoClienteHelper
    {
        private string _ligacaoBD = "";

        public TipoClienteHelper(string strConexao)
        {
            _ligacaoBD = strConexao;
        }

        /*============================================ EXTRAIR TODOS OS TIPOS DE CLIENTE =============================================*/
        public List<ItemLista> getList()
        {
            DataTable dtTipos = new DataTable();
            //Instancimento de uma lista de objetos tipo ItemLista
            List<ItemLista> listaSaida = new List<ItemLista>();

            //Criada a conexão com a BD
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;
            comando.CommandText = "SELECT * FROM tblTipoCliente ORDER BY tipoCliente";
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtTipos); 
            conexao.Close();
            conexao.Dispose();

            //Para cada linha do DATATABLE é criada uma linha com o respectivo uid e designação
            foreach (DataRow linha in dtTipos.Rows)
            {
                ItemLista l = new ItemLista();
                l.Uid = Guid.Parse(linha["uid"].ToString());
                l.Designacao = linha["tipoCliente"].ToString();
                listaSaida.Add(l);
            }
            return listaSaida;
        }
        /*=============================================================================================================================*/


        /*=============================================== EXTRAIR UM TIPO DE CLIENTE ==================================================*/
        public ItemLista getTipoCliente(Guid idClienteAVer)
        {
            DataTable dtTipoCliente = new DataTable();

            /*--------------------- Criada a conexão com a BD ---------------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            /*---------------------------------------------------------------------*/

            /*--------------------- Comando Select do TipoCliente ---------------------*/
            telefone.SelectCommand = comando;
            comando.CommandText = "SELECT tipoCliente FROM tblTipoCliente" +
                " INNER JOIN tblCliente ON tblTipoCliente.uid = tblCliente.idTipoCliente" +
                " WHERE tblCliente.uid = @client";
            comando.Parameters.AddWithValue("@client", idClienteAVer);
            /*-------------------------------------------------------------------------*/

            /*------------- Passagem de dados e conclusão da ligação -------------*/
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            
            telefone.Fill(dtTipoCliente);
            conexao.Close();
            conexao.Dispose();
            /*--------------------------------------------------------------------*/


            //Se o TipoCliente for extraido transpor dados para um objeto tipo ItemLista
            ItemLista itemLista = new ItemLista();
            if (dtTipoCliente.Rows.Count > 0)
            {
                DataRow linha = dtTipoCliente.Rows[0];
                itemLista.Designacao = linha["tipoCliente"].ToString(); //Passagem da info da BD para o ItemList
            }
            return itemLista;
        }
        /*=============================================================================================================================*/


    }
}
