using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using Telerik.Licensing;

namespace Telerik.Web.UI
{
	// Token: 0x02000FDE RID: 4062
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[PersistChildren(false)]
	[ToolboxBitmap(typeof(RadAjaxManagerProxy), "Telerik.Web.UI.Ajax.png")]
	[TelerikToolboxCategory("Miscellaneous")]
	[ParseChildren(true)]
	[Designer("Telerik.Web.Design.RadAjaxManagerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadAjaxManagerProxy : Control
	{
		// Token: 0x06009E12 RID: 40466 RVA: 0x00233E17 File Offset: 0x00232017
		public RadAjaxManagerProxy()
		{
			this.ajaxSettings = new AjaxSettingsCollection();
		}

		// Token: 0x170031FE RID: 12798
		// (get) Token: 0x06009E13 RID: 40467 RVA: 0x00233E2A File Offset: 0x0023202A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("Telerik.Web.Design.AjaxSettingsTypeEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Control Configuration")]
		[Category("Data")]
		public AjaxSettingsCollection AjaxSettings
		{
			get
			{
				return this.ajaxSettings;
			}
		}

		// Token: 0x06009E14 RID: 40468 RVA: 0x00233E34 File Offset: 0x00232034
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			if (!base.DesignMode)
			{
				RadAjaxManager current = RadAjaxManager.GetCurrent(this.Page);
				if (current != null)
				{
					current.RegisterProxy(this);
				}
			}
		}

		// Token: 0x04002C71 RID: 11377
		internal AjaxSettingsCollection ajaxSettings;
	}
}
