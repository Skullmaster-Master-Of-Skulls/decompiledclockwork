using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200031A RID: 794
	internal sealed class SqlDecimalStorage : DataStorage
	{
		// Token: 0x06003202 RID: 12802 RVA: 0x00136AD4 File Offset: 0x00135ED4
		public SqlDecimalStorage(DataColumn column) : base(column, typeof(SqlDecimal), SqlDecimal.Null, SqlDecimal.Null, StorageType.SqlDecimal)
		{
		}

		// Token: 0x06003203 RID: 12803 RVA: 0x00136B08 File Offset: 0x00135F08
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
				throw ExprException.Overflow(typeof(SqlDecimal));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x00136F2C File Offset: 0x0013632C
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x00136F58 File Offset: 0x00136358
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlDecimal)value);
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x00136F7C File Offset: 0x0013637C
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDecimal(value);
			}
			return this.NullValue;
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x00136FA0 File Offset: 0x001363A0
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x00136FC8 File Offset: 0x001363C8
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x00136FE8 File Offset: 0x001363E8
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x00137008 File Offset: 0x00136408
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlDecimal(value);
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x00137028 File Offset: 0x00136428
		public override void SetCapacity(int capacity)
		{
			SqlDecimal[] destinationArray = new SqlDecimal[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x0600320C RID: 12812 RVA: 0x00137068 File Offset: 0x00136468
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

		// Token: 0x0600320D RID: 12813 RVA: 0x001370E0 File Offset: 0x001364E0
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600320E RID: 12814 RVA: 0x0013713C File Offset: 0x0013653C
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDecimal[recordCount];
		}

		// Token: 0x0600320F RID: 12815 RVA: 0x00137150 File Offset: 0x00136550
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlDecimal[] array = (SqlDecimal[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06003210 RID: 12816 RVA: 0x00137188 File Offset: 0x00136588
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlDecimal[])store;
		}

		// Token: 0x04001DB6 RID: 7606
		private SqlDecimal[] values;
	}
}
