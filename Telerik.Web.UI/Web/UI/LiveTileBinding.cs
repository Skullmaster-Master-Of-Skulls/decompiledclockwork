using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020008F3 RID: 2291
	[DefaultProperty("DataTextField")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class LiveTileBinding : StateManager
	{
		// Token: 0x17001CA1 RID: 7329
		// (get) Token: 0x06005690 RID: 22160 RVA: 0x0010905C File Offset: 0x0010725C
		// (set) Token: 0x06005691 RID: 22161 RVA: 0x0010907C File Offset: 0x0010727C
		[Description("Gets or sets the HTML template that will be instantiated in the tile after live data request.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		public virtual string ClientTemplate
		{
			get
			{
				return (base.ViewState["TileClientTemplate"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["TileClientTemplate"] = value;
			}
		}

		// Token: 0x17001CA2 RID: 7330
		// (get) Token: 0x06005692 RID: 22162 RVA: 0x0010908F File Offset: 0x0010728F
		// (set) Token: 0x06005693 RID: 22163 RVA: 0x001090AF File Offset: 0x001072AF
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public string DataClientTemplateField
		{
			get
			{
				return (string)(base.ViewState["DataClientTemplateField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataClientTemplateField"] = value;
			}
		}

		// Token: 0x17001CA3 RID: 7331
		// (get) Token: 0x06005694 RID: 22164 RVA: 0x001090C2 File Offset: 0x001072C2
		// (set) Token: 0x06005695 RID: 22165 RVA: 0x001090E2 File Offset: 0x001072E2
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DataUpdateIntervalField
		{
			get
			{
				return (string)(base.ViewState["DataUpdateIntervalField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataUpdateIntervalField"] = value;
			}
		}
	}
}
