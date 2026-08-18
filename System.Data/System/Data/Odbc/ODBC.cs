using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data.Odbc
{
	// Token: 0x020001BC RID: 444
	internal static class ODBC
	{
		// Token: 0x0600194A RID: 6474 RVA: 0x00258EF8 File Offset: 0x002582F8
		internal static Exception UnknownSQLType(ODBC32.SQL_TYPE sqltype)
		{
			return ADP.Argument(Res.GetString("Odbc_UnknownSQLType", new object[]
			{
				sqltype.ToString()
			}));
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x00258F38 File Offset: 0x00258338
		internal static Exception ConnectionStringTooLong()
		{
			return ADP.Argument(Res.GetString("OdbcConnection_ConnectionStringTooLong", new object[]
			{
				1024
			}));
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x00258F78 File Offset: 0x00258378
		internal static ArgumentException GetSchemaRestrictionRequired()
		{
			return ADP.Argument(Res.GetString("ODBC_GetSchemaRestrictionRequired"));
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00258F98 File Offset: 0x00258398
		internal static ArgumentOutOfRangeException NotSupportedEnumerationValue(Type type, int value)
		{
			return ADP.ArgumentOutOfRange(Res.GetString("ODBC_NotSupportedEnumerationValue", new object[]
			{
				type.Name,
				value.ToString(CultureInfo.InvariantCulture)
			}), type.Name);
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x00258FE8 File Offset: 0x002583E8
		internal static ArgumentOutOfRangeException NotSupportedCommandType(CommandType value)
		{
			return ODBC.NotSupportedEnumerationValue(typeof(CommandType), (int)value);
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00259008 File Offset: 0x00258408
		internal static ArgumentOutOfRangeException NotSupportedIsolationLevel(IsolationLevel value)
		{
			return ODBC.NotSupportedEnumerationValue(typeof(IsolationLevel), (int)value);
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x00259028 File Offset: 0x00258428
		internal static InvalidOperationException NoMappingForSqlTransactionLevel(int value)
		{
			return ADP.DataAdapter(Res.GetString("Odbc_NoMappingForSqlTransactionLevel", new object[]
			{
				value.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00259068 File Offset: 0x00258468
		internal static Exception NegativeArgument()
		{
			return ADP.Argument(Res.GetString("Odbc_NegativeArgument"));
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00259088 File Offset: 0x00258488
		internal static Exception CantSetPropertyOnOpenConnection()
		{
			return ADP.InvalidOperation(Res.GetString("Odbc_CantSetPropertyOnOpenConnection"));
		}

		// Token: 0x06001953 RID: 6483 RVA: 0x002590A8 File Offset: 0x002584A8
		internal static Exception CantEnableConnectionpooling(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(Res.GetString("Odbc_CantEnableConnectionpooling", new object[]
			{
				ODBC32.RetcodeToString(retcode)
			}));
		}

		// Token: 0x06001954 RID: 6484 RVA: 0x002590D8 File Offset: 0x002584D8
		internal static Exception CantAllocateEnvironmentHandle(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(Res.GetString("Odbc_CantAllocateEnvironmentHandle", new object[]
			{
				ODBC32.RetcodeToString(retcode)
			}));
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00259108 File Offset: 0x00258508
		internal static Exception FailedToGetDescriptorHandle(ODBC32.RetCode retcode)
		{
			return ADP.DataAdapter(Res.GetString("Odbc_FailedToGetDescriptorHandle", new object[]
			{
				ODBC32.RetcodeToString(retcode)
			}));
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x00259138 File Offset: 0x00258538
		internal static Exception NotInTransaction()
		{
			return ADP.InvalidOperation(Res.GetString("Odbc_NotInTransaction"));
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00259158 File Offset: 0x00258558
		internal static Exception UnknownOdbcType(OdbcType odbctype)
		{
			return ADP.InvalidEnumerationValue(typeof(OdbcType), (int)odbctype);
		}

		// Token: 0x06001958 RID: 6488 RVA: 0x00259178 File Offset: 0x00258578
		internal static void TraceODBC(int level, string method, ODBC32.RetCode retcode)
		{
			Bid.TraceSqlReturn("<odbc|API|ODBC|RET> %08X{SQLRETURN}, method=%ls\n", retcode, method);
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x00259198 File Offset: 0x00258598
		internal static void TraceODBC(int level, string method, string param, ODBC32.RetCode retcode)
		{
			Bid.TraceSqlReturn("<odbc|API|ODBC|RET> %08X{SQLRETURN}, method=%ls, param=%ls\n", retcode, method, param);
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x002591B8 File Offset: 0x002585B8
		internal static short ShortStringLength(string inputString)
		{
			return checked((short)ADP.StringLength(inputString));
		}

		// Token: 0x04000E4A RID: 3658
		internal const string Pwd = "pwd";
	}
}
