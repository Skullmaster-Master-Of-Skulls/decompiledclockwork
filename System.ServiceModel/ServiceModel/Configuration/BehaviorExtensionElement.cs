using System;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005F3 RID: 1523
	public abstract class BehaviorExtensionElement : ServiceModelExtensionElement
	{
		// Token: 0x06003AA7 RID: 15015
		protected internal abstract object CreateBehavior();

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06003AA8 RID: 15016
		public abstract Type BehaviorType { get; }
	}
}
