using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000314 RID: 788
	internal sealed class SqlBinaryStorage : DataStorage
	{
		// Token: 0x060031AA RID: 12714 RVA: 0x0013528C File Offset: 0x0013468C
		public SqlBinaryStorage(DataColumn column) : base(column, typeof(SqlBinary), SqlBinary.Null, SqlBinary.Null, StorageType.SqlBinary)
		{
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x001352C0 File Offset: 0x001346C0
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
				throw ExprException.Overflow(typeof(SqlBinary));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x00135360 File Offset: 0x00134760
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x0013538C File Offset: 0x0013478C
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlBinary)value);
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x001353B0 File Offset: 0x001347B0
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlBinary(value);
			}
			return this.NullValue;
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x001353D4 File Offset: 0x001347D4
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x001353FC File Offset: 0x001347FC
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x0013541C File Offset: 0x0013481C
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x0013543C File Offset: 0x0013483C
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlBinary(value);
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x0013545C File Offset: 0x0013485C
		public override void SetCapacity(int capacity)
		{
			SqlBinary[] destinationArray = new SqlBinary[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x0013549C File Offset: 0x0013489C
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

		// Token: 0x060031B5 RID: 12725 RVA: 0x00135514 File Offset: 0x00134914
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x00135570 File Offset: 0x00134970
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBinary[recordCount];
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x00135584 File Offset: 0x00134984
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlBinary[] array = (SqlBinary[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x001355BC File Offset: 0x001349BC
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlBinary[])store;
		}

		// Token: 0x04001DB0 RID: 7600
		private SqlBinary[] values;
	}
}
