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
    public class FornecedorController : Controller
    {
        //private LojaProdutosEntities db = new LojaProdutosEntities();
        private RepositoryFornecedor _Repository = new RepositoryFornecedor();

        // GET: Fornecedor
        public ActionResult Index()
        {
            var fornecedor = _Repository.SelecionarTodos();
            return View(fornecedor);
        }

        // GET: Fornecedor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = _Repository.Selecionar(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // GET: Fornecedor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fornecedor/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "For_ID,For_Nome")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                _Repository.Incluir(fornecedor);
                return RedirectToAction("Index");
            }

            return View(fornecedor);
        }

        // GET: Fornecedor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fornecedor fornecedor = _Repository.Selecionar(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        // POST: Fornecedor/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "For_ID,For_Nome")] Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                _Repository.Alterar(fornecedor);
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }

        // GET: Fornecedor/Delete/5
        public ActionResult Delete(int id)
        {
            _Repository.Excluir(id);
            return RedirectToAction("Index");
        }

        // POST: Fornecedor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _Repository.Excluir(id);
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
