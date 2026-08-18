using System;
using System.Collections;
using OracleInternal.Common;

namespace Oracle.ManagedDataAccess.Client
{
	// Token: 0x02000064 RID: 100
	internal static class OracleTypeMapper
	{
		// Token: 0x0600050D RID: 1293 RVA: 0x0002E9A8 File Offset: 0x0002CBA8
		static OracleTypeMapper()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_CHAR, typeof(string));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_CHARN, typeof(string));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_DATE, typeof(DateTime));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_INTERVAL_DS, typeof(TimeSpan));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_INTERVAL_DS_DTY, typeof(TimeSpan));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_INTERVAL_YM, typeof(long));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_INTERVAL_YM_DTY, typeof(long));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_LONG, typeof(string));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_LONGRAW, typeof(byte[]));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_NUMBER, typeof(decimal));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_OCIBFileLocator, typeof(byte[]));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_OCIBLobLocator, typeof(byte[]));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_OCICLobLocator, typeof(string));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_UROWID, typeof(string));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_ROWID, typeof(string));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_RAW, typeof(byte[]));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_TIMESTAMP, typeof(DateTime));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_TIMESTAMP_DTY, typeof(DateTime));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_TIMESTAMP_TZ, typeof(DateTime));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_TIMESTAMP_TZ_DTY, typeof(DateTime));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_TIMESTAMP_LTZ, typeof(DateTime));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_TIMESTAMP_LTZ_DTY, typeof(DateTime));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_XMLTYPE, typeof(string));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_IBFLOAT, typeof(float));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_IBDOUBLE, typeof(double));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_BFLOAT, typeof(float));
				OracleTypeMapper.m_OraToNET.Add(OraType.ORA_BDOUBLE, typeof(double));
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_CHAR, OracleDbType.Char);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_CHARN, OracleDbType.Varchar2);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_DATE, OracleDbType.Date);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_INTERVAL_YM, OracleDbType.IntervalYM);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_INTERVAL_YM_DTY, OracleDbType.IntervalYM);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_INTERVAL_DS, OracleDbType.IntervalDS);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_INTERVAL_DS_DTY, OracleDbType.IntervalDS);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_LONG, OracleDbType.Long);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_LONGRAW, OracleDbType.LongRaw);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_NUMBER, OracleDbType.Decimal);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_OCIBFileLocator, OracleDbType.BFile);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_OCIBLobLocator, OracleDbType.Blob);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_OCICLobLocator, OracleDbType.Clob);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_UROWID, OracleDbType.Varchar2);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_ROWID, OracleDbType.Varchar2);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_RAW, OracleDbType.Raw);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_TIMESTAMP, OracleDbType.TimeStamp);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_TIMESTAMP_DTY, OracleDbType.TimeStamp);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_TIMESTAMP_TZ, OracleDbType.TimeStampTZ);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_TIMESTAMP_TZ_DTY, OracleDbType.TimeStampTZ);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_TIMESTAMP_LTZ, OracleDbType.TimeStampLTZ);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_TIMESTAMP_LTZ_DTY, OracleDbType.TimeStampLTZ);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_XMLTYPE, OracleDbType.XmlType);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_IBFLOAT, OracleDbType.BinaryFloat);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_IBDOUBLE, OracleDbType.BinaryDouble);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_BFLOAT, OracleDbType.BinaryFloat);
				OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_BDOUBLE, OracleDbType.BinaryDouble);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.Error, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x0400061B RID: 1563
		internal const string VARCHAR2 = "VARCHAR2";

		// Token: 0x0400061C RID: 1564
		internal const string NVARCHAR2 = "NVARCHAR2";

		// Token: 0x0400061D RID: 1565
		internal const string NUMBER = "NUMBER";

		// Token: 0x0400061E RID: 1566
		internal const string LONG = "LONG";

		// Token: 0x0400061F RID: 1567
		internal const string DATE = "DATE";

		// Token: 0x04000620 RID: 1568
		internal const string RAW = "RAW";

		// Token: 0x04000621 RID: 1569
		internal const string LONG_RAW = "LONG RAW";

		// Token: 0x04000622 RID: 1570
		internal const string ROWID = "ROWID";

		// Token: 0x04000623 RID: 1571
		internal const string CHAR = "CHAR";

		// Token: 0x04000624 RID: 1572
		internal const string NCHAR = "NCHAR";

		// Token: 0x04000625 RID: 1573
		internal const string BINARY_FLOAT = "BINARY_FLOAT";

		// Token: 0x04000626 RID: 1574
		internal const string BINARY_DOUBLE = "BINARY_DOUBLE";

		// Token: 0x04000627 RID: 1575
		internal const string UDT = "USERDEFINEDTYPE";

		// Token: 0x04000628 RID: 1576
		internal const string REF = "REF";

		// Token: 0x04000629 RID: 1577
		internal const string CLOB = "CLOB";

		// Token: 0x0400062A RID: 1578
		internal const string NCLOB = "NCLOB";

		// Token: 0x0400062B RID: 1579
		internal const string BLOB = "BLOB";

		// Token: 0x0400062C RID: 1580
		internal const string BFILE = "BFILE";

		// Token: 0x0400062D RID: 1581
		internal const string TIMESTAMP = "TIMESTAMP";

		// Token: 0x0400062E RID: 1582
		internal const string TIMESTAMP_WITH_TIME_ZONE = "TIMESTAMP WITH TIME ZONE";

		// Token: 0x0400062F RID: 1583
		internal const string INTERVAL_YEAR_TO_MONTH = "INTERVAL YEAR TO MONTH";

		// Token: 0x04000630 RID: 1584
		internal const string INTERVAL_DAY_TO_SECOND = "INTERVAL DAY TO SECOND";

		// Token: 0x04000631 RID: 1585
		internal const string UROWID = "UROWID";

		// Token: 0x04000632 RID: 1586
		internal const string TIMESTAMP_WITH_LOCAL_TIME_ZONE = "TIMESTAMP WITH LOCAL TIME ZONE";

		// Token: 0x04000633 RID: 1587
		internal const string XML = "XMLTYPE";

		// Token: 0x04000634 RID: 1588
		internal const int ORACLEDBTYPE_MINVAL = 101;

		// Token: 0x04000635 RID: 1589
		internal const int ORACLEDBTYPE_ENUM_COUNT = 34;

		// Token: 0x04000636 RID: 1590
		public static Hashtable m_OraToOraDb = new Hashtable(19);

		// Token: 0x04000637 RID: 1591
		public static Hashtable m_OraToNET = new Hashtable(19);

		// Token: 0x04000638 RID: 1592
		internal static string[] m_OraDbToOraNative = new string[]
		{
			"BFILE",
			"BLOB",
			"NUMBER",
			"CHAR",
			"CLOB",
			"DATE",
			"NUMBER",
			"NUMBER",
			"LONG",
			"LONG RAW",
			"NUMBER",
			"NUMBER",
			"NUMBER",
			"INTERVAL DAY TO SECOND",
			"INTERVAL YEAR TO MONTH",
			"NCLOB",
			"NCHAR",
			string.Empty,
			"NVARCHAR2",
			"RAW",
			string.Empty,
			"NUMBER",
			"TIMESTAMP",
			"TIMESTAMP WITH LOCAL TIME ZONE",
			"TIMESTAMP WITH TIME ZONE",
			"VARCHAR2",
			"XMLTYPE",
			string.Empty,
			string.Empty,
			string.Empty,
			string.Empty,
			"BINARY_DOUBLE",
			"BINARY_FLOAT",
			string.Empty
		};
	}
}
