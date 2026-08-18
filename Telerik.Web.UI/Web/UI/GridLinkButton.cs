using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001152 RID: 4434
	internal class GridLinkButton : LinkButton
	{
		// Token: 0x0600B49E RID: 46238 RVA: 0x0027C680 File Offset: 0x0027A880
		protected override void Render(HtmlTextWriter writer)
		{
			this.SetForeColor();
			base.Render(writer);
		}

		// Token: 0x0600B49F RID: 46239 RVA: 0x0027C690 File Offset: 0x0027A890
		private void SetForeColor()
		{
			if (base.ControlStyle.ForeColor.IsEmpty)
			{
				Control control = this;
				for (int i = 0; i < 3; i++)
				{
					control = control.Parent;
					if (control is UserControl)
					{
						control = control.Parent;
					}
					Color foreColor = ((WebControl)control).ForeColor;
					if (foreColor != Color.Empty)
					{
						this.ForeColor = foreColor;
						return;
					}
				}
			}
		}
	}
}
