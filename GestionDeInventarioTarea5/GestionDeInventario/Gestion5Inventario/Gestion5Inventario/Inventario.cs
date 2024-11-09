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
            Console.WriteLine($"Producto '{producto.Nombre}' agregado al inventario. ");
        }
        public IEnumerable<Producto> FiltrarYOrdenarProductos(decimal precioMinimo)
        {
            return productos
                .Where(Producto => Producto.Precio > precioMinimo)
                .OrderBy(Productos => Productos);         
        }
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
        public void ActualizarPrecio(List<Producto> productos, string nombreProducto, decimal nuevoPrecio)
        {
            var producto = productos.FirstOrDefault((producto => producto.Nombre.Equals(nombreProducto, StringComparison.OrdinalIgnoreCase)));
            if (producto != null)
            {
                producto.Precio = nuevoPrecio;
                Console.WriteLine($"El precio de '{nuevoPrecio}' ha sido actualizado a {nuevoPrecio}.");
            }
            else
            {
                Console.WriteLine($"Precio '{nuevoPrecio}' no encontrado.");
            }
        }
        public void EliminarProducto(List<Producto> productos, string nombreProducto)
        {
            var productoAEliminar = productos.FirstOrDefault(produc => produc.Nombre.Equals(nombreProducto, StringComparison.OrdinalIgnoreCase));
            if (productoAEliminar != null)
            {
                productos.Remove(productoAEliminar);
                Console.WriteLine($"Producto '{productoAEliminar}' ha sido eliminado del inventario.");
            }
            else
            {
                Console.WriteLine($"Producto '{productoAEliminar}' no encontrado. ");
            }
        }
        public void GenerarReporteResumido()
        {
            if (!productos.Any())
            {
                Console.WriteLine("Inventario vacío: no hay productos disponibles.");
                return;
            }

            int totalProductos = productos.Count;
            decimal precioPromedio = productos.Select(p => p.Precio).Average();
            var productoMasCaro = productos.MaxBy(p => p.Precio);
            var productoMasBarato = productos.MinBy(p => p.Precio);

            Console.WriteLine("=== Reporte Resumido del Inventario ===");
            Console.WriteLine($"Total de productos en inventario: {totalProductos}");
            Console.WriteLine($"Precio promedio de productos: {precioPromedio:C}");
            Console.WriteLine($"Producto más caro: {productoMasCaro?.Nombre} - {productoMasCaro?.Precio:C}");
            Console.WriteLine($"Producto más barato: {productoMasBarato?.Nombre} - {productoMasBarato?.Precio:C}");
        }
    }
}
