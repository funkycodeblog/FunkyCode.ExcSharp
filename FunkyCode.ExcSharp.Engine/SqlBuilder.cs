using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using FunkyCode.ExcSharp.Engine.Extensions;

namespace FunkyCode.ExcSharp.Engine
{
    public static class SqlBuilder
    {

        public static string GenerateInsert(string tableName, string[,] data, bool isVertical = false)
        {
            var names = new List<string>();

            var separator = !isVertical ? ", " : $", {Environment.NewLine}";

            var columns = data.ColumnCount();

            for (var c = 0; c < columns; c++)
                names.Add(data[0, c]);

            var namesJoined = string.Join(separator, names.Select(n => $"[{n}]"));

            var rows = new List<string>();
            for (var r = 1; r < data.RowCount(); r++)
            {
                var values = new List<string>();

                for (var c = 0; c < columns; c++)
                {
                    var iValue = data[r, c];
                    var resolved = ResolveValue(iValue);
                    values.Add(resolved);
                }

                var row = $"({string.Join(separator, values)})";
                rows.Add(row);
            }


            var rowSeparator = $",{Environment.NewLine}";

            var sql = $@"
INSERT INTO {tableName} ({namesJoined})
VALUES 
{string.Join(rowSeparator, rows)};";

            return sql;
        }

        private static string ResolveValue(string value)
        {

            if (null == value) return string.Empty;

            if (value == "NULL" || IsVarbinary(value) || value.IsNumeric() )
                return value;
            else
            {
                var converted = $"'{value}'";
                return converted;
            }
        }

        static bool IsVarbinary(string value)
        {
            if (null == value) return false;
            var isVarbinary = value.Length >= 2 && value[1] == 'x';
            return isVarbinary;
        }

        public static string SetIdentityInsert(string tableName, bool isOn)
        {
            var state = isOn ? "ON" : "OFF";
            var sql = $"SET IDENTITY_INSERT {tableName} {state};";
            return sql;
        }

    }

    
}
