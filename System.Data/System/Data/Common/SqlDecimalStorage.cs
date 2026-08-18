using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000191 RID: 401
	internal sealed class SqlDecimalStorage : DataStorage
	{
		// Token: 0x06001787 RID: 6023 RVA: 0x0024C268 File Offset: 0x0024B668
		public SqlDecimalStorage(DataColumn column) : base(column, typeof(SqlDecimal), SqlDecimal.Null, SqlDecimal.Null)
		{
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x0024C2A8 File Offset: 0x0024B6A8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlDecimal sqlDecimal = 0L;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlDecimal += this.values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDecimal;
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
							x += this.values[num3];
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						SqlDecimal sqlDecimal2 = 0L;
						sqlDecimal2 = x / (long)num2;
						return sqlDecimal2;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					SqlDecimal sqlDecimal3 = SqlDecimal.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlDecimal.LessThan(this.values[num4], sqlDecimal3).IsTrue)
							{
								sqlDecimal3 = this.values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDecimal3;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					SqlDecimal sqlDecimal4 = SqlDecimal.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlDecimal.GreaterThan(this.values[num5], sqlDecimal4).IsTrue)
							{
								sqlDecimal4 = this.values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDecimal4;
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
				throw ExprException.Overflow(typeof(SqlDecimal));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x0024C6F8 File Offset: 0x0024BAF8
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x0024C728 File Offset: 0x0024BB28
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlDecimal)value);
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x0024C758 File Offset: 0x0024BB58
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDecimal(value);
			}
			return this.NullValue;
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x0024C788 File Offset: 0x0024BB88
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0024C7B8 File Offset: 0x0024BBB8
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x0024C7E8 File Offset: 0x0024BBE8
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x0024C808 File Offset: 0x0024BC08
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlDecimal(value);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0024C838 File Offset: 0x0024BC38
		public override void SetCapacity(int capacity)
		{
			SqlDecimal[] destinationArray = new SqlDecimal[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x0024C878 File Offset: 0x0024BC78
		public override object ConvertXmlToObject(string s)
		{
			SqlDecimal sqlDecimal = default(SqlDecimal);
			string s2 = "<col>" + s + "</col>";
			StringReader input = new StringReader(s2);
			IXmlSerializable xmlSerializable = sqlDecimal;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlDecimal)xmlSerializable;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0024C8F8 File Offset: 0x0024BCF8
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x0024C958 File Offset: 0x0024BD58
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDecimal[recordCount];
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x0024C978 File Offset: 0x0024BD78
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlDecimal[] array = (SqlDecimal[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06001795 RID: 6037 RVA: 0x0024C9C8 File Offset: 0x0024BDC8
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlDecimal[])store;
		}

		// Token: 0x04000D10 RID: 3344
		private SqlDecimal[] values;
	}
}
