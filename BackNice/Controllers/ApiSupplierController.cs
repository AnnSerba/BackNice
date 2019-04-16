using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackNice.Data;
using BackNice.Models;

namespace BackNice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiSupplierController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiSupplierController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiSupplier
        [HttpGet]
        public JsonResult GetSuppliers()
        {
            return new JsonResult(_context.Suppliers);
        }

        // GET: api/ApiSupplier/5
        [HttpGet("{id}")]
        public JsonResult GetSupplierModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest(ModelState));
            }

            var supplierModel = _context.Suppliers.FindAsync(id);

            if (supplierModel == null)
            {
                return new JsonResult(NotFound());
            }

            return new JsonResult(supplierModel);
        }

        // PUT: api/ApiSupplier/5
        [HttpPut("{id}")]
        public JsonResult PutSupplierModel([FromRoute] int id, [FromBody] SupplierModel supplierModel)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest(ModelState));
            }

            if (id != supplierModel.ID)
            {
                return new JsonResult(BadRequest());
            }

            _context.Entry(supplierModel).State = EntityState.Modified;

            try
            {
                _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierModelExists(id))
                {
                    return new JsonResult(NotFound());
                }
                else
                {
                    throw;
                }
            }

            return new JsonResult(NoContent());
        }

        // POST: api/ApiSupplier
        [HttpPost]
        public JsonResult PostSupplierModel([FromBody] SupplierModel supplierModel)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest(ModelState));
            }

            _context.Suppliers.Add(supplierModel);
            _context.SaveChangesAsync();

            return new JsonResult(CreatedAtAction("GetSupplierModel", new { id = supplierModel.ID }, supplierModel));
        }

        // DELETE: api/ApiSupplier/5
        [HttpDelete("{id}")]
        public JsonResult DeleteSupplierModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(BadRequest(ModelState));
            }

            var supplierModel = _context.Suppliers.Find(id);
            if (supplierModel == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Suppliers.Remove(supplierModel);
            _context.SaveChangesAsync();

            return new JsonResult(supplierModel);
        }

        private bool SupplierModelExists(int id)
        {
            return _context.Suppliers.Any(e => e.ID == id);
        }
    }
}