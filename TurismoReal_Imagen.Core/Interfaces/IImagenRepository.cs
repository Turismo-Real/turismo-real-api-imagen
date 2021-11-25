using System.Threading.Tasks;
using TurismoReal_Imagen.Core.Entities;

namespace TurismoReal_Imagen.Core.Interfaces
{
    public interface IImagenRepository
    {
        // GET IMAGES BY DEPTO ID
        Task<DeptoImagenes> GetImages(int id);

        // UPLOAD DEPTO IMAGE
        Task<int> UploadImage(ImagenPayload imagen);

        // UPDATE DEPTO IMAGE
        Task<object> UpdateImage(int id, ImagenPayload imagen);

        // DELETE DEPTO IMAGE
        Task<object> DeleteImage(int id);
    }
}
