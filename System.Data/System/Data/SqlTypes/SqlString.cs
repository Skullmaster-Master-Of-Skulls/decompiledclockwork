using System;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000357 RID: 855
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlString : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06002EBF RID: 11967 RVA: 0x002D2328 File Offset: 0x002D1728
		private SqlString(bool fNull)
		{
			this.m_value = null;
			this.m_cmpInfo = null;
			this.m_lcid = 0;
			this.m_flag = SqlCompareOptions.None;
			this.m_fNotNull = false;
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x002D2358 File Offset: 0x002D1758
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data, int index, int count, bool fUnicode)
		{
			this.m_lcid = lcid;
			SqlString.ValidateSqlCompareOptions(compareOptions);
			this.m_flag = compareOptions;
			if (data == null)
			{
				this.m_fNotNull = false;
				this.m_value = null;
				this.m_cmpInfo = null;
				return;
			}
			this.m_fNotNull = true;
			this.m_cmpInfo = null;
			if (fUnicode)
			{
				this.m_value = SqlString.x_UnicodeEncoding.GetString(data, index, count);
				return;
			}
			CultureInfo cultureInfo = new CultureInfo(this.m_lcid);
			Encoding encoding = Encoding.GetEncoding(cultureInfo.TextInfo.ANSICodePage);
			this.m_value = encoding.GetString(data, index, count);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x002D23E8 File Offset: 0x002D17E8
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data, bool fUnicode)
		{
			this = new SqlString(lcid, compareOptions, data, 0, data.Length, fUnicode);
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x002D2408 File Offset: 0x002D1808
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data, int index, int count)
		{
			this = new SqlString(lcid, compareOptions, data, index, count, true);
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x002D2428 File Offset: 0x002D1828
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data)
		{
			this = new SqlString(lcid, compareOptions, data, 0, data.Length, true);
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x002D2448 File Offset: 0x002D1848
		public SqlString(string data, int lcid, SqlCompareOptions compareOptions)
		{
			this.m_lcid = lcid;
			SqlString.ValidateSqlCompareOptions(compareOptions);
			this.m_flag = compareOptions;
			this.m_cmpInfo = null;
			if (data == null)
			{
				this.m_fNotNull = false;
				this.m_value = null;
				return;
			}
			this.m_fNotNull = true;
			this.m_value = data;
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x002D2498 File Offset: 0x002D1898
		public SqlString(string data, int lcid)
		{
			this = new SqlString(data, lcid, SqlString.x_iDefaultFlag);
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x002D24B8 File Offset: 0x002D18B8
		public SqlString(string data)
		{
			this = new SqlString(data, CultureInfo.CurrentCulture.LCID, SqlString.x_iDefaultFlag);
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x002D24E8 File Offset: 0x002D18E8
		private SqlString(int lcid, SqlCompareOptions compareOptions, string data, CompareInfo cmpInfo)
		{
			this.m_lcid = lcid;
			SqlString.ValidateSqlCompareOptions(compareOptions);
			this.m_flag = compareOptions;
			if (data == null)
			{
				this.m_fNotNull = false;
				this.m_value = null;
				this.m_cmpInfo = null;
				return;
			}
			this.m_value = data;
			this.m_cmpInfo = cmpInfo;
			this.m_fNotNull = true;
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002EC8 RID: 11976 RVA: 0x002D2538 File Offset: 0x002D1938
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x002D2558 File Offset: 0x002D1958
		public string Value
		{
			get
			{
				if (!this.IsNull)
				{
					return this.m_value;
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x06002ECA RID: 11978 RVA: 0x002D2588 File Offset: 0x002D1988
		public int LCID
		{
			get
			{
				if (!this.IsNull)
				{
					return this.m_lcid;
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x002D25B8 File Offset: 0x002D19B8
		public CultureInfo CultureInfo
		{
			get
			{
				if (!this.IsNull)
				{
					return CultureInfo.GetCultureInfo(this.m_lcid);
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x002D25E8 File Offset: 0x002D19E8
		private void SetCompareInfo()
		{
			if (this.m_cmpInfo == null)
			{
				this.m_cmpInfo = CultureInfo.GetCultureInfo(this.m_lcid).CompareInfo;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x002D2618 File Offset: 0x002D1A18
		public CompareInfo CompareInfo
		{
			get
			{
				if (!this.IsNull)
				{
					this.SetCompareInfo();
					return this.m_cmpInfo;
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002ECE RID: 11982 RVA: 0x002D2648 File Offset: 0x002D1A48
		public SqlCompareOptions SqlCompareOptions
		{
			get
			{
				if (!this.IsNull)
				{
					return this.m_flag;
				}
				throw new SqlNullValueException();
			}
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x002D2678 File Offset: 0x002D1A78
		public static implicit operator SqlString(string x)
		{
			return new SqlString(x);
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x002D2698 File Offset: 0x002D1A98
		public static explicit operator string(SqlString x)
		{
			return x.Value;
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x002D26B8 File Offset: 0x002D1AB8
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value;
			}
			return SQLResource.NullString;
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x002D26E8 File Offset: 0x002D1AE8
		public byte[] GetUnicodeBytes()
		{
			if (this.IsNull)
			{
				return null;
			}
			return SqlString.x_UnicodeEncoding.GetBytes(this.m_value);
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x002D2718 File Offset: 0x002D1B18
		public byte[] GetNonUnicodeBytes()
		{
			if (this.IsNull)
			{
				return null;
			}
			CultureInfo cultureInfo = new CultureInfo(this.m_lcid);
			Encoding encoding = Encoding.GetEncoding(cultureInfo.TextInfo.ANSICodePage);
			return encoding.GetBytes(this.m_value);
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x002D2758 File Offset: 0x002D1B58
		public static SqlString operator +(SqlString x, SqlString y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlString.Null;
			}
			if (x.m_lcid != y.m_lcid || x.m_flag != y.m_flag)
			{
				throw new SqlTypeException(SQLResource.ConcatDiffCollationMessage);
			}
			return new SqlString(x.m_lcid, x.m_flag, x.m_value + y.m_value, (x.m_cmpInfo == null) ? y.m_cmpInfo : x.m_cmpInfo);
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x002D27E8 File Offset: 0x002D1BE8
		private static SqlBoolean Compare(SqlString x, SqlString y, EComparison ecExpectedResult)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			if (x.m_lcid != y.m_lcid || x.m_flag != y.m_flag)
			{
				throw new SqlTypeException(SQLResource.CompareDiffCollationMessage);
			}
			x.SetCompareInfo();
			y.SetCompareInfo();
			int num;
			if ((x.m_flag & SqlCompareOptions.BinarySort) != SqlCompareOptions.None)
			{
				num = SqlString.CompareBinary(x, y);
			}
			else if ((x.m_flag & SqlCompareOptions.BinarySort2) != SqlCompareOptions.None)
			{
				num = SqlString.CompareBinary2(x, y);
			}
			else
			{
				char[] array = x.m_value.ToCharArray();
				char[] array2 = y.m_value.ToCharArray();
				int i = array.Length;
				int num2 = array2.Length;
				while (i > 0)
				{
					if (array[i - 1] != ' ')
					{
						break;
					}
					i--;
				}
				while (num2 > 0 && array2[num2 - 1] == ' ')
				{
					num2--;
				}
				string @string = (i == array.Length) ? x.m_value : new string(array, 0, i);
				string string2 = (num2 == array2.Length) ? y.m_value : new string(array2, 0, num2);
				CompareOptions options = SqlString.CompareOptionsFromSqlCompareOptions(x.m_flag);
				num = x.m_cmpInfo.Compare(@string, string2, options);
			}
			bool value;
			switch (ecExpectedResult)
			{
			case EComparison.LT:
				value = (num < 0);
				break;
			case EComparison.LE:
				value = (num <= 0);
				break;
			case EComparison.EQ:
				value = (num == 0);
				break;
			case EComparison.GE:
				value = (num >= 0);
				break;
			case EComparison.GT:
				value = (num > 0);
				break;
			default:
				return SqlBoolean.Null;
			}
			return new SqlBoolean(value);
		}

		// Token: 0x06002ED6 RID: 11990 RVA: 0x002D2988 File Offset: 0x002D1D88
		public static explicit operator SqlString(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString());
			}
			return SqlString.Null;
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x002D29B8 File Offset: 0x002D1DB8
		public static explicit operator SqlString(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x002D29F8 File Offset: 0x002D1DF8
		public static explicit operator SqlString(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x002D2A38 File Offset: 0x002D1E38
		public static explicit operator SqlString(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x002D2A78 File Offset: 0x002D1E78
		public static explicit operator SqlString(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x002D2AB8 File Offset: 0x002D1EB8
		public static explicit operator SqlString(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x002D2AF8 File Offset: 0x002D1EF8
		public static explicit operator SqlString(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x002D2B38 File Offset: 0x002D1F38
		public static explicit operator SqlString(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.ToString());
			}
			return SqlString.Null;
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x002D2B68 File Offset: 0x002D1F68
		public static explicit operator SqlString(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.ToString());
			}
			return SqlString.Null;
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x002D2B98 File Offset: 0x002D1F98
		public static explicit operator SqlString(SqlDateTime x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.ToString());
			}
			return SqlString.Null;
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x002D2BC8 File Offset: 0x002D1FC8
		public static explicit operator SqlString(SqlGuid x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.ToString());
			}
			return SqlString.Null;
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x002D2BF8 File Offset: 0x002D1FF8
		public SqlString Clone()
		{
			if (this.IsNull)
			{
				return new SqlString(true);
			}
			SqlString result = new SqlString(this.m_value, this.m_lcid, this.m_flag);
			return result;
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x002D2C38 File Offset: 0x002D2038
		public static SqlBoolean operator ==(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.EQ);
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x002D2C58 File Offset: 0x002D2058
		public static SqlBoolean operator !=(SqlString x, SqlString y)
		{
			return !(x == y);
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x002D2C78 File Offset: 0x002D2078
		public static SqlBoolean operator <(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.LT);
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x002D2C98 File Offset: 0x002D2098
		public static SqlBoolean operator >(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.GT);
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x002D2CB8 File Offset: 0x002D20B8
		public static SqlBoolean operator <=(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.LE);
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x002D2CD8 File Offset: 0x002D20D8
		public static SqlBoolean operator >=(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.GE);
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x002D2CF8 File Offset: 0x002D20F8
		public static SqlString Concat(SqlString x, SqlString y)
		{
			return x + y;
		}

		// Token: 0x06002EE9 RID: 12009 RVA: 0x002D2D18 File Offset: 0x002D2118
		public static SqlString Add(SqlString x, SqlString y)
		{
			return x + y;
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x002D2D38 File Offset: 0x002D2138
		public static SqlBoolean Equals(SqlString x, SqlString y)
		{
			return x == y;
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x002D2D58 File Offset: 0x002D2158
		public static SqlBoolean NotEquals(SqlString x, SqlString y)
		{
			return x != y;
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x002D2D78 File Offset: 0x002D2178
		public static SqlBoolean LessThan(SqlString x, SqlString y)
		{
			return x < y;
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x002D2D98 File Offset: 0x002D2198
		public static SqlBoolean GreaterThan(SqlString x, SqlString y)
		{
			return x > y;
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x002D2DB8 File Offset: 0x002D21B8
		public static SqlBoolean LessThanOrEqual(SqlString x, SqlString y)
		{
			return x <= y;
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x002D2DD8 File Offset: 0x002D21D8
		public static SqlBoolean GreaterThanOrEqual(SqlString x, SqlString y)
		{
			return x >= y;
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x002D2DF8 File Offset: 0x002D21F8
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x002D2E18 File Offset: 0x002D2218
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x002D2E38 File Offset: 0x002D2238
		public SqlDateTime ToSqlDateTime()
		{
			return (SqlDateTime)this;
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x002D2E58 File Offset: 0x002D2258
		public SqlDouble ToSqlDouble()
		{
			return (SqlDouble)this;
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x002D2E78 File Offset: 0x002D2278
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x06002EF5 RID: 12021 RVA: 0x002D2E98 File Offset: 0x002D2298
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		// Token: 0x06002EF6 RID: 12022 RVA: 0x002D2EB8 File Offset: 0x002D22B8
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x002D2ED8 File Offset: 0x002D22D8
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x002D2EF8 File Offset: 0x002D22F8
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x002D2F18 File Offset: 0x002D2318
		public SqlSingle ToSqlSingle()
		{
			return (SqlSingle)this;
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x002D2F38 File Offset: 0x002D2338
		public SqlGuid ToSqlGuid()
		{
			return (SqlGuid)this;
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x002D2F58 File Offset: 0x002D2358
		private static void ValidateSqlCompareOptions(SqlCompareOptions compareOptions)
		{
			if ((compareOptions & SqlString.x_iValidSqlCompareOptionMask) != compareOptions)
			{
				throw new ArgumentOutOfRangeException("compareOptions");
			}
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x002D2F88 File Offset: 0x002D2388
		public static CompareOptions CompareOptionsFromSqlCompareOptions(SqlCompareOptions compareOptions)
		{
			CompareOptions compareOptions2 = CompareOptions.None;
			SqlString.ValidateSqlCompareOptions(compareOptions);
			if ((compareOptions & (SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2)) != SqlCompareOptions.None)
			{
				throw ADP.ArgumentOutOfRange("compareOptions");
			}
			if ((compareOptions & SqlCompareOptions.IgnoreCase) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreCase;
			}
			if ((compareOptions & SqlCompareOptions.IgnoreNonSpace) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreNonSpace;
			}
			if ((compareOptions & SqlCompareOptions.IgnoreKanaType) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreKanaType;
			}
			if ((compareOptions & SqlCompareOptions.IgnoreWidth) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreWidth;
			}
			return compareOptions2;
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x002D2FD8 File Offset: 0x002D23D8
		private bool FBinarySort()
		{
			return !this.IsNull && (this.m_flag & (SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2)) != SqlCompareOptions.None;
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x002D3008 File Offset: 0x002D2408
		private static int CompareBinary(SqlString x, SqlString y)
		{
			byte[] bytes = SqlString.x_UnicodeEncoding.GetBytes(x.m_value);
			byte[] bytes2 = SqlString.x_UnicodeEncoding.GetBytes(y.m_value);
			int num = bytes.Length;
			int num2 = bytes2.Length;
			int num3 = (num < num2) ? num : num2;
			int i;
			for (i = 0; i < num3; i++)
			{
				if (bytes[i] < bytes2[i])
				{
					return -1;
				}
				if (bytes[i] > bytes2[i])
				{
					return 1;
				}
			}
			i = num3;
			int num4 = 32;
			if (num < num2)
			{
				while (i < num2)
				{
					int num5 = (int)bytes2[i + 1] << (int)(8 + bytes2[i]);
					if (num5 != num4)
					{
						if (num4 <= num5)
						{
							return -1;
						}
						return 1;
					}
					else
					{
						i += 2;
					}
				}
			}
			else
			{
				while (i < num)
				{
					int num5 = (int)bytes[i + 1] << (int)(8 + bytes[i]);
					if (num5 != num4)
					{
						if (num5 <= num4)
						{
							return -1;
						}
						return 1;
					}
					else
					{
						i += 2;
					}
				}
			}
			return 0;
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x002D30D8 File Offset: 0x002D24D8
		private static int CompareBinary2(SqlString x, SqlString y)
		{
			char[] array = x.m_value.ToCharArray();
			char[] array2 = y.m_value.ToCharArray();
			int num = array.Length;
			int num2 = array2.Length;
			int num3 = (num < num2) ? num : num2;
			for (int i = 0; i < num3; i++)
			{
				if (array[i] < array2[i])
				{
					return -1;
				}
				if (array[i] > array2[i])
				{
					return 1;
				}
			}
			char c = ' ';
			if (num < num2)
			{
				int i = num3;
				while (i < num2)
				{
					if (array2[i] != c)
					{
						if (c <= array2[i])
						{
							return -1;
						}
						return 1;
					}
					else
					{
						i++;
					}
				}
			}
			else
			{
				int i = num3;
				while (i < num)
				{
					if (array[i] != c)
					{
						if (array[i] <= c)
						{
							return -1;
						}
						return 1;
					}
					else
					{
						i++;
					}
				}
			}
			return 0;
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x002D3188 File Offset: 0x002D2588
		public int CompareTo(object value)
		{
			if (value is SqlString)
			{
				SqlString value2 = (SqlString)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlString));
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x002D31C8 File Offset: 0x002D25C8
		public int CompareTo(SqlString value)
		{
			if (this.IsNull)
			{
				if (!value.IsNull)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (value.IsNull)
				{
					return 1;
				}
				if (this < value)
				{
					return -1;
				}
				if (this > value)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x002D3228 File Offset: 0x002D2628
		public override bool Equals(object value)
		{
			if (!(value is SqlString))
			{
				return false;
			}
			SqlString y = (SqlString)value;
			if (y.IsNull || this.IsNull)
			{
				return y.IsNull && this.IsNull;
			}
			return (this == y).Value;
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x002D3288 File Offset: 0x002D2688
		public override int GetHashCode()
		{
			if (this.IsNull)
			{
				return 0;
			}
			byte[] array;
			if (this.FBinarySort())
			{
				array = SqlString.x_UnicodeEncoding.GetBytes(this.m_value.TrimEnd(new char[0]));
			}
			else
			{
				this.SetCompareInfo();
				CompareOptions options = SqlString.CompareOptionsFromSqlCompareOptions(this.m_flag);
				array = this.m_cmpInfo.GetSortKey(this.m_value.TrimEnd(new char[0]), options).KeyData;
			}
			return SqlBinary.HashByteArray(array, array.Length);
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x002D3308 File Offset: 0x002D2708
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x002D3318 File Offset: 0x002D2718
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				this.m_fNotNull = false;
				return;
			}
			this.m_value = reader.ReadElementString();
			this.m_fNotNull = true;
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x002D3368 File Offset: 0x002D2768
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(this.m_value);
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x002D33A8 File Offset: 0x002D27A8
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04001D43 RID: 7491
		private string m_value;

		// Token: 0x04001D44 RID: 7492
		private CompareInfo m_cmpInfo;

		// Token: 0x04001D45 RID: 7493
		private int m_lcid;

		// Token: 0x04001D46 RID: 7494
		private SqlCompareOptions m_flag;

		// Token: 0x04001D47 RID: 7495
		private bool m_fNotNull;

		// Token: 0x04001D48 RID: 7496
		public static readonly SqlString Null = new SqlString(true);

		// Token: 0x04001D49 RID: 7497
		internal static readonly UnicodeEncoding x_UnicodeEncoding = new UnicodeEncoding();

		// Token: 0x04001D4A RID: 7498
		public static readonly int IgnoreCase = 1;

		// Token: 0x04001D4B RID: 7499
		public static readonly int IgnoreWidth = 16;

		// Token: 0x04001D4C RID: 7500
		public static readonly int IgnoreNonSpace = 2;

		// Token: 0x04001D4D RID: 7501
		public static readonly int IgnoreKanaType = 8;

		// Token: 0x04001D4E RID: 7502
		public static readonly int BinarySort = 32768;

		// Token: 0x04001D4F RID: 7503
		public static readonly int BinarySort2 = 16384;

		// Token: 0x04001D50 RID: 7504
		private static readonly SqlCompareOptions x_iDefaultFlag = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x04001D51 RID: 7505
		private static readonly CompareOptions x_iValidCompareOptionMask = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x04001D52 RID: 7506
		internal static readonly SqlCompareOptions x_iValidSqlCompareOptionMask = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth | SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2;

		// Token: 0x04001D53 RID: 7507
		internal static readonly int x_lcidUSEnglish = 1033;

		// Token: 0x04001D54 RID: 7508
		private static readonly int x_lcidBinary = 33280;
	}
}
