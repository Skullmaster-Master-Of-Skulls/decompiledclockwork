using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem
{
	// Token: 0x02000385 RID: 901
	[Serializable]
	public abstract class DynamicDataItemBase<T> : IDynamicDataSerializableItem where T : class
	{
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06001BDD RID: 7133 RVA: 0x0001F8BA File Offset: 0x0001DABA
		// (set) Token: 0x06001BDE RID: 7134 RVA: 0x0001F8C2 File Offset: 0x0001DAC2
		public DynamicField Field { get; set; }

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06001BDF RID: 7135 RVA: 0x0001F8CB File Offset: 0x0001DACB
		// (set) Token: 0x06001BE0 RID: 7136 RVA: 0x0001F8D3 File Offset: 0x0001DAD3
		public T Value { get; set; }

		// Token: 0x06001BE1 RID: 7137
		public abstract void ReadFromStorage(DynamicDataStorageItem item);

		// Token: 0x06001BE2 RID: 7138
		public abstract DynamicDataStorageItem WriteToStorage();

		// Token: 0x06001BE3 RID: 7139 RVA: 0x0001F8DC File Offset: 0x0001DADC
		public bool IsEqualTo(IDynamicDataSerializableItem item)
		{
			bool flag = item == null || !(item is DynamicDataItemBase<T>);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				DynamicDataItemBase<T> dynamicDataItemBase = (DynamicDataItemBase<T>)item;
				DynamicDataStorageItem dynamicDataStorageItem = this.WriteToStorage();
				DynamicDataStorageItem item2 = dynamicDataItemBase.WriteToStorage();
				result = dynamicDataStorageItem.IsEqualTo(item2);
			}
			return result;
		}

		// Token: 0x06001BE4 RID: 7140 RVA: 0x0001F928 File Offset: 0x0001DB28
		public void Clear()
		{
			this.Value = default(T);
		}

		// Token: 0x06001BE5 RID: 7141 RVA: 0x0000D55A File Offset: 0x0000B75A
		public DynamicDataItemBase()
		{
		}

		// Token: 0x06001BE6 RID: 7142 RVA: 0x0001F946 File Offset: 0x0001DB46
		public DynamicDataItemBase(DynamicField field)
		{
			this.Field = field;
		}

		// Token: 0x06001BE7 RID: 7143 RVA: 0x0001F958 File Offset: 0x0001DB58
		public DynamicDataItemBase(DynamicField field, T val)
		{
			this.Field = field;
			this.Value = val;
		}
	}
}
