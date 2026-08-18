using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200109A RID: 4250
	public abstract class GridBoolColumnEditor : GridColumnEditorBase
	{
		// Token: 0x0600ACBA RID: 44218 RVA: 0x0025205C File Offset: 0x0025025C
		public GridBoolColumnEditor()
		{
		}

		// Token: 0x170037D0 RID: 14288
		// (get) Token: 0x0600ACBB RID: 44219
		// (set) Token: 0x0600ACBC RID: 44220
		public abstract bool Value { get; set; }
	}
}
