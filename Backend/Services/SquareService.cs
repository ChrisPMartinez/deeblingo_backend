using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace Backend.Services
{
    public class SquareService : ISquareService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly Random _random = new Random();
        public SquareService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public List<PlayerViewModel> GetPlayers()
        {
            string path = _hostingEnvironment.ContentRootPath;
            var json = System.IO.File.ReadAllText(path + "/allsquares.json");
            AllSquares allSquares = JsonConvert.DeserializeObject<AllSquares>(json);

            List<Player> players = allSquares.Players;

            List<PlayerViewModel> playerNames = new List<PlayerViewModel>{ new PlayerViewModel { Name = "Guest" } };
            
            foreach(Player player in players)
            {
                playerNames.Add(new PlayerViewModel { Name = player.Name });
            }

            return playerNames;
        }

        public CardViewModel GetSquares(string player = null)
        {
            List<string> squares = new List<string>();

            string path = _hostingEnvironment.ContentRootPath;
            var json = System.IO.File.ReadAllText(path + "/allsquares.json");
            AllSquares allSquares = JsonConvert.DeserializeObject<AllSquares>(json);

            if (!string.IsNullOrEmpty(player))
            {
                Player activePlayer = allSquares.Players.Find(p => p.Name.ToLower() == player.ToLower());
                if (activePlayer != null) allSquares.Players.Remove(activePlayer);
            }

            squares.AddRange(allSquares.Anyone.Squares);

            foreach(Player p in allSquares.Players)
            {
                squares.AddRange(p.Squares);
            }            

            while(squares.Count > 24)
            {
                int index = _random.Next(0, (squares.Count - 1));

                squares.RemoveAt(index);
            }
            for (int i = 0; i < 24; i++)
            {
                int j = _random.Next(i + 1);
                string v = squares[j];
                squares[j] = squares[i];
                squares[i] = v;
            }

            squares.Insert(12, "FREE SPACE");

            CardViewModel card = new CardViewModel { Rows = new List<CardRow>() };

            for (int i = 0; i < 5; i++)
            {
                CardRow row = new CardRow { Squares = new List<Square>() };

                for(int j = 0; j<5; j++)
                {
                    Square square = new Square { Text = squares.First() };
                    row.Squares.Add(square);
                    squares.RemoveAt(0);
                }

                card.Rows.Add(row);
            }

            return card;
        }
    }
}
