using System;
using System.Collections.Generic;
using System.Linq;
using Balls_Holes;
using Tilt_Game.Extensions;
using Tilt_Game.Models;

namespace Tilt_Game.Managers
{
    public class TiltPlayer
    {
        private readonly TiltManager _tiltManager;

        public TiltPlayer()
        {
            _tiltManager = new TiltManager();
        }

        public (GameState state, List<TiltDirection> directions) GetWinningStrategy(Field field)
        {
            //TODO Check that each ball has corresponding hole
            var usedStates = new HashSet<string>();
            Queue<FieldNode> queue = new Queue<FieldNode>();
            queue.Enqueue(new FieldNode(field, new List<TiltDirection>()));
            while (queue.TryDequeue(out var node))
            {
                var ballsState = node.Field.GetBallsState();
                if (!usedStates.Contains(ballsState))
                {
                    usedStates.Add(ballsState);
                    StateResult result = CheckState(node.Field);
                    switch (result.GameState)
                    {
                        case GameState.Fail:
                            continue;
                        case GameState.Win:
                            return (result.GameState, node.Directions);
                        case GameState.Continue:
                        default:
                            var childs = GetChildStates(node.Field, node);
                            foreach (var child in childs)
                            {
                                queue.Enqueue(child);
                            }
                            break;
                    }
                }
            }
            return (GameState.Fail, new List<TiltDirection>());
        }

        private IEnumerable<FieldNode> GetChildStates(Field field, FieldNode node)
        {
            var childs = new List<FieldNode>();
            foreach (TiltDirection direction in Enum.GetValues(typeof(TiltDirection)))
            {
                var stateField = field.Copy();
                _tiltManager.Tilt(stateField, direction);
                var newDirections = node.Directions.ToList();
                newDirections.Add(direction);
                childs.Add(new FieldNode(stateField, newDirections));
            }

            return childs;
        }

        private StateResult CheckState(Field field)
        {
            GameState result = GameState.Continue;
            if (field.Holes.Any(x => x.BallId.HasValue && x.Id != x.BallId))
            {
                result = GameState.Fail;
            }
            else if (field.Holes.All(x => x.Id == x.BallId))
            {
                result = GameState.Win;
            }
            int ballsInHoleCount = field.Holes.Count(x => x.BallId.HasValue);
            return new StateResult(result, ballsInHoleCount);
        }
    }
}
