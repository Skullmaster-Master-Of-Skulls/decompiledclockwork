using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200031C RID: 796
	internal sealed class SqlGuidStorage : DataStorage
	{
		// Token: 0x06003220 RID: 12832 RVA: 0x0013787C File Offset: 0x00136C7C
		public SqlGuidStorage(DataColumn column) : base(column, typeof(SqlGuid), SqlGuid.Null, SqlGuid.Null, StorageType.SqlGuid)
		{
		}

		// Token: 0x06003221 RID: 12833 RVA: 0x001378B0 File Offset: 0x00136CB0
		public override object Aggregate(int[] records, AggregateType kind)
		{
			try
			{
				if (kind != AggregateType.First)
				{
					if (kind == AggregateType.Count)
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
				else
				{
					if (records.Length != 0)
					{
						return this.values[records[0]];
					}
					return null;
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(SqlGuid));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06003222 RID: 12834 RVA: 0x00137950 File Offset: 0x00136D50
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x0013797C File Offset: 0x00136D7C
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlGuid)value);
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x001379A0 File Offset: 0x00136DA0
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlGuid(value);
			}
			return this.NullValue;
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x001379C4 File Offset: 0x00136DC4
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06003226 RID: 12838 RVA: 0x001379EC File Offset: 0x00136DEC
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06003227 RID: 12839 RVA: 0x00137A0C File Offset: 0x00136E0C
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x00137A2C File Offset: 0x00136E2C
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlGuid(value);
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x00137A4C File Offset: 0x00136E4C
		public override void SetCapacity(int capacity)
		{
			SqlGuid[] destinationArray = new SqlGuid[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x0600322A RID: 12842 RVA: 0x00137A8C File Offset: 0x00136E8C
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

		// Token: 0x0600322B RID: 12843 RVA: 0x00137B04 File Offset: 0x00136F04
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x00137B60 File Offset: 0x00136F60
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlGuid[recordCount];
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x00137B74 File Offset: 0x00136F74
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlGuid[] array = (SqlGuid[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600322E RID: 12846 RVA: 0x00137BAC File Offset: 0x00136FAC
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlGuid[])store;
		}

		// Token: 0x04001DB8 RID: 7608
		private SqlGuid[] values;
	}
}
