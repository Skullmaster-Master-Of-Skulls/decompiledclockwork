using System;
using System.Collections;
using Oracle.DataAccess.Types;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000111 RID: 273
	internal class OraDb_DbTypeTable
	{
		// Token: 0x06000AB3 RID: 2739 RVA: 0x0006EB98 File Offset: 0x0006DB98
		private OraDb_DbTypeTable()
		{
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0006EBA0 File Offset: 0x0006DBA0
		static OraDb_DbTypeTable()
		{
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[0] = 126;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[22] = 104;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[1] = 120;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[2] = 103;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[5] = 106;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[6] = 123;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[7] = 107;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[8] = 108;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[10] = 111;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[11] = 112;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[12] = 113;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[15] = 122;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[16] = 126;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[23] = 104;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[17] = 123;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[13] = 129;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[101] = 13;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[102] = 13;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[103] = 2;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[104] = 23;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[105] = 13;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[106] = 5;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[107] = 7;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[108] = 8;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[132] = 8;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[111] = 10;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[112] = 11;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[113] = 12;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[114] = 13;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[115] = 12;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[109] = 16;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[110] = 1;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[117] = 23;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[116] = 13;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[119] = 16;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[120] = 1;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[121] = 13;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[122] = 15;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[133] = 15;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[123] = 6;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[124] = 6;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[125] = 6;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[126] = 16;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[127] = 16;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[129] = 13;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[130] = 13;
			OraDb_DbTypeTable.dbTypeToOracleDbTypeMapping[128] = 13;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[96] = 104;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[1] = 126;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[12] = 106;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[189] = 115;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[190] = 114;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[8] = 109;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[24] = 110;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[2] = 107;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[101] = 132;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[100] = 133;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[22] = 132;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[21] = 133;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[114] = 101;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[113] = 102;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[112] = 105;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[104] = 126;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[23] = 120;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[187] = 123;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[188] = 125;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[232] = 124;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[108] = 127;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[110] = 130;
			OraDb_DbTypeTable.oraTypeToOracleDbTypeMapping[122] = 128;
			OraDb_DbTypeTable.InsertTableEntries();
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0006EEB8 File Offset: 0x0006DEB8
		internal static void InsertTableEntries()
		{
			OraDb_DbTypeTable.s_table.Add(typeof(byte), OracleDbType.Byte);
			OraDb_DbTypeTable.s_table.Add(typeof(byte[]), OracleDbType.Raw);
			OraDb_DbTypeTable.s_table.Add(typeof(char), OracleDbType.Varchar2);
			OraDb_DbTypeTable.s_table.Add(typeof(char[]), OracleDbType.Varchar2);
			OraDb_DbTypeTable.s_table.Add(typeof(DateTime), OracleDbType.TimeStamp);
			OraDb_DbTypeTable.s_table.Add(typeof(short), OracleDbType.Int16);
			OraDb_DbTypeTable.s_table.Add(typeof(int), OracleDbType.Int32);
			OraDb_DbTypeTable.s_table.Add(typeof(long), OracleDbType.Int64);
			OraDb_DbTypeTable.s_table.Add(typeof(float), OracleDbType.Single);
			OraDb_DbTypeTable.s_table.Add(typeof(double), OracleDbType.Double);
			OraDb_DbTypeTable.s_table.Add(typeof(decimal), OracleDbType.Decimal);
			OraDb_DbTypeTable.s_table.Add(typeof(string), OracleDbType.Varchar2);
			OraDb_DbTypeTable.s_table.Add(typeof(TimeSpan), OracleDbType.IntervalDS);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleBFile), OracleDbType.BFile);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleBinary), OracleDbType.Raw);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleBlob), OracleDbType.Blob);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleClob), OracleDbType.Clob);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleDate), OracleDbType.Date);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleDecimal), OracleDbType.Decimal);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleIntervalDS), OracleDbType.IntervalDS);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleIntervalYM), OracleDbType.IntervalYM);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleRefCursor), OracleDbType.RefCursor);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleString), OracleDbType.Varchar2);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleTimeStamp), OracleDbType.TimeStamp);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleTimeStampLTZ), OracleDbType.TimeStampLTZ);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleTimeStampTZ), OracleDbType.TimeStampTZ);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleXmlType), OracleDbType.XmlType);
			OraDb_DbTypeTable.s_table.Add(typeof(OracleRef), OracleDbType.Ref);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0006F1BC File Offset: 0x0006E1BC
		internal static OracleDbType ConvertNumberToOraDbType(int precision, int scale)
		{
			OracleDbType result = OracleDbType.Decimal;
			if (scale <= 0 && precision - scale < 5)
			{
				result = OracleDbType.Int16;
			}
			else if (scale <= 0 && precision - scale < 10)
			{
				result = OracleDbType.Int32;
			}
			else if (scale <= 0 && precision - scale < 19)
			{
				result = OracleDbType.Int64;
			}
			else if (precision < 8 && ((scale <= 0 && precision - scale <= 38) || (scale > 0 && scale <= 44)))
			{
				result = OracleDbType.Single;
			}
			else if (precision < 16)
			{
				result = OracleDbType.Double;
			}
			return result;
		}

		// Token: 0x040008DE RID: 2270
		internal static Hashtable s_table = new Hashtable(91);

		// Token: 0x040008DF RID: 2271
		internal static int[] dbTypeToOracleDbTypeMapping = new int[134];

		// Token: 0x040008E0 RID: 2272
		internal static int[] oraTypeToOracleDbTypeMapping = new int[242];
	}
}
