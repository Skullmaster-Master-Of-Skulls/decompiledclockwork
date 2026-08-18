using System;

namespace System.ServiceModel
{
	// Token: 0x020000F6 RID: 246
	[__DynamicallyInvokable]
	public interface IExtension<T> where T : IExtensibleObject<T>
	{
		// Token: 0x0600052E RID: 1326
		[__DynamicallyInvokable]
		void Attach(T owner);

		// Token: 0x0600052F RID: 1327
		[__DynamicallyInvokable]
		void Detach(T owner);
	}
}
