using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200018B RID: 395
	internal sealed class SqlBinaryStorage : DataStorage
	{
		// Token: 0x0600172F RID: 5935 RVA: 0x0024A728 File Offset: 0x00249B28
		public SqlBinaryStorage(DataColumn column) : base(column, typeof(SqlBinary), SqlBinary.Null, SqlBinary.Null)
		{
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0024A768 File Offset: 0x00249B68
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
				throw ExprException.Overflow(typeof(SqlBinary));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0024A818 File Offset: 0x00249C18
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0024A848 File Offset: 0x00249C48
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlBinary)value);
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0024A878 File Offset: 0x00249C78
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlBinary(value);
			}
			return this.NullValue;
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0024A8A8 File Offset: 0x00249CA8
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0024A8D8 File Offset: 0x00249CD8
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0024A908 File Offset: 0x00249D08
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0024A928 File Offset: 0x00249D28
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlBinary(value);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x0024A958 File Offset: 0x00249D58
		public override void SetCapacity(int capacity)
		{
			SqlBinary[] destinationArray = new SqlBinary[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x0024A998 File Offset: 0x00249D98
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
			return (SqlBinary)xmlSerializable;
		}

		// Token: 0x0600173A RID: 5946 RVA: 0x0024AA18 File Offset: 0x00249E18
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0024AA78 File Offset: 0x00249E78
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBinary[recordCount];
		}

		// Token: 0x0600173C RID: 5948 RVA: 0x0024AA98 File Offset: 0x00249E98
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlBinary[] array = (SqlBinary[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x0024AAE8 File Offset: 0x00249EE8
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlBinary[])store;
		}

		// Token: 0x04000D0A RID: 3338
		private SqlBinary[] values;
	}
}
