using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrabBimestral.MODEL;
using TrabBimestral.MODEL.Repositories;
using TrabBimestral.VIEW.Models;

namespace TrabBimestral.VIEW.Views
{
    [Authorize]
    public class VendaController : Controller
    {
        private LojaProdutosEntities db = new LojaProdutosEntities();

        RepositoryVenda _RepositoryVenda;
        RepositoryCliente _RepositoryCliente;
        RepositoryVendaProduto _RepositoryVendaProduto;
        RepositoryProduto _RepositoryProduto;
        RepositoryCategoria _RepositoryCateogoria;
        RepositoryFornecedor _RepositoryFornecedor;
        int idVenda = 0;
        List<Venda> ListVenda = null;

        public VendaController()
        {
            _RepositoryCliente = new RepositoryCliente();
            _RepositoryVenda = new RepositoryVenda(_RepositoryCliente.Contexto);
            _RepositoryVendaProduto = new RepositoryVendaProduto(_RepositoryCliente.Contexto);
            _RepositoryProduto = new RepositoryProduto(_RepositoryCliente.Contexto);
            _RepositoryCateogoria = new RepositoryCategoria(_RepositoryCliente.Contexto);
            _RepositoryFornecedor = new RepositoryFornecedor(_RepositoryCliente.Contexto);
        }

        // GET: Venda
        public ActionResult Index(string info = null, string tipo = null)
        {
            if (info == null || info == "")
            {
                var venda = _RepositoryVenda.SelecionarTodos();
                return View(venda);
            }
            else
            {
                busca(info, tipo);
                return View(ListVenda);
            }
           
        }

        public void busca(string info, string tipo)
        {
            ListVenda = null;
            List<Venda> oList = null;

            if (tipo == "Data")
            {
                DateTime oDate = Convert.ToDateTime(info);
                oList = _RepositoryVenda.SelecionarPorData(oDate);
            }
            else if (tipo == "NomeCliente")
            {
                Cliente c = new Cliente();
                c = _RepositoryCliente.SelecionarUnicoPorNome(info);

                oList = _RepositoryVenda.SelecionarPorCliente(c.Cli_ID);
            }
            else if(tipo == "VendaFechada")
            {
                
                bool ven = (info == "false");

                oList = _RepositoryVenda.SelecionarPorVendaFechada(ven);
            }

            ListVenda = oList;
        }

        public ActionResult AutoCompleteCliente(string term)
        {
            List<Cliente> oRetorno = _RepositoryCliente.SelecionarPorNome(term);
            return Json(oRetorno, JsonRequestBehavior.AllowGet);
        }

        // GET: Venda/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Venda venda = _RepositoryVenda.Selecionar(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // GET: Venda/Create
        public ActionResult Create()
        {
            ViewBag.ID_Cli = new SelectList(db.Cliente, "Cli_ID", "Cli_Nome");
            if (idVenda != 0)
            {
                ViewBag.VendaCriada = idVenda;
            }

            ViewBag.ListaProdutos = null;

            return View();
        }



        // POST: Venda/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(VendaView model = null)
        {
            var finalizarVenda = model.Ven_Fechada;
            if (finalizarVenda == false)
            {
                if (model.Ven_ID == 0)
                {
                    int idCliente = model.ID_Cli;

                    Venda venda = new Venda();
                    venda.ID_Cli = idCliente;

                    int ano = DateTime.Today.Year;
                    int mes = DateTime.Today.Month;
                    int dia = DateTime.Today.Day;

                    DateTime dataEntrada = new DateTime(ano, mes, dia);

                    venda.Ven_Data = dataEntrada;
                    int idVendaCriada = _RepositoryVenda.Incluir(venda);
                    idVenda = idVendaCriada;
                    ViewBag.VendaCriada = idVendaCriada;

                    return View();
                }
                else if (model.Ven_ID != 0)
                {
                    if (model.idExcluirProduto == 0)
                    {
                        int qtd = Convert.ToInt32(model.VeP_Qtd);
                        criarVendaProduto(model.Ven_ID, model.Pro_ID, qtd);
                        CalcularValorTotal(model.Ven_ID);
                        return View();
                    }
                    else
                    {
                        removeProd(model.idExcluirProduto);
                        ListaProdutos(model.Ven_ID);
                        ViewBag.VendaCriada = model.Ven_ID;
                        CalcularValorTotal(model.Ven_ID);
                        return View();
                    }
                }
            }
            else if (finalizarVenda == true)
            {
                Venda v = _RepositoryVenda.Selecionar(model.Ven_ID);
                v.Ven_Fechada = true;
                _RepositoryVenda.Alterar(v);


                return RedirectToAction("Index");
            }


            //ViewBag.ID_Cli = new SelectList(db.Cliente, "Cli_ID", "Cli_Nome", venda.ID_Cli);
            return View();
        }

        public void CalcularValorTotal(int id)
        {
            Venda v = _RepositoryVenda.Selecionar(id);

            List<VendaProduto> oListVendaProduto = _RepositoryVendaProduto.SelecionarIdVenda(id);

            decimal valorTotal = 0;
            foreach (var i in oListVendaProduto)
            {
                valorTotal += i.VeP_PrecoVenda;
            }

            ViewBag.ValorTotal = valorTotal;
        }

        public void removeProd(int idVendaProduto)
        {
            if (idVendaProduto != 0)
            {
                _RepositoryVendaProduto.Excluir(idVendaProduto);
            }

        }

        public void criarVendaProduto(int idVendaCriada, int idProduto, int qtd)
        {
            VendaProduto vp = new VendaProduto();
            vp.ID_Venda = idVendaCriada;
            vp.ID_Produto = idProduto;
            vp.VeP_Qtd = qtd;

            Produto oProd = _RepositoryProduto.Selecionar(idProduto);
            vp.VeP_PrecoVenda = oProd.Pro_Preco * qtd;

            _RepositoryVendaProduto.Incluir(vp);

            ListaProdutos(idVendaCriada);

            ViewBag.VendaCriada = idVendaCriada;
        }

        public void ListaProdutos(int idVenda)
        {
            List<VendaProduto> oListVendaProduto = _RepositoryVendaProduto.SelecionarIdVenda(idVenda);
            List<VendaProdutoView> oListVendaProdutoView = new List<VendaProdutoView>();

            foreach (var i in oListVendaProduto)
            {
                int idPro = i.ID_Produto;

                Produto p = new Produto();
                p = _RepositoryProduto.Selecionar(idPro);

                Categoria c = new Categoria();
                c = _RepositoryCateogoria.Selecionar(p.Pro_Categoria);

                Fornecedor f = new Fornecedor();
                f = _RepositoryFornecedor.Selecionar(p.Pro_Fornecedor);

                VendaProdutoView venp = new VendaProdutoView();
                venp.idVendaProduto = i.VeP_ID;
                venp.Produto = p.Pro_Nome;
                venp.Preco = p.Pro_Preco;
                venp.Categoria = c.Cat_Nome;
                venp.Fornecedor = f.For_Nome;
                venp.Qtd = i.VeP_Qtd;

                oListVendaProdutoView.Add(venp);
                ViewBag.ListaProdutos = oListVendaProdutoView;
            }
        }


        // GET: Venda/Edit/5
        public ActionResult Edit(int id)
        {
            Venda v = _RepositoryVenda.Selecionar(id);

            List<VendaProduto> oListVendaProduto = _RepositoryVendaProduto.SelecionarIdVenda(id);

            decimal valorTotal = 0;
            foreach (var i in oListVendaProduto)
            {
                valorTotal += i.VeP_PrecoVenda;
            }

            VendaView vw = new VendaView();

            Cliente c = new Cliente();
            c = _RepositoryCliente.Selecionar(v.ID_Cli);

            vw.Ven_ID = v.Ven_ID;
            vw.Nome = c.Cli_Nome;
            vw.ID_Cli = v.ID_Cli;
            //vw.ValorTotal = valorTotal;

            ViewBag.ValorTotal = valorTotal;
            ListaProdutos(v.Ven_ID);
            ViewBag.VendaCriada = v.Ven_ID;
            return View(vw);
        }

        // POST: Venda/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VendaView model)
        {
            Venda v = new Venda();
            v = _RepositoryVenda.Selecionar(model.Ven_ID);

            if (model.idExcluirProduto != 0) // Excluir Produto
            {
                removeProd(model.idExcluirProduto);
                if (model.ID_Cli != 0)
                {
                    int idClienteView = model.ID_Cli;
                    v.ID_Cli = idClienteView;
                    _RepositoryVenda.Alterar(v);
                }
            }
            else if (model.finalizarEdicao == true)
            {
                v.Ven_Fechada = true;
                _RepositoryVenda.Alterar(v);
                return RedirectToAction("Index");
            }
            else
            {
                int qtd = Convert.ToInt32(model.VeP_Qtd);
                criarVendaProduto(v.Ven_ID, model.Pro_ID, qtd);
            }


            return RedirectToAction("Edit", model.Ven_ID);
        }

        // GET: Venda/Delete/5
        public ActionResult Delete(int id)
        {
            Venda v = new Venda();
            v = _RepositoryVenda.Selecionar(id);

            List<VendaProduto> oListaVendaProduto = _RepositoryVendaProduto.SelecionarIdVenda(id);

            foreach (var i in oListaVendaProduto)
            {
                VendaProduto vp = new VendaProduto();
                _RepositoryVendaProduto.Excluir(i.VeP_ID);
            }

            _RepositoryVenda.Excluir(id);

            return RedirectToAction("Index");
        }

        // POST: Venda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Venda venda = _RepositoryVenda.Selecionar(id);
            _RepositoryVenda.Excluir(id);
            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
