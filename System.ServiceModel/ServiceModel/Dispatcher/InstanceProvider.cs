using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000546 RID: 1350
	internal class InstanceProvider : IInstanceProvider
	{
		// Token: 0x06003363 RID: 13155 RVA: 0x000C6A20 File Offset: 0x000C4C20
		internal InstanceProvider(CreateInstanceDelegate creator)
		{
			if (creator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("creator");
			}
			this.creator = creator;
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x000C6A42 File Offset: 0x000C4C42
		public object GetInstance(InstanceContext instanceContext)
		{
			return this.creator();
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000C6A4F File Offset: 0x000C4C4F
		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			return this.creator();
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000C6A5C File Offset: 0x000C4C5C
		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			IDisposable disposable = instance as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}

		// Token: 0x04002787 RID: 10119
		private CreateInstanceDelegate creator;
	}
}
