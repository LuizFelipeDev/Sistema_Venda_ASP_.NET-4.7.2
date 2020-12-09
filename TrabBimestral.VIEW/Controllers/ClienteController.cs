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
    public class ClienteController : Controller
    {
        //private LojaProdutosEntities db = new LojaProdutosEntities();
        private RepositoryCliente _Repository = new RepositoryCliente();

        // GET: Cliente
        public ActionResult Index()
        {
            var cliente = _Repository.SelecionarTodos();
            return View(cliente);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = _Repository.Selecionar(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Cli_ID,Cli_Nome,Cli_Email,Cli_CPF")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _Repository.Incluir(cliente);
                return RedirectToAction("Index");
            }

            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = _Repository.Selecionar(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Cli_ID,Cli_Nome,Cli_Email,Cli_CPF")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _Repository.Alterar(cliente);
                return RedirectToAction("Index");
            }
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            _Repository.Excluir(id);
            return RedirectToAction("Index");
        }

        // POST: Cliente/Delete/5
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
