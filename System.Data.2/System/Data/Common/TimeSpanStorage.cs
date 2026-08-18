using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000327 RID: 807
	internal sealed class TimeSpanStorage : DataStorage
	{
		// Token: 0x060032BC RID: 12988 RVA: 0x0013ADBC File Offset: 0x0013A1BC
		public TimeSpanStorage(DataColumn column) : base(column, typeof(TimeSpan), TimeSpanStorage.defaultValue, StorageType.TimeSpan)
		{
		}

		// Token: 0x060032BD RID: 12989 RVA: 0x0013ADE8 File Offset: 0x0013A1E8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					decimal num = 0m;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							num += this.values[num2].Ticks;
							flag = true;
						}
					}
					if (flag)
					{
						return TimeSpan.FromTicks((long)Math.Round(num));
					}
					return null;
				}
				case AggregateType.Mean:
				{
					decimal d = 0m;
					int num3 = 0;
					foreach (int num4 in records)
					{
						if (!this.IsNull(num4))
						{
							d += this.values[num4].Ticks;
							num3++;
						}
					}
					if (num3 > 0)
					{
						return TimeSpan.FromTicks((long)Math.Round(d / num3));
					}
					return null;
				}
				case AggregateType.Min:
				{
					TimeSpan timeSpan = TimeSpan.MaxValue;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							timeSpan = ((TimeSpan.Compare(this.values[num5], timeSpan) < 0) ? this.values[num5] : timeSpan);
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
					foreach (int num6 in records)
					{
						if (!this.IsNull(num6))
						{
							timeSpan2 = ((TimeSpan.Compare(this.values[num6], timeSpan2) >= 0) ? this.values[num6] : timeSpan2);
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
					if (records.Length != 0)
					{
						return this.values[records[0]];
					}
					return null;
				case AggregateType.Count:
					return base.Aggregate(records, kind);
				case AggregateType.StDev:
				{
					int num7 = 0;
					decimal d2 = 0m;
					foreach (int num8 in records)
					{
						if (!this.IsNull(num8))
						{
							d2 += this.values[num8].Ticks;
							num7++;
						}
					}
					if (num7 > 1)
					{
						double num9 = 0.0;
						decimal d3 = d2 / num7;
						foreach (int num10 in records)
						{
							if (!this.IsNull(num10))
							{
								double num11 = (double)(this.values[num10].Ticks - d3);
								num9 += num11 * num11;
							}
						}
						ulong num12 = (ulong)Math.Round(Math.Sqrt(num9 / (double)(num7 - 1)));
						if (num12 > 9223372036854775807UL)
						{
							num12 = 9223372036854775807UL;
						}
						return TimeSpan.FromTicks((long)num12);
					}
					return null;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(TimeSpan));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060032BE RID: 12990 RVA: 0x0013B184 File Offset: 0x0013A584
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

		// Token: 0x060032BF RID: 12991 RVA: 0x0013B1DC File Offset: 0x0013A5DC
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

		// Token: 0x060032C0 RID: 12992 RVA: 0x0013B234 File Offset: 0x0013A634
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

		// Token: 0x060032C1 RID: 12993 RVA: 0x0013B2AC File Offset: 0x0013A6AC
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

		// Token: 0x060032C2 RID: 12994 RVA: 0x0013B2E0 File Offset: 0x0013A6E0
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060032C3 RID: 12995 RVA: 0x0013B310 File Offset: 0x0013A710
		public override object Get(int record)
		{
			TimeSpan timeSpan = this.values[record];
			if (timeSpan != TimeSpanStorage.defaultValue)
			{
				return timeSpan;
			}
			return base.GetBits(record);
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x0013B348 File Offset: 0x0013A748
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

		// Token: 0x060032C5 RID: 12997 RVA: 0x0013B394 File Offset: 0x0013A794
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

		// Token: 0x060032C6 RID: 12998 RVA: 0x0013B3DC File Offset: 0x0013A7DC
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToTimeSpan(s);
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x0013B3F4 File Offset: 0x0013A7F4
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((TimeSpan)value);
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x0013B40C File Offset: 0x0013A80C
		protected override object GetEmptyStorage(int recordCount)
		{
			return new TimeSpan[recordCount];
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x0013B420 File Offset: 0x0013A820
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			TimeSpan[] array = (TimeSpan[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x0013B458 File Offset: 0x0013A858
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (TimeSpan[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001DCB RID: 7627
		private static readonly TimeSpan defaultValue = TimeSpan.Zero;

		// Token: 0x04001DCC RID: 7628
		private TimeSpan[] values;
	}
}
