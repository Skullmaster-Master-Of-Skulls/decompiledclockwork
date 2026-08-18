using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x0200029D RID: 669
	public interface IControlDesignerAccessor
	{
		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x06001F7E RID: 8062
		IDictionary UserData { get; }

		// Token: 0x06001F7F RID: 8063
		IDictionary GetDesignModeState();

		// Token: 0x06001F80 RID: 8064
		void SetDesignModeState(IDictionary data);

		// Token: 0x06001F81 RID: 8065
		void SetOwnerControl(Control owner);
	}
}
