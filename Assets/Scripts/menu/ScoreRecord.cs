using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ScoreRecord : System.IComparable<ScoreRecord>
{
    public int m_eggsCollected;
    public System.DateTime m_date;
    public System.TimeSpan m_neededTime;

    public ScoreRecord( int eggsCollected, System.TimeSpan neededTime, System.DateTime date )
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
        PlayerPrefs.SetString(keyPrefix + nameof(m_date), m_date.ToString("dd MM yyyy"));
        PlayerPrefs.SetString(keyPrefix + nameof(m_neededTime), m_neededTime.ToString("hh:mm:ss"));
        PlayerPrefs.Save();
    }

    private void Load( string keyPrefix )
    {
        if ( keyPrefix[keyPrefix.Length - 1] != '.' )
        {
            keyPrefix = keyPrefix + '.';
        }

        m_eggsCollected = PlayerPrefs.GetInt(keyPrefix + nameof(m_eggsCollected), 0 );
        
        var date = PlayerPrefs.GetString(keyPrefix + nameof(m_date), "00 00 0000");
        m_date = System.DateTime.ParseExact(date, "dd MM yyyy", System.Globalization.CultureInfo.InvariantCulture);

        var timespan = PlayerPrefs.GetString(keyPrefix + nameof(m_neededTime), "00:00:00" );
        m_neededTime = System.TimeSpan.ParseExact(timespan, "hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
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