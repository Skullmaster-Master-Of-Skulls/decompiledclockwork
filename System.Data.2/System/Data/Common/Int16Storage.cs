using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000304 RID: 772
	internal sealed class Int16Storage : DataStorage
	{
		// Token: 0x060030E6 RID: 12518 RVA: 0x00130584 File Offset: 0x0012F984
		internal Int16Storage(DataColumn column) : base(column, typeof(short), 0, StorageType.Int16)
		{
		}

		// Token: 0x060030E7 RID: 12519 RVA: 0x001305AC File Offset: 0x0012F9AC
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
						short num6 = checked((short)(num3 / unchecked((long)num4)));
						return num6;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					short num7 = short.MaxValue;
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
					short num9 = short.MinValue;
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
				throw ExprException.Overflow(typeof(short));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060030E8 RID: 12520 RVA: 0x001308D4 File Offset: 0x0012FCD4
		public override int Compare(int recordNo1, int recordNo2)
		{
			short num = this.values[recordNo1];
			short num2 = this.values[recordNo2];
			if (num == 0 || num2 == 0)
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return (int)(num - num2);
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x0013090C File Offset: 0x0012FD0C
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
				short num = this.values[recordNo];
				if (num == 0 && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((short)value);
			}
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x00130954 File Offset: 0x0012FD54
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToInt16(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x00130990 File Offset: 0x0012FD90
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x001309B8 File Offset: 0x0012FDB8
		public override object Get(int record)
		{
			short num = this.values[record];
			if (num != 0)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x001309E0 File Offset: 0x0012FDE0
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = 0;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToInt16(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x00130A2C File Offset: 0x0012FE2C
		public override void SetCapacity(int capacity)
		{
			short[] destinationArray = new short[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x00130A74 File Offset: 0x0012FE74
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToInt16(s);
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x00130A8C File Offset: 0x0012FE8C
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((short)value);
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x00130AA4 File Offset: 0x0012FEA4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new short[recordCount];
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x00130AB8 File Offset: 0x0012FEB8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			short[] array = (short[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x060030F3 RID: 12531 RVA: 0x00130AEC File Offset: 0x0012FEEC
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (short[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001D66 RID: 7526
		private const short defaultValue = 0;

		// Token: 0x04001D67 RID: 7527
		private short[] values;
	}
}
