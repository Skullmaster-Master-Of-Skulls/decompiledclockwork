using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000328 RID: 808
	internal sealed class UInt16Storage : DataStorage
	{
		// Token: 0x060032CC RID: 13004 RVA: 0x0013B490 File Offset: 0x0013A890
		public UInt16Storage(DataColumn column) : base(column, typeof(ushort), 0, StorageType.UInt16)
		{
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x0013B4B8 File Offset: 0x0013A8B8
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
						ushort num6 = checked((ushort)(num3 / unchecked((long)num4)));
						return num6;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					ushort num7 = ushort.MaxValue;
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
					ushort num9 = 0;
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
				throw ExprException.Overflow(typeof(ushort));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060032CE RID: 13006 RVA: 0x0013B7DC File Offset: 0x0013ABDC
		public override int Compare(int recordNo1, int recordNo2)
		{
			ushort num = this.values[recordNo1];
			ushort num2 = this.values[recordNo2];
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

		// Token: 0x060032CF RID: 13007 RVA: 0x0013B814 File Offset: 0x0013AC14
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
				ushort num = this.values[recordNo];
				if (num == 0 && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((ushort)value);
			}
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x0013B85C File Offset: 0x0013AC5C
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToUInt16(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x0013B898 File Offset: 0x0013AC98
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060032D2 RID: 13010 RVA: 0x0013B8C0 File Offset: 0x0013ACC0
		public override object Get(int record)
		{
			ushort num = this.values[record];
			if (!num.Equals(0))
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x060032D3 RID: 13011 RVA: 0x0013B8F0 File Offset: 0x0013ACF0
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = 0;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToUInt16(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x0013B93C File Offset: 0x0013AD3C
		public override void SetCapacity(int capacity)
		{
			ushort[] destinationArray = new ushort[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x0013B984 File Offset: 0x0013AD84
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToUInt16(s);
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x0013B99C File Offset: 0x0013AD9C
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((ushort)value);
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x0013B9B4 File Offset: 0x0013ADB4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new ushort[recordCount];
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x0013B9C8 File Offset: 0x0013ADC8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			ushort[] array = (ushort[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x0013B9FC File Offset: 0x0013ADFC
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (ushort[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001DCD RID: 7629
		private const ushort defaultValue = 0;

		// Token: 0x04001DCE RID: 7630
		private ushort[] values;
	}
}
