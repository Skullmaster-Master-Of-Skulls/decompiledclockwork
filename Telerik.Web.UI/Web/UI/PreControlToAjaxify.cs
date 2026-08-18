using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000FD9 RID: 4057
	internal class PreControlToAjaxify : Control
	{
		// Token: 0x06009DA2 RID: 40354 RVA: 0x00232951 File Offset: 0x00230B51
		public PreControlToAjaxify(OurUpdatePanel updatePanel)
		{
			this.EnableViewState = false;
			this._updatePanel = updatePanel;
		}

		// Token: 0x06009DA3 RID: 40355 RVA: 0x00232968 File Offset: 0x00230B68
		protected override void Render(HtmlTextWriter writer)
		{
			if (this._updatePanel.Page != null)
			{
				RadAjaxControl ajaxControl = this.GetAjaxControl(this._updatePanel);
				if (!ajaxControl.renderedPanels.ContainsKey(this._updatePanel.UniqueID))
				{
					ajaxControl.renderedPanels.Add(this._updatePanel.UniqueID, this._updatePanel);
					this._updatePanel.RenderControl(writer);
				}
			}
		}

		// Token: 0x06009DA4 RID: 40356 RVA: 0x002329D0 File Offset: 0x00230BD0
		internal RadAjaxControl GetAjaxControl(OurUpdatePanel panel)
		{
			for (Control control = panel; control != null; control = control.Parent)
			{
				RadAjaxControl radAjaxControl = control as RadAjaxControl;
				if (radAjaxControl != null)
				{
					return radAjaxControl;
				}
			}
			return null;
		}

		// Token: 0x04002C5F RID: 11359
		private OurUpdatePanel _updatePanel;
	}
}
