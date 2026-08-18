using System;
using System.Reflection;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200057E RID: 1406
	[__DynamicallyInvokable]
	public interface IClientOperationSelector
	{
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06003657 RID: 13911
		[__DynamicallyInvokable]
		bool AreParametersRequiredForSelection { [__DynamicallyInvokable] get; }

		// Token: 0x06003658 RID: 13912
		[__DynamicallyInvokable]
		string SelectOperation(MethodBase method, object[] parameters);
	}
}
