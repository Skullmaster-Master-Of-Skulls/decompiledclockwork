using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AjaxControlToolkit
{
	// Token: 0x0200018A RID: 394
	[ToolboxItem(false)]
	public class SeadragonScalableOverlay : SeadragonOverlay
	{
		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0001C73B File Offset: 0x0001A93B
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SeadragonRect Rect
		{
			get
			{
				if (this.rect == null)
				{
					this.rect = new SeadragonRect();
				}
				return this.rect;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0001C75E File Offset: 0x0001A95E
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x0001C761 File Offset: 0x0001A961
		[DefaultValue(SeadragonOverlayPlacement.TopLeft)]
		[Browsable(false)]
		public sealed override SeadragonOverlayPlacement Placement
		{
			get
			{
				return SeadragonOverlayPlacement.TopLeft;
			}
			[CompilerGenerated]
			set
			{
				base.Placement = value;
			}
		}

		// Token: 0x0400042B RID: 1067
		private SeadragonRect rect;
	}
}
