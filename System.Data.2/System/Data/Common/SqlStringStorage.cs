using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000322 RID: 802
	internal sealed class SqlStringStorage : DataStorage
	{
		// Token: 0x0600327A RID: 12922 RVA: 0x00139E40 File Offset: 0x00139240
		public SqlStringStorage(DataColumn column) : base(column, typeof(SqlString), SqlString.Null, SqlString.Null, StorageType.SqlString)
		{
		}

		// Token: 0x0600327B RID: 12923 RVA: 0x00139E74 File Offset: 0x00139274
		public override object Aggregate(int[] recordNos, AggregateType kind)
		{
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					int num = -1;
					int i;
					for (i = 0; i < recordNos.Length; i++)
					{
						if (!this.IsNull(recordNos[i]))
						{
							num = recordNos[i];
							break;
						}
					}
					if (num >= 0)
					{
						for (i++; i < recordNos.Length; i++)
						{
							if (!this.IsNull(recordNos[i]) && this.Compare(num, recordNos[i]) > 0)
							{
								num = recordNos[i];
							}
						}
						return this.Get(num);
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					int num2 = -1;
					int i;
					for (i = 0; i < recordNos.Length; i++)
					{
						if (!this.IsNull(recordNos[i]))
						{
							num2 = recordNos[i];
							break;
						}
					}
					if (num2 >= 0)
					{
						for (i++; i < recordNos.Length; i++)
						{
							if (this.Compare(num2, recordNos[i]) < 0)
							{
								num2 = recordNos[i];
							}
						}
						return this.Get(num2);
					}
					return this.NullValue;
				}
				case AggregateType.Count:
				{
					int num3 = 0;
					for (int i = 0; i < recordNos.Length; i++)
					{
						if (!this.IsNull(recordNos[i]))
						{
							num3++;
						}
					}
					return num3;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlString));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x0600327C RID: 12924 RVA: 0x00139FCC File Offset: 0x001393CC
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.Compare(this.values[recordNo1], this.values[recordNo2]);
		}

		// Token: 0x0600327D RID: 12925 RVA: 0x00139FF8 File Offset: 0x001393F8
		public int Compare(SqlString valueNo1, SqlString valueNo2)
		{
			if (valueNo1.IsNull && valueNo2.IsNull)
			{
				return 0;
			}
			if (valueNo1.IsNull)
			{
				return -1;
			}
			if (valueNo2.IsNull)
			{
				return 1;
			}
			return this.Table.Compare(valueNo1.Value, valueNo2.Value);
		}

		// Token: 0x0600327E RID: 12926 RVA: 0x0013A048 File Offset: 0x00139448
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.Compare(this.values[recordNo], (SqlString)value);
		}

		// Token: 0x0600327F RID: 12927 RVA: 0x0013A070 File Offset: 0x00139470
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlString(value);
			}
			return this.NullValue;
		}

		// Token: 0x06003280 RID: 12928 RVA: 0x0013A094 File Offset: 0x00139494
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06003281 RID: 12929 RVA: 0x0013A0BC File Offset: 0x001394BC
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06003282 RID: 12930 RVA: 0x0013A0DC File Offset: 0x001394DC
		public override int GetStringLength(int record)
		{
			SqlString sqlString = this.values[record];
			if (!sqlString.IsNull)
			{
				return sqlString.Value.Length;
			}
			return 0;
		}

		// Token: 0x06003283 RID: 12931 RVA: 0x0013A110 File Offset: 0x00139510
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x06003284 RID: 12932 RVA: 0x0013A130 File Offset: 0x00139530
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlString(value);
		}

		// Token: 0x06003285 RID: 12933 RVA: 0x0013A150 File Offset: 0x00139550
		public override void SetCapacity(int capacity)
		{
			SqlString[] destinationArray = new SqlString[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x0013A190 File Offset: 0x00139590
		public override object ConvertXmlToObject(string s)
		{
			SqlString sqlString = default(SqlString);
			string s2 = "<col>" + s + "</col>";
			StringReader input = new StringReader(s2);
			IXmlSerializable xmlSerializable = sqlString;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlString)xmlSerializable;
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x0013A208 File Offset: 0x00139608
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x0013A264 File Offset: 0x00139664
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlString[recordCount];
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x0013A278 File Offset: 0x00139678
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlString[] array = (SqlString[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x0013A2B0 File Offset: 0x001396B0
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlString[])store;
		}

		// Token: 0x04001DBE RID: 7614
		private SqlString[] values;
	}
}
