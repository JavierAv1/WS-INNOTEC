using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Pedido
    {
        public int? IdPedido { get; set; }
        public int? IdCompra { get; set; }
        public DateTime? FechaPedido { get; set; }
        public string? EstadoPedido { get; set; }

        public Compra? Compra { get; set; } // Relación con Compra
        public ICollection<Envio>? Envios { get; set; } // Relación con Envio

        public int? UsuarioId { get; set; }
    }



}
