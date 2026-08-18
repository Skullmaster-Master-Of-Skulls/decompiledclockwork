using System;
using System.Collections;
using System.Globalization;
using System.Numerics;

namespace System.Data.Common
{
	// Token: 0x02000331 RID: 817
	internal sealed class BigIntegerStorage : DataStorage
	{
		// Token: 0x06003356 RID: 13142 RVA: 0x0013CDA8 File Offset: 0x0013C1A8
		internal BigIntegerStorage(DataColumn column) : base(column, typeof(BigInteger), BigInteger.Zero, StorageType.BigInteger)
		{
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x0013CDD4 File Offset: 0x0013C1D4
		public override object Aggregate(int[] records, AggregateType kind)
		{
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x0013CDF0 File Offset: 0x0013C1F0
		public override int Compare(int recordNo1, int recordNo2)
		{
			BigInteger bigInteger = this.values[recordNo1];
			BigInteger other = this.values[recordNo2];
			if (bigInteger.IsZero || other.IsZero)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return bigInteger.CompareTo(other);
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x0013CE40 File Offset: 0x0013C240
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
				BigInteger bigInteger = this.values[recordNo];
				if (bigInteger.IsZero && !base.HasValue(recordNo))
				{
					return -1;
				}
				return bigInteger.CompareTo((BigInteger)value);
			}
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x0013CE94 File Offset: 0x0013C294
		internal static BigInteger ConvertToBigInteger(object value, IFormatProvider formatProvider)
		{
			if (value.GetType() == typeof(BigInteger))
			{
				return (BigInteger)value;
			}
			if (value.GetType() == typeof(string))
			{
				return BigInteger.Parse((string)value, formatProvider);
			}
			if (value.GetType() == typeof(long))
			{
				return (long)value;
			}
			if (value.GetType() == typeof(int))
			{
				return (int)value;
			}
			if (value.GetType() == typeof(short))
			{
				return (short)value;
			}
			if (value.GetType() == typeof(sbyte))
			{
				return (sbyte)value;
			}
			if (value.GetType() == typeof(ulong))
			{
				return (ulong)value;
			}
			if (value.GetType() == typeof(uint))
			{
				return (uint)value;
			}
			if (value.GetType() == typeof(ushort))
			{
				return (ushort)value;
			}
			if (value.GetType() == typeof(byte))
			{
				return (byte)value;
			}
			throw ExceptionBuilder.ConvertFailed(value.GetType(), typeof(BigInteger));
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x0013D010 File Offset: 0x0013C410
		internal static object ConvertFromBigInteger(BigInteger value, Type type, IFormatProvider formatProvider)
		{
			if (type == typeof(string))
			{
				return value.ToString("D", formatProvider);
			}
			if (type == typeof(sbyte))
			{
				return (sbyte)value;
			}
			if (type == typeof(short))
			{
				return (short)value;
			}
			if (type == typeof(int))
			{
				return (int)value;
			}
			if (type == typeof(long))
			{
				return (long)value;
			}
			if (type == typeof(byte))
			{
				return (byte)value;
			}
			if (type == typeof(ushort))
			{
				return (ushort)value;
			}
			if (type == typeof(uint))
			{
				return (uint)value;
			}
			if (type == typeof(ulong))
			{
				return (ulong)value;
			}
			if (type == typeof(float))
			{
				return (float)value;
			}
			if (type == typeof(double))
			{
				return (double)value;
			}
			if (type == typeof(decimal))
			{
				return (decimal)value;
			}
			if (type == typeof(BigInteger))
			{
				return value;
			}
			throw ExceptionBuilder.ConvertFailed(typeof(BigInteger), type);
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x0013D1B4 File Offset: 0x0013C5B4
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = BigIntegerStorage.ConvertToBigInteger(value, base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x0013D1EC File Offset: 0x0013C5EC
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x0013D21C File Offset: 0x0013C61C
		public override object Get(int record)
		{
			BigInteger bigInteger = this.values[record];
			if (!bigInteger.IsZero)
			{
				return bigInteger;
			}
			return base.GetBits(record);
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x0013D250 File Offset: 0x0013C650
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = BigInteger.Zero;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = BigIntegerStorage.ConvertToBigInteger(value, base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x0013D2A0 File Offset: 0x0013C6A0
		public override void SetCapacity(int capacity)
		{
			BigInteger[] destinationArray = new BigInteger[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x0013D2E8 File Offset: 0x0013C6E8
		public override object ConvertXmlToObject(string s)
		{
			return BigInteger.Parse(s, CultureInfo.InvariantCulture);
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x0013D308 File Offset: 0x0013C708
		public override string ConvertObjectToXml(object value)
		{
			return ((BigInteger)value).ToString("D", CultureInfo.InvariantCulture);
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x0013D330 File Offset: 0x0013C730
		protected override object GetEmptyStorage(int recordCount)
		{
			return new BigInteger[recordCount];
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x0013D344 File Offset: 0x0013C744
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			BigInteger[] array = (BigInteger[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x0013D380 File Offset: 0x0013C780
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (BigInteger[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04001E0F RID: 7695
		private BigInteger[] values;
	}
}
