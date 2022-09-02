﻿using VizORM_Common;

namespace VizORM_Backend.Bodies
{
    public class Filter
    {
        private string _leftColumn;

        private string _rightColumn;

        public ComparisonType ComparisonType;

        public string LeftColumn
        {
            get
            {
                return _leftColumn;
            }
            set
            {
                Argument.NotNullOrEmpty(value, nameof(LeftColumn));
                _leftColumn = value;
            }
        }

        public string RightColumn
        {
            get
            {
                return _rightColumn;
            }

            set
            {
                Argument.NotNullOrEmpty(value, nameof(RightColumn));
                _rightColumn = value;
            }
        }
    }
