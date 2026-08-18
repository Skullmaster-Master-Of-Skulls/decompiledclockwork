using System;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006DB RID: 1755
	public sealed class SynchronousReceiveElement : BehaviorExtensionElement
	{
		// Token: 0x060043DD RID: 17373 RVA: 0x001005CB File Offset: 0x000FE7CB
		protected internal override object CreateBehavior()
		{
			return new SynchronousReceiveBehavior();
		}

		// Token: 0x17001192 RID: 4498
		// (get) Token: 0x060043DE RID: 17374 RVA: 0x001005D2 File Offset: 0x000FE7D2
		public override Type BehaviorType
		{
			get
			{
				return typeof(SynchronousReceiveBehavior);
			}
		}
	}
}
