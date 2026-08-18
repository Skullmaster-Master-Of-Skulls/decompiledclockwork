using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000317 RID: 791
	internal sealed class SqlByteStorage : DataStorage
	{
		// Token: 0x060031D6 RID: 12758 RVA: 0x00135CD0 File Offset: 0x001350D0
		public SqlByteStorage(DataColumn column) : base(column, typeof(SqlByte), SqlByte.Null, SqlByte.Null, StorageType.SqlByte)
		{
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x00135D04 File Offset: 0x00135104
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
						SqlByte sqlByte = 0;
						sqlByte = (x / (long)num2).ToSqlByte();
						return sqlByte;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					SqlByte sqlByte2 = SqlByte.MaxValue;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							if (SqlByte.LessThan(this.values[num4], sqlByte2).IsTrue)
							{
								sqlByte2 = this.values[num4];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlByte2;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					SqlByte sqlByte3 = SqlByte.MinValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							if (SqlByte.GreaterThan(this.values[num5], sqlByte3).IsTrue)
							{
								sqlByte3 = this.values[num5];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlByte3;
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
				throw ExprException.Overflow(typeof(SqlByte));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x0013613C File Offset: 0x0013553C
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x00136168 File Offset: 0x00135568
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlByte)value);
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x0013618C File Offset: 0x0013558C
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlByte(value);
			}
			return this.NullValue;
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x001361B0 File Offset: 0x001355B0
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x001361D8 File Offset: 0x001355D8
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x001361F8 File Offset: 0x001355F8
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x00136218 File Offset: 0x00135618
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlByte(value);
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x00136238 File Offset: 0x00135638
		public override void SetCapacity(int capacity)
		{
			SqlByte[] destinationArray = new SqlByte[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x00136278 File Offset: 0x00135678
		public override object ConvertXmlToObject(string s)
		{
			SqlByte sqlByte = default(SqlByte);
			string s2 = "<col>" + s + "</col>";
			StringReader input = new StringReader(s2);
			IXmlSerializable xmlSerializable = sqlByte;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlByte)xmlSerializable;
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x001362F0 File Offset: 0x001356F0
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x0013634C File Offset: 0x0013574C
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlByte[recordCount];
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x00136360 File Offset: 0x00135760
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlByte[] array = (SqlByte[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(record, this.IsNull(record));
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x00136398 File Offset: 0x00135798
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlByte[])store;
		}

		// Token: 0x04001DB3 RID: 7603
		private SqlByte[] values;
	}
}
