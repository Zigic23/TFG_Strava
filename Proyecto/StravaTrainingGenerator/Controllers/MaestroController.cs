using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StoneMVCCore.Models.Configuration.Settings;
using StoneMVCCore.Models.Samples.Maestro;
using System;
using System.Collections.Generic;
using System.Net;

namespace StoneMVCCore.Controllers
{
    public class MaestroController : BaseController
    {
        public MaestroController(ILogger<BaseController> logger, IOptions<ConnectionStrings> connectionStrings, IOptions<KeysSettings> keysSettings, IConfiguration configuration) : base(logger, connectionStrings, keysSettings, configuration)
        {
        }

        public ActionResult Index()
        {
            ViewBag.MaestroActive = "active";

            return View("~/Views/Maestro/Maestro.cshtml");
        }

        public PartialViewResult Maestro_Grid()
        {
            Partial_DataGrid model = new Partial_DataGrid();

            model.datasource = new List<BusinessLogicLayer.Objects.Demo_Opcion1>();

            for (int i = 1; i <= 10; i++)
            {
                BusinessLogicLayer.Objects.Demo_Opcion1 item = new BusinessLogicLayer.Objects.Demo_Opcion1();

                item.id = i;
                item.param1 = "Param1_" + i;
                item.param2 = "Param2_" + i;
                item.param3 = "Param3_" + i;
                item.param4 = "Param4_" + i;

                model.datasource.Add(item);
            }

            return PartialView("~/Views/Maestro/Partial_DataGrid.cshtml", model);
        }


        public PartialViewResult Maestro_Editor(int? id = null)
        {
            /* Llevamos la carga del editor a una acción por que con bastante seguridad
             * deban rellenarse combos de datos.
             */
            Partial_Editor editor = null;

            if (id.HasValue)
            {
                editor = new Partial_Editor();
                editor.id = id.Value;
            }

            return PartialView("~/Views/Maestro/Partial_Editor.cshtml", editor);
        }

        [HttpDelete]
        public object Maestro_Borrar(int id)
        {
            // Realizamos las operaciones oportunas
            // ### Construir capa DAL ###            
            try
            {
                return StatusCode(200, "Deleted");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}