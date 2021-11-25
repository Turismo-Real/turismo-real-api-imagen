using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TurismoReal_Imagen.Core.Entities;
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
        public async Task<DeptoImagenes> GetImagenes(int id)
        {
            DeptoImagenes response = await _imagenRepository.GetImages(id);
            return response;
        }

        [HttpPost]
        public async Task<object> NewImagen([FromBody] ImagenPayload imagen)
        {
            int response = await _imagenRepository.UploadImage(imagen);

            if (response == 0) return new { message = "Error al agregar imagen" };
            return new { message = "Imagen agregada.", imagenId = response };
        }

        [HttpPut("{id}")]
        public async Task<object> UpdateImagen(int id, [FromBody] ImagenPayload imagen)
        {
            var response = await _imagenRepository.UpdateImage(id, imagen);
            return response;
        }

        [HttpDelete("{id}")]
        public async Task<object> DeleteImagen(int id)
        {
            int response = await _imagenRepository.DeleteImage(id);

            if (response == -1) return new { message=$"No existe imagen con id {id}.", removed=false };
            if (response == 0) return new { message="Error al eliminar imagen.", removed=false };
            return new { message="Imagen eliminada.", removed=true };
        }

    }
}
