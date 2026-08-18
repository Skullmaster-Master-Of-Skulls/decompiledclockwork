using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200018F RID: 399
	internal sealed class SqlCharsStorage : DataStorage
	{
		// Token: 0x0600176A RID: 5994 RVA: 0x0024BA68 File Offset: 0x0024AE68
		public SqlCharsStorage(DataColumn column) : base(column, typeof(SqlChars), SqlChars.Null, SqlChars.Null)
		{
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x0024BA98 File Offset: 0x0024AE98
		public override object Aggregate(int[] records, AggregateType kind)
		{
			try
			{
				switch (kind)
				{
				case AggregateType.First:
					if (records.Length > 0)
					{
						return this.values[records[0]];
					}
					return null;
				case AggregateType.Count:
				{
					int num = 0;
					for (int i = 0; i < records.Length; i++)
					{
						if (!this.IsNull(records[i]))
						{
							num++;
						}
					}
					return num;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlChars));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x0024BB38 File Offset: 0x0024AF38
		public override int Compare(int recordNo1, int recordNo2)
		{
			return 0;
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x0024BB48 File Offset: 0x0024AF48
		public override int CompareValueTo(int recordNo, object value)
		{
			return 0;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x0024BB58 File Offset: 0x0024AF58
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x0024BB78 File Offset: 0x0024AF78
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x0024BB98 File Offset: 0x0024AF98
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x0024BBB8 File Offset: 0x0024AFB8
		public override void Set(int record, object value)
		{
			if (value == DBNull.Value || value == null)
			{
				this.values[record] = SqlChars.Null;
				return;
			}
			this.values[record] = (SqlChars)value;
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x0024BBF8 File Offset: 0x0024AFF8
		public override void SetCapacity(int capacity)
		{
			SqlChars[] destinationArray = new SqlChars[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x0024BC38 File Offset: 0x0024B038
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
			return new SqlChars((SqlString)xmlSerializable);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0024BCB8 File Offset: 0x0024B0B8
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0024BD18 File Offset: 0x0024B118
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlChars[recordCount];
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0024BD38 File Offset: 0x0024B138
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlChars[] array = (SqlChars[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x0024BD68 File Offset: 0x0024B168
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlChars[])store;
		}

		// Token: 0x04000D0E RID: 3342
		private SqlChars[] values;
	}
}
