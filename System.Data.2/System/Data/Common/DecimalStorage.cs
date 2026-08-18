using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x020002FF RID: 767
	internal sealed class DecimalStorage : DataStorage
	{
		// Token: 0x060030C4 RID: 12484 RVA: 0x0012F8B0 File Offset: 0x0012ECB0
		internal DecimalStorage(DataColumn column) : base(column, typeof(decimal), DecimalStorage.defaultValue, StorageType.Decimal)
		{
		}

		// Token: 0x060030C5 RID: 12485 RVA: 0x0012F8DC File Offset: 0x0012ECDC
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					decimal num = DecimalStorage.defaultValue;
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
				case AggregateType.Mean:
				{
					decimal d = DecimalStorage.defaultValue;
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
						decimal num5 = d / num3;
						return num5;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					decimal num6 = decimal.MaxValue;
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
					decimal num8 = decimal.MinValue;
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
					double num11 = (double)DecimalStorage.defaultValue;
					double num12 = (double)DecimalStorage.defaultValue;
					double num13 = (double)DecimalStorage.defaultValue;
					double num14 = (double)DecimalStorage.defaultValue;
					foreach (int num15 in records)
					{
						if (base.HasValue(num15))
						{
							num13 += (double)this.values[num15];
							num14 += (double)this.values[num15] * (double)this.values[num15];
							num10++;
						}
					}
					if (num10 <= 1)
					{
						return this.NullValue;
					}
					num11 = (double)num10 * num14 - num13 * num13;
					num12 = num11 / (num13 * num13);
					if (num12 < 1E-15 || num11 < 0.0)
					{
						num11 = 0.0;
					}
					else
					{
						num11 /= (double)(num10 * (num10 - 1));
					}
					if (kind == AggregateType.StDev)
					{
						return Math.Sqrt(num11);
					}
					return num11;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(decimal));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060030C6 RID: 12486 RVA: 0x0012FC34 File Offset: 0x0012F034
		public override int Compare(int recordNo1, int recordNo2)
		{
			decimal d = this.values[recordNo1];
			decimal num = this.values[recordNo2];
			if (d == DecimalStorage.defaultValue || num == DecimalStorage.defaultValue)
			{
				int num2 = base.CompareBits(recordNo1, recordNo2);
				if (num2 != 0)
				{
					return num2;
				}
			}
			return decimal.Compare(d, num);
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x0012FC8C File Offset: 0x0012F08C
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
				decimal num = this.values[recordNo];
				if (DecimalStorage.defaultValue == num && !base.HasValue(recordNo))
				{
					return -1;
				}
				return decimal.Compare(num, (decimal)value);
			}
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x0012FCE0 File Offset: 0x0012F0E0
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToDecimal(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x0012FD1C File Offset: 0x0012F11C
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x0012FD4C File Offset: 0x0012F14C
		public override object Get(int record)
		{
			if (!base.HasValue(record))
			{
				return this.NullValue;
			}
			return this.values[record];
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x0012FD7C File Offset: 0x0012F17C
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = DecimalStorage.defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToDecimal(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x0012FDD4 File Offset: 0x0012F1D4
		public override void SetCapacity(int capacity)
		{
			decimal[] destinationArray = new decimal[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x0012FE1C File Offset: 0x0012F21C
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToDecimal(s);
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x0012FE34 File Offset: 0x0012F234
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((decimal)value);
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x0012FE4C File Offset: 0x0012F24C
		protected override object GetEmptyStorage(int recordCount)
		{
			return new decimal[recordCount];
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x0012FE60 File Offset: 0x0012F260
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			decimal[] array = (decimal[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x060030D1 RID: 12497 RVA: 0x0012FE9C File Offset: 0x0012F29C
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (decimal[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001D58 RID: 7512
		private static readonly decimal defaultValue;

		// Token: 0x04001D59 RID: 7513
		private decimal[] values;
	}
}
