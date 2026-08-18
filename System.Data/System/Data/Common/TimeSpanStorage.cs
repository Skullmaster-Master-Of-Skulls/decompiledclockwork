using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000167 RID: 359
	internal sealed class TimeSpanStorage : DataStorage
	{
		// Token: 0x0600162B RID: 5675 RVA: 0x00248F48 File Offset: 0x00248348
		public TimeSpanStorage(DataColumn column) : base(column, typeof(TimeSpan), TimeSpanStorage.defaultValue)
		{
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00248F78 File Offset: 0x00248378
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					TimeSpan timeSpan = TimeSpan.MaxValue;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							timeSpan = ((TimeSpan.Compare(this.values[num], timeSpan) < 0) ? this.values[num] : timeSpan);
							flag = true;
						}
					}
					if (flag)
					{
						return timeSpan;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					TimeSpan timeSpan2 = TimeSpan.MinValue;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							timeSpan2 = ((TimeSpan.Compare(this.values[num2], timeSpan2) >= 0) ? this.values[num2] : timeSpan2);
							flag = true;
						}
					}
					if (flag)
					{
						return timeSpan2;
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
				throw ExprException.Overflow(typeof(TimeSpan));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x00249108 File Offset: 0x00248508
		public override int Compare(int recordNo1, int recordNo2)
		{
			TimeSpan t = this.values[recordNo1];
			TimeSpan timeSpan = this.values[recordNo2];
			if (t == TimeSpanStorage.defaultValue || timeSpan == TimeSpanStorage.defaultValue)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return TimeSpan.Compare(t, timeSpan);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x00249168 File Offset: 0x00248568
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
				TimeSpan t = this.values[recordNo];
				if (TimeSpanStorage.defaultValue == t && this.IsNull(recordNo))
				{
					return -1;
				}
				return t.CompareTo((TimeSpan)value);
			}
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x002491C8 File Offset: 0x002485C8
		private static TimeSpan ConvertToTimeSpan(object value)
		{
			Type type = value.GetType();
			if (type == typeof(string))
			{
				return TimeSpan.Parse((string)value);
			}
			if (type == typeof(int))
			{
				return new TimeSpan((long)((int)value));
			}
			if (type == typeof(long))
			{
				return new TimeSpan((long)value);
			}
			return (TimeSpan)value;
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x00249238 File Offset: 0x00248638
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = TimeSpanStorage.ConvertToTimeSpan(value);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x00249278 File Offset: 0x00248678
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x002492B8 File Offset: 0x002486B8
		public override object Get(int record)
		{
			TimeSpan timeSpan = this.values[record];
			if (timeSpan != TimeSpanStorage.defaultValue)
			{
				return timeSpan;
			}
			return base.GetBits(record);
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x002492F8 File Offset: 0x002486F8
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = TimeSpanStorage.defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = TimeSpanStorage.ConvertToTimeSpan(value);
			base.SetNullBit(record, false);
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00249358 File Offset: 0x00248758
		public override void SetCapacity(int capacity)
		{
			TimeSpan[] destinationArray = new TimeSpan[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x002493A8 File Offset: 0x002487A8
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToTimeSpan(s);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x002493C8 File Offset: 0x002487C8
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((TimeSpan)value);
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x002493E8 File Offset: 0x002487E8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new TimeSpan[recordCount];
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00249408 File Offset: 0x00248808
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			TimeSpan[] array = (TimeSpan[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00249458 File Offset: 0x00248858
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (TimeSpan[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000CFD RID: 3325
		private static readonly TimeSpan defaultValue = TimeSpan.Zero;

		// Token: 0x04000CFE RID: 3326
		private TimeSpan[] values;
	}
}
