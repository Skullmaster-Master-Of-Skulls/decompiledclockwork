using System;
using System.ComponentModel;

namespace System.Windows.Forms.Design
{
	// Token: 0x020002FC RID: 764
	internal interface IOleDragClient
	{
		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001E5E RID: 7774
		IComponent Component { get; }

		// Token: 0x06001E5F RID: 7775
		bool AddComponent(IComponent component, string name, bool firstAdd);

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001E60 RID: 7776
		bool CanModifyComponents { get; }

		// Token: 0x06001E61 RID: 7777
		bool IsDropOk(IComponent component);

		// Token: 0x06001E62 RID: 7778
		Control GetDesignerControl();

		// Token: 0x06001E63 RID: 7779
		Control GetControlForComponent(object component);
	}
}
