using System;
using System.Collections.Generic;

namespace HelloWorld {
    class Program {

        static void Main (string[] args) {
            var gameOver = false;
            var hasKey = false;
            var playersPosition = new Position (0, 1);

            var worldMap = GetWorldMap ();
            var worldItems = GetItemMap ();

            while (!gameOver) {
                Console.Clear ();
                var currentTile = worldMap[playersPosition.ToString ()];
                string itemName;
                var itemExists = worldItems.TryGetValue (playersPosition.ToString (), out itemName);
                Console.WriteLine ("You can see " + currentTile);
                if(itemExists)
                {
                    Console.WriteLine($"would you like to pick {itemName} up?...(Y/N)");
                    var itemPrompt = Console.ReadLine ().ToUpper ();

                    if(itemPrompt == "Y")
                    {
                       hasKey = true;
                       Console.WriteLine("you now have a " + itemName);

                       worldItems.Remove(playersPosition.ToString());
                    }
                    
                }

                

                var newPosition = PlayerControls (new Position (playersPosition.X, playersPosition.Y));
                var mapTileExists = worldMap.TryGetValue (newPosition.ToString (), out string value);

                if (mapTileExists) {
                    playersPosition = newPosition;
                } else {
                    Console.WriteLine ("You cant go that way ...");
                    Console.ReadLine ();
                }

            }

            Console.WriteLine ("Game Over.");
        }

        private static Dictionary<string, string> GetWorldMap () {
            var worldMap = new Dictionary<string, string> ();
            worldMap.Add ("0,1", "some trees.");
            worldMap.Add ("0,2", "some rocks.");
            worldMap.Add ("1,1", "a lake. ");
            return worldMap;
        }
        private static Dictionary<string, string> GetItemMap () {
            var worldMap = new Dictionary<string, string> ();

            worldMap.Add ("0,2", "Key");

            return worldMap;
        }

        private static Position PlayerControls (Position playersPosition) {
            Console.WriteLine ("Where would you like to move? (N,S,E,W) ...");
            var movement = Console.ReadLine ().ToUpper ();
            var newPosition = playersPosition;
            Console.WriteLine ($"You wanted to move {movement}");

            if (movement == "N") {
                newPosition.Y += 1;
            } else if (movement == "S") {
                newPosition.Y -= 1;
            } else if (movement == "E") {
                newPosition.X += 1;
            } else if (movement == "W") {
                newPosition.X -= 1;
            }
            return newPosition;
        }

        //  playersPosition.Y += 1;

    }

    public class Position {
        public Position (int _x, int _y) {
            X = _x;
            Y = _y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString () {
            return $"{X},{Y}";
        }
    }
}