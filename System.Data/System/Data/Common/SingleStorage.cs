using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000163 RID: 355
	internal sealed class SingleStorage : DataStorage
	{
		// Token: 0x060015FA RID: 5626 RVA: 0x00246E58 File Offset: 0x00246258
		public SingleStorage(DataColumn column) : base(column, typeof(float), 0f)
		{
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00246E88 File Offset: 0x00246288
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Sum:
				{
					float num = 0f;
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
							num3 += (double)this.values[num5];
							num4++;
							flag = true;
						}
					}
					if (flag)
					{
						float num6 = (float)(num3 / (double)num4);
						return num6;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					float num7 = float.MaxValue;
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
					float num9 = float.MinValue;
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
					int num11 = 0;
					double num12 = 0.0;
					double num13 = 0.0;
					foreach (int num14 in records)
					{
						if (!this.IsNull(num14))
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
				throw ExprException.Overflow(typeof(float));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x002471A8 File Offset: 0x002465A8
		public override int Compare(int recordNo1, int recordNo2)
		{
			float num = this.values[recordNo1];
			float num2 = this.values[recordNo2];
			if (num == 0f || num2 == 0f)
			{
				int num3 = base.CompareBits(recordNo1, recordNo2);
				if (num3 != 0)
				{
					return num3;
				}
			}
			return num.CompareTo(num2);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x002471F8 File Offset: 0x002465F8
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
				float num = this.values[recordNo];
				if (0f == num && this.IsNull(recordNo))
				{
					return -1;
				}
				return num.CompareTo((float)value);
			}
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00247248 File Offset: 0x00246648
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToSingle(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00247288 File Offset: 0x00246688
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x002472B8 File Offset: 0x002466B8
		public override object Get(int record)
		{
			float num = this.values[record];
			if (num != 0f)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x002472E8 File Offset: 0x002466E8
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = 0f;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToSingle(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00247338 File Offset: 0x00246738
		public override void SetCapacity(int capacity)
		{
			float[] destinationArray = new float[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00247388 File Offset: 0x00246788
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToSingle(s);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x002473A8 File Offset: 0x002467A8
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((float)value);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x002473C8 File Offset: 0x002467C8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new float[recordCount];
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x002473E8 File Offset: 0x002467E8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			float[] array = (float[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00247418 File Offset: 0x00246818
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (float[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000CF4 RID: 3316
		private const float defaultValue = 0f;

		// Token: 0x04000CF5 RID: 3317
		private float[] values;
	}
}
