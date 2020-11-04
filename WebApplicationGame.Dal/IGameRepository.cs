using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplicationGame.Dal
{
    public interface IGameRepository
    {
        IEnumerable<Game> GetAll();
        Dictionary<Guid, int> GetCurrentGame();
    }
}
