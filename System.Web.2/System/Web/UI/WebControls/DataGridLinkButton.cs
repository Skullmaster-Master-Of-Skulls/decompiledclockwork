using System;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003C3 RID: 963
	[SupportsEventValidation]
	internal sealed class DataGridLinkButton : LinkButton
	{
		// Token: 0x06002E76 RID: 11894 RVA: 0x000982D4 File Offset: 0x000964D4
		internal DataGridLinkButton()
		{
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x000982DC File Offset: 0x000964DC
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.SetForeColor();
			base.Render(writer);
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x000982EC File Offset: 0x000964EC
		private void SetForeColor()
		{
			if (!base.ControlStyle.IsSet(4))
			{
				Control control = this;
				for (int i = 0; i < 3; i++)
				{
					control = control.Parent;
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
