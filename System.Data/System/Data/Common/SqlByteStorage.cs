using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200018E RID: 398
	internal sealed class SqlByteStorage : DataStorage
	{
		// Token: 0x0600175B RID: 5979 RVA: 0x0024B2E8 File Offset: 0x0024A6E8
		public SqlByteStorage(DataColumn column) : base(column, typeof(SqlByte), SqlByte.Null, SqlByte.Null)
		{
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x0024B328 File Offset: 0x0024A728
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlInt64 sqlInt = 0L;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlInt += this.values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlInt;
					}
					return this.NullValue;
				}
				case AggregateType.Mean:
				{
					SqlInt64 x = 0L;
					int num2 = 0;
					foreach (int num3 in records)
					{
						if (!this.IsNull(num3))
						{
							x += this.values[num3].ToSqlInt64();
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						SqlByte sqlByte = 0;
						sqlByte = (x / (long)num2).ToSqlByte();
						return sqlByte;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					SqlByte sqlByte2 = SqlByte.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlByte.LessThan(this.values[num4], sqlByte2).IsTrue)
							{
								sqlByte2 = this.values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlByte2;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					SqlByte sqlByte3 = SqlByte.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlByte.GreaterThan(this.values[num5], sqlByte3).IsTrue)
							{
								sqlByte3 = this.values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlByte3;
					}
					return this.NullValue;
				}
				case AggregateType.First:
					if (records.Length > 0)
					{
						return this.values[records[0]];
					}
					return null;
				case AggregateType.Count:
				{
					int num6 = 0;
					for (int m = 0; m < records.Length; m++)
					{
						if (!this.IsNull(records[m]))
						{
							num6++;
						}
					}
					return num6;
				}
				case AggregateType.Var:
				case AggregateType.StDev:
				{
					int num6 = 0;
					SqlDouble sqlDouble = 0.0;
					SqlDouble x2 = 0.0;
					SqlDouble sqlDouble2 = 0.0;
					SqlDouble sqlDouble3 = 0.0;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
						{
							sqlDouble2 += this.values[num7].ToSqlDouble();
							sqlDouble3 += this.values[num7].ToSqlDouble() * this.values[num7].ToSqlDouble();
							num6++;
						}
					}
					if (num6 <= 1)
					{
						return this.NullValue;
					}
					sqlDouble = (double)num6 * sqlDouble3 - sqlDouble2 * sqlDouble2;
					x2 = sqlDouble / (sqlDouble2 * sqlDouble2);
					if (x2 < 1E-15 || sqlDouble < 0.0)
					{
						sqlDouble = 0.0;
					}
					else
					{
						sqlDouble /= (double)(num6 * (num6 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(sqlDouble.Value);
					}
					return sqlDouble;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlByte));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x0024B788 File Offset: 0x0024AB88
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0024B7B8 File Offset: 0x0024ABB8
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlByte)value);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x0024B7E8 File Offset: 0x0024ABE8
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlByte(value);
			}
			return this.NullValue;
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0024B818 File Offset: 0x0024AC18
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x0024B848 File Offset: 0x0024AC48
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x0024B878 File Offset: 0x0024AC78
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0024B898 File Offset: 0x0024AC98
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlByte(value);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0024B8C8 File Offset: 0x0024ACC8
		public override void SetCapacity(int capacity)
		{
			SqlByte[] destinationArray = new SqlByte[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0024B908 File Offset: 0x0024AD08
		public override object ConvertXmlToObject(string s)
		{
			SqlByte sqlByte = default(SqlByte);
			string s2 = "<col>" + s + "</col>";
			StringReader input = new StringReader(s2);
			IXmlSerializable xmlSerializable = sqlByte;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlByte)xmlSerializable;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0024B988 File Offset: 0x0024AD88
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x0024B9E8 File Offset: 0x0024ADE8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlByte[recordCount];
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x0024BA08 File Offset: 0x0024AE08
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlByte[] array = (SqlByte[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(record, this.IsNull(record));
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0024BA48 File Offset: 0x0024AE48
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlByte[])store;
		}

		// Token: 0x04000D0D RID: 3341
		private SqlByte[] values;
	}
}
