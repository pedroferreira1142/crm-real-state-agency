using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace projetoCurricular_v002.Models
{
    public class ClienteHelper
    {
        private string _ligacaoBD = "";

        public ClienteHelper(string strConexao)
        {
            _ligacaoBD = strConexao;
        }

        /*=================================================== INSERÇÃO DE UM CLIENTE ===============================================*/
        public void inserir(Cliente c)
        {
            /*----------- Conexão com a BD -----------*/
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            comando.CommandType = CommandType.Text;

            /*----------- Comando INSERT para inserção de dados na tabela BD -----------*/
            comando.CommandText = "INSERT INTO tblCliente (uid, nome, contacto, eMail, dataNascimento, dataCriacao, observacoes, precoPretendido, idLocalizacao, idTipoCliente, idTipologia, estado) " +
                        " VALUES (@uid, @nome, @contacto, @eMail, @dataNascimento, @dataCriacao, @observacoes, @precoPretendido, @idLocalizacao, @idTipoCliente, @idTipologia, @estado)";
            c.Uid = Guid.NewGuid(); //Criação do Uid
            comando.Parameters.AddWithValue("@uid", c.Uid);
            comando.Parameters.AddWithValue("@idLocalizacao", c.UidLocalizacao);
            comando.Parameters.AddWithValue("@idTipoCliente", c.UidTipoCliente);
            comando.Parameters.AddWithValue("@idTipologia", c.UidTipologia);
            comando.Parameters.AddWithValue("@nome", c.Nome);
            comando.Parameters.AddWithValue("@contacto", c.Contacto);
            comando.Parameters.AddWithValue("@eMail", c.EMail);
            comando.Parameters.AddWithValue("@dataCriacao", DateTime.Now);
            comando.Parameters.AddWithValue("@dataNascimento", c.DataNascimento);
            comando.Parameters.AddWithValue("@observacoes", c.Observacoes);
            comando.Parameters.AddWithValue("@precoPretendido", c.PrecoPretendido);
            comando.Parameters.AddWithValue("@estado", 1);

            comando.Connection = conexao;
            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }
        /*============================================================================================================================*/


        /*==================================================== LISTAGEM DOS CLIENTES =================================================*/
        public List<Cliente> getList()
        {
            /*----------- DataTable e Lista de Clientes -----------*/
            DataTable dtClientes = new DataTable();
            List<Cliente> listaSaida = new List<Cliente>();


            /*----------- Conexão com a BD -----------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;

            /*---------------------- Comando SELECT na BD ----------------------*/
            comando.CommandText = "SELECT * FROM (((tblCliente" +
                " INNER JOIN tblTipoCliente ON tblCliente.idTipoCliente = tblTipoCliente.uid)" +
                " INNER JOIN tblTipologia ON tblCliente.idTipologia = tblTipologia.uid)" +
                " INNER JOIN tblLocalizacao ON tblCliente.idLocalizacao = tblLocalizacao.uid)";

            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtClientes);
            conexao.Close();
            conexao.Dispose();
            
            //Para cada linha da DataTable
            foreach (DataRow linha in dtClientes.Rows)
            {
                Cliente c = new Cliente();
                c.Uid = Guid.Parse(linha["uid"].ToString());
                c.Nome = linha["nome"].ToString();
                c.Contacto = linha["contacto"].ToString();
                c.PrecoPretendido = Convert.ToDecimal(linha["precoPretendido"]);
                c.Localizacao = linha["localizacao"].ToString();
                c.TipoCliente = linha["tipoCliente"].ToString();
                c.Tipologia = linha["tipologia"].ToString();
                listaSaida.Add(c);
            }
            return listaSaida;
        }
        /*============================================================================================================================*/


        /*==================================================== LISTAGEM DOS CLIENTES ID =================================================*/
        public List<Cliente> getList(int estadoAVer)
        {
            /*----------- DataTable e Lista de Clientes -----------*/
            DataTable dtClientes = new DataTable();
            List<Cliente> listaSaida = new List<Cliente>();


            /*----------- Conexão com a BD -----------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;

            /*---------------------- Comando SELECT na BD ----------------------*/
            comando.CommandText = "SELECT * FROM (((tblCliente" +
                " INNER JOIN tblTipoCliente ON tblCliente.idTipoCliente = tblTipoCliente.uid)" +
                " INNER JOIN tblTipologia ON tblCliente.idTipologia = tblTipologia.uid)" +
                " INNER JOIN tblLocalizacao ON tblCliente.idLocalizacao = tblLocalizacao.uid) " +
                " WHERE estado = @status";
            comando.Parameters.AddWithValue("@status", estadoAVer);

            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtClientes);
            conexao.Close();
            conexao.Dispose();

            //Para cada linha da DataTable
            foreach (DataRow linha in dtClientes.Rows)
            {
                Cliente c = new Cliente();
                c.Uid = Guid.Parse(linha["uid"].ToString());
                c.Nome = linha["nome"].ToString();
                c.Contacto = linha["contacto"].ToString();
                c.PrecoPretendido = Convert.ToDecimal(linha["precoPretendido"]);
                c.Localizacao = linha["localizacao"].ToString();
                c.TipoCliente = linha["tipoCliente"].ToString();
                c.Tipologia = linha["tipologia"].ToString();
                c.Estado = Convert.ToInt32(linha["estado"]);
                listaSaida.Add(c);
            }
            return listaSaida;
        }
        /*============================================================================================================================*/


        /*==================================================== MOSTRAR UM CLIENTE =================================================*/
        public Cliente getCliente(Guid idClienteAVer)
        {
            /*----------- DataTable e Lista de Clientes -----------*/
            DataTable dtCliente = new DataTable();
            /*-----------------------------------------------------*/

            /*------------------ Conexão com a BD ------------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;
            /*------------------------------------------------------*/

            /*----------------------------- Comando SELECT na BD -----------------------------*/
            comando.CommandText = "SELECT * FROM tblCliente" +
                " WHERE uid = @client";
            comando.Parameters.AddWithValue("@client", idClienteAVer);
            /*---------------------------------------------------------------------------------*/

            /*-- Passagem de dados e conclusão da ligação --*/
            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtCliente);
            conexao.Close();
            conexao.Dispose();
            /*----------------------------------------------*/

            //Criação do cliente
            Cliente cliente = new Cliente();
            if (dtCliente.Rows.Count > 0)
            {
                DataRow linha = dtCliente.Rows[0];
                cliente.Uid = Guid.Parse(linha["uid"].ToString());
                cliente.Nome = linha["nome"].ToString();
                cliente.Contacto = linha["contacto"].ToString();
                cliente.EMail = linha["eMail"].ToString();
                cliente.DataNascimento = Convert.ToDateTime(linha["dataNascimento"]);
                cliente.DataCriacao = Convert.ToDateTime(linha["dataCriacao"]);
                cliente.Observacoes = linha["observacoes"].ToString();
                cliente.PrecoPretendido = Convert.ToDecimal(linha["precoPretendido"]);
                cliente.UidLocalizacao = Guid.Parse(linha["idLocalizacao"].ToString());
                cliente.UidTipoCliente = Guid.Parse(linha["idTipoCliente"].ToString());
                cliente.UidTipologia = Guid.Parse(linha["idTipologia"].ToString());
            }
            return cliente;
        }
        /*============================================================================================================================*/


        /*=============================================== SELECT CLIENTE ANIVERSARIANTE ===============================================*/
        public List<Cliente> getClientesAniversariantes()
        {
            /*----------- DataTable e Lista de Clientes -----------*/
            DataTable dtClientes = new DataTable();
            List<Cliente> listaSaida = new List<Cliente>();
            /*-----------------------------------------------------*/

            /*----------- Conexão com a BD ----------------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;
            /*---------------------------------------------------*/

            /*---------------------- Seleção do cliente aniversariante ----------------------*/
            comando.CommandText = "SELECT uid, dataNascimento, nome FROM tblCliente" +
                " WHERE DAY([dataNascimento]) = DAY(GETDATE())" +
                " AND MONTH([dataNascimento]) = MONTH(GETDATE())";
            /*-------------------------------------------------------------------------------*/

            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtClientes);
            conexao.Close();
            conexao.Dispose();

            //Para cada linha da DataTable
            foreach (DataRow linha in dtClientes.Rows)
            {
                Cliente cliente = new Cliente();
                cliente.Uid = Guid.Parse(linha["uid"].ToString());
                cliente.Nome = linha["nome"].ToString();
                cliente.DataNascimento = Convert.ToDateTime(linha["dataNascimento"]);
                listaSaida.Add(cliente);
            }
            return listaSaida;
        }
        /*=========================================================================================================================*/


        /*============================================ TESTAR SE O CLIENTE ESTÁ NOS LEMBRETES ======================================*/
        public bool oClienteEstaNosLembretes(Guid uid)
        {
            /*----------- DataTable e Lista de Clientes -----------*/
            DataTable dtLembrete = new DataTable();
            /*-----------------------------------------------------*/

            /*----------- Conexão com a BD ----------------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;
            /*---------------------------------------------------*/

            /*---------------------- Seleção do cliente aniversariante ----------------------*/
            comando.CommandText = "SELECT * FROM tblLembrete" +
                " WHERE idCliente = @client";
            comando.Parameters.AddWithValue("@client", uid);
            /*-------------------------------------------------------------------------------*/

            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtLembrete);
            conexao.Close();
            conexao.Dispose();

            //Se encontrar o cliente
            if (dtLembrete.Rows.Count > 0) return true;

            return false;
        }
        /*=========================================================================================================================*/


        /*======================================== VERIFICAR SE O CLIENTE JÁ TEM O LEMBRETE CRIADO ================================*/
        public bool oLembreteClienteJaEstaCriado(Guid uid)
        {
            /*----------- DataTable e Lista de Clientes -----------*/
            DataTable dtLembrete = new DataTable();
            /*-----------------------------------------------------*/

            /*----------- Conexão com a BD ----------------------*/
            SqlDataAdapter telefone = new SqlDataAdapter();
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            telefone.SelectCommand = comando;
            /*---------------------------------------------------*/

            /*----------- Seleção do cliente com lembrete criado no próprio dia ------------*/
            comando.CommandText = "SELECT * FROM tblLembrete" +
                " WHERE idCliente = @client" +
                " AND DAY([dataLembrete]) != DAY(GETDATE())" +
                " AND MONTH([dataLembrete]) != MONTH(GETDATE())";
            comando.Parameters.AddWithValue("@client", uid);
            /*-------------------------------------------------------------------------------*/

            comando.CommandType = CommandType.Text;
            comando.Connection = conexao;
            telefone.Fill(dtLembrete);
            conexao.Close();
            conexao.Dispose();

            //Se encontrar o cliente
            if (dtLembrete.Rows.Count > 0) return true;

            return false;
        }
        /*=========================================================================================================================*/


        /*=============================================== EDITAR CLIENTE =========================================================*/
        public void atualizar(Cliente c)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            comando.CommandType = CommandType.Text;
            comando.CommandText = "UPDATE tblCliente SET nome = @nome, " +
                                    " contacto = @contacto, " +
                                    " eMail = @eMail, dataNascimento = @dataNascimento, " +
                                    " observacoes = @observacoes, precoPretendido = @precoPretendido, " +
                                    " idLocalizacao = @idLocalizacao, idTipoCliente = @idTipoCliente, " +
                                    " idTipologia = @idTipologia " +
                                    " WHERE uid = @uid ";
            comando.Parameters.AddWithValue("@uid", c.Uid);
            comando.Parameters.AddWithValue("@nome", c.Nome);
            comando.Parameters.AddWithValue("@contacto", c.Contacto);
            comando.Parameters.AddWithValue("@eMail", c.EMail);
            comando.Parameters.AddWithValue("@dataNascimento", c.DataNascimento);
            comando.Parameters.AddWithValue("@observacoes", c.Observacoes);
            comando.Parameters.AddWithValue("@precoPretendido", c.PrecoPretendido);
            comando.Parameters.AddWithValue("@idLocalizacao", c.UidLocalizacao);
            comando.Parameters.AddWithValue("@idTipoCliente", c.UidTipoCliente);
            comando.Parameters.AddWithValue("@idTipologia", c.UidTipologia);


            comando.Connection = conexao;
            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }
        /*=======================================================================================================================*/


        /*=========================================== ATIVAR CLIENTE =============================================================*/
        public void ativar(Guid uidClienteAAtivar)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            comando.CommandType = CommandType.Text;

            comando.CommandText = "UPDATE tblCliente SET estado = 1 WHERE uid = @uid";
            comando.Parameters.AddWithValue("@uid", uidClienteAAtivar);

            comando.Connection = conexao;
            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }
        /*=======================================================================================================================*/


        /*======================================== DESATIVAR CLIENTE =============================================================*/
        public void desativar(Guid uidClienteAAtivar)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            comando.CommandType = CommandType.Text;

            comando.CommandText = "UPDATE tblCliente SET estado = 0 WHERE uid = @uid";
            comando.Parameters.AddWithValue("@uid", uidClienteAAtivar);

            comando.Connection = conexao;
            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }
        /*=======================================================================================================================*/

        /*======================================== REMOVER CLIENTE ==============================================================*/
        public void remover(Guid uidClienteARemover)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(_ligacaoBD);
            comando.CommandType = CommandType.Text;

            comando.CommandText = "DELETE FROM tblCliente " + 
                " WHERE uid = @uid";
            comando.Parameters.AddWithValue("@uid", uidClienteARemover);

            comando.Connection = conexao;
            conexao.Open();
            comando.ExecuteNonQuery();
            conexao.Close();
            conexao.Dispose();
        }
        /*=======================================================================================================================*/

    }

}
