using System;
using System.Data.Common;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	// Token: 0x02000168 RID: 360
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlString : INullable, IComparable, IXmlSerializable
	{
		// Token: 0x06001714 RID: 5908 RVA: 0x000A71C0 File Offset: 0x000A65C0
		private SqlString(bool fNull)
		{
			this.m_value = null;
			this.m_cmpInfo = null;
			this.m_lcid = 0;
			this.m_flag = SqlCompareOptions.None;
			this.m_fNotNull = false;
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x000A71F0 File Offset: 0x000A65F0
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

		// Token: 0x06001716 RID: 5910 RVA: 0x000A7280 File Offset: 0x000A6680
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data, bool fUnicode)
		{
			this = new SqlString(lcid, compareOptions, data, 0, data.Length, fUnicode);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x000A729C File Offset: 0x000A669C
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data, int index, int count)
		{
			this = new SqlString(lcid, compareOptions, data, index, count, true);
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x000A72B8 File Offset: 0x000A66B8
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data)
		{
			this = new SqlString(lcid, compareOptions, data, 0, data.Length, true);
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x000A72D4 File Offset: 0x000A66D4
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

		// Token: 0x0600171A RID: 5914 RVA: 0x000A731C File Offset: 0x000A671C
		public SqlString(string data, int lcid)
		{
			this = new SqlString(data, lcid, SqlString.x_iDefaultFlag);
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x000A7338 File Offset: 0x000A6738
		public SqlString(string data)
		{
			this = new SqlString(data, CultureInfo.CurrentCulture.LCID, SqlString.x_iDefaultFlag);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x000A735C File Offset: 0x000A675C
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

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x000A73AC File Offset: 0x000A67AC
		public bool IsNull
		{
			get
			{
				return !this.m_fNotNull;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x000A73C4 File Offset: 0x000A67C4
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

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x0600171F RID: 5919 RVA: 0x000A73E8 File Offset: 0x000A67E8
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

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001720 RID: 5920 RVA: 0x000A740C File Offset: 0x000A680C
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

		// Token: 0x06001721 RID: 5921 RVA: 0x000A7434 File Offset: 0x000A6834
		private void SetCompareInfo()
		{
			if (this.m_cmpInfo == null)
			{
				this.m_cmpInfo = CultureInfo.GetCultureInfo(this.m_lcid).CompareInfo;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x000A7460 File Offset: 0x000A6860
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

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x000A7488 File Offset: 0x000A6888
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

		// Token: 0x06001724 RID: 5924 RVA: 0x000A74AC File Offset: 0x000A68AC
		public static implicit operator SqlString(string x)
		{
			return new SqlString(x);
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x000A74C0 File Offset: 0x000A68C0
		public static explicit operator string(SqlString x)
		{
			return x.Value;
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x000A74D4 File Offset: 0x000A68D4
		public override string ToString()
		{
			if (!this.IsNull)
			{
				return this.m_value;
			}
			return SQLResource.NullString;
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x000A74F8 File Offset: 0x000A68F8
		public byte[] GetUnicodeBytes()
		{
			if (this.IsNull)
			{
				return null;
			}
			return SqlString.x_UnicodeEncoding.GetBytes(this.m_value);
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x000A7520 File Offset: 0x000A6920
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

		// Token: 0x06001729 RID: 5929 RVA: 0x000A7560 File Offset: 0x000A6960
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

		// Token: 0x0600172A RID: 5930 RVA: 0x000A75E4 File Offset: 0x000A69E4
		private static int StringCompare(SqlString x, SqlString y)
		{
			if (x.m_lcid != y.m_lcid || x.m_flag != y.m_flag)
			{
				throw new SqlTypeException(SQLResource.CompareDiffCollationMessage);
			}
			x.SetCompareInfo();
			y.SetCompareInfo();
			int result;
			if ((x.m_flag & SqlCompareOptions.BinarySort) != SqlCompareOptions.None)
			{
				result = SqlString.CompareBinary(x, y);
			}
			else if ((x.m_flag & SqlCompareOptions.BinarySort2) != SqlCompareOptions.None)
			{
				result = SqlString.CompareBinary2(x, y);
			}
			else
			{
				string value = x.m_value;
				string value2 = y.m_value;
				int i = value.Length;
				int num = value2.Length;
				while (i > 0)
				{
					if (value[i - 1] != ' ')
					{
						break;
					}
					i--;
				}
				while (num > 0 && value2[num - 1] == ' ')
				{
					num--;
				}
				CompareOptions options = SqlString.CompareOptionsFromSqlCompareOptions(x.m_flag);
				result = x.m_cmpInfo.Compare(x.m_value, 0, i, y.m_value, 0, num, options);
			}
			return result;
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x000A76D8 File Offset: 0x000A6AD8
		private static SqlBoolean Compare(SqlString x, SqlString y, EComparison ecExpectedResult)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			int num = SqlString.StringCompare(x, y);
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

		// Token: 0x0600172C RID: 5932 RVA: 0x000A7758 File Offset: 0x000A6B58
		public static explicit operator SqlString(SqlBoolean x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString());
			}
			return SqlString.Null;
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x000A7788 File Offset: 0x000A6B88
		public static explicit operator SqlString(SqlByte x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x000A77BC File Offset: 0x000A6BBC
		public static explicit operator SqlString(SqlInt16 x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x000A77F0 File Offset: 0x000A6BF0
		public static explicit operator SqlString(SqlInt32 x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x000A7824 File Offset: 0x000A6C24
		public static explicit operator SqlString(SqlInt64 x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x000A7858 File Offset: 0x000A6C58
		public static explicit operator SqlString(SqlSingle x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x000A788C File Offset: 0x000A6C8C
		public static explicit operator SqlString(SqlDouble x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.Value.ToString(null));
			}
			return SqlString.Null;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x000A78C0 File Offset: 0x000A6CC0
		public static explicit operator SqlString(SqlDecimal x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.ToString());
			}
			return SqlString.Null;
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x000A78F0 File Offset: 0x000A6CF0
		public static explicit operator SqlString(SqlMoney x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.ToString());
			}
			return SqlString.Null;
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x000A7920 File Offset: 0x000A6D20
		public static explicit operator SqlString(SqlDateTime x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.ToString());
			}
			return SqlString.Null;
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x000A7950 File Offset: 0x000A6D50
		public static explicit operator SqlString(SqlGuid x)
		{
			if (!x.IsNull)
			{
				return new SqlString(x.ToString());
			}
			return SqlString.Null;
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x000A7980 File Offset: 0x000A6D80
		public SqlString Clone()
		{
			if (this.IsNull)
			{
				return new SqlString(true);
			}
			SqlString result = new SqlString(this.m_value, this.m_lcid, this.m_flag);
			return result;
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x000A79B8 File Offset: 0x000A6DB8
		public static SqlBoolean operator ==(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.EQ);
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x000A79D0 File Offset: 0x000A6DD0
		public static SqlBoolean operator !=(SqlString x, SqlString y)
		{
			return !(x == y);
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x000A79EC File Offset: 0x000A6DEC
		public static SqlBoolean operator <(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.LT);
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x000A7A04 File Offset: 0x000A6E04
		public static SqlBoolean operator >(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.GT);
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x000A7A1C File Offset: 0x000A6E1C
		public static SqlBoolean operator <=(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.LE);
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x000A7A34 File Offset: 0x000A6E34
		public static SqlBoolean operator >=(SqlString x, SqlString y)
		{
			return SqlString.Compare(x, y, EComparison.GE);
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x000A7A4C File Offset: 0x000A6E4C
		public static SqlString Concat(SqlString x, SqlString y)
		{
			return x + y;
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x000A7A60 File Offset: 0x000A6E60
		public static SqlString Add(SqlString x, SqlString y)
		{
			return x + y;
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x000A7A74 File Offset: 0x000A6E74
		public static SqlBoolean Equals(SqlString x, SqlString y)
		{
			return x == y;
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x000A7A88 File Offset: 0x000A6E88
		public static SqlBoolean NotEquals(SqlString x, SqlString y)
		{
			return x != y;
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x000A7A9C File Offset: 0x000A6E9C
		public static SqlBoolean LessThan(SqlString x, SqlString y)
		{
			return x < y;
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x000A7AB0 File Offset: 0x000A6EB0
		public static SqlBoolean GreaterThan(SqlString x, SqlString y)
		{
			return x > y;
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x000A7AC4 File Offset: 0x000A6EC4
		public static SqlBoolean LessThanOrEqual(SqlString x, SqlString y)
		{
			return x <= y;
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x000A7AD8 File Offset: 0x000A6ED8
		public static SqlBoolean GreaterThanOrEqual(SqlString x, SqlString y)
		{
			return x >= y;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x000A7AEC File Offset: 0x000A6EEC
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x000A7B04 File Offset: 0x000A6F04
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x000A7B1C File Offset: 0x000A6F1C
		public SqlDateTime ToSqlDateTime()
		{
			return (SqlDateTime)this;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x000A7B34 File Offset: 0x000A6F34
		public SqlDouble ToSqlDouble()
		{
			return (SqlDouble)this;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x000A7B4C File Offset: 0x000A6F4C
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x000A7B64 File Offset: 0x000A6F64
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x000A7B7C File Offset: 0x000A6F7C
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x000A7B94 File Offset: 0x000A6F94
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x000A7BAC File Offset: 0x000A6FAC
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x000A7BC4 File Offset: 0x000A6FC4
		public SqlSingle ToSqlSingle()
		{
			return (SqlSingle)this;
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x000A7BDC File Offset: 0x000A6FDC
		public SqlGuid ToSqlGuid()
		{
			return (SqlGuid)this;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x000A7BF4 File Offset: 0x000A6FF4
		private static void ValidateSqlCompareOptions(SqlCompareOptions compareOptions)
		{
			if ((compareOptions & SqlString.x_iValidSqlCompareOptionMask) != compareOptions)
			{
				throw new ArgumentOutOfRangeException("compareOptions");
			}
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x000A7C18 File Offset: 0x000A7018
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

		// Token: 0x06001753 RID: 5971 RVA: 0x000A7C68 File Offset: 0x000A7068
		private bool FBinarySort()
		{
			return !this.IsNull && (this.m_flag & (SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2)) > SqlCompareOptions.None;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x000A7C90 File Offset: 0x000A7090
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

		// Token: 0x06001755 RID: 5973 RVA: 0x000A7D58 File Offset: 0x000A7158
		private static int CompareBinary2(SqlString x, SqlString y)
		{
			string value = x.m_value;
			string value2 = y.m_value;
			int length = value.Length;
			int length2 = value2.Length;
			int num = (length < length2) ? length : length2;
			for (int i = 0; i < num; i++)
			{
				if (value[i] < value2[i])
				{
					return -1;
				}
				if (value[i] > value2[i])
				{
					return 1;
				}
			}
			char c = ' ';
			if (length < length2)
			{
				int i = num;
				while (i < length2)
				{
					if (value2[i] != c)
					{
						if (c <= value2[i])
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
				int i = num;
				while (i < length)
				{
					if (value[i] != c)
					{
						if (value[i] <= c)
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

		// Token: 0x06001756 RID: 5974 RVA: 0x000A7E1C File Offset: 0x000A721C
		public int CompareTo(object value)
		{
			if (value is SqlString)
			{
				SqlString value2 = (SqlString)value;
				return this.CompareTo(value2);
			}
			throw ADP.WrongType(value.GetType(), typeof(SqlString));
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x000A7E58 File Offset: 0x000A7258
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
				int num = SqlString.StringCompare(this, value);
				if (num < 0)
				{
					return -1;
				}
				if (num > 0)
				{
					return 1;
				}
				return 0;
			}
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x000A7EA0 File Offset: 0x000A72A0
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

		// Token: 0x06001759 RID: 5977 RVA: 0x000A7EF8 File Offset: 0x000A72F8
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
				CompareInfo compareInfo;
				CompareOptions options;
				try
				{
					this.SetCompareInfo();
					compareInfo = this.m_cmpInfo;
					options = SqlString.CompareOptionsFromSqlCompareOptions(this.m_flag);
				}
				catch (ArgumentException)
				{
					compareInfo = CultureInfo.InvariantCulture.CompareInfo;
					options = CompareOptions.None;
				}
				array = compareInfo.GetSortKey(this.m_value.TrimEnd(new char[0]), options).KeyData;
			}
			return SqlBinary.HashByteArray(array, array.Length);
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x000A7FA4 File Offset: 0x000A73A4
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x000A7FB4 File Offset: 0x000A73B4
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			string attribute = reader.GetAttribute("nil", "http://www.w3.org/2001/XMLSchema-instance");
			if (attribute != null && XmlConvert.ToBoolean(attribute))
			{
				reader.ReadElementString();
				this.m_fNotNull = false;
				return;
			}
			this.m_value = reader.ReadElementString();
			this.m_fNotNull = true;
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x000A8000 File Offset: 0x000A7400
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			if (this.IsNull)
			{
				writer.WriteAttributeString("xsi", "nil", "http://www.w3.org/2001/XMLSchema-instance", "true");
				return;
			}
			writer.WriteString(this.m_value);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x000A803C File Offset: 0x000A743C
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		// Token: 0x04000E2C RID: 3628
		private string m_value;

		// Token: 0x04000E2D RID: 3629
		private CompareInfo m_cmpInfo;

		// Token: 0x04000E2E RID: 3630
		private int m_lcid;

		// Token: 0x04000E2F RID: 3631
		private SqlCompareOptions m_flag;

		// Token: 0x04000E30 RID: 3632
		private bool m_fNotNull;

		// Token: 0x04000E31 RID: 3633
		public static readonly SqlString Null = new SqlString(true);

		// Token: 0x04000E32 RID: 3634
		internal static readonly UnicodeEncoding x_UnicodeEncoding = new UnicodeEncoding();

		// Token: 0x04000E33 RID: 3635
		public static readonly int IgnoreCase = 1;

		// Token: 0x04000E34 RID: 3636
		public static readonly int IgnoreWidth = 16;

		// Token: 0x04000E35 RID: 3637
		public static readonly int IgnoreNonSpace = 2;

		// Token: 0x04000E36 RID: 3638
		public static readonly int IgnoreKanaType = 8;

		// Token: 0x04000E37 RID: 3639
		public static readonly int BinarySort = 32768;

		// Token: 0x04000E38 RID: 3640
		public static readonly int BinarySort2 = 16384;

		// Token: 0x04000E39 RID: 3641
		private static readonly SqlCompareOptions x_iDefaultFlag = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;

		// Token: 0x04000E3A RID: 3642
		private static readonly CompareOptions x_iValidCompareOptionMask = CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth;

		// Token: 0x04000E3B RID: 3643
		internal static readonly SqlCompareOptions x_iValidSqlCompareOptionMask = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth | SqlCompareOptions.BinarySort | SqlCompareOptions.BinarySort2;

		// Token: 0x04000E3C RID: 3644
		internal const int x_lcidUSEnglish = 1033;

		// Token: 0x04000E3D RID: 3645
		private const int x_lcidBinary = 33280;
	}
}
