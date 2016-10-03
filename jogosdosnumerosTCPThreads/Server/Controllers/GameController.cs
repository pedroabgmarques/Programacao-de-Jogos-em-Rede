using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Models;
using Core.Stores;
using Newtonsoft.Json;
using Server.Configs;

namespace Server.Controllers
{
    public class GameController
    {
        /// <summary>
        /// This controller would be responsible for managing the
        /// game state. We should try to follow best practiced guide lines
        /// and not focus all our code in the "serverController"
        /// This standard is defined in the S.O.L.I.D guide lines (google it)
        /// </summary>
        public GameController()
        {
            Random random = new Random();
            GameStore.Instance.Game.TargetNumber = random.Next(0, 101);
            GameStore.Instance.Game.PlayerList.Last().Turn = true;
            GameStore.Instance.Game.GameState = GameState.GameStarted;
        }


        public void NextTurn()
        {
            if (GameStore.Instance.Game.PlayerList.FindIndex(p => p.Turn) + 1 ==
                GameStore.Instance.Game.PlayerList.Count)
            {
                GameStore.Instance.Game.PlayerList.Find(p => p.Turn).Turn = false;
                GameStore.Instance.Game.PlayerList.First().Turn = true;
            }
            else
            {
                GameStore.Instance.Game.PlayerList[GameStore.Instance.Game.PlayerList.FindIndex(p => p.Turn) + 1].Turn = true;
                GameStore.Instance.Game.PlayerList.Find(p => p.Turn).Turn = false;
            }
        }

        public void AskPlayerToPlay()
        {
            NetworkMessage networkMessageToSend = new NetworkMessage()
            {
                Message = "Please make a guess!",
                NetworkInstruction = NetworkInstruction.MakeMove
            };

            string networkMessageToSenndJsonString = JsonConvert.SerializeObject(networkMessageToSend);

            GameStore.Instance.Game.PlayerList.Find(p => p.Turn).BinaryWriter.Write(networkMessageToSenndJsonString);
        }

        public void ReceiveAnswer()
        {
            // We know that the server will send a JSON string
            // so we prepare the statement for it
            int answer = GameStore.Instance.Game.PlayerList.Find(p => p.Turn).BinaryReader.Read();
            while(answer == 0)
            {
                answer = GameStore.Instance.Game.PlayerList.Find(p => p.Turn).BinaryReader.Read();
                Thread.Sleep(100);
            }

            string hint = string.Empty;
            NetworkInstruction targetNetworkInstruction = NetworkInstruction.Wait;
            if (answer == GameStore.Instance.Game.TargetNumber)
            {
                hint = "O jogador acertou no número correto!";
                GameStore.Instance.Game.GameState = GameState.GameEnded;
                targetNetworkInstruction = NetworkInstruction.GameEnded;
            } else if(answer < GameStore.Instance.Game.TargetNumber)
            {
                hint = "O número é superior ao valor introduzido";
            }
            else if (answer > GameStore.Instance.Game.TargetNumber)
            {
                hint = "O número é inferior ao valor introduzido";
            }


            NetworkMessage networkMessageToSend = new NetworkMessage()
            {
                Message = "O jogador " + GameStore.Instance.Game.PlayerList.Find(p => p.Turn).PlayerName + " tentou "+ answer + "\n" + hint,

                NetworkInstruction = targetNetworkInstruction
            };

            string networkMessageToSenndJsonString = JsonConvert.SerializeObject(networkMessageToSend);
            
            foreach (Player player in GameStore.Instance.Game.PlayerList)
            {
                player.BinaryWriter.Write(networkMessageToSenndJsonString);
            }

            //TODO: Log player moves
        }
    }
}
