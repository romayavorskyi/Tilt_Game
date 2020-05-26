using System.Collections.Generic;
using Tilt_Game.Managers;
using Tilt_Game.Models;

namespace Tilt_Game
{
    public struct StateResult
    {
        public GameState GameState { get; }
        //TODO Use BallsInHolesCount to sort field states before adding to queue?
        public int BallsInHolesCount { get; set; }

        public StateResult(GameState gameState, int ballsCount)
        {
            GameState = gameState;
            BallsInHolesCount = ballsCount;
        }
    }

    public struct FieldNode
    {
        public Field Field { get; }
        public List<TiltDirection> Directions { get; }

        public FieldNode(Field field, List<TiltDirection> directions)
        {
            Field = field;
            Directions = directions;
        }
    }
}
