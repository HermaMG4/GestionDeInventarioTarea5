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
            Console.Write("¿Cuantos productos desea ingresar: ");
            int cantidad = int.Parse(Console.ReadLine());

            // Se ocupa el ciclo para pedir exactamente la cantidad de productos que desea ingresar el usuario
            for (int iteradorproductos = 0; iteradorproductos < cantidad; iteradorproductos++)
            {
                Console.WriteLine($"Producto {iteradorproductos + 1}");
                string nombreProducto;
                do
                {
                    Console.Write("Nombre: ");
                    nombreProducto = Convert.ToString(Console.ReadLine());
                    if (nombreProducto == "")
                    {
                        Console.WriteLine("Ingresa por favor el nombre del producto..");
                    }
                } while (string.IsNullOrEmpty(nombreProducto));


                decimal precioProducto;
                do
                {
                    Console.Write("Precio :");
                    precioProducto = decimal.Parse(Console.ReadLine());
                    if (precioProducto <= 0)
                    {
                        Console.WriteLine("El precio no puede ser menor o igual a 0,ingresa un precio valido..");
                    }
                } while (precioProducto <= 0);


                Console.WriteLine("----------------------------------------------");

                Producto producto = new Producto(nombreProducto, precioProducto);
                inventario.AgregarProducto(producto);


            }

            //Ingresar el precio minimo para el filtro
            Console.WriteLine("Ingrese el precio minimo para filtrar los productos: ");
            decimal precioMinimo = decimal.Parse(Console.ReadLine());

            //Filtrar y mostrar productos
            var productosFiltrados = inventario.FiltrarYOrdenarProductos(precioMinimo);

            Console.WriteLine("Productos filtrados y ordenados: ");
            foreach (var producto in productosFiltrados)
            {
                producto.MostrarInformacion();
            }
            inventario.ContarYAgruparProductos();
            inventario.GenerarReporteResumido();
        } 
    }
}