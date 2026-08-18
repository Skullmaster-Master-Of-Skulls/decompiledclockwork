using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020005F1 RID: 1521
	public interface IInheritanceService
	{
		// Token: 0x0600383C RID: 14396
		void AddInheritedComponents(IComponent component, IContainer container);

		// Token: 0x0600383D RID: 14397
		InheritanceAttribute GetInheritanceAttribute(IComponent component);
	}
}
