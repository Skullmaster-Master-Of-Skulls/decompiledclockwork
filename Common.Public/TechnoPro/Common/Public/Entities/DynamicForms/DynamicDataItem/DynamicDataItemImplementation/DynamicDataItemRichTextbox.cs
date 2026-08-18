using System;
using System.Text;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x0200039E RID: 926
	public class DynamicDataItemRichTextbox : DynamicDataItemBase<string>
	{
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06001C41 RID: 7233 RVA: 0x00020578 File Offset: 0x0001E778
		private UTF8Encoding utf8Encoder
		{
			get
			{
				bool flag = this._utf8Encoder == null;
				if (flag)
				{
					this._utf8Encoder = new UTF8Encoding();
				}
				return this._utf8Encoder;
			}
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x000205A8 File Offset: 0x0001E7A8
		public override void ReadFromStorage(DynamicDataStorageItem item)
		{
			bool flag = item == null || (string.IsNullOrEmpty(item.OtherValue) && (item.ImageValue == null || item.ImageValue.Length < 1));
			if (flag)
			{
				base.Clear();
			}
			else
			{
				base.Value = (string.IsNullOrEmpty(item.OtherValue) ? this.utf8Encoder.GetString(item.ImageValue) : item.OtherValue);
			}
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00020620 File Offset: 0x0001E820
		public override DynamicDataStorageItem WriteToStorage()
		{
			bool flag = base.Value == null || base.Value.Trim().Length < 1;
			DynamicDataStorageItem result;
			if (flag)
			{
				result = new DynamicDataStorageItem(base.Field);
			}
			else
			{
				result = new DynamicDataStorageItem(base.Field)
				{
					ImageValue = this.utf8Encoder.GetBytes(base.Value)
				};
			}
			return result;
		}

		// Token: 0x04001671 RID: 5745
		private UTF8Encoding _utf8Encoder = null;
	}
}
