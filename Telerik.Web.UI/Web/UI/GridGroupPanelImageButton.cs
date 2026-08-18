using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001110 RID: 4368
	[ToolboxItem(false)]
	public class GridGroupPanelImageButton : ImageButton
	{
		// Token: 0x0600B2D1 RID: 45777 RVA: 0x0026E108 File Offset: 0x0026C308
		public GridGroupPanelImageButton()
		{
			this.CausesValidation = false;
			base.Style["cursor"] = "pointer";
		}

		// Token: 0x170039EA RID: 14826
		// (get) Token: 0x0600B2D2 RID: 45778 RVA: 0x0026E12C File Offset: 0x0026C32C
		// (set) Token: 0x0600B2D3 RID: 45779 RVA: 0x0026E148 File Offset: 0x0026C348
		[DefaultValue(typeof(Unit), "0px")]
		public override Unit BorderWidth
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return Unit.Pixel(0);
				}
				return base.ControlStyle.BorderWidth;
			}
			set
			{
				if (value.Value < 0.0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value.Value > 0.0)
				{
					base.ControlStyle.BorderWidth = value;
				}
			}
		}
	}
}
