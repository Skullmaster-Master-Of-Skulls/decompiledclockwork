using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000316 RID: 790
	internal sealed class SqlBytesStorage : DataStorage
	{
		// Token: 0x060031C8 RID: 12744 RVA: 0x001359F4 File Offset: 0x00134DF4
		public SqlBytesStorage(DataColumn column) : base(column, typeof(SqlBytes), SqlBytes.Null, SqlBytes.Null, StorageType.SqlBytes)
		{
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x00135A20 File Offset: 0x00134E20
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
				throw ExprException.Overflow(typeof(SqlBytes));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x00135AB4 File Offset: 0x00134EB4
		public override int Compare(int recordNo1, int recordNo2)
		{
			return 0;
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x00135AC4 File Offset: 0x00134EC4
		public override int CompareValueTo(int recordNo, object value)
		{
			return 0;
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x00135AD4 File Offset: 0x00134ED4
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x00135AF4 File Offset: 0x00134EF4
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x00135B0C File Offset: 0x00134F0C
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x00135B28 File Offset: 0x00134F28
		public override void Set(int record, object value)
		{
			if (value == DBNull.Value || value == null)
			{
				this.values[record] = SqlBytes.Null;
				return;
			}
			this.values[record] = (SqlBytes)value;
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x00135B5C File Offset: 0x00134F5C
		public override void SetCapacity(int capacity)
		{
			SqlBytes[] destinationArray = new SqlBytes[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x00135B9C File Offset: 0x00134F9C
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

		// Token: 0x060031D2 RID: 12754 RVA: 0x00135C14 File Offset: 0x00135014
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x00135C70 File Offset: 0x00135070
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBytes[recordCount];
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x00135C84 File Offset: 0x00135084
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlBytes[] array = (SqlBytes[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x00135CB4 File Offset: 0x001350B4
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlBytes[])store;
		}

		// Token: 0x04001DB2 RID: 7602
		private SqlBytes[] values;
	}
}
