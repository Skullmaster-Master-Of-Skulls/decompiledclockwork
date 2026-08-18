using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x0200030F RID: 783
	internal sealed class SByteStorage : DataStorage
	{
		// Token: 0x06003179 RID: 12665 RVA: 0x00132FD0 File Offset: 0x001323D0
		public SByteStorage(DataColumn column) : base(column, typeof(sbyte), 0, StorageType.SByte)
		{
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x00132FF8 File Offset: 0x001323F8
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
							if (!this.IsNull(num2))
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
						if (!this.IsNull(num5))
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
						sbyte b = checked((sbyte)(num3 / unchecked((long)num4)));
						return b;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					sbyte b2 = sbyte.MaxValue;
					foreach (int num6 in records)
					{
						if (!this.IsNull(num6))
						{
							b2 = Math.Min(this.values[num6], b2);
							flag = true;
						}
					}
					if (flag)
					{
						return b2;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					sbyte b3 = sbyte.MinValue;
					foreach (int num7 in records)
					{
						if (!this.IsNull(num7))
						{
							b3 = Math.Max(this.values[num7], b3);
							flag = true;
						}
					}
					if (flag)
					{
						return b3;
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
					int num8 = 0;
					double num9 = 0.0;
					double num10 = 0.0;
					foreach (int num11 in records)
					{
						if (!this.IsNull(num11))
						{
							num9 += (double)this.values[num11];
							num10 += (double)this.values[num11] * (double)this.values[num11];
							num8++;
						}
					}
					if (num8 <= 1)
					{
						return this.NullValue;
					}
					double num12 = (double)num8 * num10 - num9 * num9;
					double num13 = num12 / (num9 * num9);
					if (num13 < 1E-15 || num12 < 0.0)
					{
						num12 = 0.0;
					}
					else
					{
						num12 /= (double)(num8 * (num8 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(num12);
					}
					return num12;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(sbyte));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x001332F8 File Offset: 0x001326F8
		public override int Compare(int recordNo1, int recordNo2)
		{
			sbyte b = this.values[recordNo1];
			sbyte value = this.values[recordNo2];
			if (b.Equals(0) || value.Equals(0))
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return b.CompareTo(value);
		}

		// Token: 0x0600317C RID: 12668 RVA: 0x00133344 File Offset: 0x00132744
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
				sbyte b = this.values[recordNo];
				if (b == 0 && this.IsNull(recordNo))
				{
					return -1;
				}
				return b.CompareTo((sbyte)value);
			}
		}

		// Token: 0x0600317D RID: 12669 RVA: 0x0013338C File Offset: 0x0013278C
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToSByte(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x001333C8 File Offset: 0x001327C8
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x001333F0 File Offset: 0x001327F0
		public override object Get(int record)
		{
			sbyte b = this.values[record];
			if (!b.Equals(0))
			{
				return b;
			}
			return base.GetBits(record);
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x00133420 File Offset: 0x00132820
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = 0;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToSByte(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x0013346C File Offset: 0x0013286C
		public override void SetCapacity(int capacity)
		{
			sbyte[] destinationArray = new sbyte[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x06003182 RID: 12674 RVA: 0x001334B4 File Offset: 0x001328B4
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToSByte(s);
		}

		// Token: 0x06003183 RID: 12675 RVA: 0x001334CC File Offset: 0x001328CC
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((sbyte)value);
		}

		// Token: 0x06003184 RID: 12676 RVA: 0x001334E4 File Offset: 0x001328E4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new sbyte[recordCount];
		}

		// Token: 0x06003185 RID: 12677 RVA: 0x001334F8 File Offset: 0x001328F8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			sbyte[] array = (sbyte[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06003186 RID: 12678 RVA: 0x00133528 File Offset: 0x00132928
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (sbyte[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001D8D RID: 7565
		private const sbyte defaultValue = 0;

		// Token: 0x04001D8E RID: 7566
		private sbyte[] values;
	}
}
