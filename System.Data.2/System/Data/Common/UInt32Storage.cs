using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000329 RID: 809
	internal sealed class UInt32Storage : DataStorage
	{
		// Token: 0x060032DA RID: 13018 RVA: 0x0013BA1C File Offset: 0x0013AE1C
		public UInt32Storage(DataColumn column) : base(column, typeof(uint), 0U, StorageType.UInt32)
		{
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x0013BA44 File Offset: 0x0013AE44
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					ulong num = 0UL;
					checked
					{
						foreach (int num2 in records)
						{
							if (base.HasValue(num2))
							{
								num += unchecked((ulong)this.values[num2]);
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
								num3 += (long)(unchecked((ulong)this.values[num5]));
							}
							num4++;
							flag = true;
						}
					}
					if (flag)
					{
						uint num6 = checked((uint)(num3 / unchecked((long)num4)));
						return num6;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					uint num7 = uint.MaxValue;
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
					uint num9 = 0U;
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
					if (records.Length != 0)
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
							num12 += this.values[num14];
							num13 += this.values[num14] * this.values[num14];
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
				throw ExprException.Overflow(typeof(uint));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x0013BD68 File Offset: 0x0013B168
		public override int Compare(int recordNo1, int recordNo2)
		{
			uint num = this.values[recordNo1];
			uint num2 = this.values[recordNo2];
			if (num == 0U || num2 == 0U)
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

		// Token: 0x060032DD RID: 13021 RVA: 0x0013BDA8 File Offset: 0x0013B1A8
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
				uint num = this.values[recordNo];
				if (num == 0U && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((uint)value);
			}
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x0013BDF0 File Offset: 0x0013B1F0
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToUInt32(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x0013BE2C File Offset: 0x0013B22C
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x0013BE54 File Offset: 0x0013B254
		public override object Get(int record)
		{
			uint num = this.values[record];
			if (!num.Equals(0U))
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x0013BE84 File Offset: 0x0013B284
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = 0U;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToUInt32(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x0013BED0 File Offset: 0x0013B2D0
		public override void SetCapacity(int capacity)
		{
			uint[] destinationArray = new uint[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x0013BF18 File Offset: 0x0013B318
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToUInt32(s);
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x0013BF30 File Offset: 0x0013B330
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((uint)value);
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x0013BF48 File Offset: 0x0013B348
		protected override object GetEmptyStorage(int recordCount)
		{
			return new uint[recordCount];
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x0013BF5C File Offset: 0x0013B35C
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			uint[] array = (uint[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x0013BF90 File Offset: 0x0013B390
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (uint[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001DCF RID: 7631
		private const uint defaultValue = 0U;

		// Token: 0x04001DD0 RID: 7632
		private uint[] values;
	}
}
