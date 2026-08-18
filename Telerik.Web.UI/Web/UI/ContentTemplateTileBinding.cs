using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020008F2 RID: 2290
	[DefaultProperty("DataTextField")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ContentTemplateTileBinding : StateManager
	{
		// Token: 0x17001C9E RID: 7326
		// (get) Token: 0x06005689 RID: 22153 RVA: 0x00108FDD File Offset: 0x001071DD
		// (set) Token: 0x0600568A RID: 22154 RVA: 0x00108FE5 File Offset: 0x001071E5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(RadContentTemplateTile))]
		public ITemplate ContentTemplate
		{
			get
			{
				return this._contentTemplate;
			}
			set
			{
				this._contentTemplate = value;
			}
		}

		// Token: 0x17001C9F RID: 7327
		// (get) Token: 0x0600568B RID: 22155 RVA: 0x00108FEE File Offset: 0x001071EE
		// (set) Token: 0x0600568C RID: 22156 RVA: 0x0010900E File Offset: 0x0010720E
		[Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Gets or sets the HTML template, which will be used as ContentTemplate property value of the tile after it is bound to client datasource item.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual string ClientContentTemplate
		{
			get
			{
				return (base.ViewState["ClientContentTemplate"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ClientContentTemplate"] = value;
			}
		}

		// Token: 0x17001CA0 RID: 7328
		// (get) Token: 0x0600568D RID: 22157 RVA: 0x00109021 File Offset: 0x00107221
		// (set) Token: 0x0600568E RID: 22158 RVA: 0x00109041 File Offset: 0x00107241
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string DataClientContentTemplateField
		{
			get
			{
				return (string)(base.ViewState["DataClientContentTemplateField"] ?? string.Empty);
			}
			set
			{
				base.ViewState["DataClientContentTemplateField"] = value;
			}
		}

		// Token: 0x04001526 RID: 5414
		private ITemplate _contentTemplate;
	}
}
