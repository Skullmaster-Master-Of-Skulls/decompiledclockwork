using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000160 RID: 352
	internal sealed class SByteStorage : DataStorage
	{
		// Token: 0x060015EA RID: 5610 RVA: 0x00246728 File Offset: 0x00245B28
		public SByteStorage(DataColumn column) : base(column, typeof(sbyte), 0)
		{
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00246758 File Offset: 0x00245B58
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

		// Token: 0x060015EC RID: 5612 RVA: 0x00246A68 File Offset: 0x00245E68
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

		// Token: 0x060015ED RID: 5613 RVA: 0x00246AB8 File Offset: 0x00245EB8
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

		// Token: 0x060015EE RID: 5614 RVA: 0x00246B08 File Offset: 0x00245F08
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

		// Token: 0x060015EF RID: 5615 RVA: 0x00246B48 File Offset: 0x00245F48
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00246B78 File Offset: 0x00245F78
		public override object Get(int record)
		{
			sbyte b = this.values[record];
			if (!b.Equals(0))
			{
				return b;
			}
			return base.GetBits(record);
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00246BA8 File Offset: 0x00245FA8
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

		// Token: 0x060015F2 RID: 5618 RVA: 0x00246BF8 File Offset: 0x00245FF8
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

		// Token: 0x060015F3 RID: 5619 RVA: 0x00246C48 File Offset: 0x00246048
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToSByte(s);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00246C68 File Offset: 0x00246068
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((sbyte)value);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00246C88 File Offset: 0x00246088
		protected override object GetEmptyStorage(int recordCount)
		{
			return new sbyte[recordCount];
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00246CA8 File Offset: 0x002460A8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			sbyte[] array = (sbyte[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00246CD8 File Offset: 0x002460D8
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (sbyte[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000CD3 RID: 3283
		private const sbyte defaultValue = 0;

		// Token: 0x04000CD4 RID: 3284
		private sbyte[] values;
	}
}
