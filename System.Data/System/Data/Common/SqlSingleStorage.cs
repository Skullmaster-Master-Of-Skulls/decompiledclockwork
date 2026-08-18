using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000198 RID: 408
	internal sealed class SqlSingleStorage : DataStorage
	{
		// Token: 0x060017F0 RID: 6128 RVA: 0x0024F388 File Offset: 0x0024E788
		public SqlSingleStorage(DataColumn column) : base(column, typeof(SqlSingle), SqlSingle.Null, SqlSingle.Null)
		{
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x0024F3C8 File Offset: 0x0024E7C8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					SqlSingle sqlSingle = 0f;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlSingle += this.values[num];
							flag = true;
						}
					}
					if (flag)
					{
						return sqlSingle;
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
							x += this.values[num3].ToSqlDouble();
							num2++;
							flag = true;
						}
					}
					if (flag)
					{
						SqlSingle sqlSingle2 = 0f;
						sqlSingle2 = (x / (double)num2).ToSqlSingle();
						return sqlSingle2;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					SqlSingle sqlSingle3 = SqlSingle.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlSingle.LessThan(this.values[num4], sqlSingle3).IsTrue)
							{
								sqlSingle3 = this.values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlSingle3;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					SqlSingle sqlSingle4 = SqlSingle.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlSingle.GreaterThan(this.values[num5], sqlSingle4).IsTrue)
							{
								sqlSingle4 = this.values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlSingle4;
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
				throw ExprException.Overflow(typeof(SqlSingle));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x0024F828 File Offset: 0x0024EC28
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x0024F858 File Offset: 0x0024EC58
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlSingle)value);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x0024F888 File Offset: 0x0024EC88
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlSingle(value);
			}
			return this.NullValue;
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x0024F8B8 File Offset: 0x0024ECB8
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x0024F8E8 File Offset: 0x0024ECE8
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x0024F918 File Offset: 0x0024ED18
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x0024F938 File Offset: 0x0024ED38
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlSingle(value);
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x0024F968 File Offset: 0x0024ED68
		public override void SetCapacity(int capacity)
		{
			SqlSingle[] destinationArray = new SqlSingle[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x0024F9A8 File Offset: 0x0024EDA8
		public override object ConvertXmlToObject(string s)
		{
			SqlSingle sqlSingle = default(SqlSingle);
			string s2 = "<col>" + s + "</col>";
			StringReader input = new StringReader(s2);
			IXmlSerializable xmlSerializable = sqlSingle;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlSingle)xmlSerializable;
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x0024FA28 File Offset: 0x0024EE28
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x0024FA88 File Offset: 0x0024EE88
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlSingle[recordCount];
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x0024FAA8 File Offset: 0x0024EEA8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlSingle[] array = (SqlSingle[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x0024FAF8 File Offset: 0x0024EEF8
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlSingle[])store;
		}

		// Token: 0x04000D17 RID: 3351
		private SqlSingle[] values;
	}
}
