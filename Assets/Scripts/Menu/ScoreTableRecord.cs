using Menu;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace menu
{
    public class ScoreTableRecord : MonoBehaviour
    {
        public Text eggs;
        public Text time;
        public Text date;

        public void Fill(ScoreRecord record)
        {
            var timespan = record.NeededTime.ToString(ScoreRecord.TimeFormat);

            if (!"00:00:00".Equals(timespan))
            {
                eggs.text = record.EggsCollected.ToString();
                time.text = timespan;
                date.text = record.Date.ToString(ScoreRecord.DateFormat);
            }
        }
    }
}
