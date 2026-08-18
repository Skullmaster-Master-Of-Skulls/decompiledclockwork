using System;
using System.Collections;

namespace System.Data.Common
{
	// Token: 0x02000325 RID: 805
	internal sealed class StringStorage : DataStorage
	{
		// Token: 0x060032AC RID: 12972 RVA: 0x0013AA5C File Offset: 0x00139E5C
		public StringStorage(DataColumn column) : base(column, typeof(string), string.Empty, StorageType.String)
		{
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x0013AA84 File Offset: 0x00139E84
		public override object Aggregate(int[] recordNos, AggregateType kind)
		{
			switch (kind)
			{
			case AggregateType.Min:
			{
				int num = -1;
				int i;
				for (i = 0; i < recordNos.Length; i++)
				{
					if (!this.IsNull(recordNos[i]))
					{
						num = recordNos[i];
						break;
					}
				}
				if (num >= 0)
				{
					for (i++; i < recordNos.Length; i++)
					{
						if (!this.IsNull(recordNos[i]) && this.Compare(num, recordNos[i]) > 0)
						{
							num = recordNos[i];
						}
					}
					return this.Get(num);
				}
				return this.NullValue;
			}
			case AggregateType.Max:
			{
				int num2 = -1;
				int i;
				for (i = 0; i < recordNos.Length; i++)
				{
					if (!this.IsNull(recordNos[i]))
					{
						num2 = recordNos[i];
						break;
					}
				}
				if (num2 >= 0)
				{
					for (i++; i < recordNos.Length; i++)
					{
						if (this.Compare(num2, recordNos[i]) < 0)
						{
							num2 = recordNos[i];
						}
					}
					return this.Get(num2);
				}
				return this.NullValue;
			}
			case AggregateType.Count:
			{
				int num3 = 0;
				for (int i = 0; i < recordNos.Length; i++)
				{
					object obj = this.values[recordNos[i]];
					if (obj != null)
					{
						num3++;
					}
				}
				return num3;
			}
			}
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x0013AB9C File Offset: 0x00139F9C
		public override int Compare(int recordNo1, int recordNo2)
		{
			string text = this.values[recordNo1];
			string text2 = this.values[recordNo2];
			if (text == text2)
			{
				return 0;
			}
			if (text == null)
			{
				return -1;
			}
			if (text2 == null)
			{
				return 1;
			}
			return this.Table.Compare(text, text2);
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x0013ABD8 File Offset: 0x00139FD8
		public override int CompareValueTo(int recordNo, object value)
		{
			string text = this.values[recordNo];
			if (text == null)
			{
				if (this.NullValue == value)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (this.NullValue == value)
				{
					return 1;
				}
				return this.Table.Compare(text, (string)value);
			}
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x0013AC1C File Offset: 0x0013A01C
		public override object ConvertValue(object value)
		{
			if (this.NullValue != value)
			{
				if (value != null)
				{
					value = value.ToString();
				}
				else
				{
					value = this.NullValue;
				}
			}
			return value;
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x0013AC48 File Offset: 0x0013A048
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x0013AC68 File Offset: 0x0013A068
		public override object Get(int recordNo)
		{
			string text = this.values[recordNo];
			if (text != null)
			{
				return text;
			}
			return this.NullValue;
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x0013AC8C File Offset: 0x0013A08C
		public override int GetStringLength(int record)
		{
			string text = this.values[record];
			if (text == null)
			{
				return 0;
			}
			return text.Length;
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x0013ACB0 File Offset: 0x0013A0B0
		public override bool IsNull(int record)
		{
			return this.values[record] == null;
		}

		// Token: 0x060032B5 RID: 12981 RVA: 0x0013ACC8 File Offset: 0x0013A0C8
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = null;
				return;
			}
			this.values[record] = value.ToString();
		}

		// Token: 0x060032B6 RID: 12982 RVA: 0x0013ACF8 File Offset: 0x0013A0F8
		public override void SetCapacity(int capacity)
		{
			string[] destinationArray = new string[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x0013AD38 File Offset: 0x0013A138
		public override object ConvertXmlToObject(string s)
		{
			return s;
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x0013AD48 File Offset: 0x0013A148
		public override string ConvertObjectToXml(object value)
		{
			return (string)value;
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x0013AD5C File Offset: 0x0013A15C
		protected override object GetEmptyStorage(int recordCount)
		{
			return new string[recordCount];
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x0013AD70 File Offset: 0x0013A170
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			string[] array = (string[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x0013ADA0 File Offset: 0x0013A1A0
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (string[])store;
		}

		// Token: 0x04001DC4 RID: 7620
		private string[] values;
	}
}
