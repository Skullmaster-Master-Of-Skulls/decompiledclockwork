using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000150 RID: 336
	internal sealed class Int32Storage : DataStorage
	{
		// Token: 0x0600155C RID: 5468 RVA: 0x00243E28 File Offset: 0x00243228
		internal Int32Storage(DataColumn column) : base(column, typeof(int), 0)
		{
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00243E58 File Offset: 0x00243258
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					long num = 0L;
					checked
					{
						foreach (int num2 in records)
						{
							if (base.HasValue(num2))
							{
								num += unchecked((long)this.values[num2]);
								flag = true;
							}
						}
						if (flag)
						{
							return num;
						}
						return this.NullValue;
					}
				}
				case AggregateType.Mean:
				{
					long num3 = 0L;
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (base.HasValue(num5))
						{
							checked
							{
								num3 += unchecked((long)this.values[num5]);
							}
							num4++;
							flag = true;
						}
					}
					if (flag)
					{
						int num6 = checked((int)(num3 / unchecked((long)num4)));
						return num6;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					int num7 = int.MaxValue;
					foreach (int num8 in records)
					{
						if (base.HasValue(num8))
						{
							num7 = Math.Min(this.values[num8], num7);
							flag = true;
						}
					}
					if (flag)
					{
						return num7;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					int num9 = int.MinValue;
					foreach (int num10 in records)
					{
						if (base.HasValue(num10))
						{
							num9 = Math.Max(this.values[num10], num9);
							flag = true;
						}
					}
					if (flag)
					{
						return num9;
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
					int num11 = 0;
					for (int m = 0; m < records.Length; m++)
					{
						if (base.HasValue(records[m]))
						{
							num11++;
						}
					}
					return num11;
				}
				case AggregateType.Var:
				case AggregateType.StDev:
				{
					int num11 = 0;
					double num12 = 0.0;
					double num13 = 0.0;
					foreach (int num14 in records)
					{
						if (base.HasValue(num14))
						{
							num12 += (double)this.values[num14];
							num13 += (double)this.values[num14] * (double)this.values[num14];
							num11++;
						}
					}
					if (num11 <= 1)
					{
						return this.NullValue;
					}
					double num15 = (double)num11 * num13 - num12 * num12;
					double num16 = num15 / (num12 * num12);
					if (num16 < 1E-15 || num15 < 0.0)
					{
						num15 = 0.0;
					}
					else
					{
						num15 /= (double)(num11 * (num11 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(num15);
					}
					return num15;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(int));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00244188 File Offset: 0x00243588
		public override int Compare(int recordNo1, int recordNo2)
		{
			int num = this.values[recordNo1];
			int num2 = this.values[recordNo2];
			if (num == 0 || num2 == 0)
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			if (num < num2)
			{
				return -1;
			}
			if (num <= num2)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x002441C8 File Offset: 0x002435C8
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
				int num = this.values[recordNo];
				if (num == 0 && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((int)value);
			}
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x00244218 File Offset: 0x00243618
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToInt32(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00244258 File Offset: 0x00243658
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00244288 File Offset: 0x00243688
		public override object Get(int record)
		{
			int num = this.values[record];
			if (num != 0)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x002442B8 File Offset: 0x002436B8
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = 0;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToInt32(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00244308 File Offset: 0x00243708
		public override void SetCapacity(int capacity)
		{
			int[] destinationArray = new int[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x00244358 File Offset: 0x00243758
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToInt32(s);
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00244378 File Offset: 0x00243778
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((int)value);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00244398 File Offset: 0x00243798
		protected override object GetEmptyStorage(int recordCount)
		{
			return new int[recordCount];
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x002443B8 File Offset: 0x002437B8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			int[] array = (int[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x002443F8 File Offset: 0x002437F8
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (int[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000CA0 RID: 3232
		private const int defaultValue = 0;

		// Token: 0x04000CA1 RID: 3233
		private int[] values;
	}
}
