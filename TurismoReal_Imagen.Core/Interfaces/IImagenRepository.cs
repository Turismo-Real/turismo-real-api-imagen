using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Imagen.Core.Entities;

namespace TurismoReal_Imagen.Core.Interfaces
{
    public interface IImagenRepository
    {
        // GET IMAGES BY DEPTO ID
        Task<object> GetImages(int id);

        // UPLOAD DEPTO IMAGE
        Task<object> UploadImage(int id, ImagenPayload imagen);

        // UPDATE DEPTO IMAGE
        Task<object> UpdateImage(int id, ImagenPayload imagen);

        // DELETE DEPTO IMAGE
        Task<object> DeleteImage(int id);
    }
}
