using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000542 RID: 1346
	public interface ITrackingPersonalizable
	{
		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x060044C6 RID: 17606
		bool TracksChanges { get; }

		// Token: 0x060044C7 RID: 17607
		void BeginLoad();

		// Token: 0x060044C8 RID: 17608
		void BeginSave();

		// Token: 0x060044C9 RID: 17609
		void EndLoad();

		// Token: 0x060044CA RID: 17610
		void EndSave();
	}
}
