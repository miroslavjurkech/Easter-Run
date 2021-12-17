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
            var timespan = record.m_neededTime.ToString(ScoreRecord.TimeFormat);

            if (!"00:00:00".Equals(timespan))
            {
                eggs.text = record.m_eggsCollected.ToString();
                time.text = timespan;
                date.text = record.m_date.ToString(ScoreRecord.DateFormat);
            }
        }
    }
}
