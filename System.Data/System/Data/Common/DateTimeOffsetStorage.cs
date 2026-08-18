using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000124 RID: 292
	internal sealed class DateTimeOffsetStorage : DataStorage
	{
		// Token: 0x060012C1 RID: 4801 RVA: 0x00238388 File Offset: 0x00237788
		internal DateTimeOffsetStorage(DataColumn column) : base(column, typeof(DateTimeOffset), DateTimeOffsetStorage.defaultValue)
		{
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x002383B8 File Offset: 0x002377B8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					DateTimeOffset dateTimeOffset = DateTimeOffset.MaxValue;
					foreach (int num in records)
					{
						if (base.HasValue(num))
						{
							dateTimeOffset = ((DateTimeOffset.Compare(this.values[num], dateTimeOffset) < 0) ? this.values[num] : dateTimeOffset);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTimeOffset;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					DateTimeOffset dateTimeOffset2 = DateTimeOffset.MinValue;
					foreach (int num2 in records)
					{
						if (base.HasValue(num2))
						{
							dateTimeOffset2 = ((DateTimeOffset.Compare(this.values[num2], dateTimeOffset2) >= 0) ? this.values[num2] : dateTimeOffset2);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTimeOffset2;
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
				{
					int num3 = 0;
					for (int k = 0; k < records.Length; k++)
					{
						if (base.HasValue(records[k]))
						{
							num3++;
						}
					}
					return num3;
				}
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(DateTimeOffset));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00238568 File Offset: 0x00237968
		public override int Compare(int recordNo1, int recordNo2)
		{
			DateTimeOffset dateTimeOffset = this.values[recordNo1];
			DateTimeOffset dateTimeOffset2 = this.values[recordNo2];
			if (dateTimeOffset == DateTimeOffsetStorage.defaultValue || dateTimeOffset2 == DateTimeOffsetStorage.defaultValue)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return DateTimeOffset.Compare(dateTimeOffset, dateTimeOffset2);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x002385C8 File Offset: 0x002379C8
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
				DateTimeOffset dateTimeOffset = this.values[recordNo];
				if (DateTimeOffsetStorage.defaultValue == dateTimeOffset && !base.HasValue(recordNo))
				{
					return -1;
				}
				return DateTimeOffset.Compare(dateTimeOffset, (DateTimeOffset)value);
			}
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00238628 File Offset: 0x00237A28
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = (DateTimeOffset)value;
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00238668 File Offset: 0x00237A68
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x002386A8 File Offset: 0x00237AA8
		public override object Get(int record)
		{
			DateTimeOffset dateTimeOffset = this.values[record];
			if (dateTimeOffset != DateTimeOffsetStorage.defaultValue || base.HasValue(record))
			{
				return dateTimeOffset;
			}
			return this.NullValue;
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x002386F8 File Offset: 0x00237AF8
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = DateTimeOffsetStorage.defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = (DateTimeOffset)value;
			base.SetNullBit(record, false);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x00238758 File Offset: 0x00237B58
		public override void SetCapacity(int capacity)
		{
			DateTimeOffset[] destinationArray = new DateTimeOffset[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x002387A8 File Offset: 0x00237BA8
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToDateTimeOffset(s);
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x002387C8 File Offset: 0x00237BC8
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((DateTimeOffset)value);
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x002387E8 File Offset: 0x00237BE8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new DateTimeOffset[recordCount];
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00238808 File Offset: 0x00237C08
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			DateTimeOffset[] array = (DateTimeOffset[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, !base.HasValue(record));
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00238858 File Offset: 0x00237C58
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (DateTimeOffset[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000BC4 RID: 3012
		private static readonly DateTimeOffset defaultValue = DateTimeOffset.MinValue;

		// Token: 0x04000BC5 RID: 3013
		private DateTimeOffset[] values;
	}
}
