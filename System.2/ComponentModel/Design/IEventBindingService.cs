using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	// Token: 0x020005ED RID: 1517
	[ComVisible(true)]
	public interface IEventBindingService
	{
		// Token: 0x0600382A RID: 14378
		string CreateUniqueMethodName(IComponent component, EventDescriptor e);

		// Token: 0x0600382B RID: 14379
		ICollection GetCompatibleMethods(EventDescriptor e);

		// Token: 0x0600382C RID: 14380
		EventDescriptor GetEvent(PropertyDescriptor property);

		// Token: 0x0600382D RID: 14381
		PropertyDescriptorCollection GetEventProperties(EventDescriptorCollection events);

		// Token: 0x0600382E RID: 14382
		PropertyDescriptor GetEventProperty(EventDescriptor e);

		// Token: 0x0600382F RID: 14383
		bool ShowCode();

		// Token: 0x06003830 RID: 14384
		bool ShowCode(int lineNumber);

		// Token: 0x06003831 RID: 14385
		bool ShowCode(IComponent component, EventDescriptor e);
	}
}
