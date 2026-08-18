using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000321 RID: 801
	internal sealed class SqlSingleStorage : DataStorage
	{
		// Token: 0x0600326B RID: 12907 RVA: 0x00139754 File Offset: 0x00138B54
		public SqlSingleStorage(DataColumn column) : base(column, typeof(SqlSingle), SqlSingle.Null, SqlSingle.Null, StorageType.SqlSingle)
		{
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x00139788 File Offset: 0x00138B88
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
				throw ExprException.Overflow(typeof(SqlSingle));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x00139BC8 File Offset: 0x00138FC8
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x00139BF4 File Offset: 0x00138FF4
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlSingle)value);
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x00139C18 File Offset: 0x00139018
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlSingle(value);
			}
			return this.NullValue;
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x00139C3C File Offset: 0x0013903C
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06003271 RID: 12913 RVA: 0x00139C64 File Offset: 0x00139064
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x00139C84 File Offset: 0x00139084
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x00139CA4 File Offset: 0x001390A4
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlSingle(value);
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x00139CC4 File Offset: 0x001390C4
		public override void SetCapacity(int capacity)
		{
			SqlSingle[] destinationArray = new SqlSingle[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x06003275 RID: 12917 RVA: 0x00139D04 File Offset: 0x00139104
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

		// Token: 0x06003276 RID: 12918 RVA: 0x00139D7C File Offset: 0x0013917C
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x00139DD8 File Offset: 0x001391D8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlSingle[recordCount];
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x00139DEC File Offset: 0x001391EC
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlSingle[] array = (SqlSingle[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06003279 RID: 12921 RVA: 0x00139E24 File Offset: 0x00139224
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlSingle[])store;
		}

		// Token: 0x04001DBD RID: 7613
		private SqlSingle[] values;
	}
}
