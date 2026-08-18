using System;

namespace System.ServiceModel
{
	// Token: 0x020000F5 RID: 245
	[__DynamicallyInvokable]
	public interface IExtensibleObject<T> where T : IExtensibleObject<T>
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600052D RID: 1325
		[__DynamicallyInvokable]
		IExtensionCollection<T> Extensions { [__DynamicallyInvokable] get; }
	}
}
