﻿namespace CrossCutting.MainModule.Logging
{
    public struct Priority
    {
        public const int Lowest = 0;

        public const int Low = 1;
        
        public const int Normal = 2;
        
        public const int High = 3;
        
        public const int Highest = 4;
    }

    public struct Category
    {
        public const string General = "General";
    }
}