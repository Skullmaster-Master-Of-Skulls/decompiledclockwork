using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000306 RID: 774
	internal sealed class Int64Storage : DataStorage
	{
		// Token: 0x06003102 RID: 12546 RVA: 0x0013109C File Offset: 0x0013049C
		internal Int64Storage(DataColumn column) : base(column, typeof(long), 0L, StorageType.Int64)
		{
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x001310C4 File Offset: 0x001304C4
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
								num += this.values[num2];
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
					decimal d = 0m;
					int num3 = 0;
					foreach (int num4 in records)
					{
						if (base.HasValue(num4))
						{
							d += this.values[num4];
							num3++;
							flag = true;
						}
					}
					if (flag)
					{
						long num5 = (long)(d / num3);
						return num5;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					long num6 = long.MaxValue;
					foreach (int num7 in records)
					{
						if (base.HasValue(num7))
						{
							num6 = Math.Min(this.values[num7], num6);
							flag = true;
						}
					}
					if (flag)
					{
						return num6;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					long num8 = long.MinValue;
					foreach (int num9 in records)
					{
						if (base.HasValue(num9))
						{
							num8 = Math.Max(this.values[num9], num8);
							flag = true;
						}
					}
					if (flag)
					{
						return num8;
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
				case AggregateType.Var:
				case AggregateType.StDev:
				{
					int num10 = 0;
					double num11 = 0.0;
					double num12 = 0.0;
					foreach (int num13 in records)
					{
						if (base.HasValue(num13))
						{
							num11 += (double)this.values[num13];
							num12 += (double)this.values[num13] * (double)this.values[num13];
							num10++;
						}
					}
					if (num10 <= 1)
					{
						return this.NullValue;
					}
					double num14 = (double)num10 * num12 - num11 * num11;
					double num15 = num14 / (num11 * num11);
					if (num15 < 1E-15 || num14 < 0.0)
					{
						num14 = 0.0;
					}
					else
					{
						num14 /= (double)(num10 * (num10 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(num14);
					}
					return num14;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(long));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06003104 RID: 12548 RVA: 0x001313E8 File Offset: 0x001307E8
		public override int Compare(int recordNo1, int recordNo2)
		{
			long num = this.values[recordNo1];
			long num2 = this.values[recordNo2];
			if (num == 0L || num2 == 0L)
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

		// Token: 0x06003105 RID: 12549 RVA: 0x00131428 File Offset: 0x00130828
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
				long num = this.values[recordNo];
				if (num == 0L && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((long)value);
			}
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x00131470 File Offset: 0x00130870
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToInt64(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x001314AC File Offset: 0x001308AC
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x001314D4 File Offset: 0x001308D4
		public override object Get(int record)
		{
			long num = this.values[record];
			if (num != 0L)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x001314FC File Offset: 0x001308FC
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = 0L;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToInt64(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x00131548 File Offset: 0x00130948
		public override void SetCapacity(int capacity)
		{
			long[] destinationArray = new long[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x00131590 File Offset: 0x00130990
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToInt64(s);
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x001315A8 File Offset: 0x001309A8
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((long)value);
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x001315C0 File Offset: 0x001309C0
		protected override object GetEmptyStorage(int recordCount)
		{
			return new long[recordCount];
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x001315D4 File Offset: 0x001309D4
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			long[] array = (long[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x0600310F RID: 12559 RVA: 0x00131608 File Offset: 0x00130A08
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (long[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001D6A RID: 7530
		private const long defaultValue = 0L;

		// Token: 0x04001D6B RID: 7531
		private long[] values;
	}
}
