using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using webapi2.Models;
using Microsoft.EntityFrameworkCore;

namespace webapi2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class empleadosController : ControllerBase
    {
        private readonly empleadosContext _empleadosContexto;

        public empleadosController(empleadosContext empleadosContexto)
        {
            _empleadosContexto = empleadosContexto;
        }
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {
            List<clientes> listadoclientes=(from e in _empleadosContexto.clientes
                                            select e).ToList();
            if (listadoclientes.Count()==0)
            {
                return NotFound();
            }
            return Ok(listadoclientes);
        }
        // buscar por id
        [HttpGet]
        [Route("GetById/{id}")]

        public IActionResult Get(int id)
        {
            clientes? cliente = (from e in _empleadosContexto.clientes
                                 where e.id_nombre == id
                                 select e).FirstOrDefault();
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }
        // buscar por descripcion

        [HttpGet]
        [Route("Find/{filtro})")]
        public IActionResult Buscarpordescripcion(string filtro)
        {
            clientes? cliente = (from e in _empleadosContexto.clientes
                                 where e.nombre.Contains(filtro)
                                 select e).FirstOrDefault();
            if(cliente== null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        // agregar datos a la tabla
        [HttpPost]
        [Route("Add")]
        public IActionResult guardarcliente([FromBody] clientes cliente)
        {
            try
            {
                _empleadosContexto.clientes.Add(cliente);
                _empleadosContexto.SaveChanges();
                return Ok(cliente);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            

        }
        [HttpPut]
        [Route("actualizar/{idname}")]

        public IActionResult actualizarclientes(int idname, [FromBody] clientes clientesModificar)
        {
            clientes? clienteActual = (from e in _empleadosContexto.clientes
                                       where e.id_nombre == idname
                                       select e).FirstOrDefault();
            // verficar si hay registro
            if (clienteActual == null)
            {
                return NotFound();
            }
            // si hay registro asemos el llamado y modificamos
            clienteActual.nombre= clientesModificar.nombre;
            clienteActual.apellido = clientesModificar.apellido;
            clienteActual.direccion = clientesModificar.direccion;
            clienteActual.id_departamento = clientesModificar.id_departamento;

            // se marca el registro modifcado y se manda la nueva info a
           // la base de datos
           _empleadosContexto.Entry(clienteActual).State=EntityState.Modified;
            _empleadosContexto.SaveChanges();

            return Ok();
        }
        // eliminar registro de una tabla
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult eliminarcliente(int id)
        {
            clientes? cliente=(from e in _empleadosContexto.clientes
                               where e.id_nombre==id
                               select e).FirstOrDefault();
            if(cliente == null)
            {
                return NotFound();

            }
            _empleadosContexto.clientes.Attach(cliente);
            _empleadosContexto.clientes.Remove(cliente);
            _empleadosContexto.SaveChanges();

            return Ok(cliente);
        }

    }
}
