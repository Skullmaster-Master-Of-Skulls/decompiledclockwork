using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200018D RID: 397
	internal sealed class SqlBytesStorage : DataStorage
	{
		// Token: 0x0600174D RID: 5965 RVA: 0x0024AFC8 File Offset: 0x0024A3C8
		public SqlBytesStorage(DataColumn column) : base(column, typeof(SqlBytes), SqlBytes.Null, SqlBytes.Null)
		{
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x0024AFF8 File Offset: 0x0024A3F8
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
				throw ExprException.Overflow(typeof(SqlBytes));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x0600174F RID: 5967 RVA: 0x0024B098 File Offset: 0x0024A498
		public override int Compare(int recordNo1, int recordNo2)
		{
			return 0;
		}

		// Token: 0x06001750 RID: 5968 RVA: 0x0024B0A8 File Offset: 0x0024A4A8
		public override int CompareValueTo(int recordNo, object value)
		{
			return 0;
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0024B0B8 File Offset: 0x0024A4B8
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001752 RID: 5970 RVA: 0x0024B0D8 File Offset: 0x0024A4D8
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06001753 RID: 5971 RVA: 0x0024B0F8 File Offset: 0x0024A4F8
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x06001754 RID: 5972 RVA: 0x0024B118 File Offset: 0x0024A518
		public override void Set(int record, object value)
		{
			if (value == DBNull.Value || value == null)
			{
				this.values[record] = SqlBytes.Null;
				return;
			}
			this.values[record] = (SqlBytes)value;
		}

		// Token: 0x06001755 RID: 5973 RVA: 0x0024B158 File Offset: 0x0024A558
		public override void SetCapacity(int capacity)
		{
			SqlBytes[] destinationArray = new SqlBytes[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x06001756 RID: 5974 RVA: 0x0024B198 File Offset: 0x0024A598
		public override object ConvertXmlToObject(string s)
		{
			SqlBinary sqlBinary = default(SqlBinary);
			string s2 = "<col>" + s + "</col>";
			StringReader input = new StringReader(s2);
			IXmlSerializable xmlSerializable = sqlBinary;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return new SqlBytes((SqlBinary)xmlSerializable);
		}

		// Token: 0x06001757 RID: 5975 RVA: 0x0024B218 File Offset: 0x0024A618
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06001758 RID: 5976 RVA: 0x0024B278 File Offset: 0x0024A678
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBytes[recordCount];
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0024B298 File Offset: 0x0024A698
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlBytes[] array = (SqlBytes[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x0024B2C8 File Offset: 0x0024A6C8
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlBytes[])store;
		}

		// Token: 0x04000D0C RID: 3340
		private SqlBytes[] values;
	}
}
