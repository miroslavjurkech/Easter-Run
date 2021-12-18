using System;
using UnityEngine;

namespace Menu
{
    public struct ScoreRecord : IComparable<ScoreRecord>
    {
        public const string DateFormat = "dd.MM.yyyy";
        public const string TimeFormat = @"hh\:mm\:ss";

        public int EggsCollected;
        public DateTime Date;
        public TimeSpan NeededTime;

        public ScoreRecord(GameState state)
        {
            EggsCollected = state.GetEggs();
            Date = state.GetStart();
            NeededTime = state.GetTime();
        }

        public ScoreRecord( string keyPrefix )
        {
            EggsCollected = 0;
            NeededTime = new System.TimeSpan();
            Date = new System.DateTime();
            Load(keyPrefix);
        }

        public void Save( string keyPrefix )
        {
            if ( keyPrefix[keyPrefix.Length - 1] != '.' )
            {
                keyPrefix = keyPrefix + '.';
            }

            PlayerPrefs.SetInt(keyPrefix + nameof(EggsCollected), EggsCollected);
            PlayerPrefs.SetString(keyPrefix + nameof(Date), Date.ToString(DateFormat));
            PlayerPrefs.SetString(keyPrefix + nameof(NeededTime), NeededTime.ToString(TimeFormat));
            PlayerPrefs.Save();
        }

        private void Load( string keyPrefix )
        {
            if ( keyPrefix[keyPrefix.Length - 1] != '.' )
            {
                keyPrefix = keyPrefix + '.';
            }

            EggsCollected = PlayerPrefs.GetInt(keyPrefix + nameof(EggsCollected), 0 );
        
            var date = PlayerPrefs.GetString(keyPrefix + nameof(Date), "01.01.2021");
            Debug.Log("Date: " + date);
            Date = DateTime.ParseExact(date, DateFormat, System.Globalization.CultureInfo.InvariantCulture);

            var timespan = PlayerPrefs.GetString(keyPrefix + nameof(NeededTime), "00:00:00");
            Debug.Log("TimeSpan: " + timespan);
            NeededTime = TimeSpan.ParseExact(timespan, TimeFormat, System.Globalization.CultureInfo.InvariantCulture);
        }

        public int CompareTo(ScoreRecord other) 
        {
            if ( EggsCollected != other.EggsCollected )
            {
                return EggsCollected.CompareTo(other.EggsCollected);
            }

            if ( !NeededTime.Equals(other.NeededTime) )
            {
                return NeededTime.CompareTo(other.NeededTime);
            }

            return Date.CompareTo(other.Date);
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
