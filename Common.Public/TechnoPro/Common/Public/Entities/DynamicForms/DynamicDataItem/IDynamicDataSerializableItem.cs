using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataItem
{
	// Token: 0x02000387 RID: 903
	public interface IDynamicDataSerializableItem
	{
		// Token: 0x06001BF9 RID: 7161
		void ReadFromStorage(DynamicDataStorageItem item);

		// Token: 0x06001BFA RID: 7162
		DynamicDataStorageItem WriteToStorage();

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06001BFB RID: 7163
		// (set) Token: 0x06001BFC RID: 7164
		DynamicField Field { get; set; }

		// Token: 0x06001BFD RID: 7165
		bool IsEqualTo(IDynamicDataSerializableItem item);
	}
}
