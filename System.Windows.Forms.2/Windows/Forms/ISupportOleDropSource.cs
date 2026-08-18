using System;

namespace System.Windows.Forms
{
	// Token: 0x020002A3 RID: 675
	internal interface ISupportOleDropSource
	{
		// Token: 0x06002A31 RID: 10801
		void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent);

		// Token: 0x06002A32 RID: 10802
		void OnGiveFeedback(GiveFeedbackEventArgs gfbevent);
	}
}
