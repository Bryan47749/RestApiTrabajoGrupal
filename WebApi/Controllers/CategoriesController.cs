using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebService_WebAPI.Models;

namespace WebService_WebAPI.Controllers
{
    public class CategoriesController : ApiController
    {
        private CategoryDAL data;
        public CategoriesController()
        {
            // Instanciar el objeto.
            this.data = new CategoryDAL();
        }
        // Ruta URL: "GET: api/categories"
        public IEnumerable<Category> GetCategories()
        {
            // Simular una demora de 3 segundos para hacer pruebas.
            // System.Threading.Thread.Sleep(3000);//3 segundos
            // Llamar a la Capa de Acceso a Datos
            return data.GetCategories();
        }
        // Ruta URL: "GET: api/categories/masks"
        // NOTA: "masks" es un valor de ejemplo para el parametro: id
        public Category GetCategoryById(string id)
        {
            //Llamar a la Capa de Acceso a Datos
            return data.GetCategoryById(id);
        }
        // Ruta URL: "GET: api/categories/?name=Masks"
        // NOTA: "Masks" es un valor de ejemplo para el parametro: name
        public IEnumerable<Category> GetCategoryByShortName(string name)
        {
            //Llamar a la Capa de Acceso a Datos
            return data.GetCategoryByShortName(name);
        }
        // Ruta URL: "POST(Insert): api/categories"
        public int PostCategory([FromBody] Category category)
        {
            // Llamar a la Capa de Acceso a Datos
            return data.InsertCategory(category);
        }
        // Ruta URL: "PUT(Update): api/categories/masks"
        public int PutCategory(string id, [FromBody] Category category)
        {
            category.CategoryID = id;
            // Llamar a la Capa de Acceso a Datos
            return data.UpdateCategory(category);
        }
        // Ruta URL: "DELETE: api/categories/masks"
        public int DeleteCategory(string id)
        {
            Category category = new Category();
            category.CategoryID = id;
            // Llamar a la Capa de Acceso a Datos
            return data.DeleteCategory(category);
        }
    }
}
