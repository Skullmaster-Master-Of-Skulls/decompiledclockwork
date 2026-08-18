using System;
using System.Drawing;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000540 RID: 1344
	[SupportsEventValidation]
	internal sealed class DataGridLinkButton : LinkButton
	{
		// Token: 0x06004231 RID: 16945 RVA: 0x0011226C File Offset: 0x0011126C
		internal DataGridLinkButton()
		{
		}

		// Token: 0x06004232 RID: 16946 RVA: 0x00112274 File Offset: 0x00111274
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.SetForeColor();
			base.Render(writer);
		}

		// Token: 0x06004233 RID: 16947 RVA: 0x00112284 File Offset: 0x00111284
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
