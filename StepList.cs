using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gomoku
{
    [JsonObject(MemberSerialization.OptIn)]
    public class StepList
    {
        /// <summary>
        /// List of Steps of the game.
        /// </summary>
        [JsonProperty]
        public List<Step> Steps { get; set; }

        private StepList()
        {
            Steps = new List<Step>();
        }

        public static StepList Instance { get; } = new StepList();

        public void AddAStep(Step step)
        {
            Steps.Add(step);
        }

        public void UndoAStep()
        {
            Steps.RemoveAt(Steps.Count - 1);
        }

        public Step GetLastStep()
        {
            return Steps[Steps.Count - 1];
        }
    }

    [JsonObject]
    public struct Step
    {
        public string Player { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }
    }
}
