using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200018C RID: 396
	internal sealed class SqlBooleanStorage : DataStorage
	{
		// Token: 0x0600173E RID: 5950 RVA: 0x0024AB08 File Offset: 0x00249F08
		public SqlBooleanStorage(DataColumn column) : base(column, typeof(SqlBoolean), SqlBoolean.Null, SqlBoolean.Null)
		{
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x0024AB48 File Offset: 0x00249F48
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					SqlBoolean sqlBoolean = true;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							sqlBoolean = SqlBoolean.And(this.values[num], sqlBoolean);
							flag = true;
						}
					}
					if (flag)
					{
						return sqlBoolean;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					SqlBoolean sqlBoolean2 = false;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							sqlBoolean2 = SqlBoolean.Or(this.values[num2], sqlBoolean2);
							flag = true;
						}
					}
					if (flag)
					{
						return sqlBoolean2;
					}
					return this.NullValue;
				}
				case AggregateType.First:
					if (records.Length > 0)
					{
						return this.values[records[0]];
					}
					return this.NullValue;
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
				throw ExprException.Overflow(typeof(SqlBoolean));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06001740 RID: 5952 RVA: 0x0024ACD8 File Offset: 0x0024A0D8
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x06001741 RID: 5953 RVA: 0x0024AD08 File Offset: 0x0024A108
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlBoolean)value);
		}

		// Token: 0x06001742 RID: 5954 RVA: 0x0024AD38 File Offset: 0x0024A138
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlBoolean(value);
			}
			return this.NullValue;
		}

		// Token: 0x06001743 RID: 5955 RVA: 0x0024AD68 File Offset: 0x0024A168
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001744 RID: 5956 RVA: 0x0024AD98 File Offset: 0x0024A198
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x06001745 RID: 5957 RVA: 0x0024ADC8 File Offset: 0x0024A1C8
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x06001746 RID: 5958 RVA: 0x0024ADE8 File Offset: 0x0024A1E8
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlBoolean(value);
		}

		// Token: 0x06001747 RID: 5959 RVA: 0x0024AE18 File Offset: 0x0024A218
		public override void SetCapacity(int capacity)
		{
			SqlBoolean[] destinationArray = new SqlBoolean[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x06001748 RID: 5960 RVA: 0x0024AE58 File Offset: 0x0024A258
		public override object ConvertXmlToObject(string s)
		{
			SqlBoolean sqlBoolean = default(SqlBoolean);
			string s2 = "<col>" + s + "</col>";
			StringReader input = new StringReader(s2);
			IXmlSerializable xmlSerializable = sqlBoolean;
			using (XmlTextReader xmlTextReader = new XmlTextReader(input))
			{
				xmlSerializable.ReadXml(xmlTextReader);
			}
			return (SqlBoolean)xmlSerializable;
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0024AED8 File Offset: 0x0024A2D8
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0024AF38 File Offset: 0x0024A338
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBoolean[recordCount];
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0024AF58 File Offset: 0x0024A358
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlBoolean[] array = (SqlBoolean[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0024AFA8 File Offset: 0x0024A3A8
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlBoolean[])store;
		}

		// Token: 0x04000D0B RID: 3339
		private SqlBoolean[] values;
	}
}
