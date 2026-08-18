using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000169 RID: 361
	internal sealed class UInt32Storage : DataStorage
	{
		// Token: 0x06001649 RID: 5705 RVA: 0x00249AA8 File Offset: 0x00248EA8
		public UInt32Storage(DataColumn column) : base(column, typeof(uint), UInt32Storage.defaultValue)
		{
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00249AD8 File Offset: 0x00248ED8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					ulong num = (ulong)UInt32Storage.defaultValue;
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
					long num3 = (long)((ulong)UInt32Storage.defaultValue);
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (base.HasValue(num5))
						{
							checked
							{
								num3 += (long)this.values[num5];
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

		// Token: 0x0600164B RID: 5707 RVA: 0x00249E18 File Offset: 0x00249218
		public override int Compare(int recordNo1, int recordNo2)
		{
			uint num = this.values[recordNo1];
			uint num2 = this.values[recordNo2];
			if (num == UInt32Storage.defaultValue || num2 == UInt32Storage.defaultValue)
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

		// Token: 0x0600164C RID: 5708 RVA: 0x00249E68 File Offset: 0x00249268
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
				if (UInt32Storage.defaultValue == num && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((uint)value);
			}
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00249EB8 File Offset: 0x002492B8
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

		// Token: 0x0600164E RID: 5710 RVA: 0x00249EF8 File Offset: 0x002492F8
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x00249F28 File Offset: 0x00249328
		public override object Get(int record)
		{
			uint num = this.values[record];
			if (!num.Equals(UInt32Storage.defaultValue))
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00249F68 File Offset: 0x00249368
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = UInt32Storage.defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToUInt32(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00249FB8 File Offset: 0x002493B8
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

		// Token: 0x06001652 RID: 5714 RVA: 0x0024A008 File Offset: 0x00249408
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToUInt32(s);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0024A028 File Offset: 0x00249428
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((uint)value);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0024A048 File Offset: 0x00249448
		protected override object GetEmptyStorage(int recordCount)
		{
			return new uint[recordCount];
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0024A068 File Offset: 0x00249468
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			uint[] array = (uint[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0024A0A8 File Offset: 0x002494A8
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (uint[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000D01 RID: 3329
		private static readonly uint defaultValue;

		// Token: 0x04000D02 RID: 3330
		private uint[] values;
	}
}
