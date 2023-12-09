using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using projetoCurricular_v002.Models;

namespace projetoCurricular_v002.Controllers
{
    public class ClienteController : Controller
    {
        TipologiaHelper tipologiaHelper = new TipologiaHelper(Program._ligacao);
        ClienteHelper clienteHelper = new ClienteHelper(Program._ligacao);
        TransacaoHelper transacaoHelper = new TransacaoHelper(Program._ligacao);
        TipoClienteHelper tipoClienteHelper = new TipoClienteHelper(Program._ligacao);
        LocalizacaoHelper localizacaoHelper = new LocalizacaoHelper(Program._ligacao);
        LembreteHelper lembreteHelper = new LembreteHelper(Program._ligacao);
        TipoLembreteHelper tipoLembreteHelper = new TipoLembreteHelper(Program._ligacao);

        //Lista de objetos TipoCliente
        List<ItemLista> TipoClienteL;
        //Lista de objetos Localizacao
        List<ItemLista> LocalizacaoL;
        //Lista de objetos Cliente
        List<Cliente> Contentor;

        /*=================================================== LISTAGEM DE CLIENTES ===============================================*/
        public IActionResult Listar(string id)
        {
            //int estadoDoCliente = (id == "0") ? 0 : 1;
            int estadoDoCliente = 0;
            if (id == "0") estadoDoCliente = 0;
            else estadoDoCliente = 1;

            Contentor = clienteHelper.getList(estadoDoCliente);
            return View(Contentor);
        }
        /*========================================================================================================================*/


        /*=================================================== MOSTRAR UM CLIENTE ===============================================*/
        [HttpGet]
        public IActionResult Cliente(Guid id)
        {
            /*----------------------- Cliente -----------------------*/
            ViewBag.Cliente = clienteHelper.getCliente(id);
            /*-------------------------------------------------------*/

            /*----------------------- Transações do Cliente -----------------------*/
            ViewBag.Transacao = transacaoHelper.getList(id);
            /*---------------------------------------------------------------------*/

            /*----------------------- Tipo de Cliente -----------------------*/
            ViewBag.TipoCliente = tipoClienteHelper.getTipoCliente(id);
            /*---------------------------------------------------------------*/

            /*----------------------- Tipologia Pretendida -----------------------*/
            ViewBag.TipologiaClienteId = tipologiaHelper.getTipologiaCliente(id);
            ViewBag.TipologiaLista = tipologiaHelper.getList();
            /*--------------------------------------------------------------------*/

            /*----------------------- Localizacao Pretendida -----------------------*/
            ViewBag.Localizacao = localizacaoHelper.getLocalizacao(id);
            ViewBag.LocalizacaoLista = localizacaoHelper.getList();
            /*----------------------------------------------------------------------*/

            /*------------------------------ Lembretes -----------------------------*/
            ViewBag.Lembretes = lembreteHelper.getLembretePorIdCliente(id);
            ViewBag.TipoLembrete = tipoLembreteHelper.getList();
            ViewBag.TipoLembreteOpcional = tipoLembreteHelper.getList("Opcional");
            /*----------------------------------------------------------------------*/

            return View();
        }
        /*======================================================================================================================*/


        /*=================================================== INSERÇÃO DE UM CLIENTE ===============================================*/
        [HttpGet]   //Retorna o formulário vazio
        public IActionResult Criar()
        {
            /*----------------------- Lista de Tipos de Cliente -----------------------*/
            
            ViewBag.TipoCliente = tipoClienteHelper.getList();
            /*--------------------------------------------------------------------------*/


            /*------------------------- Lista de Localizações ----------------------*/
            ViewBag.Localizacao = localizacaoHelper.getList();
            /*----------------------------------------------------------------------*/


            /*------------------------- Lista de Tipologia -------------------------*/
            ViewBag.Tipologia = tipologiaHelper.getList();
            /*-----------------------------------------------------------------------*/

            return View();
        }

        [HttpPost]
        public IActionResult Criar(Cliente clienteACriar)
        {
            /*------------------------- Recebe info da inserida na view e envia para a BD -------------------------*/
            string erros = "";
            if (ModelState.IsValid)
            {
                //Comparação entre o UID criado e o UID vazio
                if (clienteACriar.Uid.ToString() == Program.EmptyUID)
                {
                    clienteHelper.inserir(clienteACriar);
                }
                else erros = "Objeto já tem uid válido?";


            }
            else erros = "Preencha campos <b>corretamente</b>";

            /*----- Senão tiver erros redireciona para a home page, caso contrárior retorna os erros -----*/
            if (erros == "") return RedirectToAction("Listar", "Cliente");
            return Content(erros, "text/html");
        }
        /*===========================================================================================================================*/


        /*=================================================== EDIÇÃO DE UM CLIENTE ===============================================*/
        [HttpGet]
        public IActionResult Editar (Guid id)
        {
            /*----------------------- Lista de Tipos de Cliente -----------------------*/
            ViewBag.TipoCliente = tipoClienteHelper.getList();
            /*--------------------------------------------------------------------------*/


            /*------------------------- Lista de Localizações ----------------------*/
            ViewBag.Localizacao = localizacaoHelper.getList();
            /*----------------------------------------------------------------------*/


            /*------------------------- Lista de Tipologia -------------------------*/
            ViewBag.Tipologia = tipologiaHelper.getList();
            /*-----------------------------------------------------------------------*/


            /*------------------------- Cliente -------------------------*/
            Cliente clienteLido = clienteHelper.getCliente(id);
            /*-----------------------------------------------------------------------*/

            return View(clienteLido);
        }

        [HttpPost]
        public IActionResult Editar(Cliente clienteAEditar)
        {
            string erros = "";
            if (ModelState.IsValid)
            {
                //Comparação entre o UID criado e o UID vazio
                if (clienteAEditar.Uid.ToString() != Program.EmptyUID)
                {
                    clienteHelper.atualizar(clienteAEditar);
                }
                else erros = "O Objeto tem uid valido?";


            }
            else erros = "Preencha campos <b>corretamente</b>";

            /*----- Senão tiver erros redireciona para a listagem de clientes, caso contrário retorna os erros -----*/
            if (erros == "") return RedirectToAction("Listar", "Cliente");
            return Content(erros, "text/html");
        }
        /*========================================================================================================================*/


        /*============================================= ATIVAR E DESATIVAR CLIENTE ================================================*/
        public IActionResult Ativar(Guid id)
        {
            clienteHelper.ativar(id);
            return RedirectToAction("Listar", "Cliente");
        }

        public IActionResult Desativar(Guid id)
        {
            clienteHelper.desativar(id);
            return RedirectToAction("Listar", "Cliente");
        }
        /*=========================================================================================================================*/

        /*================================================== REMOVER CLIENTE =====================================================*/
        public IActionResult Remover(Guid id)
        {
            clienteHelper.remover(id);
            return RedirectToAction("Listar", "Cliente");
        }
        /*========================================================================================================================*/







    }


}