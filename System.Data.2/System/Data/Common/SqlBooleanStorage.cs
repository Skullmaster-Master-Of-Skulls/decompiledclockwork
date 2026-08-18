using System;
using System.Collections;
using System.Data.SqlTypes;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000315 RID: 789
	internal sealed class SqlBooleanStorage : DataStorage
	{
		// Token: 0x060031B9 RID: 12729 RVA: 0x001355D8 File Offset: 0x001349D8
		public SqlBooleanStorage(DataColumn column) : base(column, typeof(SqlBoolean), SqlBoolean.Null, SqlBoolean.Null, StorageType.SqlBoolean)
		{
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x0013560C File Offset: 0x00134A0C
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
					if (records.Length != 0)
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

		// Token: 0x060031BB RID: 12731 RVA: 0x0013577C File Offset: 0x00134B7C
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.values[recordNo1].CompareTo(this.values[recordNo2]);
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x001357A8 File Offset: 0x00134BA8
		public override int CompareValueTo(int recordNo, object value)
		{
			return this.values[recordNo].CompareTo((SqlBoolean)value);
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x001357CC File Offset: 0x00134BCC
		public override object ConvertValue(object value)
		{
			if (value != null)
			{
				return SqlConvert.ConvertToSqlBoolean(value);
			}
			return this.NullValue;
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x001357F0 File Offset: 0x00134BF0
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x00135818 File Offset: 0x00134C18
		public override object Get(int record)
		{
			return this.values[record];
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x00135838 File Offset: 0x00134C38
		public override bool IsNull(int record)
		{
			return this.values[record].IsNull;
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x00135858 File Offset: 0x00134C58
		public override void Set(int record, object value)
		{
			this.values[record] = SqlConvert.ConvertToSqlBoolean(value);
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x00135878 File Offset: 0x00134C78
		public override void SetCapacity(int capacity)
		{
			SqlBoolean[] destinationArray = new SqlBoolean[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x001358B8 File Offset: 0x00134CB8
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

		// Token: 0x060031C4 RID: 12740 RVA: 0x00135930 File Offset: 0x00134D30
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
			{
				((IXmlSerializable)value).WriteXml(xmlTextWriter);
			}
			return stringWriter.ToString();
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x0013598C File Offset: 0x00134D8C
		protected override object GetEmptyStorage(int recordCount)
		{
			return new SqlBoolean[recordCount];
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x001359A0 File Offset: 0x00134DA0
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			SqlBoolean[] array = (SqlBoolean[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x001359D8 File Offset: 0x00134DD8
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (SqlBoolean[])store;
		}

		// Token: 0x04001DB1 RID: 7601
		private SqlBoolean[] values;
	}
}
