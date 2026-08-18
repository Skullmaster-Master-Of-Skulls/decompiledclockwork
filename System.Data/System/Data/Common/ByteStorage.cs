using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000115 RID: 277
	internal sealed class ByteStorage : DataStorage
	{
		// Token: 0x0600118A RID: 4490 RVA: 0x00233F98 File Offset: 0x00233398
		internal ByteStorage(DataColumn column) : base(column, typeof(byte), 0)
		{
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x00233FC8 File Offset: 0x002333C8
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
							if (!this.IsNull(num2))
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
					long num3 = 0L;
					int num4 = 0;
					foreach (int num5 in records)
					{
						if (!this.IsNull(num5))
						{
							checked
							{
								num3 += (long)(unchecked((ulong)this.values[num5]));
							}
							num4++;
							flag = true;
						}
					}
					if (flag)
					{
						byte b = checked((byte)(num3 / unchecked((long)num4)));
						return b;
					}
					return this.NullValue;
				}
				case AggregateType.Min:
				{
					byte b2 = byte.MaxValue;
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
					byte b3 = 0;
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
				throw ExprException.Overflow(typeof(byte));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x002342D8 File Offset: 0x002336D8
		public override int Compare(int recordNo1, int recordNo2)
		{
			byte b = this.values[recordNo1];
			byte b2 = this.values[recordNo2];
			if (b == 0 || b2 == 0)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return b.CompareTo(b2);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x00234318 File Offset: 0x00233718
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
				byte b = this.values[recordNo];
				if (b == 0 && this.IsNull(recordNo))
				{
					return -1;
				}
				return b.CompareTo((byte)value);
			}
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00234368 File Offset: 0x00233768
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToByte(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x002343A8 File Offset: 0x002337A8
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x002343D8 File Offset: 0x002337D8
		public override object Get(int record)
		{
			byte b = this.values[record];
			if (b != 0)
			{
				return b;
			}
			return base.GetBits(record);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x00234408 File Offset: 0x00233808
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = 0;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToByte(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00234458 File Offset: 0x00233858
		public override void SetCapacity(int capacity)
		{
			byte[] destinationArray = new byte[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x002344A8 File Offset: 0x002338A8
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToByte(s);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x002344C8 File Offset: 0x002338C8
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((byte)value);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x002344E8 File Offset: 0x002338E8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new byte[recordCount];
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x00234508 File Offset: 0x00233908
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			byte[] array = (byte[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00234538 File Offset: 0x00233938
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (byte[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000B78 RID: 2936
		private const byte defaultValue = 0;

		// Token: 0x04000B79 RID: 2937
		private byte[] values;
	}
}
