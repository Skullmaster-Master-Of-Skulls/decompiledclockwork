using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x020002D4 RID: 724
	internal sealed class CharStorage : DataStorage
	{
		// Token: 0x06002CD7 RID: 11479 RVA: 0x00121F98 File Offset: 0x00121398
		internal CharStorage(DataColumn column) : base(column, typeof(char), '\0', StorageType.Char)
		{
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x00121FC0 File Offset: 0x001213C0
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
					if (records.Length != 0)
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

		// Token: 0x06002CD9 RID: 11481 RVA: 0x00122104 File Offset: 0x00121504
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

		// Token: 0x06002CDA RID: 11482 RVA: 0x00122140 File Offset: 0x00121540
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

		// Token: 0x06002CDB RID: 11483 RVA: 0x00122188 File Offset: 0x00121588
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

		// Token: 0x06002CDC RID: 11484 RVA: 0x001221C4 File Offset: 0x001215C4
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x001221EC File Offset: 0x001215EC
		public override object Get(int record)
		{
			char c = this.values[record];
			if (c != '\0')
			{
				return c;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x00122214 File Offset: 0x00121614
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

		// Token: 0x06002CDF RID: 11487 RVA: 0x0012228C File Offset: 0x0012168C
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

		// Token: 0x06002CE0 RID: 11488 RVA: 0x001222D4 File Offset: 0x001216D4
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToChar(s);
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x001222EC File Offset: 0x001216EC
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((char)value);
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x00122304 File Offset: 0x00121704
		protected override object GetEmptyStorage(int recordCount)
		{
			return new char[recordCount];
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x00122318 File Offset: 0x00121718
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			char[] array = (char[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x00122348 File Offset: 0x00121748
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (char[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001C2E RID: 7214
		private const char defaultValue = '\0';

		// Token: 0x04001C2F RID: 7215
		private char[] values;
	}
}
