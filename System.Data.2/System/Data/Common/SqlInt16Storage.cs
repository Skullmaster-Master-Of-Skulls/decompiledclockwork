using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200031D RID: 797
	internal sealed class SqlInt16Storage : DataStorage
	{
		// Token: 0x0600322F RID: 12847 RVA: 0x00137BC8 File Offset: 0x00136FC8
		public SqlInt16Storage(DataColumn column) : base(column, typeof(SqlInt16), SqlInt16.Null, SqlInt16.Null, StorageType.SqlInt16)
		{
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x00137BFC File Offset: 0x00136FFC
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
				throw ExprException.Overflow(typeof(SqlInt16));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x00138034 File Offset: 0x00137434
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x06003232 RID: 12850 RVA: 0x00138060 File Offset: 0x00137460
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlInt16)value);
		}

		// Token: 0x06003233 RID: 12851 RVA: 0x00138084 File Offset: 0x00137484
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlInt16(value);
			}
			return this.NullValue;
		}

		// Token: 0x06003234 RID: 12852 RVA: 0x001380A8 File Offset: 0x001374A8
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06003235 RID: 12853 RVA: 0x001380D0 File Offset: 0x001374D0
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06003236 RID: 12854 RVA: 0x001380F0 File Offset: 0x001374F0
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x06003237 RID: 12855 RVA: 0x00138110 File Offset: 0x00137510
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlInt16(value);
		}

		// Token: 0x06003238 RID: 12856 RVA: 0x00138130 File Offset: 0x00137530
		public override void SetCapacity(int capacity)
		{
			SqlInt16[] destinationArray = new SqlInt16[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x06003239 RID: 12857 RVA: 0x00138170 File Offset: 0x00137570
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

		// Token: 0x0600323A RID: 12858 RVA: 0x001381E8 File Offset: 0x001375E8
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600323B RID: 12859 RVA: 0x00138244 File Offset: 0x00137644
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlInt16[recordCount];
		}

		// Token: 0x0600323C RID: 12860 RVA: 0x00138258 File Offset: 0x00137658
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlInt16[] array = (SqlInt16[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600323D RID: 12861 RVA: 0x00138290 File Offset: 0x00137690
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlInt16[])store;
		}

		// Token: 0x04001DB9 RID: 7609
		private SqlInt16[] values;
	}
}
