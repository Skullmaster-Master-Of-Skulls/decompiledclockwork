using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000193 RID: 403
	internal sealed class SqlGuidStorage : DataStorage
	{
		// Token: 0x060017A5 RID: 6053 RVA: 0x0024D178 File Offset: 0x0024C578
		public SqlGuidStorage(DataColumn column) : base(column, typeof(SqlGuid), SqlGuid.Null, SqlGuid.Null)
		{
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0024D1B8 File Offset: 0x0024C5B8
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
				throw ExprException.Overflow(typeof(SqlGuid));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0024D268 File Offset: 0x0024C668
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0024D298 File Offset: 0x0024C698
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlGuid)value);
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0024D2C8 File Offset: 0x0024C6C8
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlGuid(value);
			}
			return this.NullValue;
		}

		// Token: 0x060017AA RID: 6058 RVA: 0x0024D2F8 File Offset: 0x0024C6F8
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x0024D328 File Offset: 0x0024C728
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x0024D358 File Offset: 0x0024C758
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x0024D378 File Offset: 0x0024C778
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlGuid(value);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0024D3A8 File Offset: 0x0024C7A8
		public override void SetCapacity(int capacity)
		{
			SqlGuid[] destinationArray = new SqlGuid[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0024D3E8 File Offset: 0x0024C7E8
		public override object ConvertXmlToObject(string s)
		{
			SqlGuid sqlGuid = default(SqlGuid);
			string s2 = "<col>" + s + "</col>";
			StringReader input = new StringReader(s2);
			IXmlSerializable xmlSerializable = sqlGuid;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlGuid)xmlSerializable;
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x0024D468 File Offset: 0x0024C868
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x0024D4C8 File Offset: 0x0024C8C8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlGuid[recordCount];
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0024D4E8 File Offset: 0x0024C8E8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlGuid[] array = (SqlGuid[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0024D538 File Offset: 0x0024C938
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlGuid[])store;
		}

		// Token: 0x04000D12 RID: 3346
		private SqlGuid[] values;
	}
}
