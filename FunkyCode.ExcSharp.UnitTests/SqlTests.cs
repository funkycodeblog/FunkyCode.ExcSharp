using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FunkyCode.ExcSharp.Console;
using FunkyCode.ExcSharp.Engine;
using FunkyCode.ExcSharp.Engine.Tools;
using NUnit.Framework;

namespace FunkyCode.ExcSharp.UnitTests
{
    
    public class SqlTests
    {


        [Test]
        public void HeaderRecognitionTest()
        {
            // RequestTypeAccess, RequestTypeAccess, LedgerTransactionTypeAccess
            var sheetName = "LogonUser";
            var workBookPath = @"c:\Data\Projects\client-specific-rebates-processor\_resx\excel\Client Specific Rebates Processor - Ledger Configuration.xlsx";
            var tableName = "[Genesis].[dbo].[LogonUser]";

            // var sqlLogonUser = GetSql(workBookPath, "LogonUser", "[Genesis].[dbo].[LogonUser]", transpose: true, isVertical: true);
            // var sqlInternalUser = GetSql(workBookPath, "LogonUser", "[Genesis].[dbo].[InternalUser]", transpose: true, isVertical: true);

            // var sqlRequestTypeAccess = GetSql(workBookPath, "RequestTypeAccess", "[ExecutionVenue].[dbo].[RequestTypeAccess]", transpose: false);

            var sqlLedgerTransactionTypeAccess = GetSql(workBookPath, "LedgerTransactionTypeAccess", "[LedgerProcessing].[dbo].[LedgerTransactionTypeAccess]", transpose: false);
        }

        string GetSql(string workBookPath, string sheetName, string tableName, bool transpose = false, bool isVertical = false)
        {
            //var address = Excel.GetTableNameAddress(workBookPath, sheetName, tableName);

            //var tableRange = Excel.GetTableRange(workBookPath, sheetName, address);

            //var table = Excel.GetTextTableData(workBookPath, sheetName, tableRange);

            //if (transpose)
            //    table = table.GetTransposed();

            //var sql = SqlBuilder.GenerateInsert(tableName, table, isVertical);

            //return sql;

            return null;
        }


    }

    
}
