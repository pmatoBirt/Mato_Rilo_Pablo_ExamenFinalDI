using BusinessLogicLayer;
using ObjetosTransferencia.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InterfazGrafica.UC
{
    public partial class ListaPedidosUC : UserControl
    {
        private List<PedidoDTO> listaPedidos;

        public ListaPedidosUC(String nombreCliente)
        {
            InitializeComponent();
            // TODO: haz la peticion de los clientes y asociala a la lista y cargalo en el DataGridView
            listaPedidos = ControladorBLL.ListarPedidosCliente(nombreCliente);
            this.dGV_Pedidos.Columns.Add("iDPedido", "idPedido");
            this.dGV_Pedidos.Columns.Add("idCliente", "idCliente");
            this.dGV_Pedidos.Columns.Add("FechaPedido", "FechaPedido");
            this.dGV_Pedidos.Columns.Add("FechaEnvio", "FechaEnvio");
            this.dGV_Pedidos.Columns.Add("Precio", "Precio");
            this.dGV_Pedidos.Columns.Add("Direccion", "Direccion");
            foreach (PedidoDTO p in listaPedidos)
            {
                this.dGV_Pedidos.Rows.Add(p.IdPedido, p.IdCliente, p.FechaPedido, p.FechaEnvio, p.PrecioEnvio, p.Direccion); 
            }

        }


        public List<PedidoDTO> ListaPedidos { get => listaPedidos; set => listaPedidos = value; }


    }
}
