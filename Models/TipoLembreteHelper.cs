using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace projetoCurricular_v002.Models
{
    public class TipoLembreteHelper
    {
        private string _ligacaoBD = "";

        public TipoLembreteHelper(string strConexao)
        {
            _ligacaoBD = strConexao;
        }

        /*============================================ EXTRAIR TODOS OS TIPOS DE LEMBRETE =============================================*/
        public List<ItemLista> getList()
        {
            DataTable dtTipoLembretes = new DataTable();
            //Instancimento de uma lista de objetos tipo ItemLista
            List<ItemLista> listaSaida = new List<ItemLista>();

            /*----------------------------- Criada a conexão com a BD -----------------------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;

            /*----------------------------- Comando Select -----------------------------*/
            comando.CommandText = "SELECT * FROM tblTipoLembrete ORDER BY tipoLembrete";
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtTipoLembretes);
            conexao.Close();
            conexao.Dispose();

            //Para cada linha do DATATABLE é criada uma linha com o respectivo uid e designação
            foreach (DataRow linha in dtTipoLembretes.Rows)
            {
                ItemLista l = new ItemLista();
                l.Uid = Guid.Parse(linha["uid"].ToString());
                l.Designacao = linha["tipoLembrete"].ToString();
                listaSaida.Add(l);
            }
            return listaSaida;
        }
        /*============================================================================================================================*/


        /*================================================= EXTRAIR UM TIPO DE LEMBRETE =================================================*/
        public ItemLista getList(string tipoLembreteAVer) //Overloading de getList
        {
            DataTable dtTipoLembrete = new DataTable();

            /*----------------------------- Criada a conexão com a BD -----------------------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;

            /*----------------------------- Comando Select -----------------------------*/
            comando.CommandText = "SELECT * FROM tblTipoLembrete WHERE tipoLembrete = @type";
            comando.Parameters.AddWithValue("@type", tipoLembreteAVer);
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtTipoLembrete);
            conexao.Close();
            conexao.Dispose();

            /*--------------- Transposição do DataTable para um ItemLista -----------------*/
            ItemLista itemLista = new ItemLista();
            if (dtTipoLembrete.Rows.Count > 0)
            {
                DataRow row = dtTipoLembrete.Rows[0];
                itemLista.Uid = Guid.Parse(row["uid"].ToString());
            }
            /*-------------------------------------------------------------------------*/

            return itemLista;
        }
        /*============================================================================================================================*/

        

    }
}
