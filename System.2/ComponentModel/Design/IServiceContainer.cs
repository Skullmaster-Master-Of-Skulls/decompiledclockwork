using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	// Token: 0x020005F7 RID: 1527
	[ComVisible(true)]
	public interface IServiceContainer : IServiceProvider
	{
		// Token: 0x06003859 RID: 14425
		void AddService(Type serviceType, object serviceInstance);

		// Token: 0x0600385A RID: 14426
		void AddService(Type serviceType, object serviceInstance, bool promote);

		// Token: 0x0600385B RID: 14427
		void AddService(Type serviceType, ServiceCreatorCallback callback);

		// Token: 0x0600385C RID: 14428
		void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote);

		// Token: 0x0600385D RID: 14429
		void RemoveService(Type serviceType);

		// Token: 0x0600385E RID: 14430
		void RemoveService(Type serviceType, bool promote);
	}
}
