using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000197 RID: 407
	internal sealed class SqlMoneyStorage : DataStorage
	{
		// Token: 0x060017E1 RID: 6113 RVA: 0x0024EBF8 File Offset: 0x0024DFF8
		public SqlMoneyStorage(DataColumn column) : base(column, typeof(SqlMoney), SqlMoney.Null, SqlMoney.Null)
		{
		}

		// Token: 0x060017E2 RID: 6114 RVA: 0x0024EC38 File Offset: 0x0024E038
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
							x += this.values[num3].ToSqlDecimal();
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						SqlMoney sqlMoney = 0L;
						sqlMoney = (x / (long)num2).ToSqlMoney();
						return sqlMoney;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					SqlMoney sqlMoney2 = SqlMoney.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlMoney.LessThan(this.values[num4], sqlMoney2).IsTrue)
							{
								sqlMoney2 = this.values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlMoney2;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					SqlMoney sqlMoney3 = SqlMoney.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlMoney.GreaterThan(this.values[num5], sqlMoney3).IsTrue)
							{
								sqlMoney3 = this.values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlMoney3;
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
				throw ExprException.Overflow(typeof(SqlMoney));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0024F098 File Offset: 0x0024E498
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0024F0C8 File Offset: 0x0024E4C8
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlMoney)value);
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0024F0F8 File Offset: 0x0024E4F8
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlMoney(value);
			}
			return this.NullValue;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x0024F128 File Offset: 0x0024E528
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0024F158 File Offset: 0x0024E558
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x0024F188 File Offset: 0x0024E588
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x0024F1A8 File Offset: 0x0024E5A8
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlMoney(value);
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x0024F1D8 File Offset: 0x0024E5D8
		public override void SetCapacity(int capacity)
		{
			SqlMoney[] destinationArray = new SqlMoney[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x0024F218 File Offset: 0x0024E618
		public override object ConvertXmlToObject(string s)
		{
			SqlMoney sqlMoney = default(SqlMoney);
			string s2 = "<col>" + s + "</col>";
			StringReader input = new StringReader(s2);
			IXmlSerializable xmlSerializable = sqlMoney;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlMoney)xmlSerializable;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x0024F298 File Offset: 0x0024E698
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0024F2F8 File Offset: 0x0024E6F8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlMoney[recordCount];
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x0024F318 File Offset: 0x0024E718
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlMoney[] array = (SqlMoney[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060017EF RID: 6127 RVA: 0x0024F368 File Offset: 0x0024E768
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlMoney[])store;
		}

		// Token: 0x04000D16 RID: 3350
		private SqlMoney[] values;
	}
}
