using UnityEngine;

public enum RoadType
{
    Reachable, // Clear and reachable road
    Unreachable, // Clear but unreachable piece of road
    BarierHigh, // Cannot be crossed
    BarierLow, // Can be jumped over or crawled under
    TheWhiper
}

public class RoadLayoutGenerator
{
    private const int GENERATED_ROAD_END = 102;
    private const int GENERATED_ROAD_LENGTH = GENERATED_ROAD_END + 1;
    private const int GENERATED_ROAD_WIDTH = 3;
    private const int SPACE_NEEDED_WHIPER_FRONT = 3;
    private const int SPACE_NEEDED_WHIPER_BACK = 2;

    private RoadType[,] _road = new RoadType[GENERATED_ROAD_LENGTH, GENERATED_ROAD_WIDTH];
    private int _nextRowToGenerate;
    private int _nextRowToGet;

    private float _chanceForHighBarier;
    private float _chanceForLowBarier;
    private uint _numberOfWhipersPer100Rows;

    public RoadLayoutGenerator(float chanceForHighBarier, float chanceForLowBarier, uint numberOfWhipersPer100Rows)
    {
        _chanceForLowBarier = chanceForLowBarier;
        _chanceForHighBarier = chanceForHighBarier;
        _numberOfWhipersPer100Rows = numberOfWhipersPer100Rows;

        for (int i = 0; i < 2; ++i)
        {
            for (int e = 0; e < GENERATED_ROAD_WIDTH; ++e )
            {
                _road[i,e] = RoadType.Reachable;
            }
        }

        ClearRoad(2);
        _nextRowToGenerate = 2;
        Generate();
        AddWhipers();
        _nextRowToGet = 0;
    }

    void ClearRoad(int rowFrom, int rowTo )
    {
        int rows = _road.GetLength(0);
        int columns = _road.GetLength(1);
        for (int row = rowFrom; row < rowTo; ++row )
        {
            for (int column = 0; column < columns; ++column)
            {
                _road[row, column] = RoadType.Unreachable;
            }
        }
    }

    void ClearRoad(int rowFrom )
    {
        int rows = _road.GetLength(0);
        ClearRoad(rowFrom, rows);
    }

    void CopyLastTwoRowsToBegining()
    {
        int rows = _road.GetLength(0);
        int columns = _road.GetLength(1);
        for (int column = 0; column < columns; ++column)
        {
            _road[0, column] = _road[rows - 2, column];
            _road[1, column] = _road[rows - 1, column];
        }

        _nextRowToGenerate = 2;
    }

    void GenerateNextRow()
    {
        for (int i = 0; i < GENERATED_ROAD_WIDTH; i++ )
        {
            if ( _road[_nextRowToGenerate, i] != RoadType.Unreachable )
                continue;

            float random_value = UnityEngine.Random.value * 100.0f;
            
            if (random_value <= _chanceForHighBarier)
            {
                _road[_nextRowToGenerate, i] = RoadType.BarierHigh;
            }
            else {
                random_value -= _chanceForHighBarier;
                if (random_value <= _chanceForLowBarier)
                {
                    _road[_nextRowToGenerate, i] = RoadType.BarierLow;
                    if ( _road[_nextRowToGenerate - 1, i] == RoadType.Reachable )
                    {
                        _road[_nextRowToGenerate + 1, i] = RoadType.Reachable;
                    }
                }
            }
        }
    }


    void MarkReachable()
    {
        for ( uint i = 0; i < (uint)GENERATED_ROAD_WIDTH; ++i )
        {
            if (_road[_nextRowToGenerate,i] == RoadType.Unreachable)
            {
                if ( _road[_nextRowToGenerate - 1,i] == RoadType.Reachable )
                {
                    _road[_nextRowToGenerate,i] = RoadType.Reachable;
                    if ( (i - 1) < (uint)GENERATED_ROAD_WIDTH )
                        _road[_nextRowToGenerate, i - 1] = RoadType.Reachable;
                    if ( (i + 1) < (uint)GENERATED_ROAD_WIDTH )
                        _road[_nextRowToGenerate, i + 1] = RoadType.Reachable;
                }
            }
        }
    }

    bool IsStillPassable()
    {
        for (int i = 0; i < GENERATED_ROAD_WIDTH; ++i)
        {
            if ( _road[_nextRowToGenerate, i] == RoadType.Reachable || _road[_nextRowToGenerate + 1, i] == RoadType.Reachable )
            {
                return true;
            }
        }

        return false;
    }

    void AddWhipers()
    {
        uint sumOfPlacedWhipers = 0;
        while ( sumOfPlacedWhipers < _numberOfWhipersPer100Rows )
        {
            int whiper_x_coord = Random.Range(0, GENERATED_ROAD_WIDTH);
            int whiper_y_coord = Random.Range(SPACE_NEEDED_WHIPER_FRONT + 1, GENERATED_ROAD_LENGTH - SPACE_NEEDED_WHIPER_BACK - 1);

            if ( _road[whiper_y_coord - SPACE_NEEDED_WHIPER_FRONT, whiper_x_coord] != RoadType.Reachable 
                || _road[whiper_y_coord + SPACE_NEEDED_WHIPER_BACK, whiper_x_coord] != RoadType.Reachable )
                continue; //First and last square of whiper block must be reachable, so player can acually reach enemy whiper and safely exit him

            bool whiperExistItThisBlock = false;
            for (int i = 1 - SPACE_NEEDED_WHIPER_FRONT; i < SPACE_NEEDED_WHIPER_BACK; ++i)
            {
                if ( _road[whiper_y_coord + i, whiper_x_coord] == RoadType.TheWhiper )
                {
                    whiperExistItThisBlock = true;
                }
            }
            if (whiperExistItThisBlock) continue; //Cant place whiper here, the one already here would be overwritten

            //Placing enemy whiper
            for (int i = 1 - SPACE_NEEDED_WHIPER_FRONT; i < SPACE_NEEDED_WHIPER_BACK; ++i)
            {
                _road[whiper_y_coord + i, whiper_x_coord] = RoadType.Reachable;
            }
            _road[whiper_y_coord, whiper_x_coord] = RoadType.TheWhiper;
            ++sumOfPlacedWhipers;
        }
    }

    void Generate()
    {
        while ( _nextRowToGenerate < GENERATED_ROAD_END )
        {
            GenerateNextRow();
            MarkReachable();
            if ( IsStillPassable() )
            {
                _nextRowToGenerate++;
            }
            else //Revert Generation and Mark Reachable
            {
                ClearRoad(_nextRowToGenerate, _nextRowToGenerate + 2);
            }
        }
    }

    public RoadType[] GetNextRow()
    {
        RoadType[] row = new RoadType[GENERATED_ROAD_WIDTH];
        for ( uint i = 0; i <??GENERATED_ROAD_WIDTH; ++i)
            row[i] = _road[_nextRowToGet, i];

        _nextRowToGet++;
        if ( _nextRowToGet > GENERATED_ROAD_END )
        {
            CopyLastTwoRowsToBegining();
            ClearRoad(2);
            _nextRowToGenerate = 1;
            Generate();
            AddWhipers();
            _nextRowToGet = 1;
        }

        return row;
    }
}
