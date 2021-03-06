﻿namespace Macro
{
    public enum ErrorCode
    {
        ecNone = 0,
        ecDBConnection = 1,
        ecUserNotDefined = 2,
        ecServerConnection = 3,
        ecClientConnection = 4,
        ecNotInitialized = 5
    }

    public enum TableStatus
    {
        tsNone = 0,
        tsInsert = 1,
        tsEdit = 2,
        tsBrowse = 3,
        tsEmpty = 4,
        tsDelete = 5,
        tsOnProcess = 6,
        tsUnEditable = 7
    }

    public enum FormStatus
    {
        fsBase = 0,
        fsMain = 1,
        fsSubform = 2
    }

    public enum Action
    {
        aUnknown = 0,
        aCascadeDelete = 1,
        aCascadeUpdate = 2,
        aDelete = 3,
        aError = 4,
        aInsert = 5,
        aLogIn = 6,
        aLogOut = 7,
        aPrint = 8,
        aSelect = 9,
        aUpdate = 10,
        aFind = 11,
        aFilter = 12,
        aPrintPreview = 13
    }

    public enum InputLanguageList
    {
        illFarsi = 0,
        illEnglish = 1
    }

    public enum SystemTable
    {
        stAction = 100,
        stField = 101,
        stFieldOperation = 102,
        stFieldType = 103,
        stHistory = 104,
        stKeyType = 105,
        stLog = 106,
        stPermission = 107,
        stTable = 108,
        stTableType = 109,
        stUser = 110,
        stVersion = 111,
        stWorkGroup = 112,
        stWorkStation = 113
    }

    public enum TableType
    {
        ttUnknown = 0,
        ttSystem = 1,
        ttBase = 2,
        ttMain = 3,
        ttRelation = 4,
        ttSubTable = 5,
        ttObject = 6,
        ttWindowsProject = 7,
        ttWebProject = 8,
        ttDatabaseServer = 9,
        ttExtraBase = 10,
        ttStaticBase = 11,
        ttExtraMain = 12,
        ttSolution = 13,
        ttReport = 14,
        ttHistory = 15,
        ttView = 16,
        ttViewBase = 17,
        ttViewMain = 18,
        ttSeperator = 19,
        ttButton = 20,
        ttExtraSubTable = 21,
    }

    public enum UserType
    {
        utNone = 0,
        utServerAdministrator = 1,
        utLocal = 2
    }

    public enum FieldType
    {
        ftNone = 0,
        ftBigInt = 127,
        ftBinary = 173,
        ftBit = 104,
        ftChar = 175,
        ftDate = 40,
        ftDateTime = 61,
        ftDateTime2 = 42,
        ftDateTimeOffset = 43,
        ftDecimal = 106,
        ftFloat = 62,
        ftGeoGraphy = 130,
        ftGeoMetry = 129,
        ftHierarchyID = 128,
        ftImage = 34,
        ftInt = 56,
        ftMoney = 60,
        ftNChar = 239,
        ftNText = 99,
        ftNumeric = 108,
        ftNVarChar = 231,
        ftReal = 59,
        ftSmallDateTime = 58,
        ftSmallInt = 52,
        ftSmallMoney = 122,
        ftSQL_Variant = 98,
        ftSysname = 256,
        ftText = 35,
        ftTime = 41,
        ftTimeStamp = 189,
        ftTinyInt = 48,
        ftUniqueIdentifier = 36,
        ftVarBinary = 165,
        ftVarChar = 167,
        ftXML = 241
    }

    public enum PartnerType
    {
        ptCustomer = 1,
        ptSupplier = 2,
        ptEmployee = 3
    }

    public enum InvoiceState
    {
        isNone = 0,
        isUsual = 1,
        isReturn = 2
    }

    public enum InvoiceType
    {
        itNone = 0,
        itSell = 1,
        itPurchase = 2
    }
}
