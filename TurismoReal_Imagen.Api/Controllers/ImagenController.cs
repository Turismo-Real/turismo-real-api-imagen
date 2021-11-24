using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurismoReal_Imagen.Core.Interfaces;

namespace TurismoReal_Imagen.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ImagenController : ControllerBase
    {
        private readonly IImagenRepository _imagenRepository;

        public ImagenController(IImagenRepository imagenRepository)
        {
            _imagenRepository = imagenRepository;
        }

        [HttpGet("{id}")]
        public async Task<object> GetImagen(int id)
        {
            await Task.Delay(1);
            return new object();
        }


    }
}
