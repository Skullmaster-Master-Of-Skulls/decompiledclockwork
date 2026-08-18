using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200057B RID: 1403
	[__DynamicallyInvokable]
	public interface IParameterInspector
	{
		// Token: 0x06003651 RID: 13905
		[__DynamicallyInvokable]
		object BeforeCall(string operationName, object[] inputs);

		// Token: 0x06003652 RID: 13906
		[__DynamicallyInvokable]
		void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState);
	}
}
