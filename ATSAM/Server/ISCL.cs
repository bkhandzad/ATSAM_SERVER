﻿using System;
using System.Data.SqlClient;
using Macro;

namespace Atsam.Server
{
    // Server Common Layer
    public interface ISCL
    {
        string GetResourceString(string strString, string strResourceFile = "ServerCommonLayer.Server");

        string GetSolarDate(char chrDivider = '\0');

        string GetSolarDate(DateTime dtDateTime, char chrDivider = '\0');

        DateTime GetJulianDate(string strSolarDate);

        string GetSolarTime(char chrDivider = '\0');

        string GetSolarTime(DateTime dtDateTime, char chrDivider = '\0');

        int GetSolarDayOfYear();

        int GetSolarDayOfYear(DateTime dtDateTime);

        string GetSolarDayOfWeek();

        string GetSolarDayOfWeek(DateTime dtDateTime);

        string GetSolarDayOfWeekEnglish();

        DateTime GetDateTime(Boolean DateAndTime);

        string GetErrorMessage(SqlException eSqlException, TableStatus tsTableStatus);

        string DLToDF(string DL);

        string NormalizeValue(string strValue);

        string GetCodedSolarDate(Boolean IsConverse, int intCodeNumber);
    }
}
