using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200110F RID: 4367
	[ToolboxItem(false)]
	public class GridImageButton : ImageButton
	{
		// Token: 0x0600B2CD RID: 45773 RVA: 0x0026E07A File Offset: 0x0026C27A
		public GridImageButton(GridColumn ownerColumn)
		{
			this.owner = ownerColumn;
		}

		// Token: 0x0600B2CE RID: 45774 RVA: 0x0026E089 File Offset: 0x0026C289
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if (!this.owner.ShowSortIcon)
			{
				writer.AddAttribute("style", "display:none");
			}
		}

		// Token: 0x170039E9 RID: 14825
		// (get) Token: 0x0600B2CF RID: 45775 RVA: 0x0026E0AF File Offset: 0x0026C2AF
		// (set) Token: 0x0600B2D0 RID: 45776 RVA: 0x0026E0CB File Offset: 0x0026C2CB
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

		// Token: 0x04002F20 RID: 12064
		private GridColumn owner;
	}
}
