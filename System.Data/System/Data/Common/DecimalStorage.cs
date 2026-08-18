using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000148 RID: 328
	internal sealed class DecimalStorage : DataStorage
	{
		// Token: 0x06001522 RID: 5410 RVA: 0x002427A8 File Offset: 0x00241BA8
		internal DecimalStorage(DataColumn column) : base(column, typeof(decimal), DecimalStorage.defaultValue)
		{
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x002427D8 File Offset: 0x00241BD8
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

		// Token: 0x06001524 RID: 5412 RVA: 0x00242B68 File Offset: 0x00241F68
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

		// Token: 0x06001525 RID: 5413 RVA: 0x00242BC8 File Offset: 0x00241FC8
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

		// Token: 0x06001526 RID: 5414 RVA: 0x00242C28 File Offset: 0x00242028
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

		// Token: 0x06001527 RID: 5415 RVA: 0x00242C68 File Offset: 0x00242068
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00242CA8 File Offset: 0x002420A8
		public override object Get(int record)
		{
			if (!base.HasValue(record))
			{
				return this.NullValue;
			}
			return this.values[record];
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00242CE8 File Offset: 0x002420E8
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

		// Token: 0x0600152A RID: 5418 RVA: 0x00242D48 File Offset: 0x00242148
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

		// Token: 0x0600152B RID: 5419 RVA: 0x00242D98 File Offset: 0x00242198
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToDecimal(s);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00242DB8 File Offset: 0x002421B8
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((decimal)value);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x00242DD8 File Offset: 0x002421D8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new decimal[recordCount];
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00242DF8 File Offset: 0x002421F8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			decimal[] array = (decimal[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00242E48 File Offset: 0x00242248
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (decimal[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000C87 RID: 3207
		private static readonly decimal defaultValue;

		// Token: 0x04000C88 RID: 3208
		private decimal[] values;
	}
}
