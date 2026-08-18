using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000192 RID: 402
	internal sealed class SqlDoubleStorage : DataStorage
	{
		// Token: 0x06001796 RID: 6038 RVA: 0x0024C9E8 File Offset: 0x0024BDE8
		public SqlDoubleStorage(DataColumn column) : base(column, typeof(SqlDouble), SqlDouble.Null, SqlDouble.Null)
		{
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x0024CA28 File Offset: 0x0024BE28
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlDouble sqlDouble = 0.0;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlDouble += this.values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDouble;
					}
					return this.NullValue;
				}
				case AggregateType.Mean:
				{
					SqlDouble x = 0.0;
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
						SqlDouble sqlDouble2 = 0.0;
						sqlDouble2 = x / (double)num2;
						return sqlDouble2;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					SqlDouble sqlDouble3 = SqlDouble.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlDouble.LessThan(this.values[num4], sqlDouble3).IsTrue)
							{
								sqlDouble3 = this.values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDouble3;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					SqlDouble sqlDouble4 = SqlDouble.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlDouble.GreaterThan(this.values[num5], sqlDouble4).IsTrue)
							{
								sqlDouble4 = this.values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDouble4;
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
					SqlDouble sqlDouble5 = 0.0;
					SqlDouble x2 = 0.0;
					SqlDouble sqlDouble6 = 0.0;
					SqlDouble sqlDouble7 = 0.0;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
						{
							sqlDouble6 += this.values[num7];
							sqlDouble7 += this.values[num7] * this.values[num7];
							num6++;
						}
					}
					if (num6 <= 1)
					{
						return this.NullValue;
					}
					sqlDouble5 = (double)num6 * sqlDouble7 - sqlDouble6 * sqlDouble6;
					x2 = sqlDouble5 / (sqlDouble6 * sqlDouble6);
					if (x2 < 1E-15 || sqlDouble5 < 0.0)
					{
						sqlDouble5 = 0.0;
					}
					else
					{
						sqlDouble5 /= (double)(num6 * (num6 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(sqlDouble5.Value);
					}
					return sqlDouble5;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlDouble));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x0024CE88 File Offset: 0x0024C288
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x0024CEB8 File Offset: 0x0024C2B8
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlDouble)value);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x0024CEE8 File Offset: 0x0024C2E8
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDouble(value);
			}
			return this.NullValue;
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x0024CF18 File Offset: 0x0024C318
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x0024CF48 File Offset: 0x0024C348
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x0024CF78 File Offset: 0x0024C378
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x0024CF98 File Offset: 0x0024C398
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlDouble(value);
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x0024CFC8 File Offset: 0x0024C3C8
		public override void SetCapacity(int capacity)
		{
			SqlDouble[] destinationArray = new SqlDouble[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x0024D008 File Offset: 0x0024C408
		public override object ConvertXmlToObject(string s)
		{
			SqlDouble sqlDouble = default(SqlDouble);
			string s2 = "<col>" + s + "</col>";
			StringReader input = new StringReader(s2);
			IXmlSerializable xmlSerializable = sqlDouble;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlDouble)xmlSerializable;
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x0024D088 File Offset: 0x0024C488
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0024D0E8 File Offset: 0x0024C4E8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDouble[recordCount];
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0024D108 File Offset: 0x0024C508
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlDouble[] array = (SqlDouble[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0024D158 File Offset: 0x0024C558
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlDouble[])store;
		}

		// Token: 0x04000D11 RID: 3345
		private SqlDouble[] values;
	}
}
