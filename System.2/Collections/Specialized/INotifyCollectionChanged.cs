using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Specialized
{
	// Token: 0x020003AB RID: 939
	[TypeForwardedFrom("WindowsBase, Version=3.0.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[__DynamicallyInvokable]
	public interface INotifyCollectionChanged
	{
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600231A RID: 8986
		// (remove) Token: 0x0600231B RID: 8987
		[__DynamicallyInvokable]
		event NotifyCollectionChangedEventHandler CollectionChanged;
	}
}
