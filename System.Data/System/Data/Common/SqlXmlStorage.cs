using System;
using System.Collections;
using System.Data.SqlTypes;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x0200019B RID: 411
	internal sealed class SqlXmlStorage : DataStorage
	{
		// Token: 0x06001823 RID: 6179 RVA: 0x002505D8 File Offset: 0x0024F9D8
		public SqlXmlStorage(DataColumn column) : base(column, typeof(SqlXml), SqlXml.Null, SqlXml.Null)
		{
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00250608 File Offset: 0x0024FA08
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
				throw ExprException.Overflow(typeof(SqlXml));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x002506A8 File Offset: 0x0024FAA8
		public override int Compare(int recordNo1, int recordNo2)
		{
			return 0;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x002506B8 File Offset: 0x0024FAB8
		public override int CompareValueTo(int recordNo, object value)
		{
			return 0;
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x002506C8 File Offset: 0x0024FAC8
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001828 RID: 6184 RVA: 0x002506E8 File Offset: 0x0024FAE8
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06001829 RID: 6185 RVA: 0x00250708 File Offset: 0x0024FB08
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x0600182A RID: 6186 RVA: 0x00250728 File Offset: 0x0024FB28
		public override void Set(int record, object value)
		{
			if (value == DBNull.Value || value == null)
			{
				this.values[record] = SqlXml.Null;
				return;
			}
			this.values[record] = (SqlXml)value;
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x00250768 File Offset: 0x0024FB68
		public override void SetCapacity(int capacity)
		{
			SqlXml[] destinationArray = new SqlXml[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x002507A8 File Offset: 0x0024FBA8
		public override object ConvertXmlToObject(string s)
		{
			XmlTextReader value = new XmlTextReader(s, XmlNodeType.Element, null);
			return new SqlXml(value);
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x002507C8 File Offset: 0x0024FBC8
		public override string ConvertObjectToXml(object value)
		{
			SqlXml sqlXml = (SqlXml)value;
			if (sqlXml.IsNull)
			{
				return ADP.StrEmpty;
			}
			return sqlXml.Value;
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x002507F8 File Offset: 0x0024FBF8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlXml[recordCount];
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x00250818 File Offset: 0x0024FC18
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlXml[] array = (SqlXml[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x00250848 File Offset: 0x0024FC48
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlXml[])store;
		}

		// Token: 0x04000D1D RID: 3357
		private SqlXml[] values;
	}
}
