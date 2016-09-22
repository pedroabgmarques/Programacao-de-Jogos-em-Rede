using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{

    /// <summary>
    /// Descreve uma jogada
    /// </summary>
    public class Move
    {
        /// <summary>
        /// Identifica de forma unica uma jogada
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Jogador que efetuou a jogada
        /// </summary>
        public Player jogador { get; set; }

        /// <summary>
        /// Data e hora em que a jogada foi efetuada
        /// </summary>
        public DateTime DateTimePlay { get; set; }

        /// <summary>
        /// Numero que o jogador tentou adivinhar
        /// </summary>
        public int PlayedNumber { get; set; }
    }
}
