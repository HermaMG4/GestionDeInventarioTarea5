using Gestion5Inventario;
using System;
using System.Diagnostics.Metrics;
using System.Net.NetworkInformation;
using System.Runtime.Intrinsics.Arm;
namespace Gestion5Inventario
{
    class Program
    {
        public static void Main(string[] args)
        {
            Inventario inventario = new Inventario();
            Console.WriteLine("Bienvenido al sistema de gestion de inventario la rosalia.");

            // Ingreso de productos por el usuario
            Console.Write("¿Cuantos productos desea ingresar? ");
            int cantidad = int.Parse(Console.ReadLine());

            // Ciclo para pedir exactamente la cantidad de productos
            for (int iteradorproductos = 0; iteradorproductos < cantidad; iteradorproductos++)
            {
                Console.WriteLine($"Producto {iteradorproductos + 1}");
                string nombreProducto = inventario.ObtenerNombreProductoValido();
                decimal precioProducto = inventario.ObtenerPrecioValido();

                Console.WriteLine("----------------------------------------------");

                Producto producto = new Producto(nombreProducto, precioProducto);
                inventario.AgregarProducto(producto);
            }

            char seleccion;

            do
            {
                Console.WriteLine("\n¿Qué deseas hacer?");
                Console.WriteLine("a - Actualizar precio");
                Console.WriteLine("e - Eliminar producto");
                Console.WriteLine("c - Contar y agrupar productos");
                Console.WriteLine("f - Filtrar y ordenar productos");
                Console.WriteLine("s - Salir");
                Console.Write("Selecciona: ");
                seleccion = Console.ReadLine().ToLower()[0];

                switch (seleccion)
                {
                    case 'a':
                        Console.WriteLine("\n** Actualizar precio **");
                        string nombreProductoActualizar = inventario.ObtenerNombreProductoValido();
                        decimal nuevoPrecio = inventario.ObtenerPrecioValido();
                        inventario.ActualizarPrecio(nombreProductoActualizar, nuevoPrecio);
                        break;

                    case 'e':
                        Console.WriteLine("\n** Eliminar producto **");
                        string nombreProductoEliminar = inventario.ObtenerNombreProductoValido();
                        inventario.EliminarProducto(nombreProductoEliminar);
                        break;

                    case 'c':
                        Console.WriteLine("\n** Contar y agrupar productos **");
                        inventario.ContarYAgruparProductos();
                        break;

                    case 'f':
                        Console.WriteLine("\n** Filtrar y ordenar productos **");
                        Console.Write("Ingresa el precio mínimo: ");
                        decimal precioMinimo = decimal.Parse(Console.ReadLine());
                        var productosFiltrados = inventario.FiltrarYOrdenarProductos(precioMinimo);
                        Console.WriteLine("Productos filtrados y ordenados:");
                        foreach (var producto in productosFiltrados)
                        {
                            producto.MostrarInformacion();
                        }
                        break;

                    case 's':
                        Console.WriteLine("\nGracias por usar el sistema de gestion de inventario.");
                        break;

                    default:
                        Console.WriteLine($"'{seleccion}' no es una selección válida.");
                        break;
                }
            } while (seleccion != 's');
        }
    }
}