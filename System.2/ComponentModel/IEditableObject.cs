using System;

namespace System.ComponentModel
{
	// Token: 0x02000561 RID: 1377
	[__DynamicallyInvokable]
	public interface IEditableObject
	{
		// Token: 0x060033A5 RID: 13221
		[__DynamicallyInvokable]
		void BeginEdit();

		// Token: 0x060033A6 RID: 13222
		[__DynamicallyInvokable]
		void EndEdit();

		// Token: 0x060033A7 RID: 13223
		[__DynamicallyInvokable]
		void CancelEdit();
	}
}
