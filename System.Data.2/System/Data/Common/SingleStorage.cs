using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000312 RID: 786
	internal sealed class SingleStorage : DataStorage
	{
		// Token: 0x06003189 RID: 12681 RVA: 0x0013369C File Offset: 0x00132A9C
		public SingleStorage(DataColumn column) : base(column, typeof(float), 0f, StorageType.Single)
		{
		}

		// Token: 0x0600318A RID: 12682 RVA: 0x001336C8 File Offset: 0x00132AC8
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

		// Token: 0x0600318B RID: 12683 RVA: 0x001339D8 File Offset: 0x00132DD8
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

		// Token: 0x0600318C RID: 12684 RVA: 0x00133A20 File Offset: 0x00132E20
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

		// Token: 0x0600318D RID: 12685 RVA: 0x00133A6C File Offset: 0x00132E6C
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

		// Token: 0x0600318E RID: 12686 RVA: 0x00133AA8 File Offset: 0x00132EA8
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x00133AD0 File Offset: 0x00132ED0
		public override object Get(int record)
		{
			float num = this.values[record];
			if (num != 0f)
			{
				return num;
			}
			return base.GetBits(record);
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x00133AFC File Offset: 0x00132EFC
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

		// Token: 0x06003191 RID: 12689 RVA: 0x00133B4C File Offset: 0x00132F4C
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

		// Token: 0x06003192 RID: 12690 RVA: 0x00133B94 File Offset: 0x00132F94
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToSingle(s);
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x00133BAC File Offset: 0x00132FAC
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((float)value);
		}

		// Token: 0x06003194 RID: 12692 RVA: 0x00133BC4 File Offset: 0x00132FC4
		protected override object GetEmptyStorage(int recordCount)
		{
			return new float[recordCount];
		}

		// Token: 0x06003195 RID: 12693 RVA: 0x00133BD8 File Offset: 0x00132FD8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			float[] array = (float[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06003196 RID: 12694 RVA: 0x00133C08 File Offset: 0x00133008
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (float[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001DAE RID: 7598
		private const float defaultValue = 0f;

		// Token: 0x04001DAF RID: 7599
		private float[] values;
	}
}
