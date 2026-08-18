using System;
using System.Collections;

namespace System.Web.UI.Design
{
	// Token: 0x02000047 RID: 71
	public interface IContentResolutionService
	{
		// Token: 0x0600026B RID: 619
		ContentDesignerState GetContentDesignerState(string identifier);

		// Token: 0x0600026C RID: 620
		void SetContentDesignerState(string identifier, ContentDesignerState state);

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600026D RID: 621
		IDictionary ContentDefinitions { get; }
	}
}
