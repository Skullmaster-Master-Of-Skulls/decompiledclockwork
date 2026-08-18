using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x0200136E RID: 4974
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Miscellaneous")]
	[ToolboxBitmap(typeof(RadScriptBlock), "Telerik.Web.UI.Ajax.png")]
	public class RadScriptBlock : Control
	{
		// Token: 0x0600CFA1 RID: 53153 RVA: 0x002E0D78 File Offset: 0x002DEF78
		protected override void Render(HtmlTextWriter writer)
		{
			if (!base.DesignMode)
			{
				ScriptManager scriptManager = ScriptRegistrar.GetScriptManager(this);
				if (scriptManager.IsInAsyncPostBack)
				{
					if (this.Page != null && this.Page.Header != null && this.IsChildOf(this, this.Page.Header))
					{
						this.RegisterInScriptManager(this.Page, typeof(Page));
						return;
					}
					this.RegisterInScriptManager(this, typeof(RadScriptBlock));
					return;
				}
			}
			base.Render(writer);
		}

		// Token: 0x0600CFA2 RID: 53154 RVA: 0x002E0DF8 File Offset: 0x002DEFF8
		internal void RegisterInScriptManager(Control control, Type type)
		{
			if (!this.Registered)
			{
				this.Registered = true;
				StringBuilder stringBuilder = new StringBuilder();
				base.Render(new HtmlTextWriter(new StringWriter(stringBuilder)));
				string script = stringBuilder.ToString();
				ScriptManager.RegisterClientScriptBlock(control, type, this.UniqueID, script, false);
			}
		}

		// Token: 0x0600CFA3 RID: 53155 RVA: 0x002E0E44 File Offset: 0x002DF044
		private bool IsChildOf(Control controlToCheck, Control parent)
		{
			for (Control control = controlToCheck; control != null; control = control.Parent)
			{
				if (control == parent)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040037AD RID: 14253
		private bool Registered;
	}
}
