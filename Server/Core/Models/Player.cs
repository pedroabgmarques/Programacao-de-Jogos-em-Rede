using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{

    /// <summary>
    /// Descreve um jogador do jogo
    /// </summary>
    public class Player
    {
        /// <summary>
        /// Identifica o jogador de forma única
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Nome do jogador
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Endereço IP do jogador
        /// </summary>
        public IPAddress IPAddress { get; set; }
        
    }
}
