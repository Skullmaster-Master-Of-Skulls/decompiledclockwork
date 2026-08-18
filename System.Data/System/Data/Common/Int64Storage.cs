using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000151 RID: 337
	internal sealed class Int64Storage : DataStorage
	{
		// Token: 0x0600156A RID: 5482 RVA: 0x00244418 File Offset: 0x00243818
		internal Int64Storage(DataColumn column) : base(column, typeof(long), 0L)
		{
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00244448 File Offset: 0x00243848
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
					if (records.Length > 0)
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

		// Token: 0x0600156C RID: 5484 RVA: 0x00244778 File Offset: 0x00243B78
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

		// Token: 0x0600156D RID: 5485 RVA: 0x002447C8 File Offset: 0x00243BC8
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
				if (0L == num && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((long)value);
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00244818 File Offset: 0x00243C18
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

		// Token: 0x0600156F RID: 5487 RVA: 0x00244858 File Offset: 0x00243C58
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x00244888 File Offset: 0x00243C88
		public override object Get(int record)
		{
			long num = this.values[record];
			if (num != 0L)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x002448B8 File Offset: 0x00243CB8
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

		// Token: 0x06001572 RID: 5490 RVA: 0x00244908 File Offset: 0x00243D08
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

		// Token: 0x06001573 RID: 5491 RVA: 0x00244958 File Offset: 0x00243D58
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToInt64(s);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00244978 File Offset: 0x00243D78
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((long)value);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00244998 File Offset: 0x00243D98
		protected override object GetEmptyStorage(int recordCount)
		{
			return new long[recordCount];
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x002449B8 File Offset: 0x00243DB8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			long[] array = (long[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x002449F8 File Offset: 0x00243DF8
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (long[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000CA2 RID: 3234
		private const long defaultValue = 0L;

		// Token: 0x04000CA3 RID: 3235
		private long[] values;
	}
}
