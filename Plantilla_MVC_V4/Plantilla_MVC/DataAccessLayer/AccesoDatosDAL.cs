using ObjetosTransferencia.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataAccessLayer
{
    public static class AccesoDatosDAL
    {
        #region Atributos

        private static String consultaSQL;

        /// <summary>
        ///  Cadena de conexion
        /// </summary>
        private static SqlConnection conexion = new SqlConnection("Data Source=localhost;Initial Catalog=Northwind;User ID=usuarioDI;Password=1234");

        /// <summary>
        /// lista de clientes
        /// </summary>
        private static List<ClienteDTO> listaClientes = new List<ClienteDTO>();

        /// <summary>
        /// Lista de pedidos de un cliente concreto
        /// </summary>
        private static List<PedidoDTO> listaPedidos = new List<PedidoDTO>();

        #endregion

        #region Constructores

        #endregion

        #region Propiedades

        public static string ConsultaSQL { get => consultaSQL; set => consultaSQL = value; }
        public static List<PedidoDTO> ListaPedidos { get => listaPedidos; set => listaPedidos = value; }
        public static List<ClienteDTO> ListaClientes { get => listaClientes; set => listaClientes = value; }


        #endregion

        #region Metodos

        /// <summary>
        /// Consultar datos en una base de datos SQLServer 
        /// </summary>
        public static List<ClienteDTO> ListadoClientesNorthWind()
        {
            return RealizarConsultaClientes("select * from dbo.Customers");
        }

        /// <summary>
        /// Pide a la base de datos la lista de pedidos para un cliente concreto
        /// </summary>
        /// <param name="indiceCliente"></param>
        /// <returns></returns>
        public static List<PedidoDTO> ListadoPedidosCliente(String nombreCliente)
        {
            return RealizarConsultaPedidos("select * from dbo.Orders WHERE CustomerID = '" + nombreCliente + "'");
        }



        /// <summary>
        /// Realizar consulta a la BD
        /// </summary>
        private static List<ClienteDTO> RealizarConsultaClientes(String consulta)
        {
            SqlCommand command;

            // Objeto para elctura de datos
            SqlDataReader dataReader;

            try
            {
                conexion.Open();

                command = new SqlCommand(consulta, conexion);

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    // TODO: parsea los datos a la lista de ClientesDTO
                    ClienteDTO c = new ClienteDTO(dataReader.GetString(0), dataReader.GetString(1));
                    listaClientes.Add(c); 
                }

                dataReader.Close();
                command.Dispose();

                return listaClientes;


            }
            catch (Exception e)
            {
                throw new Exception("No se ha podido estabecer la conexion con la BD!" + e.Message);

            }
            finally
            {
                conexion.Close();

            }

        }


        /// <summary>
        /// Realizar consulta a la BD
        /// </summary>
        private static List<PedidoDTO> RealizarConsultaPedidos(String consulta)
        {
            SqlCommand command;

            // Objeto para elctura de datos
            SqlDataReader dataReader;

            try
            {
                conexion.Open();

                command = new SqlCommand(consulta, conexion);

                dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    //TODO: creamos un pedido, parseamos y añadimos a la lista de clientes
                    PedidoDTO p1 = new PedidoDTO();
                    p1.IdPedido = dataReader.GetInt32(0);
                    p1.IdCliente = dataReader.GetString(1);
                    p1.FechaPedido = DateTime.Parse(dataReader.GetValue(3).ToString());
                    p1.FechaEnvio = DateTime.Parse(dataReader.GetValue(5).ToString());
                    //p1.FechaPedido = DateTime.Now();
                    //p1.FechaEnvio = DateTime.Now(); 
                    p1.PrecioEnvio = Double.Parse(dataReader.GetValue(7).ToString());
                    p1.Direccion = dataReader.GetString(9); 
                    //PedidoDTO p = new PedidoDTO(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetDateTime(2), dataReader.GetDateTime(3), dataReader.GetDouble(4), dataReader.GetString(5));
                    ListaPedidos.Add(p1);
                }

                dataReader.Close();
                command.Dispose();

                return listaPedidos;


            }
            catch (Exception e)
            {
                throw new Exception("No se ha podido estabecer la conexion con la BD!" + e.Message);

            }
            finally
            {
                conexion.Close();

            }

        }


        #endregion

    }
}
