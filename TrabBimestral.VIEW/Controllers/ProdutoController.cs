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

namespace TrabBimestral.VIEW.Views
{
    [Authorize]
    public class ProdutoController : Controller
    {
        //private LojaProdutosEntities db = new LojaProdutosEntities();

        private RepositoryProduto _Repository = new RepositoryProduto();
        private RepositoryCategoria _RepositoryCategoria = new RepositoryCategoria();
        private RepositoryFornecedor _RepositoryFornecedor = new RepositoryFornecedor();
        private List<Produto> ListProd = null;

        // GET: Produto
        public ActionResult Index(string info = null, string tipo = null)
        {
            if (info == null || info == "")
            {
                var produto = _Repository.SelecionarTodos();
                return View(produto);
            }
            else
            {
                busca(info, tipo);
                return View(ListProd);
            }
        }

        public void busca(string info, string tipo)
        {
            ListProd = null;
            List<Produto> oList = null;

            if(tipo == "Produto")
            {
                oList = _Repository.SelecionarPorNome2(info);
            }
            else if(tipo == "Categoria")
            {
                Categoria c = new Categoria();
                c = _RepositoryCategoria.SelecionarPorNome(info);

                oList = _Repository.SelecionarPorCategoria(c.Cat_ID);
            }
            else
            {
                Fornecedor f = new Fornecedor();
                f = _RepositoryFornecedor.SelecionarPorNome(info);

                oList = _Repository.SelecionarPorFornecedor(f.For_ID);
            }

            ListProd = oList;
        }

        public ActionResult AutoCompleteProduto(string term)
        {
            List<Produto> oRetorno = _Repository.SelecionarPorNome(term);
            return Json(oRetorno, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult JsonProd(int ID)
        {
            Produto oRetorno = _Repository.Selecionar(ID);
            return Json(oRetorno.Pro_Nome);
        }



        // GET: Produto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = _Repository.Selecionar(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produto/Create
        public ActionResult Create()
        {
            List<Categoria> cat = _RepositoryCategoria.SelecionarTodos();
            List<Fornecedor> forn = _RepositoryFornecedor.SelecionarTodos();
            ViewBag.Pro_Categoria = new SelectList(cat, "Cat_ID", "Cat_Nome");
            ViewBag.Pro_Fornecedor = new SelectList(forn, "For_ID", "For_Nome");
            return View();
        }

        // POST: Produto/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Pro_ID,Pro_Nome,Pro_Categoria,Pro_Fornecedor,Pro_Preco")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _Repository.Incluir(produto);
                return RedirectToAction("Index");
            }

            List<Categoria> cat = _RepositoryCategoria.SelecionarTodos();
            List<Fornecedor> forn = _RepositoryFornecedor.SelecionarTodos();

            ViewBag.Pro_Categoria = new SelectList(cat, "Cat_ID", "Cat_Nome", produto.Pro_Categoria);
            ViewBag.Pro_Fornecedor = new SelectList(forn, "For_ID", "For_Nome", produto.Pro_Fornecedor);
            return View(produto);
        }

        // GET: Produto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = _Repository.Selecionar(id);
            if (produto == null)
            {
                return HttpNotFound();
            }

            List<Categoria> cat = _RepositoryCategoria.SelecionarTodos();
            List<Fornecedor> forn = _RepositoryFornecedor.SelecionarTodos();

            ViewBag.Pro_Categoria = new SelectList(cat, "Cat_ID", "Cat_Nome", produto.Pro_Categoria);
            ViewBag.Pro_Fornecedor = new SelectList(forn, "For_ID", "For_Nome", produto.Pro_Fornecedor);
            return View(produto);
        }

        // POST: Produto/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Pro_ID,Pro_Nome,Pro_Categoria,Pro_Fornecedor,Pro_Preco")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _Repository.Alterar(produto);
                return RedirectToAction("Index");
            }

            List<Categoria> cat = _RepositoryCategoria.SelecionarTodos();
            List<Fornecedor> forn = _RepositoryFornecedor.SelecionarTodos();

            ViewBag.Pro_Categoria = new SelectList(cat, "Cat_ID", "Cat_Nome", produto.Pro_Categoria);
            ViewBag.Pro_Fornecedor = new SelectList(forn, "For_ID", "For_Nome", produto.Pro_Fornecedor);
            return View(produto);
        }

        // GET: Produto/Delete/5
        public ActionResult Delete(int id)
        {
            _Repository.Excluir(id);
            return RedirectToAction("Index");
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _Repository.Excluir(id);
            //db.Produto.Remove(produto);
            //db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
