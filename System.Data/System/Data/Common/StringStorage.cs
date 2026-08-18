using System;
using System.Collections;

namespace System.Data.Common
{
	// Token: 0x02000165 RID: 357
	internal sealed class StringStorage : DataStorage
	{
		// Token: 0x0600161B RID: 5659 RVA: 0x00248B88 File Offset: 0x00247F88
		public StringStorage(DataColumn column) : base(column, typeof(string), string.Empty)
		{
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00248BB8 File Offset: 0x00247FB8
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

		// Token: 0x0600161D RID: 5661 RVA: 0x00248CD8 File Offset: 0x002480D8
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

		// Token: 0x0600161E RID: 5662 RVA: 0x00248D18 File Offset: 0x00248118
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

		// Token: 0x0600161F RID: 5663 RVA: 0x00248D68 File Offset: 0x00248168
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

		// Token: 0x06001620 RID: 5664 RVA: 0x00248D98 File Offset: 0x00248198
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00248DB8 File Offset: 0x002481B8
		public override object Get(int recordNo)
		{
			string text = this.values[recordNo];
			if (text != null)
			{
				return text;
			}
			return this.NullValue;
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00248DE8 File Offset: 0x002481E8
		public override int GetStringLength(int record)
		{
			string text = this.values[record];
			if (text == null)
			{
				return 0;
			}
			return text.Length;
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x00248E18 File Offset: 0x00248218
		public override bool IsNull(int record)
		{
			return null == this.values[record];
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x00248E38 File Offset: 0x00248238
		public override void Set(int record, object value)
		{
			if (this.NullValue == value)
			{
				this.values[record] = null;
				return;
			}
			this.values[record] = value.ToString();
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x00248E68 File Offset: 0x00248268
		public override void SetCapacity(int capacity)
		{
			string[] destinationArray = new string[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x00248EA8 File Offset: 0x002482A8
		public override object ConvertXmlToObject(string s)
		{
			return s;
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00248EB8 File Offset: 0x002482B8
		public override string ConvertObjectToXml(object value)
		{
			return (string)value;
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00248ED8 File Offset: 0x002482D8
		protected override object GetEmptyStorage(int recordCount)
		{
			return new string[recordCount];
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00248EF8 File Offset: 0x002482F8
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			string[] array = (string[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00248F28 File Offset: 0x00248328
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (string[])store;
		}

		// Token: 0x04000CF6 RID: 3318
		private string[] values;
	}
}
