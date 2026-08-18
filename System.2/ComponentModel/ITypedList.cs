using System;

namespace System.ComponentModel
{
	// Token: 0x0200057B RID: 1403
	public interface ITypedList
	{
		// Token: 0x060033F9 RID: 13305
		string GetListName(PropertyDescriptor[] listAccessors);

		// Token: 0x060033FA RID: 13306
		PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
	}
}
