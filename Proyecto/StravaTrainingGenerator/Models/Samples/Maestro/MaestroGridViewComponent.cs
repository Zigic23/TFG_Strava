using BussinessLogicLayer.Objects;
using Microsoft.AspNetCore.Mvc;
using StoneMVCCore.Models.Samples.Maestro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoneMVCCore.Models.Samples.Maestro
{
    public class MaestroGridViewComponent : ViewComponent
    {
        public MaestroGridViewComponent()
        {
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await GetItemsAsync();
            
            //return Task.FromResult<IViewComponentResult>(View(model));
            //return View("~/Views/Maestro/Partial_DataGrid.cshtml", model);
            return View(model);
        }

        public Task<Partial_DataGrid> GetItemsAsync()
        {
            Partial_DataGrid model = new Partial_DataGrid();
            model.datasource = new List<Demo_Opcion1>();

            for (int i = 1; i <= 10; i++)
            {
                Demo_Opcion1 item = new Demo_Opcion1();

                item.id = i;
                item.param1 = "Param1_" + i;
                item.param2 = "Param2_" + i;
                item.param3 = "Param3_" + i;
                item.param4 = "Param4_" + i;

                model.datasource.Add(item);
            }
            return Task.FromResult<Partial_DataGrid>(model);
        }

    }
}
