using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000199 RID: 409
	internal sealed class SqlStringStorage : DataStorage
	{
		// Token: 0x060017FF RID: 6143 RVA: 0x0024FB18 File Offset: 0x0024EF18
		public SqlStringStorage(DataColumn column) : base(column, typeof(SqlString), SqlString.Null, SqlString.Null)
		{
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0024FB58 File Offset: 0x0024EF58
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

		// Token: 0x06001801 RID: 6145 RVA: 0x0024FCB8 File Offset: 0x0024F0B8
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.Compare(this.values[recordNo1], this.values[recordNo2]);
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0024FCF8 File Offset: 0x0024F0F8
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

		// Token: 0x06001803 RID: 6147 RVA: 0x0024FD48 File Offset: 0x0024F148
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.Compare(this.values[recordNo], (SqlString)value);
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0024FD78 File Offset: 0x0024F178
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlString(value);
			}
			return this.NullValue;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x0024FDA8 File Offset: 0x0024F1A8
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0024FDD8 File Offset: 0x0024F1D8
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0024FE08 File Offset: 0x0024F208
		public override int GetStringLength(int record)
		{
			SqlString sqlString = this.values[record];
			if (!sqlString.IsNull)
			{
				return sqlString.Value.Length;
			}
			return 0;
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0024FE48 File Offset: 0x0024F248
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0024FE68 File Offset: 0x0024F268
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlString(value);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0024FE98 File Offset: 0x0024F298
		public override void SetCapacity(int capacity)
		{
			SqlString[] destinationArray = new SqlString[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0024FED8 File Offset: 0x0024F2D8
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

		// Token: 0x0600180C RID: 6156 RVA: 0x0024FF58 File Offset: 0x0024F358
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0024FFB8 File Offset: 0x0024F3B8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlString[recordCount];
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0024FFD8 File Offset: 0x0024F3D8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlString[] array = (SqlString[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00250028 File Offset: 0x0024F428
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlString[])store;
		}

		// Token: 0x04000D18 RID: 3352
		private SqlString[] values;
	}
}
