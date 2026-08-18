using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200110E RID: 4366
	[ToolboxItem(false)]
	public class GridImage : Image
	{
		// Token: 0x0600B2C9 RID: 45769 RVA: 0x0026DFAF File Offset: 0x0026C1AF
		public GridImage(GridColumn ownerColumn)
		{
			this.owner = ownerColumn;
		}

		// Token: 0x0600B2CA RID: 45770 RVA: 0x0026DFC0 File Offset: 0x0026C1C0
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.owner != null && this.owner.sortIcon != null)
			{
				this.ImageUrl = (this.owner.sortIcon as Image).ImageUrl;
			}
			base.AddAttributesToRender(writer);
			if (!this.owner.ShowSortIcon)
			{
				writer.AddAttribute("style", "display:none");
			}
		}

		// Token: 0x170039E8 RID: 14824
		// (get) Token: 0x0600B2CB RID: 45771 RVA: 0x0026E021 File Offset: 0x0026C221
		// (set) Token: 0x0600B2CC RID: 45772 RVA: 0x0026E03D File Offset: 0x0026C23D
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

		// Token: 0x04002F1F RID: 12063
		private GridColumn owner;
	}
}
