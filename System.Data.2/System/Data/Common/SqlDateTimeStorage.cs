using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000319 RID: 793
	internal sealed class SqlDateTimeStorage : DataStorage
	{
		// Token: 0x060031F3 RID: 12787 RVA: 0x00136690 File Offset: 0x00135A90
		public SqlDateTimeStorage(DataColumn column) : base(column, typeof(SqlDateTime), SqlDateTime.Null, SqlDateTime.Null, StorageType.SqlDateTime)
		{
		}

		// Token: 0x060031F4 RID: 12788 RVA: 0x001366C4 File Offset: 0x00135AC4
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
					if (records.Length != 0)
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

		// Token: 0x060031F5 RID: 12789 RVA: 0x0013685C File Offset: 0x00135C5C
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x00136888 File Offset: 0x00135C88
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlDateTime)value);
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x001368AC File Offset: 0x00135CAC
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlDateTime(value);
			}
			return this.NullValue;
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x001368D0 File Offset: 0x00135CD0
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x001368F8 File Offset: 0x00135CF8
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x00136918 File Offset: 0x00135D18
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x00136938 File Offset: 0x00135D38
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlDateTime(value);
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x00136958 File Offset: 0x00135D58
		public override void SetCapacity(int capacity)
		{
			SqlDateTime[] destinationArray = new SqlDateTime[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x00136998 File Offset: 0x00135D98
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

		// Token: 0x060031FE RID: 12798 RVA: 0x00136A10 File Offset: 0x00135E10
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x00136A6C File Offset: 0x00135E6C
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlDateTime[recordCount];
		}

		// Token: 0x06003200 RID: 12800 RVA: 0x00136A80 File Offset: 0x00135E80
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlDateTime[] array = (SqlDateTime[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(record, this.IsNull(record));
		}

		// Token: 0x06003201 RID: 12801 RVA: 0x00136AB8 File Offset: 0x00135EB8
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlDateTime[])store;
		}

		// Token: 0x04001DB5 RID: 7605
		private SqlDateTime[] values;
	}
}
