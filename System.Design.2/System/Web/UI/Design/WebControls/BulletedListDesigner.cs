using System;
using System.Collections;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000A1 RID: 161
	public class BulletedListDesigner : ListControlDesigner
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00003B0F File Offset: 0x00001D0F
		protected override bool UsePreviewControl
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00016D68 File Offset: 0x00014F68
		protected override void PostFilterEvents(IDictionary events)
		{
			base.PostFilterEvents(events);
			events.Remove("SelectedIndexChanged");
		}
	}
}
