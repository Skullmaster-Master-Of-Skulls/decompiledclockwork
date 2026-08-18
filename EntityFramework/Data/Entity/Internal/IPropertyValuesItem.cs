using System;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000770 RID: 1904
	internal interface IPropertyValuesItem
	{
		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06005657 RID: 22103
		// (set) Token: 0x06005658 RID: 22104
		object Value { get; set; }

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06005659 RID: 22105
		string Name { get; }

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x0600565A RID: 22106
		bool IsComplex { get; }

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x0600565B RID: 22107
		Type Type { get; }
	}
}
