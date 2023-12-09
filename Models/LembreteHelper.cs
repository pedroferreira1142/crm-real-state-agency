using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace projetoCurricular_v002.Models
{
    public class LembreteHelper
    {
        private string _ligacaoBD = "";

        public LembreteHelper(string strConexao)
        {
            _ligacaoBD = strConexao;
        }


        
        /*============================================== INSERÇÃO DE UM LEMBRETE  ===============================================*/
        public void inserir(Lembrete l)
        {
            /*----------- Conexão com a BD -----------*/
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            comando.CommandType = CommandType.Text;

            /*----------- Comando INSERT para inserção de dados na tabela BD -----------*/
            comando.CommandText = "INSERT INTO tblLembrete (uid, titulo, descricao, dataLembrete, estado, idCliente, idTipoLembrete)" +
                        " VALUES (@uid, @titulo, @descricao, @dataLembrete, @estado, @idCliente, @idTipoLembrete)";
            l.Uid = Guid.NewGuid(); //Criação do Uid
            comando.Parameters.AddWithValue("@uid", l.Uid);
            comando.Parameters.AddWithValue("@titulo", l.Titulo);
            comando.Parameters.AddWithValue("@descricao", l.Descricao);
            comando.Parameters.AddWithValue("@dataLembrete", l.DataLembrete);
            comando.Parameters.AddWithValue("@estado", 1);
            comando.Parameters.AddWithValue("@idCliente", l.IdCliente);
            comando.Parameters.AddWithValue("@idTipoLembrete", l.IdTipoLembrete);

            comando.Connection = conexao;
            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }
        /*========================================================================================================================*/


        /*================================================ LISTAGEM DE LEMBRETES =================================================*/
        public List<Lembrete> getList()
        {
            /*----------- DataTable e Lista de Clientes -----------*/
            DataTable dtLembrete = new DataTable();
            List<Lembrete> listaSaida = new List<Lembrete>();
            /*-----------------------------------------------------*/

            /*----------------- Conexão com a BD -----------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;
            /*----------------------------------------------------*/

            /*----------------- Comando SELECT na BD ------------------*/
            comando.CommandText = "SELECT * FROM tblLembrete";
            /*---------------------------------------------------------*/

            /*---- Conclusão da ligação e passagem de dados para a DT ----*/
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtLembrete);
            conexao.Close();
            conexao.Dispose();
            /*------------------------------------------------------------*/

            /*------------------------ Iteração para cada linha da DataTable ----------------------*/
            foreach (DataRow linha in dtLembrete.Rows)
            {
                if (dtLembrete.Rows.Count > 0)
                {
                    Lembrete lembrete = new Lembrete();
                    lembrete.Uid = Guid.Parse(linha["uid"].ToString());
                    lembrete.IdCliente = Guid.Parse(linha["idCliente"].ToString());
                    lembrete.IdTipoLembrete = Guid.Parse(linha["idTipoLembrete"].ToString());
                    lembrete.Titulo = linha["titulo"].ToString();
                    lembrete.Descricao = linha["descricao"].ToString();
                    lembrete.DataLembrete = Convert.ToDateTime(linha["dataLembrete"]);
                    lembrete.Estado = Convert.ToByte(linha["estado"]);

                    listaSaida.Add(lembrete);
                }
            }
            /*-------------------------------------------------------------------------------------*/

            return listaSaida;
        }
        /*===========================================================================================================================*/


        /*================================================ LISTAGEM DE LEMBRETES POR ESTADO =================================================*/
        public List<Lembrete> getList(int estadoAVer)
        {
            /*----------- DataTable e Lista de Clientes -----------*/
            DataTable dtLembrete = new DataTable();
            List<Lembrete> listaSaida = new List<Lembrete>();
            /*-----------------------------------------------------*/

            /*----------------- Conexão com a BD -----------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;
            /*----------------------------------------------------*/

            /*----------------- Comando SELECT na BD ------------------*/
            comando.CommandText = "SELECT * FROM tblLembrete " +
                " WHERE estado = @status";
            comando.Parameters.AddWithValue("@status", estadoAVer);
            /*---------------------------------------------------------*/

            /*---- Conclusão da ligação e passagem de dados para a DT ----*/
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtLembrete);
            conexao.Close();
            conexao.Dispose();
            /*------------------------------------------------------------*/

            /*------------------------ Iteração para cada linha da DataTable ----------------------*/
            foreach (DataRow linha in dtLembrete.Rows)
            {
                if (dtLembrete.Rows.Count > 0)
                {
                    Lembrete lembrete = new Lembrete();
                    lembrete.Uid = Guid.Parse(linha["uid"].ToString());
                    lembrete.IdCliente = Guid.Parse(linha["idCliente"].ToString());
                    lembrete.IdTipoLembrete = Guid.Parse(linha["idTipoLembrete"].ToString());
                    lembrete.Titulo = linha["titulo"].ToString();
                    lembrete.Descricao = linha["descricao"].ToString();
                    lembrete.DataLembrete = Convert.ToDateTime(linha["dataLembrete"]);
                    lembrete.Estado = Convert.ToByte(linha["estado"]);

                    listaSaida.Add(lembrete);
                }
            }
            /*-------------------------------------------------------------------------------------*/

            return listaSaida;
        }
        /*===========================================================================================================================*/


        /*==================================================== RETIRAR UM LEMBRETE =================================================*/
        public Lembrete getLembrete(Guid idLembreteAVer)
        {
            /*----------- DataTable e Lista de Clientes -----------*/
            DataTable dtLembrete = new DataTable();
            /*-----------------------------------------------------*/

            /*------------------ Conexão com a BD ------------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;
            /*------------------------------------------------------*/

            /*----------------------------- Comando SELECT na BD -----------------------------*/
            comando.CommandText = "SELECT * FROM tblLembrete" +
                " WHERE uid = @reminder";
            comando.Parameters.AddWithValue("@reminder", idLembreteAVer);
            /*---------------------------------------------------------------------------------*/

            /*-- Passagem de dados e conclusão da ligação --*/
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtLembrete);
            conexao.Close();
            conexao.Dispose();
            /*----------------------------------------------*/

            //Criação do cliente
            Lembrete l = new Lembrete();

            if (dtLembrete.Rows.Count > 0)
            {
                DataRow linha = dtLembrete.Rows[0];
                l.Uid = Guid.Parse(linha["uid"].ToString());
                l.Titulo = linha["titulo"].ToString();
                l.Descricao = linha["descricao"].ToString();
                l.DataLembrete = Convert.ToDateTime(linha["dataLembrete"]);
                l.IdCliente = Guid.Parse(linha["idCliente"].ToString());
                l.Estado = Convert.ToInt32(linha["estado"]);
                l.IdCliente = Guid.Parse(linha["idCliente"].ToString());
                l.IdTipoLembrete = Guid.Parse(linha["idTipoLembrete"].ToString());
            }
            return l;
        }
        /*============================================================================================================================*/


        /*================================================= SELECT LEMBRETE POR ID =================================================*/
        public List<Lembrete> getLembretePorIdCliente(Guid idClienteAVer)
        {
            /*----------- DataTable e Lista de Clientes -----------*/
            DataTable dtLembrete = new DataTable();
            List<Lembrete> listaSaida = new List<Lembrete>();
            /*-----------------------------------------------------*/

            /*----------- Conexão com a BD ----------------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;
            /*---------------------------------------------------*/

            /*----------------------------- Seleção do lembrete ----------------------------*/
            comando.CommandText = "SELECT * FROM tblLembrete" +
                " WHERE idCliente = @client";
            comando.Parameters.AddWithValue("@client", idClienteAVer);
            /*-------------------------------------------------------------------------------*/

            /*----- Passagem de dados para a DataTable e conclusão da ligação -----*/
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtLembrete);
            conexao.Close();
            conexao.Dispose();
            /*---------------------------------------------------------------------*/

            //Para cada linha da DataTable
            foreach (DataRow linha in dtLembrete.Rows)
            {
                Lembrete lembrete = new Lembrete();

                lembrete.DataLembrete = Convert.ToDateTime(linha["dataLembrete"]);
                lembrete.Titulo = linha["titulo"].ToString();
                lembrete.Descricao = linha["descricao"].ToString();
                lembrete.Estado = Convert.ToInt32(linha["estado"]);
                lembrete.Uid = Guid.Parse(linha["uid"].ToString());
                lembrete.IdCliente = Guid.Parse(linha["idCliente"].ToString());
                lembrete.IdTipoLembrete = Guid.Parse(linha["idTipoLembrete"].ToString());

                listaSaida.Add(lembrete);
            }
            return listaSaida;
        }
        /*============================================================================================================================*/


        /*============================================== SELECT CLIENTE ANIVERSARIANTE ==============================================*/
        public List<Lembrete> getLembreteClienteAniversariante(Guid idClienteAVer)
        {
            /*----------- DataTable e Lista de Clientes -----------*/
            DataTable dtLembrete = new DataTable();
            List<Lembrete> listaSaida = new List<Lembrete>();
            /*-----------------------------------------------------*/

            /*----------- Conexão com a BD ----------------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;
            /*---------------------------------------------------*/

            /*----------------------------- Seleção do lembrete ----------------------------*/
            comando.CommandText = "SELECT * FROM (tblLembrete" +
                " INNER JOIN tblCliente ON tblLembrete.idCliente = tblCliente.uid)" +
                " WHERE tblCliente.uid = @idCliente";

            comando.Parameters.AddWithValue("@idCliente", idClienteAVer);
            /*-------------------------------------------------------------------------------*/

            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtLembrete);
            conexao.Close();
            conexao.Dispose();

            //Para cada linha da DataTable
            foreach (DataRow linha in dtLembrete.Rows)
            {
                Lembrete lembrete = new Lembrete();
                lembrete.DataLembrete = Convert.ToDateTime(linha["dataLembrete"]);
                lembrete.IdCliente = Guid.Parse(linha["idCliente"].ToString());
                listaSaida.Add(lembrete);
            }
            return listaSaida;
        }
        /*============================================================================================================================*/


        /*================================================== CRIAR LEMBRETE ANIVERSARIO ==================================================*/
        public void CriarLembreteAniversario()
        {
            ClienteHelper clienteHelper = new ClienteHelper(Program._ligacao);
            LembreteHelper lembreteHelper = new LembreteHelper(Program._ligacao);
            TipoLembreteHelper tipoLembreteHelper = new TipoLembreteHelper(Program._ligacao);
            Lembrete lembreteCliente = new Lembrete();

            //Para cada cliente aniversariante
            foreach (Cliente cliente in clienteHelper.getClientesAniversariantes())
            {
                // Se o cliente não está nos lembretes ou o lembrete ainda não está criado
                if (!clienteHelper.oClienteEstaNosLembretes(cliente.Uid) || clienteHelper.oLembreteClienteJaEstaCriado(cliente.Uid))
                {
                    lembreteCliente.Estado = 1;
                    lembreteCliente.IdCliente = cliente.Uid;
                    lembreteCliente.Titulo = $"Aniversário de {cliente.Nome}";
                    lembreteCliente.Descricao = $"Hoje é o aniversário de {cliente.Nome}, dá-lhe os parabéns";
                    lembreteCliente.IdTipoLembrete = tipoLembreteHelper.getList("aniversario").Uid;
                    lembreteCliente.DataLembrete = DateTime.Now.Date;

                    lembreteHelper.inserir(lembreteCliente);//Inserção do lembrete
                }

            }
        }
        /*============================================================================================================================*/



        /*=============================================== EDITAR LEMBRETE =========================================================*/
        public void atualizar(Lembrete l)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            comando.CommandType = CommandType.Text;

            comando.CommandText = "UPDATE tblLembrete SET titulo = @titulo, " +
                                    " descricao = @descricao, " +
                                    " dataLembrete = @dataLembrete " +
                                    " WHERE uid = @uid";

            comando.Parameters.AddWithValue("@uid", l.Uid);
            comando.Parameters.AddWithValue("@titulo", l.Titulo);
            comando.Parameters.AddWithValue("@descricao", l.Descricao);
            comando.Parameters.AddWithValue("@dataLembrete", l.DataLembrete);

            comando.Connection = conexao;
            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }
        /*==========================================================================================================================*/


        /*======================================== REMOVER LEMBRETE ==============================================================*/
        public void remover(Guid uidLembreteARemover)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            comando.CommandType = CommandType.Text;

            comando.CommandText = "DELETE FROM tblLembrete " +
                " WHERE uid = @uid";
            comando.Parameters.AddWithValue("@uid", uidLembreteARemover);

            comando.Connection = conexao;
            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }
        /*=======================================================================================================================*/

        /*=========================================== CONCLUIR LEMBRETE =============================================================*/
        public void concluir(Guid uidLembreteAAtivar)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            comando.CommandType = CommandType.Text;

            comando.CommandText = "UPDATE tblLembrete SET estado = 0 WHERE uid = @uid";
            comando.Parameters.AddWithValue("@uid", uidLembreteAAtivar);

            comando.Connection = conexao;
            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }
        /*=======================================================================================================================*/

    }
}
