using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000168 RID: 360
	internal sealed class UInt16Storage : DataStorage
	{
		// Token: 0x0600163B RID: 5691 RVA: 0x00249498 File Offset: 0x00248898
		public UInt16Storage(DataColumn column) : base(column, typeof(ushort), UInt16Storage.defaultValue)
		{
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x002494C8 File Offset: 0x002488C8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					ulong num = (ulong)UInt16Storage.defaultValue;
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
					long num3 = (long)((ulong)UInt16Storage.defaultValue);
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
				throw ExprException.Overflow(typeof(ushort));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00249808 File Offset: 0x00248C08
		public override int Compare(int recordNo1, int recordNo2)
		{
			ushort num = this.values[recordNo1];
			ushort num2 = this.values[recordNo2];
			if (num == UInt16Storage.defaultValue || num2 == UInt16Storage.defaultValue)
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return (int)(num - num2);
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00249848 File Offset: 0x00248C48
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
				if (UInt16Storage.defaultValue == num && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((ushort)value);
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x00249898 File Offset: 0x00248C98
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

		// Token: 0x06001640 RID: 5696 RVA: 0x002498D8 File Offset: 0x00248CD8
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00249908 File Offset: 0x00248D08
		public override object Get(int record)
		{
			ushort num = this.values[record];
			if (!num.Equals(UInt16Storage.defaultValue))
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00249948 File Offset: 0x00248D48
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = UInt16Storage.defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToUInt16(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00249998 File Offset: 0x00248D98
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

		// Token: 0x06001644 RID: 5700 RVA: 0x002499E8 File Offset: 0x00248DE8
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToUInt16(s);
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x00249A08 File Offset: 0x00248E08
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((ushort)value);
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x00249A28 File Offset: 0x00248E28
		protected override object GetEmptyStorage(int recordCount)
		{
			return new ushort[recordCount];
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00249A48 File Offset: 0x00248E48
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			ushort[] array = (ushort[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00249A88 File Offset: 0x00248E88
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (ushort[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000CFF RID: 3327
		private static readonly ushort defaultValue;

		// Token: 0x04000D00 RID: 3328
		private ushort[] values;
	}
}
