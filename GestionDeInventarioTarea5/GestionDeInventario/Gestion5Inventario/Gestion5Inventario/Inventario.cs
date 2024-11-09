using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion5Inventario
{
    public class Inventario
    {
        private List<Producto> productos;

        public Inventario()
        {
            productos = new List<Producto>();
        }

        public void AgregarProducto(Producto producto)
        {
            productos.Add(producto);
            Console.WriteLine($"Producto '{producto.Nombre}' agregado al inventario.");
        }

        // Función para actualizar el precio de un producto
        public void ActualizarPrecio(string nombreProducto, decimal nuevoPrecio)
        {
            var producto = productos.FirstOrDefault(p => p.Nombre.Equals(nombreProducto, StringComparison.OrdinalIgnoreCase));
            if (producto != null)
            {
                producto.Precio = nuevoPrecio;
                Console.WriteLine($"El precio de '{nombreProducto}' ha sido actualizado a {nuevoPrecio:C}.");
            }
            else
            {
                Console.WriteLine($"Producto '{nombreProducto}' no encontrado.");
            }
        }

        // Función para eliminar un producto por nombre
        public void EliminarProducto(string nombreProducto)
        {
            var productoAEliminar = productos.FirstOrDefault(p => p.Nombre.Equals(nombreProducto, StringComparison.OrdinalIgnoreCase));
            if (productoAEliminar != null)
            {
                productos.Remove(productoAEliminar);
                Console.WriteLine($"Producto '{nombreProducto}' ha sido eliminado del inventario.");
            }
            else
            {
                Console.WriteLine($"Producto '{nombreProducto}' no encontrado.");
            }
        }

        // Función para contar y agrupar productos por precio
        public void ContarYAgruparProductos()
        {
            var resultado = productos
                .GroupBy(p =>
                    p.Precio < 100 ? "Menores a 100" :
                    p.Precio <= 500 ? "Entre 100 y 500" :
                    "Mayores a 500")
                .Select(grupo => new
                {
                    Rango = grupo.Key,
                    Cantidad = grupo.Count()
                });

            Console.WriteLine("Conteo de productos por rango de precios:");
            foreach (var grupo in resultado)
            {
                Console.WriteLine($"Rango: {grupo.Rango}, Cantidad: {grupo.Cantidad}");
            }
        }

        // Función para filtrar y ordenar productos por precio
        public IEnumerable<Producto> FiltrarYOrdenarProductos(decimal precioMinimo)
        {
            return productos
                .Where(p => p.Precio >= precioMinimo)
                .OrderBy(p => p.Precio);
        }

        // Validación para asegurarse de que el precio es válido
        public decimal ObtenerPrecioValido()
        {
            decimal precio;
            do
            {
                Console.Write("Precio: ");
                if (!decimal.TryParse(Console.ReadLine(), out precio) || precio <= 0)
                {
                    Console.WriteLine("Por favor ingrese un precio válido (mayor que 0).");
                }
            } while (precio <= 0);

            return precio;
        }

        // Validación para asegurarse de que el nombre del producto no está vacío
        public string ObtenerNombreProductoValido()
        {
            string nombre;
            do
            {
                Console.Write("Nombre: ");
                nombre = Console.ReadLine();
                if (string.IsNullOrEmpty(nombre))
                {
                    Console.WriteLine("El nombre del producto no puede estar vacío. Por favor ingrese un nombre válido.");
                }
            } while (string.IsNullOrEmpty(nombre));
            return nombre;
        }
    }
}