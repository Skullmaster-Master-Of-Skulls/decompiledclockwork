using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Odbc
{
	// Token: 0x0200028A RID: 650
	internal static class ODBC
	{
		// Token: 0x06002718 RID: 10008 RVA: 0x001089B8 File Offset: 0x00107DB8
		internal static Exception ConnectionClosed()
		{
			return ADP.InvalidOperation(Res.GetString("Odbc_ConnectionClosed"));
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x001089D4 File Offset: 0x00107DD4
		internal static Exception OpenConnectionNoOwner()
		{
			return ADP.InvalidOperation(Res.GetString("Odbc_OpenConnectionNoOwner"));
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x001089F0 File Offset: 0x00107DF0
		internal static Exception UnknownSQLType(ODBC32.SQL_TYPE sqltype)
		{
			return ADP.Argument(Res.GetString("Odbc_UnknownSQLType", new object[]
			{
				sqltype.ToString()
			}));
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x00108A24 File Offset: 0x00107E24
		internal static Exception ConnectionStringTooLong()
		{
			return ADP.Argument(Res.GetString("OdbcConnection_ConnectionStringTooLong", new object[]
			{
				1024
			}));
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x00108A54 File Offset: 0x00107E54
		internal static ArgumentException GetSchemaRestrictionRequired()
		{
			return ADP.Argument(Res.GetString("ODBC_GetSchemaRestrictionRequired"));
		}

		// Token: 0x0600271D RID: 10013 RVA: 0x00108A70 File Offset: 0x00107E70
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(Res.GetString("ODBC_NotSupportedEnumerationValue", new object[]
			{
				type.Name,
				value.ToString(CultureInfo.InvariantCulture)
			}), type.Name);
		}

		// Token: 0x0600271E RID: 10014 RVA: 0x00108AB0 File Offset: 0x00107EB0
		internal static ArgumentOutOfRangeException NotSupportedCommandType(CommandType value)
		{
			return ODBC.NotSupportedEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x0600271F RID: 10015 RVA: 0x00108AD0 File Offset: 0x00107ED0
		internal static ArgumentOutOfRangeException NotSupportedIsolationLevel(IsolationLevel value)
		{
			return ODBC.NotSupportedEnumerationValue(typeof(IsolationLevel), (int)value);
		}

		// Token: 0x06002720 RID: 10016 RVA: 0x00108AF0 File Offset: 0x00107EF0
		internal static InvalidOperationException NoMappingForSqlTransactionLevel(int value)
		{
			return ADP.DataAdapter(Res.GetString("Odbc_NoMappingForSqlTransactionLevel", new object[]
			{
				value.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06002721 RID: 10017 RVA: 0x00108B24 File Offset: 0x00107F24
		internal static Exception NegativeArgument()
		{
			return ADP.Argument(Res.GetString("Odbc_NegativeArgument"));
		}

		// Token: 0x06002722 RID: 10018 RVA: 0x00108B40 File Offset: 0x00107F40
		internal static Exception CantSetPropertyOnOpenConnection()
		{
			return ADP.InvalidOperation(Res.GetString("Odbc_CantSetPropertyOnOpenConnection"));
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x00108B5C File Offset: 0x00107F5C
		internal static Exception CantEnableConnectionpooling(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(Res.GetString("Odbc_CantEnableConnectionpooling", new object[]
			{
				ODBC32.RetcodeToString(retcode)
			}));
		}

		// Token: 0x06002724 RID: 10020 RVA: 0x00108B88 File Offset: 0x00107F88
		internal static Exception CantAllocateEnvironmentHandle(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(Res.GetString("Odbc_CantAllocateEnvironmentHandle", new object[]
			{
				ODBC32.RetcodeToString(retcode)
			}));
		}

		// Token: 0x06002725 RID: 10021 RVA: 0x00108BB4 File Offset: 0x00107FB4
		internal static Exception FailedToGetDescriptorHandle(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(Res.GetString("Odbc_FailedToGetDescriptorHandle", new object[]
			{
				ODBC32.RetcodeToString(retcode)
			}));
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x00108BE0 File Offset: 0x00107FE0
		internal static Exception NotInTransaction()
		{
			return ADP.InvalidOperation(Res.GetString("Odbc_NotInTransaction"));
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x00108BFC File Offset: 0x00107FFC
		internal static Exception UnknownOdbcType(OdbcType odbctype)
		{
			return ADP.InvalidEnumerationValue(typeof(OdbcType), (int)odbctype);
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x00108C1C File Offset: 0x0010801C
		internal static void TraceODBC(int level, string method, ODBC32.RetCode retcode)
		{
			Bid.TraceSqlReturn("<odbc|API|ODBC|RET> %08X{SQLRETURN}, method=%ls\n", retcode, method);
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x00108C38 File Offset: 0x00108038
		internal static short ShortStringLength(string inputString)
		{
			return checked((short)ADP.StringLength(inputString));
		}

		// Token: 0x04001A02 RID: 6658
		internal const string Pwd = "pwd";
	}
}
