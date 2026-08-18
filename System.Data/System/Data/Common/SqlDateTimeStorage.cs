using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000190 RID: 400
	internal sealed class SqlDateTimeStorage : DataStorage
	{
		// Token: 0x06001778 RID: 6008 RVA: 0x0024BD88 File Offset: 0x0024B188
		public SqlDateTimeStorage(DataColumn column) : base(column, typeof(SqlDateTime), SqlDateTime.Null, SqlDateTime.Null)
		{
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0024BDC8 File Offset: 0x0024B1C8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					SqlDateTime sqlDateTime = SqlDateTime.MaxValue;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							if (SqlDateTime.LessThan(this.values[num], sqlDateTime).IsTrue)
							{
								sqlDateTime = this.values[num];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDateTime;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					SqlDateTime sqlDateTime2 = SqlDateTime.MinValue;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							if (SqlDateTime.GreaterThan(this.values[num2], sqlDateTime2).IsTrue)
							{
								sqlDateTime2 = this.values[num2];
							}
							flag = true;
						}
					}
					if (flag)
					{
						return sqlDateTime2;
					}
					return this.NullValue;
				}
				case AggregateType.First:
					if (records.Length > 0)
					{
						return this.values[records[0]];
					}
					return null;
				case AggregateType.Count:
				{
					int num3 = 0;
					for (int k = 0; k < records.Length; k++)
					{
						if (!this.IsNull(records[k]))
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
				throw ExprException.Overflow(typeof(SqlDateTime));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0024BF88 File Offset: 0x0024B388
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x0024BFB8 File Offset: 0x0024B3B8
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlDateTime)value);
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0024BFE8 File Offset: 0x0024B3E8
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDateTime(value);
			}
			return this.NullValue;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x0024C018 File Offset: 0x0024B418
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x0024C048 File Offset: 0x0024B448
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x0024C078 File Offset: 0x0024B478
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x0024C098 File Offset: 0x0024B498
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlDateTime(value);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x0024C0C8 File Offset: 0x0024B4C8
		public override void SetCapacity(int capacity)
		{
			SqlDateTime[] destinationArray = new SqlDateTime[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x0024C108 File Offset: 0x0024B508
		public override object ConvertXmlToObject(string s)
		{
			SqlDateTime sqlDateTime = default(SqlDateTime);
			string s2 = "<col>" + s + "</col>";
			StringReader input = new StringReader(s2);
			IXmlSerializable xmlSerializable = sqlDateTime;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlDateTime)xmlSerializable;
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0024C188 File Offset: 0x0024B588
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x0024C1E8 File Offset: 0x0024B5E8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDateTime[recordCount];
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x0024C208 File Offset: 0x0024B608
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlDateTime[] array = (SqlDateTime[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(record, this.IsNull(record));
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x0024C248 File Offset: 0x0024B648
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlDateTime[])store;
		}

		// Token: 0x04000D0F RID: 3343
		private SqlDateTime[] values;
	}
}
