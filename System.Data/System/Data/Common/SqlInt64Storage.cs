using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000196 RID: 406
	internal sealed class SqlInt64Storage : DataStorage
	{
		// Token: 0x060017D2 RID: 6098 RVA: 0x0024E478 File Offset: 0x0024D878
		public SqlInt64Storage(DataColumn column) : base(column, typeof(SqlInt64), SqlInt64.Null, SqlInt64.Null)
		{
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0024E4B8 File Offset: 0x0024D8B8
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
					SqlDecimal x = 0L;
					int num2 = 0;
					foreach (int num3 in records)
					{
						if (!this.IsNull(num3))
						{
							x += this.values[num3].ToSqlDecimal();
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						SqlInt64 sqlInt2 = 0L;
						sqlInt2 = (x / (long)num2).ToSqlInt64();
						return sqlInt2;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					SqlInt64 sqlInt3 = SqlInt64.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlInt64.LessThan(this.values[num4], sqlInt3).IsTrue)
							{
								sqlInt3 = this.values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlInt3;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					SqlInt64 sqlInt4 = SqlInt64.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlInt64.GreaterThan(this.values[num5], sqlInt4).IsTrue)
							{
								sqlInt4 = this.values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlInt4;
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
				throw ExprException.Overflow(typeof(SqlInt64));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0024E908 File Offset: 0x0024DD08
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0024E938 File Offset: 0x0024DD38
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlInt64)value);
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0024E968 File Offset: 0x0024DD68
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlInt64(value);
			}
			return this.NullValue;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x0024E998 File Offset: 0x0024DD98
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x0024E9C8 File Offset: 0x0024DDC8
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0024E9F8 File Offset: 0x0024DDF8
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0024EA18 File Offset: 0x0024DE18
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlInt64(value);
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0024EA48 File Offset: 0x0024DE48
		public override void SetCapacity(int capacity)
		{
			SqlInt64[] destinationArray = new SqlInt64[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060017DC RID: 6108 RVA: 0x0024EA88 File Offset: 0x0024DE88
		public override object ConvertXmlToObject(string s)
		{
			SqlInt64 sqlInt = default(SqlInt64);
			string s2 = "<col>" + s + "</col>";
			StringReader input = new StringReader(s2);
			IXmlSerializable xmlSerializable = sqlInt;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlInt64)xmlSerializable;
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0024EB08 File Offset: 0x0024DF08
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060017DE RID: 6110 RVA: 0x0024EB68 File Offset: 0x0024DF68
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlInt64[recordCount];
		}

		// Token: 0x060017DF RID: 6111 RVA: 0x0024EB88 File Offset: 0x0024DF88
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlInt64[] array = (SqlInt64[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060017E0 RID: 6112 RVA: 0x0024EBD8 File Offset: 0x0024DFD8
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlInt64[])store;
		}

		// Token: 0x04000D15 RID: 3349
		private SqlInt64[] values;
	}
}
