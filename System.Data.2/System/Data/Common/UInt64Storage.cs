using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x0200032A RID: 810
	internal sealed class UInt64Storage : DataStorage
	{
		// Token: 0x060032E8 RID: 13032 RVA: 0x0013BFB0 File Offset: 0x0013B3B0
		public UInt64Storage(DataColumn column) : base(column, typeof(ulong), 0UL, StorageType.UInt64)
		{
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x0013BFD8 File Offset: 0x0013B3D8
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

		// Token: 0x060032EA RID: 13034 RVA: 0x0013C2F0 File Offset: 0x0013B6F0
		public override int Compare(int recordNo1, int recordNo2)
		{
			ulong num = this.values[recordNo1];
			ulong num2 = this.values[recordNo2];
			if (num.Equals(0UL) || num2.Equals(0UL))
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

		// Token: 0x060032EB RID: 13035 RVA: 0x0013C340 File Offset: 0x0013B740
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
				if (num == 0UL && !base.HasValue(recordNo))
				{
					return -1;
				}
				return num.CompareTo((ulong)value);
			}
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x0013C388 File Offset: 0x0013B788
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

		// Token: 0x060032ED RID: 13037 RVA: 0x0013C3C4 File Offset: 0x0013B7C4
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x0013C3EC File Offset: 0x0013B7EC
		public override object Get(int record)
		{
			ulong num = this.values[record];
			if (!num.Equals(0UL))
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x0013C41C File Offset: 0x0013B81C
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = 0UL;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToUInt64(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x0013C468 File Offset: 0x0013B868
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

		// Token: 0x060032F1 RID: 13041 RVA: 0x0013C4B0 File Offset: 0x0013B8B0
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToUInt64(s);
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x0013C4C8 File Offset: 0x0013B8C8
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((ulong)value);
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x0013C4E0 File Offset: 0x0013B8E0
		protected override object GetEmptyStorage(int recordCount)
		{
			return new ulong[recordCount];
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x0013C4F4 File Offset: 0x0013B8F4
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			ulong[] array = (ulong[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x0013C528 File Offset: 0x0013B928
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (ulong[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001DD1 RID: 7633
		private const ulong defaultValue = 0UL;

		// Token: 0x04001DD2 RID: 7634
		private ulong[] values;
	}
}
