using System;
using System.Data.Common;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x0200028B RID: 651
	internal static class ODBC32
	{
		// Token: 0x0600272A RID: 10026 RVA: 0x00108C4C File Offset: 0x0010804C
		internal static string RetcodeToString(ODBC32.RetCode retcode)
		{
			switch (retcode)
			{
			case ODBC32.RetCode.INVALID_HANDLE:
				return "INVALID_HANDLE";
			case ODBC32.RetCode.ERROR:
				break;
			case ODBC32.RetCode.SUCCESS:
				return "SUCCESS";
			case ODBC32.RetCode.SUCCESS_WITH_INFO:
				return "SUCCESS_WITH_INFO";
			default:
				if (retcode == ODBC32.RetCode.NO_DATA)
				{
					return "NO_DATA";
				}
				break;
			}
			return "ERROR";
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x00108C98 File Offset: 0x00108098
		internal static OdbcErrorCollection GetDiagErrors(string source, OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			OdbcErrorCollection odbcErrorCollection = new OdbcErrorCollection();
			ODBC32.GetDiagErrors(odbcErrorCollection, source, hrHandle, retcode);
			return odbcErrorCollection;
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x00108CB8 File Offset: 0x001080B8
		internal static void GetDiagErrors(OdbcErrorCollection errors, string source, OdbcHandle hrHandle, ODBC32.RetCode retcode)
		{
			if (retcode != ODBC32.RetCode.SUCCESS)
			{
				short num = 0;
				short num2 = 0;
				StringBuilder stringBuilder = new StringBuilder(1024);
				bool flag = true;
				while (flag)
				{
					num += 1;
					string state;
					int nativeerror;
					retcode = hrHandle.GetDiagnosticRecord(num, out state, stringBuilder, out nativeerror, out num2);
					if (ODBC32.RetCode.SUCCESS_WITH_INFO == retcode && stringBuilder.Capacity - 1 < (int)num2)
					{
						stringBuilder.Capacity = (int)(num2 + 1);
						retcode = hrHandle.GetDiagnosticRecord(num, out state, stringBuilder, out nativeerror, out num2);
					}
					flag = (retcode == ODBC32.RetCode.SUCCESS || retcode == ODBC32.RetCode.SUCCESS_WITH_INFO);
					if (flag)
					{
						errors.Add(new OdbcError(source, stringBuilder.ToString(), state, nativeerror));
					}
				}
			}
		}

		// Token: 0x04001A03 RID: 6659
		internal const short SQL_COMMIT = 0;

		// Token: 0x04001A04 RID: 6660
		internal const short SQL_ROLLBACK = 1;

		// Token: 0x04001A05 RID: 6661
		internal static readonly IntPtr SQL_AUTOCOMMIT_OFF = ADP.PtrZero;

		// Token: 0x04001A06 RID: 6662
		internal static readonly IntPtr SQL_AUTOCOMMIT_ON = new IntPtr(1);

		// Token: 0x04001A07 RID: 6663
		private const int SIGNED_OFFSET = -20;

		// Token: 0x04001A08 RID: 6664
		private const int UNSIGNED_OFFSET = -22;

		// Token: 0x04001A09 RID: 6665
		internal const short SQL_ALL_TYPES = 0;

		// Token: 0x04001A0A RID: 6666
		internal static readonly IntPtr SQL_HANDLE_NULL = ADP.PtrZero;

		// Token: 0x04001A0B RID: 6667
		internal const int SQL_NULL_DATA = -1;

		// Token: 0x04001A0C RID: 6668
		internal const int SQL_NO_TOTAL = -4;

		// Token: 0x04001A0D RID: 6669
		internal const int SQL_DEFAULT_PARAM = -5;

		// Token: 0x04001A0E RID: 6670
		internal const int COLUMN_NAME = 4;

		// Token: 0x04001A0F RID: 6671
		internal const int COLUMN_TYPE = 5;

		// Token: 0x04001A10 RID: 6672
		internal const int DATA_TYPE = 6;

		// Token: 0x04001A11 RID: 6673
		internal const int COLUMN_SIZE = 8;

		// Token: 0x04001A12 RID: 6674
		internal const int DECIMAL_DIGITS = 10;

		// Token: 0x04001A13 RID: 6675
		internal const int NUM_PREC_RADIX = 11;

		// Token: 0x04001A14 RID: 6676
		internal static readonly IntPtr SQL_OV_ODBC3 = new IntPtr(3);

		// Token: 0x04001A15 RID: 6677
		internal const int SQL_NTS = -3;

		// Token: 0x04001A16 RID: 6678
		internal static readonly IntPtr SQL_CP_OFF = new IntPtr(0);

		// Token: 0x04001A17 RID: 6679
		internal static readonly IntPtr SQL_CP_ONE_PER_DRIVER = new IntPtr(1);

		// Token: 0x04001A18 RID: 6680
		internal static readonly IntPtr SQL_CP_ONE_PER_HENV = new IntPtr(2);

		// Token: 0x04001A19 RID: 6681
		internal const int SQL_CD_TRUE = 1;

		// Token: 0x04001A1A RID: 6682
		internal const int SQL_CD_FALSE = 0;

		// Token: 0x04001A1B RID: 6683
		internal const int SQL_DTC_DONE = 0;

		// Token: 0x04001A1C RID: 6684
		internal const int SQL_IS_POINTER = -4;

		// Token: 0x04001A1D RID: 6685
		internal const int SQL_IS_PTR = 1;

		// Token: 0x04001A1E RID: 6686
		internal const int MAX_CONNECTION_STRING_LENGTH = 1024;

		// Token: 0x04001A1F RID: 6687
		internal const short SQL_DIAG_SQLSTATE = 4;

		// Token: 0x04001A20 RID: 6688
		internal const short SQL_RESULT_COL = 3;

		// Token: 0x02000409 RID: 1033
		internal enum SQL_HANDLE : short
		{
			// Token: 0x040021C9 RID: 8649
			ENV = 1,
			// Token: 0x040021CA RID: 8650
			DBC,
			// Token: 0x040021CB RID: 8651
			STMT,
			// Token: 0x040021CC RID: 8652
			DESC
		}

		// Token: 0x0200040A RID: 1034
		[Serializable]
		public enum RETCODE
		{
			// Token: 0x040021CE RID: 8654
			SUCCESS,
			// Token: 0x040021CF RID: 8655
			SUCCESS_WITH_INFO,
			// Token: 0x040021D0 RID: 8656
			ERROR = -1,
			// Token: 0x040021D1 RID: 8657
			INVALID_HANDLE = -2,
			// Token: 0x040021D2 RID: 8658
			NO_DATA = 100
		}

		// Token: 0x0200040B RID: 1035
		internal enum RetCode : short
		{
			// Token: 0x040021D4 RID: 8660
			SUCCESS,
			// Token: 0x040021D5 RID: 8661
			SUCCESS_WITH_INFO,
			// Token: 0x040021D6 RID: 8662
			ERROR = -1,
			// Token: 0x040021D7 RID: 8663
			INVALID_HANDLE = -2,
			// Token: 0x040021D8 RID: 8664
			NO_DATA = 100
		}

		// Token: 0x0200040C RID: 1036
		internal enum SQL_CONVERT : ushort
		{
			// Token: 0x040021DA RID: 8666
			BIGINT = 53,
			// Token: 0x040021DB RID: 8667
			BINARY,
			// Token: 0x040021DC RID: 8668
			BIT,
			// Token: 0x040021DD RID: 8669
			CHAR,
			// Token: 0x040021DE RID: 8670
			DATE,
			// Token: 0x040021DF RID: 8671
			DECIMAL,
			// Token: 0x040021E0 RID: 8672
			DOUBLE,
			// Token: 0x040021E1 RID: 8673
			FLOAT,
			// Token: 0x040021E2 RID: 8674
			INTEGER,
			// Token: 0x040021E3 RID: 8675
			LONGVARCHAR,
			// Token: 0x040021E4 RID: 8676
			NUMERIC,
			// Token: 0x040021E5 RID: 8677
			REAL,
			// Token: 0x040021E6 RID: 8678
			SMALLINT,
			// Token: 0x040021E7 RID: 8679
			TIME,
			// Token: 0x040021E8 RID: 8680
			TIMESTAMP,
			// Token: 0x040021E9 RID: 8681
			TINYINT,
			// Token: 0x040021EA RID: 8682
			VARBINARY,
			// Token: 0x040021EB RID: 8683
			VARCHAR,
			// Token: 0x040021EC RID: 8684
			LONGVARBINARY
		}

		// Token: 0x0200040D RID: 1037
		[Flags]
		internal enum SQL_CVT
		{
			// Token: 0x040021EE RID: 8686
			CHAR = 1,
			// Token: 0x040021EF RID: 8687
			NUMERIC = 2,
			// Token: 0x040021F0 RID: 8688
			DECIMAL = 4,
			// Token: 0x040021F1 RID: 8689
			INTEGER = 8,
			// Token: 0x040021F2 RID: 8690
			SMALLINT = 16,
			// Token: 0x040021F3 RID: 8691
			FLOAT = 32,
			// Token: 0x040021F4 RID: 8692
			REAL = 64,
			// Token: 0x040021F5 RID: 8693
			DOUBLE = 128,
			// Token: 0x040021F6 RID: 8694
			VARCHAR = 256,
			// Token: 0x040021F7 RID: 8695
			LONGVARCHAR = 512,
			// Token: 0x040021F8 RID: 8696
			BINARY = 1024,
			// Token: 0x040021F9 RID: 8697
			VARBINARY = 2048,
			// Token: 0x040021FA RID: 8698
			BIT = 4096,
			// Token: 0x040021FB RID: 8699
			TINYINT = 8192,
			// Token: 0x040021FC RID: 8700
			BIGINT = 16384,
			// Token: 0x040021FD RID: 8701
			DATE = 32768,
			// Token: 0x040021FE RID: 8702
			TIME = 65536,
			// Token: 0x040021FF RID: 8703
			TIMESTAMP = 131072,
			// Token: 0x04002200 RID: 8704
			LONGVARBINARY = 262144,
			// Token: 0x04002201 RID: 8705
			INTERVAL_YEAR_MONTH = 524288,
			// Token: 0x04002202 RID: 8706
			INTERVAL_DAY_TIME = 1048576,
			// Token: 0x04002203 RID: 8707
			WCHAR = 2097152,
			// Token: 0x04002204 RID: 8708
			WLONGVARCHAR = 4194304,
			// Token: 0x04002205 RID: 8709
			WVARCHAR = 8388608,
			// Token: 0x04002206 RID: 8710
			GUID = 16777216
		}

		// Token: 0x0200040E RID: 1038
		internal enum STMT : short
		{
			// Token: 0x04002208 RID: 8712
			CLOSE,
			// Token: 0x04002209 RID: 8713
			DROP,
			// Token: 0x0400220A RID: 8714
			UNBIND,
			// Token: 0x0400220B RID: 8715
			RESET_PARAMS
		}

		// Token: 0x0200040F RID: 1039
		internal enum SQL_IS
		{
			// Token: 0x0400220D RID: 8717
			POINTER = -4,
			// Token: 0x0400220E RID: 8718
			INTEGER = -6,
			// Token: 0x0400220F RID: 8719
			UINTEGER,
			// Token: 0x04002210 RID: 8720
			SMALLINT = -8
		}

		// Token: 0x02000410 RID: 1040
		internal enum SQL_TRANSACTION
		{
			// Token: 0x04002212 RID: 8722
			READ_UNCOMMITTED = 1,
			// Token: 0x04002213 RID: 8723
			READ_COMMITTED,
			// Token: 0x04002214 RID: 8724
			REPEATABLE_READ = 4,
			// Token: 0x04002215 RID: 8725
			SERIALIZABLE = 8,
			// Token: 0x04002216 RID: 8726
			SNAPSHOT = 32
		}

		// Token: 0x02000411 RID: 1041
		internal enum SQL_PARAM
		{
			// Token: 0x04002218 RID: 8728
			INPUT = 1,
			// Token: 0x04002219 RID: 8729
			INPUT_OUTPUT,
			// Token: 0x0400221A RID: 8730
			OUTPUT = 4,
			// Token: 0x0400221B RID: 8731
			RETURN_VALUE
		}

		// Token: 0x02000412 RID: 1042
		internal enum SQL_API : ushort
		{
			// Token: 0x0400221D RID: 8733
			SQLCOLUMNS = 40,
			// Token: 0x0400221E RID: 8734
			SQLEXECDIRECT = 11,
			// Token: 0x0400221F RID: 8735
			SQLGETTYPEINFO = 47,
			// Token: 0x04002220 RID: 8736
			SQLPROCEDURECOLUMNS = 66,
			// Token: 0x04002221 RID: 8737
			SQLPROCEDURES,
			// Token: 0x04002222 RID: 8738
			SQLSTATISTICS = 53,
			// Token: 0x04002223 RID: 8739
			SQLTABLES
		}

		// Token: 0x02000413 RID: 1043
		internal enum SQL_DESC : short
		{
			// Token: 0x04002225 RID: 8741
			COUNT = 1001,
			// Token: 0x04002226 RID: 8742
			TYPE,
			// Token: 0x04002227 RID: 8743
			LENGTH,
			// Token: 0x04002228 RID: 8744
			OCTET_LENGTH_PTR,
			// Token: 0x04002229 RID: 8745
			PRECISION,
			// Token: 0x0400222A RID: 8746
			SCALE,
			// Token: 0x0400222B RID: 8747
			DATETIME_INTERVAL_CODE,
			// Token: 0x0400222C RID: 8748
			NULLABLE,
			// Token: 0x0400222D RID: 8749
			INDICATOR_PTR,
			// Token: 0x0400222E RID: 8750
			DATA_PTR,
			// Token: 0x0400222F RID: 8751
			NAME,
			// Token: 0x04002230 RID: 8752
			UNNAMED,
			// Token: 0x04002231 RID: 8753
			OCTET_LENGTH,
			// Token: 0x04002232 RID: 8754
			ALLOC_TYPE = 1099,
			// Token: 0x04002233 RID: 8755
			CONCISE_TYPE = 2,
			// Token: 0x04002234 RID: 8756
			DISPLAY_SIZE = 6,
			// Token: 0x04002235 RID: 8757
			UNSIGNED = 8,
			// Token: 0x04002236 RID: 8758
			UPDATABLE = 10,
			// Token: 0x04002237 RID: 8759
			AUTO_UNIQUE_VALUE,
			// Token: 0x04002238 RID: 8760
			TYPE_NAME = 14,
			// Token: 0x04002239 RID: 8761
			TABLE_NAME,
			// Token: 0x0400223A RID: 8762
			SCHEMA_NAME,
			// Token: 0x0400223B RID: 8763
			CATALOG_NAME,
			// Token: 0x0400223C RID: 8764
			BASE_COLUMN_NAME = 22,
			// Token: 0x0400223D RID: 8765
			BASE_TABLE_NAME
		}

		// Token: 0x02000414 RID: 1044
		internal enum SQL_COLUMN
		{
			// Token: 0x0400223F RID: 8767
			COUNT,
			// Token: 0x04002240 RID: 8768
			NAME,
			// Token: 0x04002241 RID: 8769
			TYPE,
			// Token: 0x04002242 RID: 8770
			LENGTH,
			// Token: 0x04002243 RID: 8771
			PRECISION,
			// Token: 0x04002244 RID: 8772
			SCALE,
			// Token: 0x04002245 RID: 8773
			DISPLAY_SIZE,
			// Token: 0x04002246 RID: 8774
			NULLABLE,
			// Token: 0x04002247 RID: 8775
			UNSIGNED,
			// Token: 0x04002248 RID: 8776
			MONEY,
			// Token: 0x04002249 RID: 8777
			UPDATABLE,
			// Token: 0x0400224A RID: 8778
			AUTO_INCREMENT,
			// Token: 0x0400224B RID: 8779
			CASE_SENSITIVE,
			// Token: 0x0400224C RID: 8780
			SEARCHABLE,
			// Token: 0x0400224D RID: 8781
			TYPE_NAME,
			// Token: 0x0400224E RID: 8782
			TABLE_NAME,
			// Token: 0x0400224F RID: 8783
			OWNER_NAME,
			// Token: 0x04002250 RID: 8784
			QUALIFIER_NAME,
			// Token: 0x04002251 RID: 8785
			LABEL
		}

		// Token: 0x02000415 RID: 1045
		internal enum SQL_SPECIALCOLS : ushort
		{
			// Token: 0x04002253 RID: 8787
			BEST_ROWID = 1,
			// Token: 0x04002254 RID: 8788
			ROWVER
		}

		// Token: 0x02000416 RID: 1046
		internal enum SQL_SCOPE : ushort
		{
			// Token: 0x04002256 RID: 8790
			CURROW,
			// Token: 0x04002257 RID: 8791
			TRANSACTION,
			// Token: 0x04002258 RID: 8792
			SESSION
		}

		// Token: 0x02000417 RID: 1047
		internal enum SQL_NULLABILITY : ushort
		{
			// Token: 0x0400225A RID: 8794
			NO_NULLS,
			// Token: 0x0400225B RID: 8795
			NULLABLE,
			// Token: 0x0400225C RID: 8796
			UNKNOWN
		}

		// Token: 0x02000418 RID: 1048
		internal enum HANDLER
		{
			// Token: 0x0400225E RID: 8798
			IGNORE,
			// Token: 0x0400225F RID: 8799
			THROW
		}

		// Token: 0x02000419 RID: 1049
		internal enum SQL_C : short
		{
			// Token: 0x04002261 RID: 8801
			CHAR = 1,
			// Token: 0x04002262 RID: 8802
			WCHAR = -8,
			// Token: 0x04002263 RID: 8803
			SLONG = -16,
			// Token: 0x04002264 RID: 8804
			SSHORT,
			// Token: 0x04002265 RID: 8805
			REAL = 7,
			// Token: 0x04002266 RID: 8806
			DOUBLE,
			// Token: 0x04002267 RID: 8807
			BIT = -7,
			// Token: 0x04002268 RID: 8808
			UTINYINT = -28,
			// Token: 0x04002269 RID: 8809
			SBIGINT = -25,
			// Token: 0x0400226A RID: 8810
			UBIGINT = -27,
			// Token: 0x0400226B RID: 8811
			BINARY = -2,
			// Token: 0x0400226C RID: 8812
			TIMESTAMP = 11,
			// Token: 0x0400226D RID: 8813
			TYPE_DATE = 91,
			// Token: 0x0400226E RID: 8814
			TYPE_TIME,
			// Token: 0x0400226F RID: 8815
			TYPE_TIMESTAMP,
			// Token: 0x04002270 RID: 8816
			NUMERIC = 2,
			// Token: 0x04002271 RID: 8817
			GUID = -11,
			// Token: 0x04002272 RID: 8818
			DEFAULT = 99,
			// Token: 0x04002273 RID: 8819
			ARD_TYPE = -99
		}

		// Token: 0x0200041A RID: 1050
		internal enum SQL_TYPE : short
		{
			// Token: 0x04002275 RID: 8821
			CHAR = 1,
			// Token: 0x04002276 RID: 8822
			VARCHAR = 12,
			// Token: 0x04002277 RID: 8823
			LONGVARCHAR = -1,
			// Token: 0x04002278 RID: 8824
			WCHAR = -8,
			// Token: 0x04002279 RID: 8825
			WVARCHAR = -9,
			// Token: 0x0400227A RID: 8826
			WLONGVARCHAR = -10,
			// Token: 0x0400227B RID: 8827
			DECIMAL = 3,
			// Token: 0x0400227C RID: 8828
			NUMERIC = 2,
			// Token: 0x0400227D RID: 8829
			SMALLINT = 5,
			// Token: 0x0400227E RID: 8830
			INTEGER = 4,
			// Token: 0x0400227F RID: 8831
			REAL = 7,
			// Token: 0x04002280 RID: 8832
			FLOAT = 6,
			// Token: 0x04002281 RID: 8833
			DOUBLE = 8,
			// Token: 0x04002282 RID: 8834
			BIT = -7,
			// Token: 0x04002283 RID: 8835
			TINYINT,
			// Token: 0x04002284 RID: 8836
			BIGINT,
			// Token: 0x04002285 RID: 8837
			BINARY = -2,
			// Token: 0x04002286 RID: 8838
			VARBINARY = -3,
			// Token: 0x04002287 RID: 8839
			LONGVARBINARY = -4,
			// Token: 0x04002288 RID: 8840
			TYPE_DATE = 91,
			// Token: 0x04002289 RID: 8841
			TYPE_TIME,
			// Token: 0x0400228A RID: 8842
			TIMESTAMP = 11,
			// Token: 0x0400228B RID: 8843
			TYPE_TIMESTAMP = 93,
			// Token: 0x0400228C RID: 8844
			GUID = -11,
			// Token: 0x0400228D RID: 8845
			SS_VARIANT = -150,
			// Token: 0x0400228E RID: 8846
			SS_UDT = -151,
			// Token: 0x0400228F RID: 8847
			SS_XML = -152,
			// Token: 0x04002290 RID: 8848
			SS_UTCDATETIME = -153,
			// Token: 0x04002291 RID: 8849
			SS_TIME_EX = -154
		}

		// Token: 0x0200041B RID: 1051
		internal enum SQL_ATTR
		{
			// Token: 0x04002293 RID: 8851
			APP_ROW_DESC = 10010,
			// Token: 0x04002294 RID: 8852
			APP_PARAM_DESC,
			// Token: 0x04002295 RID: 8853
			IMP_ROW_DESC,
			// Token: 0x04002296 RID: 8854
			IMP_PARAM_DESC,
			// Token: 0x04002297 RID: 8855
			METADATA_ID,
			// Token: 0x04002298 RID: 8856
			ODBC_VERSION = 200,
			// Token: 0x04002299 RID: 8857
			CONNECTION_POOLING,
			// Token: 0x0400229A RID: 8858
			AUTOCOMMIT = 102,
			// Token: 0x0400229B RID: 8859
			TXN_ISOLATION = 108,
			// Token: 0x0400229C RID: 8860
			CURRENT_CATALOG,
			// Token: 0x0400229D RID: 8861
			LOGIN_TIMEOUT = 103,
			// Token: 0x0400229E RID: 8862
			QUERY_TIMEOUT = 0,
			// Token: 0x0400229F RID: 8863
			CONNECTION_DEAD = 1209,
			// Token: 0x040022A0 RID: 8864
			SQL_COPT_SS_BASE = 1200,
			// Token: 0x040022A1 RID: 8865
			SQL_COPT_SS_ENLIST_IN_DTC = 1207,
			// Token: 0x040022A2 RID: 8866
			SQL_COPT_SS_TXN_ISOLATION = 1227
		}

		// Token: 0x0200041C RID: 1052
		internal enum SQL_INFO : ushort
		{
			// Token: 0x040022A4 RID: 8868
			DATA_SOURCE_NAME = 2,
			// Token: 0x040022A5 RID: 8869
			SERVER_NAME = 13,
			// Token: 0x040022A6 RID: 8870
			DRIVER_NAME = 6,
			// Token: 0x040022A7 RID: 8871
			DRIVER_VER,
			// Token: 0x040022A8 RID: 8872
			ODBC_VER = 10,
			// Token: 0x040022A9 RID: 8873
			SEARCH_PATTERN_ESCAPE = 14,
			// Token: 0x040022AA RID: 8874
			DBMS_VER = 18,
			// Token: 0x040022AB RID: 8875
			DBMS_NAME = 17,
			// Token: 0x040022AC RID: 8876
			IDENTIFIER_CASE = 28,
			// Token: 0x040022AD RID: 8877
			IDENTIFIER_QUOTE_CHAR,
			// Token: 0x040022AE RID: 8878
			CATALOG_NAME_SEPARATOR = 41,
			// Token: 0x040022AF RID: 8879
			DRIVER_ODBC_VER = 77,
			// Token: 0x040022B0 RID: 8880
			GROUP_BY = 88,
			// Token: 0x040022B1 RID: 8881
			KEYWORDS,
			// Token: 0x040022B2 RID: 8882
			ORDER_BY_COLUMNS_IN_SELECT,
			// Token: 0x040022B3 RID: 8883
			QUOTED_IDENTIFIER_CASE = 93,
			// Token: 0x040022B4 RID: 8884
			SQL_OJ_CAPABILITIES_30 = 115,
			// Token: 0x040022B5 RID: 8885
			SQL_OJ_CAPABILITIES_20 = 65003,
			// Token: 0x040022B6 RID: 8886
			SQL_SQL92_RELATIONAL_JOIN_OPERATORS = 161
		}
	}
}
