using Microsoft.AspNetCore.Mvc;
using projetoCurricular_v002.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetoCurricular_v002.Controllers
{
    public class TransacaoController : Controller
    {

        TipologiaHelper tipologiaHelper = new TipologiaHelper(Program._ligacao);
        ClienteHelper clienteHelper = new ClienteHelper(Program._ligacao);
        TransacaoHelper transacaoHelper = new TransacaoHelper(Program._ligacao);
        TipoClienteHelper tipoClienteHelper = new TipoClienteHelper(Program._ligacao);
        LocalizacaoHelper localizacaoHelper = new LocalizacaoHelper(Program._ligacao);

        //Lista de objetos TipoCliente
        List<ItemLista> TipoClienteL;
        //Lista de objetos Localizacao
        List<ItemLista> LocalizacaoL;
        //Lista de objetos Cliente
        List<Cliente> Contentor;
        //Lista de objetos Tipologia
        List<ItemLista> TipologiaL;

        /*================================================== Inserir Transação ======================================================*/
        [HttpGet]
        public IActionResult Criar(Guid id)
        {
            /*------------------------- Lista de Localizações ----------------------*/
            ViewBag.Localizacao = localizacaoHelper.getList();
            /*----------------------------------------------------------------------*/


            /*------------------------- Lista de Tipologias -------------------------*/
            ViewBag.Tipologia = tipologiaHelper.getList();
            /*-----------------------------------------------------------------------*/

            /*----------------------------------- Cliente ------------------------------------*/
            ViewBag.Cliente = clienteHelper.getCliente(id);//Cliente onde vai ser criada a transação
            /*--------------------------------------------------------------------------------*/

            return View();
        }

        [HttpPost]
        public IActionResult Criar(Transacao transacaoACriar)
        {
            string erros = "";
            if (ModelState.IsValid)
            {
                //Comparação entre o UID criado e o UID vazio
                if (transacaoACriar.Uid.ToString() == Program.EmptyUID)
                {
                    transacaoHelper.inserir(transacaoACriar);
                }
                else erros = "O Objeto já tem id válido?";


            }
            else erros = "Preencha campos <b>corretamente</b>";

            /*----- Senão tiver erros redireciona para a home page, caso contrário retorna os erros -----*/
            if (erros == "") return RedirectToAction("Cliente", "Cliente", new { id = transacaoACriar.IdCliente}); //Redireciona para o mesmo cliente
            return Content(erros, "text/html");
        }
        /*===========================================================================================================================*/


        /*================================================== EDITAR Transação ======================================================*/
        [HttpGet]
        public IActionResult Editar(Guid id)
        {
            /*------------------------------ Transação -----------------------------*/
            Transacao transacaoLida = transacaoHelper.getTransacao(id);
            /*----------------------------------------------------------------------*/

            /*------------------------- Lista de Localizações ----------------------*/
            ViewBag.Localizacao = localizacaoHelper.getList();
            /*----------------------------------------------------------------------*/


            /*------------------------- Lista de Tipologia -------------------------*/
            ViewBag.Tipologia = tipologiaHelper.getList();
            /*-----------------------------------------------------------------------*/

            /*------------------------- Cliente do Lembrete -------------------------*/
            ViewBag.Cliente = clienteHelper.getCliente(transacaoLida.IdCliente);
            /*-----------------------------------------------------------------------*/

            return View(transacaoLida);
        }

        [HttpPost]
        public IActionResult Editar(Transacao transacaoAEditar)
        {
            string erros = "";
            if (ModelState.IsValid)
            {
                //Comparação entre o UID criado e o UID vazio
                if (transacaoAEditar.Uid.ToString() != Program.EmptyUID)
                {
                    transacaoHelper.atualizar(transacaoAEditar);
                }
                else erros = "O Objeto tem uid valido?";


            }
            else erros = "Preencha campos <b>corretamente</b>";

            /*----- Senão tiver erros redireciona para a listagem de clientes, caso contrário retorna os erros -----*/
            if (erros == "") return RedirectToAction("Cliente", "Cliente", new { id = transacaoAEditar.IdCliente });

            return Content(erros, "text/html");
        }
        /*==========================================================================================================================*/


        /*================================================== REMOVER TRANSAÇÃO ======================================================*/
        public IActionResult Remover(Guid id)
        {
            Guid idTransacao = transacaoHelper.getTransacao(id).IdCliente;
            transacaoHelper.remover(id);
            return RedirectToAction("Cliente", "Cliente", new { id = idTransacao });
        }
        /*===========================================================================================================================*/
    }
}
