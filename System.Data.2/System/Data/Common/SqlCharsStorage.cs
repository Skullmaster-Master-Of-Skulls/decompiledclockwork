using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000318 RID: 792
	internal sealed class SqlCharsStorage : DataStorage
	{
		// Token: 0x060031E5 RID: 12773 RVA: 0x001363B4 File Offset: 0x001357B4
		public SqlCharsStorage(DataColumn column) : base(column, typeof(SqlChars), SqlChars.Null, SqlChars.Null, StorageType.SqlChars)
		{
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x001363E0 File Offset: 0x001357E0
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
				throw ExprException.Overflow(typeof(SqlChars));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x00136474 File Offset: 0x00135874
		public override int Compare(int recordNo1, int recordNo2)
		{
			return 0;
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x00136484 File Offset: 0x00135884
		public override int CompareValueTo(int recordNo, object value)
		{
			return 0;
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x00136494 File Offset: 0x00135894
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x001364B4 File Offset: 0x001358B4
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x001364CC File Offset: 0x001358CC
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x060031EC RID: 12780 RVA: 0x001364E8 File Offset: 0x001358E8
		public override void Set(int record, object value)
		{
			if (value == DBNull.Value || value == null)
			{
				this.values[record] = SqlChars.Null;
				return;
			}
			this.values[record] = (SqlChars)value;
		}

		// Token: 0x060031ED RID: 12781 RVA: 0x0013651C File Offset: 0x0013591C
		public override void SetCapacity(int capacity)
		{
			SqlChars[] destinationArray = new SqlChars[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060031EE RID: 12782 RVA: 0x0013655C File Offset: 0x0013595C
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

		// Token: 0x060031EF RID: 12783 RVA: 0x001365D4 File Offset: 0x001359D4
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060031F0 RID: 12784 RVA: 0x00136630 File Offset: 0x00135A30
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlChars[recordCount];
		}

		// Token: 0x060031F1 RID: 12785 RVA: 0x00136644 File Offset: 0x00135A44
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlChars[] array = (SqlChars[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060031F2 RID: 12786 RVA: 0x00136674 File Offset: 0x00135A74
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlChars[])store;
		}

		// Token: 0x04001DB4 RID: 7604
		private SqlChars[] values;
	}
}
