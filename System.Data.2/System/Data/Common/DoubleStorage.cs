using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000300 RID: 768
	internal sealed class DoubleStorage : DataStorage
	{
		// Token: 0x060030D2 RID: 12498 RVA: 0x0012FEBC File Offset: 0x0012F2BC
		internal DoubleStorage(DataColumn column) : base(column, typeof(double), 0.0, StorageType.Double)
		{
		}

		// Token: 0x060030D3 RID: 12499 RVA: 0x0012FEEC File Offset: 0x0012F2EC
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					double num = 0.0;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
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
					double num3 = 0.0;
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							num3 += this.values[num5];
							num4++;
							flag = true;
						}
					}
					if (flag)
					{
						double num6 = num3 / (double)num4;
						return num6;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					double num7 = double.MaxValue;
					foreach (int num8 in records)
					{
						if (!this.IsNull(num8))
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
					double num9 = double.MinValue;
					foreach (int num10 in records)
					{
						if (!this.IsNull(num10))
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
					return base.Aggregate(records, kind);
				case AggregateType.Var:
				case AggregateType.StDev:
				{
					int num11 = 0;
					double num12 = 0.0;
					double num13 = 0.0;
					foreach (int num14 in records)
					{
						if (!this.IsNull(num14))
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
				throw ExprException.Overflow(typeof(double));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x00130208 File Offset: 0x0012F608
		public override int Compare(int recordNo1, int recordNo2)
		{
			double num = this.values[recordNo1];
			double num2 = this.values[recordNo2];
			if (num == 0.0 || num2 == 0.0)
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return num.CompareTo(num2);
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x00130258 File Offset: 0x0012F658
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
				double num = this.values[recordNo];
				if (0.0 == num && this.IsNull(recordNo))
				{
					return -1;
				}
				return num.CompareTo((double)value);
			}
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x001302A8 File Offset: 0x0012F6A8
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToDouble(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x001302E4 File Offset: 0x0012F6E4
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x0013030C File Offset: 0x0012F70C
		public override object Get(int record)
		{
			double num = this.values[record];
			if (num != 0.0)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x0013033C File Offset: 0x0012F73C
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = 0.0;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToDouble(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x00130390 File Offset: 0x0012F790
		public override void SetCapacity(int capacity)
		{
			double[] destinationArray = new double[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x001303D8 File Offset: 0x0012F7D8
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToDouble(s);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x001303F0 File Offset: 0x0012F7F0
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((double)value);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x00130408 File Offset: 0x0012F808
		protected override object GetEmptyStorage(int recordCount)
		{
			return new double[recordCount];
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x0013041C File Offset: 0x0012F81C
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			double[] array = (double[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x0013044C File Offset: 0x0012F84C
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (double[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001D5A RID: 7514
		private const double defaultValue = 0.0;

		// Token: 0x04001D5B RID: 7515
		private double[] values;
	}
}
