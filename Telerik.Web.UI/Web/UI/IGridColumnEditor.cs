using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000363 RID: 867
	public interface IGridColumnEditor
	{
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06001DD9 RID: 7641
		Control ContainerControl { get; }

		// Token: 0x06001DDA RID: 7642
		void InitializeInControl(Control containerControl);

		// Token: 0x06001DDB RID: 7643
		void InitializeFromControl(Control containerControl);

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06001DDC RID: 7644
		bool IsInitialized { get; }

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06001DDD RID: 7645
		bool IsInEditMode { get; }

		// Token: 0x06001DDE RID: 7646
		void SetOwner(IGridEditableColumn owner);
	}
}
