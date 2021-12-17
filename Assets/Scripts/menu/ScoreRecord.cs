using System;
using UnityEngine;

namespace Menu
{
    public struct ScoreRecord : IComparable<ScoreRecord>
    {
        public static readonly string DateFormat = "dd.MM.yyyy"; 
        public static readonly string TimeFormat = @"hh\:mm\:ss";
        
        public int m_eggsCollected;
        public DateTime m_date;
        public TimeSpan m_neededTime;

        public ScoreRecord(GameState state)
        {
            m_eggsCollected = state.GetEggs();
            m_date = state.GetStart();
            m_neededTime = state.GetTime();
        }
        
        public ScoreRecord( int eggsCollected, TimeSpan neededTime, DateTime date )
        {
            m_eggsCollected = eggsCollected;
            m_date = date;
            m_neededTime = neededTime;
        }

        public ScoreRecord( string keyPrefix )
        {
            m_eggsCollected = 0;
            m_neededTime = new System.TimeSpan();
            m_date = new System.DateTime();
            Load(keyPrefix);
        }

        public void Save( string keyPrefix )
        {
            if ( keyPrefix[keyPrefix.Length - 1] != '.' )
            {
                keyPrefix = keyPrefix + '.';
            }

            PlayerPrefs.SetInt(keyPrefix + nameof(m_eggsCollected), m_eggsCollected);
            PlayerPrefs.SetString(keyPrefix + nameof(m_date), m_date.ToString(DateFormat));
            PlayerPrefs.SetString(keyPrefix + nameof(m_neededTime), m_neededTime.ToString(TimeFormat));
            PlayerPrefs.Save();
        }

        private void Load( string keyPrefix )
        {
            if ( keyPrefix[keyPrefix.Length - 1] != '.' )
            {
                keyPrefix = keyPrefix + '.';
            }

            m_eggsCollected = PlayerPrefs.GetInt(keyPrefix + nameof(m_eggsCollected), 0 );
        
            var date = PlayerPrefs.GetString(keyPrefix + nameof(m_date), "01.01.2021");
            Debug.Log("Date: " + date);
            m_date = DateTime.ParseExact(date, DateFormat, System.Globalization.CultureInfo.InvariantCulture);

            var timespan = PlayerPrefs.GetString(keyPrefix + nameof(m_neededTime), "00:00:00");
            Debug.Log("TimeSpan: " + timespan);
            m_neededTime = TimeSpan.ParseExact(timespan, TimeFormat, System.Globalization.CultureInfo.InvariantCulture);
        }

        public int CompareTo(ScoreRecord other) 
        {
            if ( m_eggsCollected != other.m_eggsCollected )
            {
                return m_eggsCollected.CompareTo(other.m_eggsCollected);
            }

            if ( !m_neededTime.Equals(other.m_neededTime) )
            {
                return m_neededTime.CompareTo(other.m_neededTime);
            }

            return m_date.CompareTo(other.m_date);
        }

        public override bool Equals( object ob ){
            if( ob is ScoreRecord ) {
                ScoreRecord score = (ScoreRecord) ob;
                return CompareTo(score) == 0;
            }
            else {
                return false;
            }
        }
    }
}
