using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;
using Telerik.Web.UI.PersistenceFramework;

namespace Telerik.Web.UI
{
	// Token: 0x02000496 RID: 1174
	[PersistChildren(false)]
	[ToolboxData("<{0}:RadPersistenceManagerProxy runat=\"server\"></{0}:RadPersistenceManagerProxy>")]
	[ParseChildren(true)]
	[Designer("Telerik.Web.Design.RadPersistenceManagerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[TelerikToolboxCategory("Data")]
	[ToolboxBitmap(typeof(RadDropDownTree), "Telerik.Web.UI.PersistenceManager.png")]
	public class RadPersistenceManagerProxy : Control
	{
		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x060029CA RID: 10698 RVA: 0x00086AD6 File Offset: 0x00084CD6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Data")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public PersistenceSettingsCollection PersistenceSettings
		{
			get
			{
				return this.persistenceSettings;
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x060029CB RID: 10699 RVA: 0x00086ADE File Offset: 0x00084CDE
		// (set) Token: 0x060029CC RID: 10700 RVA: 0x00086AE6 File Offset: 0x00084CE6
		[DefaultValue("TelerikAspNetRadControlsPersistedState")]
		public string UniqueKey { get; set; }

		// Token: 0x060029CD RID: 10701 RVA: 0x00086AEF File Offset: 0x00084CEF
		public RadPersistenceManagerProxy()
		{
			this.persistenceSettings = new PersistenceSettingsCollection();
		}

		// Token: 0x060029CE RID: 10702 RVA: 0x00086B02 File Offset: 0x00084D02
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.PreLoad += this.Page_PreLoad;
		}

		// Token: 0x060029CF RID: 10703 RVA: 0x00086B24 File Offset: 0x00084D24
		private void Page_PreLoad(object sender, EventArgs e)
		{
			if (!base.DesignMode)
			{
				RadPersistenceManager current = RadPersistenceManager.GetCurrent(this.Page);
				if (current != null)
				{
					current.RegisterStatePersisterProxy(this);
				}
			}
			this.Page.PreLoad -= this.Page_PreLoad;
		}

		// Token: 0x04000AB9 RID: 2745
		private PersistenceSettingsCollection persistenceSettings;
	}
}
