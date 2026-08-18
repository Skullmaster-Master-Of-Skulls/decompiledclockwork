using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000195 RID: 405
	internal sealed class SqlInt32Storage : DataStorage
	{
		// Token: 0x060017C3 RID: 6083 RVA: 0x0024DCE8 File Offset: 0x0024D0E8
		public SqlInt32Storage(DataColumn column) : base(column, typeof(SqlInt32), SqlInt32.Null, SqlInt32.Null)
		{
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0024DD28 File Offset: 0x0024D128
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
						SqlInt32 sqlInt2 = 0;
						sqlInt2 = (x / (long)num2).ToSqlInt32();
						return sqlInt2;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					SqlInt32 sqlInt3 = SqlInt32.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlInt32.LessThan(this.values[num4], sqlInt3).IsTrue)
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
					SqlInt32 sqlInt4 = SqlInt32.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlInt32.GreaterThan(this.values[num5], sqlInt4).IsTrue)
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
				throw ExprException.Overflow(typeof(SqlInt32));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x0024E188 File Offset: 0x0024D588
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x0024E1B8 File Offset: 0x0024D5B8
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlInt32)value);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x0024E1E8 File Offset: 0x0024D5E8
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlInt32(value);
			}
			return this.NullValue;
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x0024E218 File Offset: 0x0024D618
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x0024E248 File Offset: 0x0024D648
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x0024E278 File Offset: 0x0024D678
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0024E298 File Offset: 0x0024D698
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlInt32(value);
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x0024E2C8 File Offset: 0x0024D6C8
		public override void SetCapacity(int capacity)
		{
			SqlInt32[] destinationArray = new SqlInt32[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x0024E308 File Offset: 0x0024D708
		public override object ConvertXmlToObject(string s)
		{
			SqlInt32 sqlInt = default(SqlInt32);
			string s2 = "<col>" + s + "</col>";
			StringReader input = new StringReader(s2);
			IXmlSerializable xmlSerializable = sqlInt;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlInt32)xmlSerializable;
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0024E388 File Offset: 0x0024D788
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x0024E3E8 File Offset: 0x0024D7E8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlInt32[recordCount];
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x0024E408 File Offset: 0x0024D808
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlInt32[] array = (SqlInt32[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0024E458 File Offset: 0x0024D858
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlInt32[])store;
		}

		// Token: 0x04000D14 RID: 3348
		private SqlInt32[] values;
	}
}
