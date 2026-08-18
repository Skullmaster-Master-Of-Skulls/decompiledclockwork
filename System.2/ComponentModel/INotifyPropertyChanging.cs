using System;

namespace System.ComponentModel
{
	// Token: 0x0200056B RID: 1387
	[__DynamicallyInvokable]
	public interface INotifyPropertyChanging
	{
		// Token: 0x1400004F RID: 79
		// (add) Token: 0x060033BD RID: 13245
		// (remove) Token: 0x060033BE RID: 13246
		[__DynamicallyInvokable]
		event PropertyChangingEventHandler PropertyChanging;
	}
}
