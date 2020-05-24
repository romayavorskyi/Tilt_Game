using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balls_Holes
{

    public enum StateResult
    {
        Win,
        Fail,
        Continue
    }

    public struct MoveResult
    {
        public StateResult TiltResult { get; }
        public int BallsInHolesCount { get; set; }

        public MoveResult(StateResult tiltResult, int ballsCount)
        {
            TiltResult = tiltResult;
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

    public class GameManager
    {
        public MoveResult CheckState(Field field)
        {
            StateResult result = StateResult.Continue;
            if (field.Holes.Any(x => x.Id != x.BallId))
            {
                result = StateResult.Fail;
            }
            else if (field.Holes.All(x => x.Id == x.BallId))
            {
                result = StateResult.Win;
            }
            int ballsInHoleCount = field.Holes.Where(x => x.BallId.HasValue).Count();
            return new MoveResult(result, ballsInHoleCount);
        }

        HashSet<string> UsedStates { get; } = new HashSet<string>();
        public List<TiltDirection> GetWinningStrategy(Field field)
        {
            var directionList = new List<TiltDirection>();
            Queue<FieldNode> queue = new Queue<FieldNode>();
            queue.Enqueue(new FieldNode(field, new List<TiltDirection>()));
            while (queue.TryDequeue(out var node))
            {
                var ballsState = node.Field.GetBallsState();
                if (!UsedStates.Contains(ballsState))
                {
                    UsedStates.Add(ballsState);
                    MoveResult result = CheckState(node.Field);
                    switch (result.TiltResult)
                    {
                        case StateResult.Fail:
                            continue;
                        case StateResult.Win:
                            return node.Directions;
                        case StateResult.Continue:
                        default:
                            foreach (TiltDirection direction in Enum.GetValues(typeof(TiltDirection)))
                            {
                                var stateField = field.Copy();
                                stateField.Tilt(direction);
                                var newDirections = node.Directions.ToList();
                                newDirections.Add(direction);
                                queue.Enqueue(new FieldNode(stateField, newDirections));
                            }
                            break;
                    }
                }
            }
            return directionList;
        }



    }
}
