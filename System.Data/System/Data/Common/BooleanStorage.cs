using System;
using System.Collections;
using System.Xml;

namespace System.Data.Common
{
	// Token: 0x02000114 RID: 276
	internal sealed class BooleanStorage : DataStorage
	{
		// Token: 0x0600117C RID: 4476 RVA: 0x00233BB8 File Offset: 0x00232FB8
		internal BooleanStorage(DataColumn column) : base(column, typeof(bool), false)
		{
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00233BE8 File Offset: 0x00232FE8
		public override object Aggregate(int[] records, AggregateType kind)
		{
			bool flag = false;
			try
			{
				switch (kind)
				{
				case AggregateType.Min:
				{
					bool flag2 = true;
					foreach (int num in records)
					{
						if (!this.IsNull(num))
						{
							flag2 = (this.values[num] && flag2);
							flag = true;
						}
					}
					if (flag)
					{
						return flag2;
					}
					return this.NullValue;
				}
				case AggregateType.Max:
				{
					bool flag3 = false;
					foreach (int num2 in records)
					{
						if (!this.IsNull(num2))
						{
							flag3 = (this.values[num2] || flag3);
							flag = true;
						}
					}
					if (flag)
					{
						return flag3;
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
				}
			}
			catch (OverflowException)
			{
				throw ExprException.Overflow(typeof(bool));
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00233D18 File Offset: 0x00233118
		public override int Compare(int recordNo1, int recordNo2)
		{
			bool flag = this.values[recordNo1];
			bool flag2 = this.values[recordNo2];
			if (!flag || !flag2)
			{
				int num = base.CompareBits(recordNo1, recordNo2);
				if (num != 0)
				{
					return num;
				}
			}
			return flag.CompareTo(flag2);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00233D58 File Offset: 0x00233158
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
				bool flag = this.values[recordNo];
				if (!flag && this.IsNull(recordNo))
				{
					return -1;
				}
				return flag.CompareTo((bool)value);
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00233DA8 File Offset: 0x002331A8
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = ((IConvertible)value).ToBoolean(base.FormatProvider);
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x00233DE8 File Offset: 0x002331E8
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x00233E18 File Offset: 0x00233218
		public override object Get(int record)
		{
			bool flag = this.values[record];
			if (flag)
			{
				return flag;
			}
			return base.GetBits(record);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x00233E48 File Offset: 0x00233248
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = false;
				base.SetNullBit(record, true);
				return;
			}
			this.values[record] = ((IConvertible)value).ToBoolean(base.FormatProvider);
			base.SetNullBit(record, false);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x00233E98 File Offset: 0x00233298
		public override void SetCapacity(int capacity)
		{
			bool[] destinationArray = new bool[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x00233EE8 File Offset: 0x002332E8
		public override object ConvertXmlToObject(string s)
		{
			return XmlConvert.ToBoolean(s);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00233F08 File Offset: 0x00233308
		public override string ConvertObjectToXml(object value)
		{
			return XmlConvert.ToString((bool)value);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00233F28 File Offset: 0x00233328
		protected override object GetEmptyStorage(int recordCount)
		{
			return new bool[recordCount];
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00233F48 File Offset: 0x00233348
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			bool[] array = (bool[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x00233F78 File Offset: 0x00233378
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (bool[])store;
			base.SetNullStorage(nullbits);
		}

		// Token: 0x04000B76 RID: 2934
		private const bool defaultValue = false;

		// Token: 0x04000B77 RID: 2935
		private bool[] values;
	}
}
