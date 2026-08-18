using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000332 RID: 818
	internal sealed class DateTimeOffsetStorage : DataStorage
	{
		// Token: 0x06003366 RID: 13158 RVA: 0x0013D3A0 File Offset: 0x0013C7A0
		internal DateTimeOffsetStorage(DataColumn column) : base(column, typeof(DateTimeOffset), DateTimeOffsetStorage.defaultValue, StorageType.DateTimeOffset)
		{
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x0013D3CC File Offset: 0x0013C7CC
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					DateTimeOffset dateTimeOffset = DateTimeOffset.MaxValue;
					foreach (int num in records)
					{
						if (base.HasValue(num))
						{
							dateTimeOffset = ((DateTimeOffset.Compare(this.values[num], dateTimeOffset) < 0) ? this.values[num] : dateTimeOffset);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTimeOffset;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					DateTimeOffset dateTimeOffset2 = DateTimeOffset.MinValue;
					foreach (int num2 in records)
					{
						if (base.HasValue(num2))
						{
							dateTimeOffset2 = ((DateTimeOffset.Compare(this.values[num2], dateTimeOffset2) >= 0) ? this.values[num2] : dateTimeOffset2);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTimeOffset2;
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
						if (base.HasValue(records[k]))
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
				throw ExprException.Overflow(typeof(DateTimeOffset));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x0013D55C File Offset: 0x0013C95C
		public override int Compare(int recordNo1, int recordNo2)
		{
			DateTimeOffset dateTimeOffset = this.values[recordNo1];
			DateTimeOffset dateTimeOffset2 = this.values[recordNo2];
			if (dateTimeOffset == DateTimeOffsetStorage.defaultValue || dateTimeOffset2 == DateTimeOffsetStorage.defaultValue)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return DateTimeOffset.Compare(dateTimeOffset, dateTimeOffset2);
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x0013D5B4 File Offset: 0x0013C9B4
		public override int CompareValueTo(int recordNo, object value)
		{
			if (this.NullValue == value)
			{
				if (!base.HasValue(recordNo))
				{
					return 0;
				}
				return 1;
			}
			else
			{
				DateTimeOffset dateTimeOffset = this.values[recordNo];
				if (DateTimeOffsetStorage.defaultValue == dateTimeOffset && !base.HasValue(recordNo))
				{
					return -1;
				}
				return DateTimeOffset.Compare(dateTimeOffset, (DateTimeOffset)value);
			}
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x0013D608 File Offset: 0x0013CA08
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = (DateTimeOffset)value;
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x0013D63C File Offset: 0x0013CA3C
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x0013D66C File Offset: 0x0013CA6C
		public override object Get(int record)
		{
			DateTimeOffset dateTimeOffset = this.values[record];
			if (dateTimeOffset != DateTimeOffsetStorage.defaultValue || base.HasValue(record))
			{
				return dateTimeOffset;
			}
			return this.NullValue;
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x0013D6AC File Offset: 0x0013CAAC
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = DateTimeOffsetStorage.defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = (DateTimeOffset)value;
			base.SetNullBit(record, false);
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x0013D6F8 File Offset: 0x0013CAF8
		public override void SetCapacity(int capacity)
		{
			DateTimeOffset[] destinationArray = new DateTimeOffset[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x0013D740 File Offset: 0x0013CB40
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToDateTimeOffset(s);
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x0013D758 File Offset: 0x0013CB58
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((DateTimeOffset)value);
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x0013D770 File Offset: 0x0013CB70
		protected override object GetEmptyStorage(int recordCount)
		{
			return new DateTimeOffset[recordCount];
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x0013D784 File Offset: 0x0013CB84
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			DateTimeOffset[] array = (DateTimeOffset[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x0013D7C0 File Offset: 0x0013CBC0
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (DateTimeOffset[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001E10 RID: 7696
		private static readonly DateTimeOffset defaultValue = DateTimeOffset.MinValue;

		// Token: 0x04001E11 RID: 7697
		private DateTimeOffset[] values;
	}
}
