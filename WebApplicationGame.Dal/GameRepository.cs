using System;
using System.Collections;
using System.Collections.Generic;

namespace WebApplicationGame.Dal
{
    public class GameRepository: IGameRepository
    {
        public IEnumerable<Game> GetAll()
        {
            return new List<Game>()
            {
                new Game { Id = 1, Name = "13 район"},
                new Game { Id = 2, Name = "В разработке"}
            };
        }

        public Dictionary<Guid, int> GetCurrentGame()
        {
            return new Dictionary<Guid, int>()
            {
                { Guid.Parse("cd4fe988-b91c-4484-8b71-087d2b50ae82"), 1 },
                { Guid.Parse("f7a4c2f4-38ea-45e7-b331-08dd202f3a82"), 2 }
            };
        }
    }
}
