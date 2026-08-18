using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x020002D2 RID: 722
	internal sealed class BooleanStorage : DataStorage
	{
		// Token: 0x06002CBB RID: 11451 RVA: 0x001216B4 File Offset: 0x00120AB4
		internal BooleanStorage(DataColumn column) : base(column, typeof(bool), false, StorageType.Boolean)
		{
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x001216DC File Offset: 0x00120ADC
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					bool flag2 = true;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							flag2 = (this.values[num] && flag2);
							flag = true;
						}
					}
					if (flag)
					{
						return flag2;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					bool flag3 = false;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							flag3 = (this.values[num2] || flag3);
							flag = true;
						}
					}
					if (flag)
					{
						return flag3;
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
				throw ExprException.Overflow(typeof(bool));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x00121800 File Offset: 0x00120C00
		public override int Compare(int recordNo1, int recordNo2)
		{
			bool flag = this.values[recordNo1];
			bool flag2 = this.values[recordNo2];
			if (!flag || !flag2)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return flag.CompareTo(flag2);
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x0012183C File Offset: 0x00120C3C
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
				bool flag = this.values[recordNo];
				if (!flag && this.IsNull(recordNo))
				{
					return -1;
				}
				return flag.CompareTo((bool)value);
			}
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x00121884 File Offset: 0x00120C84
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToBoolean(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x001218C0 File Offset: 0x00120CC0
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x001218E8 File Offset: 0x00120CE8
		public override object Get(int record)
		{
			bool flag = this.values[record];
			if (flag)
			{
				return flag;
			}
			return base.GetBits(record);
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x00121910 File Offset: 0x00120D10
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = false;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToBoolean(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x0012195C File Offset: 0x00120D5C
		public override void SetCapacity(int capacity)
		{
			bool[] destinationArray = new bool[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x001219A4 File Offset: 0x00120DA4
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToBoolean(s);
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x001219BC File Offset: 0x00120DBC
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((bool)value);
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x001219D4 File Offset: 0x00120DD4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new bool[recordCount];
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x001219E8 File Offset: 0x00120DE8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			bool[] array = (bool[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x00121A18 File Offset: 0x00120E18
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (bool[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001C2A RID: 7210
		private const bool defaultValue = false;

		// Token: 0x04001C2B RID: 7211
		private bool[] values;
	}
}
