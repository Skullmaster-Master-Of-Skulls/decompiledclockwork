using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x020002DD RID: 733
	internal sealed class DateTimeStorage : DataStorage
	{
		// Token: 0x06002DD0 RID: 11728 RVA: 0x00125230 File Offset: 0x00124630
		internal DateTimeStorage(DataColumn column) : base(column, typeof(DateTime), DateTimeStorage.defaultValue, StorageType.DateTime)
		{
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x0012525C File Offset: 0x0012465C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					DateTime dateTime = DateTime.MaxValue;
					foreach (int num in records)
					{
						if (base.HasValue(num))
						{
							dateTime = ((DateTime.Compare(this.values[num], dateTime) < 0) ? this.values[num] : dateTime);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTime;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					DateTime dateTime2 = DateTime.MinValue;
					foreach (int num2 in records)
					{
						if (base.HasValue(num2))
						{
							dateTime2 = ((DateTime.Compare(this.values[num2], dateTime2) >= 0) ? this.values[num2] : dateTime2);
							flag = true;
						}
					}
					if (flag)
					{
						return dateTime2;
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
				throw ExprException.Overflow(typeof(DateTime));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x001253EC File Offset: 0x001247EC
		public override int Compare(int recordNo1, int recordNo2)
		{
			DateTime dateTime = this.values[recordNo1];
			DateTime dateTime2 = this.values[recordNo2];
			if (dateTime == DateTimeStorage.defaultValue || dateTime2 == DateTimeStorage.defaultValue)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return DateTime.Compare(dateTime, dateTime2);
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x00125444 File Offset: 0x00124844
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
				DateTime dateTime = this.values[recordNo];
				if (DateTimeStorage.defaultValue == dateTime && !base.HasValue(recordNo))
				{
					return -1;
				}
				return DateTime.Compare(dateTime, (DateTime)value);
			}
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x00125498 File Offset: 0x00124898
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToDateTime(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x001254D4 File Offset: 0x001248D4
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x00125504 File Offset: 0x00124904
		public override object Get(int record)
		{
			DateTime dateTime = this.values[record];
			if (dateTime != DateTimeStorage.defaultValue || base.HasValue(record))
			{
				return dateTime;
			}
			return this.NullValue;
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x00125544 File Offset: 0x00124944
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = DateTimeStorage.defaultValue;
				base.SetNullBit(record, true);
				return;
			}
			DateTime dateTime = ((IConvertible)value).ToDateTime(base.FormatProvider);
			DateTime dateTime2;
			switch (base.DateTimeMode)
			{
			case DataSetDateTime.Local:
				if (dateTime.Kind == DateTimeKind.Local)
				{
					dateTime2 = dateTime;
				}
				else if (dateTime.Kind == DateTimeKind.Utc)
				{
					dateTime2 = dateTime.ToLocalTime();
				}
				else
				{
					dateTime2 = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
				}
				break;
			case DataSetDateTime.Unspecified:
			case DataSetDateTime.UnspecifiedLocal:
				dateTime2 = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
				break;
			case DataSetDateTime.Utc:
				if (dateTime.Kind == DateTimeKind.Utc)
				{
					dateTime2 = dateTime;
				}
				else if (dateTime.Kind == DateTimeKind.Local)
				{
					dateTime2 = dateTime.ToUniversalTime();
				}
				else
				{
					dateTime2 = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
				}
				break;
			default:
				throw ExceptionBuilder.InvalidDateTimeMode(base.DateTimeMode);
			}
			this.values[record] = dateTime2;
			base.SetNullBit(record, false);
		}

		// Token: 0x06002DD8 RID: 11736 RVA: 0x0012562C File Offset: 0x00124A2C
		public override void SetCapacity(int capacity)
		{
			DateTime[] destinationArray = new DateTime[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x00125674 File Offset: 0x00124A74
		public override object ConvertXmlToObject(string s)
		{
			object result;
			if (base.DateTimeMode == DataSetDateTime.UnspecifiedLocal)
			{
				result = XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.Unspecified);
			}
			else
			{
				result = XmlConvert.ToDateTime(s, XmlDateTimeSerializationMode.RoundtripKind);
			}
			return result;
		}

		// Token: 0x06002DDA RID: 11738 RVA: 0x001256A8 File Offset: 0x00124AA8
		public override string ConvertObjectToXml(object value)
		{
			string result;
			if (base.DateTimeMode == DataSetDateTime.UnspecifiedLocal)
			{
				result = XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.Local);
			}
			else
			{
				result = XmlConvert.ToString((DateTime)value, XmlDateTimeSerializationMode.RoundtripKind);
			}
			return result;
		}

		// Token: 0x06002DDB RID: 11739 RVA: 0x001256DC File Offset: 0x00124ADC
		protected override object GetEmptyStorage(int recordCount)
		{
			return new DateTime[recordCount];
		}

		// Token: 0x06002DDC RID: 11740 RVA: 0x001256F0 File Offset: 0x00124AF0
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			DateTime[] array = (DateTime[])store;
			bool flag = !base.HasValue(record);
			if (flag || (base.DateTimeMode & DataSetDateTime.Local) == (DataSetDateTime)0)
			{
				array[storeIndex] = this.values[record];
			}
			else
			{
				array[storeIndex] = this.values[record].ToUniversalTime();
			}
			nullbits.Set(storeIndex, flag);
		}

		// Token: 0x06002DDD RID: 11741 RVA: 0x00125754 File Offset: 0x00124B54
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (DateTime[])store;
			base.SetNullStorage(nullbits);
			if (base.DateTimeMode == DataSetDateTime.UnspecifiedLocal)
			{
				for (int i = 0; i < this.values.Length; i++)
				{
					if (base.HasValue(i))
					{
						this.values[i] = DateTime.SpecifyKind(this.values[i].ToLocalTime(), DateTimeKind.Unspecified);
					}
				}
				return;
			}
			if (base.DateTimeMode == DataSetDateTime.Local)
			{
				for (int j = 0; j < this.values.Length; j++)
				{
					if (base.HasValue(j))
					{
						this.values[j] = this.values[j].ToLocalTime();
					}
				}
			}
		}

		// Token: 0x04001C7E RID: 7294
		private static readonly DateTime defaultValue = DateTime.MinValue;

		// Token: 0x04001C7F RID: 7295
		private DateTime[] values;
	}
}
