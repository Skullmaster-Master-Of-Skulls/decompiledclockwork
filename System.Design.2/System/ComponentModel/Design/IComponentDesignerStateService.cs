using System;

namespace System.ComponentModel.Design
{
	// Token: 0x020001B1 RID: 433
	public interface IComponentDesignerStateService
	{
		// Token: 0x06000FCF RID: 4047
		object GetState(IComponent component, string key);

		// Token: 0x06000FD0 RID: 4048
		void SetState(IComponent component, string key, object value);
	}
}
