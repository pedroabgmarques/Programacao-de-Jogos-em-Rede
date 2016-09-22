using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{

    /// <summary>
    /// Guarda o estado do jogo
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Lista de jogadores presentes no jogo
        /// </summary>
        public List<Player> ListaJogadores { get; set; }

        /// <summary>
        /// Numero que os jogadores tentam adivinhar
        /// </summary>
        public int TargetNumber { get; set; }

        /// <summary>
        /// Lista de jogadas deste jogo
        /// </summary>
        public List<Move> ListaJogadas { get; set; }

    }
}
