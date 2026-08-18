using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000194 RID: 404
	internal sealed class SqlInt16Storage : DataStorage
	{
		// Token: 0x060017B4 RID: 6068 RVA: 0x0024D558 File Offset: 0x0024C958
		public SqlInt16Storage(DataColumn column) : base(column, typeof(SqlInt16), SqlInt16.Null, SqlInt16.Null)
		{
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0024D598 File Offset: 0x0024C998
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
						SqlInt16 sqlInt2 = 0;
						sqlInt2 = (x / (long)num2).ToSqlInt16();
						return sqlInt2;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					SqlInt16 sqlInt3 = SqlInt16.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlInt16.LessThan(this.values[num4], sqlInt3).IsTrue)
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
					SqlInt16 sqlInt4 = SqlInt16.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlInt16.GreaterThan(this.values[num5], sqlInt4).IsTrue)
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
				throw ExprException.Overflow(typeof(SqlInt16));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0024D9F8 File Offset: 0x0024CDF8
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0024DA28 File Offset: 0x0024CE28
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlInt16)value);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0024DA58 File Offset: 0x0024CE58
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlInt16(value);
			}
			return this.NullValue;
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x0024DA88 File Offset: 0x0024CE88
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x0024DAB8 File Offset: 0x0024CEB8
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0024DAE8 File Offset: 0x0024CEE8
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0024DB08 File Offset: 0x0024CF08
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlInt16(value);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0024DB38 File Offset: 0x0024CF38
		public override void SetCapacity(int capacity)
		{
			SqlInt16[] destinationArray = new SqlInt16[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0024DB78 File Offset: 0x0024CF78
		public override object ConvertXmlToObject(string s)
		{
			SqlInt16 sqlInt = default(SqlInt16);
			string s2 = "<col>" + s + "</col>";
			StringReader input = new StringReader(s2);
			IXmlSerializable xmlSerializable = sqlInt;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlInt16)xmlSerializable;
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0024DBF8 File Offset: 0x0024CFF8
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0024DC58 File Offset: 0x0024D058
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlInt16[recordCount];
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0024DC78 File Offset: 0x0024D078
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlInt16[] array = (SqlInt16[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0024DCC8 File Offset: 0x0024D0C8
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlInt16[])store;
		}

		// Token: 0x04000D13 RID: 3347
		private SqlInt16[] values;
	}
}
