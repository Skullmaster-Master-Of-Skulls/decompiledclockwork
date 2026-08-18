using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200031E RID: 798
	internal sealed class SqlInt32Storage : DataStorage
	{
		// Token: 0x0600323E RID: 12862 RVA: 0x001382AC File Offset: 0x001376AC
		public SqlInt32Storage(DataColumn column) : base(column, typeof(SqlInt32), SqlInt32.Null, SqlInt32.Null, StorageType.SqlInt32)
		{
		}

		// Token: 0x0600323F RID: 12863 RVA: 0x001382E0 File Offset: 0x001376E0
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
				throw ExprException.Overflow(typeof(SqlInt32));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06003240 RID: 12864 RVA: 0x00138718 File Offset: 0x00137B18
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x06003241 RID: 12865 RVA: 0x00138744 File Offset: 0x00137B44
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlInt32)value);
		}

		// Token: 0x06003242 RID: 12866 RVA: 0x00138768 File Offset: 0x00137B68
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlInt32(value);
			}
			return this.NullValue;
		}

		// Token: 0x06003243 RID: 12867 RVA: 0x0013878C File Offset: 0x00137B8C
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x001387B4 File Offset: 0x00137BB4
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06003245 RID: 12869 RVA: 0x001387D4 File Offset: 0x00137BD4
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x06003246 RID: 12870 RVA: 0x001387F4 File Offset: 0x00137BF4
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlInt32(value);
		}

		// Token: 0x06003247 RID: 12871 RVA: 0x00138814 File Offset: 0x00137C14
		public override void SetCapacity(int capacity)
		{
			SqlInt32[] destinationArray = new SqlInt32[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x06003248 RID: 12872 RVA: 0x00138854 File Offset: 0x00137C54
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

		// Token: 0x06003249 RID: 12873 RVA: 0x001388CC File Offset: 0x00137CCC
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600324A RID: 12874 RVA: 0x00138928 File Offset: 0x00137D28
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlInt32[recordCount];
		}

		// Token: 0x0600324B RID: 12875 RVA: 0x0013893C File Offset: 0x00137D3C
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlInt32[] array = (SqlInt32[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600324C RID: 12876 RVA: 0x00138974 File Offset: 0x00137D74
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlInt32[])store;
		}

		// Token: 0x04001DBA RID: 7610
		private SqlInt32[] values;
	}
}
