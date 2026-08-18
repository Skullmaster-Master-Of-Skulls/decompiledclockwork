using System;
using System.ComponentModel;

namespace AjaxControlToolkit
{
	// Token: 0x02000186 RID: 390
	[ToolboxItem(false)]
	public class SeadragonFixedOverlay : SeadragonOverlay
	{
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0001C67D File Offset: 0x0001A87D
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SeadragonPoint Point
		{
			get
			{
				if (this.point == null)
				{
					this.point = new SeadragonPoint();
				}
				return this.point;
			}
		}

		// Token: 0x0400041B RID: 1051
		private SeadragonPoint point;
	}
}
