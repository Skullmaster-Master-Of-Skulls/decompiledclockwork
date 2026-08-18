using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x0200016A RID: 362
	internal sealed class UInt64Storage : DataStorage
	{
		// Token: 0x06001657 RID: 5719 RVA: 0x0024A0C8 File Offset: 0x002494C8
		public UInt64Storage(DataColumn column) : base(column, typeof(ulong), UInt64Storage.defaultValue)
		{
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0024A0F8 File Offset: 0x002494F8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					ulong num = UInt64Storage.defaultValue;
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
					decimal d = UInt64Storage.defaultValue;
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
						ulong num5 = (ulong)(d / num3);
						return num5;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					ulong num6 = ulong.MaxValue;
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
					ulong num8 = 0UL;
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
							num11 += this.values[num13];
							num12 += this.values[num13] * this.values[num13];
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
				throw ExprException.Overflow(typeof(ulong));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0024A428 File Offset: 0x00249828
		public override int Compare(int recordNo1, int recordNo2)
		{
			ulong num = this.values[recordNo1];
			ulong num2 = this.values[recordNo2];
			if (num.Equals(UInt64Storage.defaultValue) || num2.Equals(UInt64Storage.defaultValue))
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

		// Token: 0x0600165A RID: 5722 RVA: 0x0024A488 File Offset: 0x00249888
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
				ulong num = this.values[recordNo];
				if (UInt64Storage.defaultValue == num && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((ulong)value);
			}
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0024A4D8 File Offset: 0x002498D8
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToUInt64(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0024A518 File Offset: 0x00249918
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0024A548 File Offset: 0x00249948
		public override object Get(int record)
		{
			ulong num = this.values[record];
			if (!num.Equals(UInt64Storage.defaultValue))
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0024A588 File Offset: 0x00249988
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = UInt64Storage.defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToUInt64(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0024A5D8 File Offset: 0x002499D8
		public override void SetCapacity(int capacity)
		{
			ulong[] destinationArray = new ulong[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0024A628 File Offset: 0x00249A28
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToUInt64(s);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0024A648 File Offset: 0x00249A48
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((ulong)value);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0024A668 File Offset: 0x00249A68
		protected override object GetEmptyStorage(int recordCount)
		{
			return new ulong[recordCount];
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0024A688 File Offset: 0x00249A88
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			ulong[] array = (ulong[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0024A6C8 File Offset: 0x00249AC8
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (ulong[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000D03 RID: 3331
		private static readonly ulong defaultValue;

		// Token: 0x04000D04 RID: 3332
		private ulong[] values;
	}
}
