using System;
using System.Collections;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000010 RID: 16
	internal class OracleTypeMapper
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000E050 File Offset: 0x0000D050
		private OracleTypeMapper()
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000E058 File Offset: 0x0000D058
		static OracleTypeMapper()
		{
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_CHAR, typeof(string));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_CHARN, typeof(string));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_DATE, typeof(DateTime));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_INTERVAL_DS, typeof(TimeSpan));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_INTERVAL_YM, typeof(long));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_LONG, typeof(string));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_LONGRAW, typeof(byte[]));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_NUMBER, typeof(decimal));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_OCIBFileLocator, typeof(byte[]));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_OCIBLobLocator, typeof(byte[]));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_OCICLobLocator, typeof(string));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_OCIRowid, typeof(string));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_RAW, typeof(byte[]));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_TIMESTAMP, typeof(DateTime));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_TIMESTAMP_TZ, typeof(DateTime));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_TIMESTAMP_LTZ, typeof(DateTime));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_NDT, typeof(string));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_IBFLOAT, typeof(float));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_IBDOUBLE, typeof(double));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_BFLOAT, typeof(float));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_BDOUBLE, typeof(double));
			OracleTypeMapper.m_OraToNET.Add(OraType.ORA_OCIRef, typeof(string));
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_CHAR, OracleDbType.Char);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_CHARN, OracleDbType.Varchar2);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_DATE, OracleDbType.Date);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_INTERVAL_YM, OracleDbType.IntervalYM);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_INTERVAL_DS, OracleDbType.IntervalDS);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_LONG, OracleDbType.Long);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_LONGRAW, OracleDbType.LongRaw);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_NUMBER, OracleDbType.Decimal);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_OCIBFileLocator, OracleDbType.BFile);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_OCIBLobLocator, OracleDbType.Blob);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_OCICLobLocator, OracleDbType.Clob);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_OCIRowid, OracleDbType.Varchar2);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_RAW, OracleDbType.Raw);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_TIMESTAMP, OracleDbType.TimeStamp);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_TIMESTAMP_TZ, OracleDbType.TimeStampTZ);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_TIMESTAMP_LTZ, OracleDbType.TimeStampLTZ);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_NDT, OracleDbType.XmlType);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_IBFLOAT, OracleDbType.BinaryFloat);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_IBDOUBLE, OracleDbType.BinaryDouble);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_BFLOAT, OracleDbType.BinaryFloat);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_BDOUBLE, OracleDbType.BinaryDouble);
			OracleTypeMapper.m_OraToOraDb.Add(OraType.ORA_OCIRef, OracleDbType.Ref);
		}

		// Token: 0x04000073 RID: 115
		public static Hashtable m_OraToOraDb = new Hashtable(19);

		// Token: 0x04000074 RID: 116
		public static Hashtable m_OraToNET = new Hashtable(19);
	}
}
