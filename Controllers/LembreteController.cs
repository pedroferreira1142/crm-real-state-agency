using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projetoCurricular_v002.Models;

namespace projetoCurricular_v002.Controllers
{
    public class LembreteController : Controller
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
        //Lista de Objetos Lembrete
        List<Lembrete> ContentorLembrete;


        /*=================================================== LISTAGEM DE LEMBRETES ===============================================*/
        public IActionResult Listar(string id)
        {
            int estadoDoLembrete = 0;
            if (id == "0") estadoDoLembrete = 0;
            else estadoDoLembrete = 1;

            /*----------------------- Tipo de Lembrete -----------------------*/
            ViewBag.TipoLembrete = tipoLembreteHelper.getList();
            ViewBag.TipoLembreteOpcional = tipoLembreteHelper.getList("Opcional");
            /*----------------------------------------------------------------*/

            /*----------------------- Cliente -----------------------*/
            ViewBag.Cliente = clienteHelper.getList(1);
            /*-------------------------------------------------------*/

            ContentorLembrete = lembreteHelper.getList(estadoDoLembrete);
            return View(ContentorLembrete);
        }
        /*=========================================================================================================================*/


        /*================================================== INSERIR LEMBRETE ======================================================*/
        [HttpGet]
        public IActionResult Criar(Guid id)
        {

            /*------------------------- Lista de Tipos de Lembrete -------------------------*/
            ViewBag.TipoLembrete = tipoLembreteHelper.getList("Opcional");
            /*------------------------------------------------------------------------------*/

            /*----------------------------------- Cliente ------------------------------------*/
            ViewBag.Cliente = clienteHelper.getCliente(id);//Cliente onde vai ser criada a transação
            /*--------------------------------------------------------------------------------*/

            return View();
        }

        [HttpPost]
        public IActionResult Criar(Lembrete lembreteACriar)
        {
            string erros = "";
            if (ModelState.IsValid)
            {
                //Comparação entre o UID criado e o UID vazio
                if (lembreteACriar.Uid.ToString() == Program.EmptyUID)
                {
                    lembreteHelper.inserir(lembreteACriar);
                }
                else erros = "O Objeto já tem id válido?";


            }
            else erros = "Preencha campos <b>corretamente</b>";

            /*----- Senão tiver erros redireciona para a home page, caso contrário retorna os erros -----*/
            if (erros == "") return RedirectToAction("Cliente", "Cliente", new { id = lembreteACriar.IdCliente }); //Redireciona para o mesmo cliente
            return Content(erros, "text/html");
        }
        /*===========================================================================================================================*/


        /*================================================== EDITAR LEMBRETE ======================================================*/
        [HttpGet]
        public IActionResult Editar(Guid id)
        {
            /*------------------------------ Lembrete -----------------------------*/
            Lembrete lembreteLido = lembreteHelper.getLembrete(id);
            /*----------------------------------------------------------------------*/

            /*------------------------- Lista de Tipos de Lembrete -------------------------*/
            ViewBag.TipoLembrete = tipoLembreteHelper.getList();
            /*-----------------------------------------------------------------------------*/

            /*------------------------- Cliente do Lembrete -------------------------*/
            ViewBag.Cliente = clienteHelper.getCliente(lembreteLido.IdCliente);
            /*-----------------------------------------------------------------------*/

            return View(lembreteLido);
        }

        [HttpPost]
        public IActionResult Editar(Lembrete lembreteAEditar)
        {
            string erros = "";
            if (ModelState.IsValid)
            {
                //Comparação entre o UID criado e o UID vazio
                if (lembreteAEditar.Uid.ToString() != Program.EmptyUID)
                {
                    lembreteHelper.atualizar(lembreteAEditar);
                }
                else erros = "O Objeto tem uid valido?";


            }
            else erros = "Preencha campos <b>corretamente</b>";

            /*----- Senão tiver erros redireciona para a listagem de clientes, caso contrário retorna os erros -----*/
            if (erros == "") return RedirectToAction("Listar", "Lembrete");

            return Content(erros, "text/html");
        }
        /*==========================================================================================================================*/


        /*================================================== REMOVER LEMBRETE ======================================================*/
        public IActionResult Remover(Guid id)
        {
            Guid idLembreteCliente = lembreteHelper.getLembrete(id).IdCliente;
            lembreteHelper.remover(id);
            return RedirectToAction("Cliente", "Cliente", new { id = idLembreteCliente });
        }
        /*===========================================================================================================================*/


        /*============================================= CONCLUIR LEMBRETE ================================================*/
        public IActionResult Concluir(Guid id)
        {
            Guid idClienteLembrete = lembreteHelper.getLembrete(id).IdCliente;
            lembreteHelper.concluir(id);
            return RedirectToAction("Cliente", "Cliente", new { id = idClienteLembrete });
        }
        /*================================================================================================================*/
    }
}
