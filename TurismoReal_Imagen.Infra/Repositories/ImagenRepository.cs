using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Imagen.Core.Entities;
using TurismoReal_Imagen.Core.Interfaces;

namespace TurismoReal_Imagen.Infra.Repositories
{
    public class ImagenRepository : IImagenRepository
    {
        public Task<object> GetImages(int id)
        {
            throw new NotImplementedException();
        }

        public Task<object> UploadImage(ImagenPayload imagen)
        {
            throw new NotImplementedException();
        }

        public Task<object> UpdateImage(int id, ImagenPayload imagen)
        {
            throw new NotImplementedException();
        }

        public Task<object> DeleteImage(int id)
        {
            throw new NotImplementedException();
        }
    }
}
