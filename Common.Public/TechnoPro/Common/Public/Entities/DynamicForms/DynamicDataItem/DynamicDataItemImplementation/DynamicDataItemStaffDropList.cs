using System;
using System.Text;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem.DynamicDataItemImplementation
{
	// Token: 0x0200039F RID: 927
	public class DynamicDataItemStaffDropList : DynamicDataItemBase<Person>
	{
		// Token: 0x06001C46 RID: 7238 RVA: 0x000206A8 File Offset: 0x0001E8A8
		public override void ReadFromStorage(DynamicDataStorageItem item)
		{
			bool flag = item == null || item.IntValue == null;
			if (flag)
			{
				base.Clear();
			}
			else
			{
				base.Value = new Person
				{
					PersonID = item.IntValue.Value,
					LastName = (item.OtherValue ?? ""),
					FirstName = ((item.ImageValue == null) ? "" : this.utf8Encoder.GetString(item.ImageValue))
				};
			}
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x0002073C File Offset: 0x0001E93C
		public override DynamicDataStorageItem WriteToStorage()
		{
			bool flag = base.Value == null || base.Value.PersonID < 1;
			DynamicDataStorageItem result;
			if (flag)
			{
				result = new DynamicDataStorageItem(base.Field);
			}
			else
			{
				result = new DynamicDataStorageItem(base.Field)
				{
					IntValue = new int?(base.Value.PersonID),
					OtherValue = base.Value.LastName,
					ImageValue = this.utf8Encoder.GetBytes(base.Value.FirstName ?? "")
				};
			}
			return result;
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06001C48 RID: 7240 RVA: 0x000207D4 File Offset: 0x0001E9D4
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

		// Token: 0x04001672 RID: 5746
		private UTF8Encoding _utf8Encoder = null;
	}
}
