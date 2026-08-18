using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000320 RID: 800
	internal sealed class SqlMoneyStorage : DataStorage
	{
		// Token: 0x0600325C RID: 12892 RVA: 0x00139070 File Offset: 0x00138470
		public SqlMoneyStorage(DataColumn column) : base(column, typeof(SqlMoney), SqlMoney.Null, SqlMoney.Null, StorageType.SqlMoney)
		{
		}

		// Token: 0x0600325D RID: 12893 RVA: 0x001390A4 File Offset: 0x001384A4
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
					if (records.Length != 0)
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
					SqlBoolean sqlBoolean = x2 < 1E-15;
					if (sqlBoolean ? sqlBoolean : (sqlBoolean | sqlDouble < 0.0))
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

		// Token: 0x0600325E RID: 12894 RVA: 0x001394DC File Offset: 0x001388DC
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x0600325F RID: 12895 RVA: 0x00139508 File Offset: 0x00138908
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlMoney)value);
		}

		// Token: 0x06003260 RID: 12896 RVA: 0x0013952C File Offset: 0x0013892C
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlMoney(value);
			}
			return this.NullValue;
		}

		// Token: 0x06003261 RID: 12897 RVA: 0x00139550 File Offset: 0x00138950
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06003262 RID: 12898 RVA: 0x00139578 File Offset: 0x00138978
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x00139598 File Offset: 0x00138998
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x001395B8 File Offset: 0x001389B8
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlMoney(value);
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x001395D8 File Offset: 0x001389D8
		public override void SetCapacity(int capacity)
		{
			SqlMoney[] destinationArray = new SqlMoney[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x00139618 File Offset: 0x00138A18
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

		// Token: 0x06003267 RID: 12903 RVA: 0x00139690 File Offset: 0x00138A90
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x001396EC File Offset: 0x00138AEC
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlMoney[recordCount];
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x00139700 File Offset: 0x00138B00
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlMoney[] array = (SqlMoney[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x00139738 File Offset: 0x00138B38
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlMoney[])store;
		}

		// Token: 0x04001DBC RID: 7612
		private SqlMoney[] values;
	}
}
