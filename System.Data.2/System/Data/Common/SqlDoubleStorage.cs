using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200031B RID: 795
	internal sealed class SqlDoubleStorage : DataStorage
	{
		// Token: 0x06003211 RID: 12817 RVA: 0x001371A4 File Offset: 0x001365A4
		public SqlDoubleStorage(DataColumn column) : base(column, typeof(SqlDouble), SqlDouble.Null, SqlDouble.Null, StorageType.SqlDouble)
		{
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x001371D8 File Offset: 0x001365D8
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
					SqlBoolean sqlBoolean = x2 < 1E-15;
					if (sqlBoolean ? sqlBoolean : (sqlBoolean | sqlDouble5 < 0.0))
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

		// Token: 0x06003213 RID: 12819 RVA: 0x00137604 File Offset: 0x00136A04
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x00137630 File Offset: 0x00136A30
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlDouble)value);
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x00137654 File Offset: 0x00136A54
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDouble(value);
			}
			return this.NullValue;
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x00137678 File Offset: 0x00136A78
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x001376A0 File Offset: 0x00136AA0
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x001376C0 File Offset: 0x00136AC0
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x001376E0 File Offset: 0x00136AE0
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlDouble(value);
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x00137700 File Offset: 0x00136B00
		public override void SetCapacity(int capacity)
		{
			SqlDouble[] destinationArray = new SqlDouble[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x00137740 File Offset: 0x00136B40
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

		// Token: 0x0600321C RID: 12828 RVA: 0x001377B8 File Offset: 0x00136BB8
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x00137814 File Offset: 0x00136C14
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDouble[recordCount];
		}

		// Token: 0x0600321E RID: 12830 RVA: 0x00137828 File Offset: 0x00136C28
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlDouble[] array = (SqlDouble[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600321F RID: 12831 RVA: 0x00137860 File Offset: 0x00136C60
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlDouble[])store;
		}

		// Token: 0x04001DB7 RID: 7607
		private SqlDouble[] values;
	}
}
