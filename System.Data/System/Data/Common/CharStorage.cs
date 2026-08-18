using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000116 RID: 278
	internal sealed class CharStorage : DataStorage
	{
		// Token: 0x06001198 RID: 4504 RVA: 0x00234558 File Offset: 0x00233958
		internal CharStorage(DataColumn column) : base(column, typeof(char), '\0')
		{
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00234588 File Offset: 0x00233988
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					char c = char.MaxValue;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							c = ((this.values[num] < c) ? this.values[num] : c);
							flag = true;
						}
					}
					if (flag)
					{
						return c;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					char c2 = '\0';
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							c2 = ((this.values[num2] > c2) ? this.values[num2] : c2);
							flag = true;
						}
					}
					if (flag)
					{
						return c2;
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
					return base.Aggregate(records, kind);
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(char));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x002346D8 File Offset: 0x00233AD8
		public override int Compare(int recordNo1, int recordNo2)
		{
			char c = this.values[recordNo1];
			char c2 = this.values[recordNo2];
			if (c == '\0' || c2 == '\0')
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return c.CompareTo(c2);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00234718 File Offset: 0x00233B18
		public override int CompareValueTo(int recordNo, object value)
		{
			if (this.NullValue == value)
			{
				if (this.IsNull(recordNo))
				{
					return 0;
				}
				return 1;
			}
			else
			{
				char c = this.values[recordNo];
				if (c == '\0' && this.IsNull(recordNo))
				{
					return -1;
				}
				return c.CompareTo((char)value);
			}
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00234768 File Offset: 0x00233B68
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToChar(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x002347A8 File Offset: 0x00233BA8
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x002347D8 File Offset: 0x00233BD8
		public override object Get(int record)
		{
			char c = this.values[record];
			if (c != '\0')
			{
				return c;
			}
			return base.GetBits(record);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00234808 File Offset: 0x00233C08
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = '\0';
				base.SetNullBit(record, true);
				return;
			}
			char c = ((IConvertible)value).ToChar(base.FormatProvider);
			if ((c >= '\ud800' && c <= '\udfff') || (c < '!' && (c == '\t' || c == '\n' || c == '\r')))
			{
				throw ExceptionBuilder.ProblematicChars(c);
			}
			this.values[record] = c;
			base.SetNullBit(record, false);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00234888 File Offset: 0x00233C88
		public override void SetCapacity(int capacity)
		{
			char[] destinationArray = new char[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x002348D8 File Offset: 0x00233CD8
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToChar(s);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x002348F8 File Offset: 0x00233CF8
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((char)value);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x00234918 File Offset: 0x00233D18
		protected override object GetEmptyStorage(int recordCount)
		{
			return new char[recordCount];
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00234938 File Offset: 0x00233D38
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			char[] array = (char[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x00234968 File Offset: 0x00233D68
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (char[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000B7A RID: 2938
		private const char defaultValue = '\0';

		// Token: 0x04000B7B RID: 2939
		private char[] values;
	}
}
