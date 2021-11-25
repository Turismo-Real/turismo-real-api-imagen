using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using TurismoReal_Imagen.Core.Entities;
using TurismoReal_Imagen.Core.Interfaces;
using TurismoReal_Imagen.Infra.Context;

namespace TurismoReal_Imagen.Infra.Repositories
{
    public class ImagenRepository : IImagenRepository
    {
        protected readonly OracleContext _context;

        public ImagenRepository()
        {
            _context = new OracleContext();
        }

        public async Task<DeptoImagenes> GetImages(int id)
        {
            _context.OpenConnection();
            OracleCommand cmd = new OracleCommand("sp_obten_imagenes_depto", _context.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.BindByName = true;
            cmd.Parameters.Add("depto_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("imagenes", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            cmd.Parameters["depto_id"].Value = id;
            OracleDataReader reader = (OracleDataReader)await cmd.ExecuteReaderAsync();

            DeptoImagenes deptoImagenes = new DeptoImagenes();
            deptoImagenes.idDepartamento = id;
            List<Imagen> imagenes = new List<Imagen>();
            while (reader.Read())
            {
                Imagen imagen = new Imagen();
                imagen.idImagen = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("id_imagen")).ToString());
                imagen.formato = reader.GetValue(reader.GetOrdinal("formato")).ToString();
                byte[] byteImage = (byte[]) reader.GetValue(reader.GetOrdinal("imagen"));
                imagen.imagen = Convert.ToBase64String(byteImage);
                imagenes.Add(imagen);
            }
            deptoImagenes.imagenes = imagenes;
            _context.CloseConnection();
            return deptoImagenes;
        }

        public Task<object> UpdateImage(int id, ImagenPayload imagen)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UploadImage(ImagenPayload imagen)
        {
            int saved = 0;
            try
            {
                byte[] byteImage = Convert.FromBase64String(imagen.b64imagen);

                _context.OpenConnection();
                OracleCommand cmd = new OracleCommand("sp_agregar_imagen", _context.GetConnection());
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.BindByName = true;
                cmd.Parameters.Add("depto_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("formato_i", OracleDbType.Varchar2).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("imagen_i", OracleDbType.Blob).Direction = ParameterDirection.Input;
                cmd.Parameters.Add("saved", OracleDbType.Int32).Direction = ParameterDirection.Output;

                cmd.Parameters["depto_id"].Value = imagen.idDepartamento;
                cmd.Parameters["formato_i"].Value = imagen.formato;
                cmd.Parameters["imagen_i"].Value = byteImage;

                await cmd.ExecuteNonQueryAsync();
                saved = int.Parse(cmd.Parameters["saved"].Value.ToString());

                _context.CloseConnection();
                return saved;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return saved;
            }
        }

        public async Task<int> DeleteImage(int id)
        {
            _context.OpenConnection();
            OracleCommand cmd = new OracleCommand("sp_eliminar_imagen_depto", _context.GetConnection());
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.BindByName = true;

            cmd.Parameters.Add("imagen_id", OracleDbType.Int32).Direction = ParameterDirection.Input;
            cmd.Parameters.Add("deleted", OracleDbType.Int32).Direction = ParameterDirection.Output;

            cmd.Parameters["imagen_id"].Value = id;
            await cmd.ExecuteNonQueryAsync();
            int removed = Convert.ToInt32(cmd.Parameters["deleted"].Value.ToString());
            return removed;
        }
    }
}
