using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem
{
	// Token: 0x02000386 RID: 902
	[Serializable]
	public class DynamicDataStorageItem
	{
		// Token: 0x06001BE8 RID: 7144 RVA: 0x0000D55A File Offset: 0x0000B75A
		public DynamicDataStorageItem()
		{
		}

		// Token: 0x06001BE9 RID: 7145 RVA: 0x0001F972 File Offset: 0x0001DB72
		public DynamicDataStorageItem(DynamicField field)
		{
			this.Field = field;
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06001BEA RID: 7146 RVA: 0x0001F984 File Offset: 0x0001DB84
		// (set) Token: 0x06001BEB RID: 7147 RVA: 0x0001F98C File Offset: 0x0001DB8C
		public DynamicField Field { get; set; }

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06001BEC RID: 7148 RVA: 0x0001F995 File Offset: 0x0001DB95
		// (set) Token: 0x06001BED RID: 7149 RVA: 0x0001F99D File Offset: 0x0001DB9D
		public string OtherValue { get; set; }

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x0001F9A6 File Offset: 0x0001DBA6
		// (set) Token: 0x06001BEF RID: 7151 RVA: 0x0001F9AE File Offset: 0x0001DBAE
		public byte[] ImageValue { get; set; }

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0001F9B7 File Offset: 0x0001DBB7
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x0001F9BF File Offset: 0x0001DBBF
		public int? IntValue { get; set; }

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x0001F9C8 File Offset: 0x0001DBC8
		// (set) Token: 0x06001BF3 RID: 7155 RVA: 0x0001F9D0 File Offset: 0x0001DBD0
		public DateTime? DateTimeValue { get; set; }

		// Token: 0x06001BF4 RID: 7156 RVA: 0x0001F9DC File Offset: 0x0001DBDC
		private bool IsOtherValueEqualTo(string item)
		{
			string text = (this.OtherValue ?? "").Trim();
			string value = (item ?? "").Trim();
			return text.Equals(value, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x0001FA1C File Offset: 0x0001DC1C
		private bool IsImageValueEqualTo(byte[] item)
		{
			byte[] array = this.ImageValue ?? new byte[0];
			byte[] array2 = item ?? new byte[0];
			bool flag = array.Length != array2.Length;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					bool flag2 = array[i] != array2[i];
					if (flag2)
					{
						return false;
					}
				}
				result = true;
			}
			return result;
		}

		// Token: 0x06001BF6 RID: 7158 RVA: 0x0001FA90 File Offset: 0x0001DC90
		private bool IsIntValueEqualTo(int? item)
		{
			bool flag = this.IntValue == null && item == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = this.IntValue == null || item == null;
				result = (!flag2 && this.IntValue.Value == item.Value);
			}
			return result;
		}

		// Token: 0x06001BF7 RID: 7159 RVA: 0x0001FB04 File Offset: 0x0001DD04
		private bool IsDateTimeValueEqualTo(DateTime? item)
		{
			bool flag = this.DateTimeValue == null && item == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = this.DateTimeValue == null || item == null;
				result = (!flag2 && this.DateTimeValue.Value == item.Value);
			}
			return result;
		}

		// Token: 0x06001BF8 RID: 7160 RVA: 0x0001FB7C File Offset: 0x0001DD7C
		public bool IsEqualTo(DynamicDataStorageItem item)
		{
			bool flag = item == null || this.Field == null || item.Field == null;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = this.Field.ControlId != item.Field.ControlId;
				result = (!flag2 && (this.IsOtherValueEqualTo(item.OtherValue) && this.IsImageValueEqualTo(item.ImageValue) && this.IsIntValueEqualTo(item.IntValue)) && this.IsDateTimeValueEqualTo(item.DateTimeValue));
			}
			return result;
		}
	}
}
