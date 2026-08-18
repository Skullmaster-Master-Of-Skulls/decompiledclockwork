using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using System.Web.UI;
using Telerik.Web.UI.Upload;

namespace Telerik.Web.UI
{
	// Token: 0x02001349 RID: 4937
	[ClientScriptResource("Telerik.Web.UI.RadProgressManager", "Telerik.Web.UI.Upload.RadProgressManager.js")]
	[Designer("Telerik.Web.Design.RadProgressManagerDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[EmbeddedSkin("Upload", "Default", typeof(RadUpload))]
	[TelerikToolboxCategory("Upload")]
	[ToolboxBitmap(typeof(RadProgressManager), "Telerik.Web.UI.Upload.png")]
	[ToolboxData("<{0}:RadProgressManager Runat=server></{0}:RadProgressManager>")]
	[EmbeddedSkin("Upload", typeof(RadUpload))]
	public class RadProgressManager : RadWebControl
	{
		// Token: 0x1700422A RID: 16938
		// (get) Token: 0x0600CDD9 RID: 52697 RVA: 0x002DCEBD File Offset: 0x002DB0BD
		// (set) Token: 0x0600CDDA RID: 52698 RVA: 0x002DCEDD File Offset: 0x002DB0DD
		[DefaultValue("~/Telerik.RadUploadProgressHandler.ashx")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Behavior")]
		[Bindable(true)]
		[Description("Gets or sets the URL which the AJAX calls will be made to. Check the help for more information.")]
		public string AjaxUrl
		{
			get
			{
				return ((string)this.ViewState["AjaxUrl"]) ?? "~/Telerik.RadUploadProgressHandler.ashx";
			}
			set
			{
				this.ViewState["AjaxUrl"] = value;
			}
		}

		// Token: 0x1700422B RID: 16939
		// (get) Token: 0x0600CDDB RID: 52699 RVA: 0x002DCEF0 File Offset: 0x002DB0F0
		// (set) Token: 0x0600CDDC RID: 52700 RVA: 0x002DCF1D File Offset: 0x002DB11D
		[Category("Behavior")]
		[ClientPropertyName("progressStarted")]
		[DefaultValue("")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Bindable(true)]
		public string OnClientProgressStarted
		{
			get
			{
				string text = this.ViewState["OnClientProgressStarted"] as string;
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["OnClientProgressStarted"] = value;
			}
		}

		// Token: 0x1700422C RID: 16940
		// (get) Token: 0x0600CDDD RID: 52701 RVA: 0x002DCF30 File Offset: 0x002DB130
		// (set) Token: 0x0600CDDE RID: 52702 RVA: 0x002DCF5D File Offset: 0x002DB15D
		[ClientPropertyName("progressUpdating")]
		[DefaultValue("")]
		[Bindable(true)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Behavior")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientProgressUpdating
		{
			get
			{
				string text = this.ViewState["OnClientProgressUpdating"] as string;
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["OnClientProgressUpdating"] = value;
			}
		}

		// Token: 0x1700422D RID: 16941
		// (get) Token: 0x0600CDDF RID: 52703 RVA: 0x002DCF70 File Offset: 0x002DB170
		// (set) Token: 0x0600CDE0 RID: 52704 RVA: 0x002DCF9D File Offset: 0x002DB19D
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[ClientControlEvent]
		[Bindable(true)]
		[ClientPropertyName("submitting")]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientSubmitting
		{
			get
			{
				string text = this.ViewState["OnClientSubmitting"] as string;
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["OnClientSubmitting"] = value;
			}
		}

		// Token: 0x1700422E RID: 16942
		// (get) Token: 0x0600CDE1 RID: 52705 RVA: 0x002DCFB0 File Offset: 0x002DB1B0
		// (set) Token: 0x0600CDE2 RID: 52706 RVA: 0x002DCFDE File Offset: 0x002DB1DE
		[DefaultValue(true)]
		[ClientControlProperty]
		[ClientPropertyName("shouldRegisterForSubmit")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Behavior")]
		[Bindable(true)]
		public bool RegisterForSubmit
		{
			get
			{
				object obj = this.ViewState["RegisterForSubmit"];
				return !(obj is bool) || (bool)obj;
			}
			set
			{
				this.ViewState["RegisterForSubmit"] = value;
			}
		}

		// Token: 0x1700422F RID: 16943
		// (get) Token: 0x0600CDE3 RID: 52707 RVA: 0x002DCFF6 File Offset: 0x002DB1F6
		// (set) Token: 0x0600CDE4 RID: 52708 RVA: 0x002DD017 File Offset: 0x002DB217
		[DefaultValue(true)]
		[Bindable(true)]
		[Obsolete("Memory optimization is implemented as part of the .NET Framework and is no longer a feature of RadUpload", false)]
		[Category("Behavior")]
		public bool EnableMemoryOptimization
		{
			get
			{
				return (bool)(this.ViewState["EnableMemoryOptimization"] ?? true);
			}
			set
			{
				this.ViewState["EnableMemoryOptimization"] = value;
			}
		}

		// Token: 0x17004230 RID: 16944
		// (get) Token: 0x0600CDE5 RID: 52709 RVA: 0x002DD030 File Offset: 0x002DB230
		// (set) Token: 0x0600CDE6 RID: 52710 RVA: 0x002DD05E File Offset: 0x002DB25E
		[PersistenceMode(PersistenceMode.Attribute)]
		[DefaultValue(false)]
		[Category("Behavior")]
		[Bindable(true)]
		[ClientControlProperty]
		[ClientPropertyName("suppressMissingHttpModuleError")]
		public bool SuppressMissingHttpModuleError
		{
			get
			{
				object obj = this.ViewState["SuppressMissingHttpModuleError"];
				return obj is bool && (bool)obj;
			}
			set
			{
				this.ViewState["SuppressMissingHttpModuleError"] = value;
			}
		}

		// Token: 0x17004231 RID: 16945
		// (get) Token: 0x0600CDE7 RID: 52711 RVA: 0x002DD078 File Offset: 0x002DB278
		// (set) Token: 0x0600CDE8 RID: 52712 RVA: 0x002DD0AA File Offset: 0x002DB2AA
		[Bindable(true)]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[ClientControlProperty]
		[DefaultValue(500)]
		[Description("Gets or sets the period (in milliseconds) of the progress data refresh.")]
		public int RefreshPeriod
		{
			get
			{
				object obj = this.ViewState["RefreshPeriod"];
				if (!(obj is int))
				{
					return 500;
				}
				return (int)obj;
			}
			set
			{
				this.ViewState["RefreshPeriod"] = value;
			}
		}

		// Token: 0x0600CDE9 RID: 52713 RVA: 0x002DD0C2 File Offset: 0x002DB2C2
		public static bool IsRegisteredOnPage(Page page)
		{
			return page.Items["Telerik.WebControls.RadProgressManager"] != null && (bool)page.Items["Telerik.WebControls.RadProgressManager"];
		}

		// Token: 0x17004232 RID: 16946
		// (get) Token: 0x0600CDEA RID: 52714 RVA: 0x002DD0F0 File Offset: 0x002DB2F0
		internal static bool AllowCustomProgress
		{
			get
			{
				string text = ConfigurationManager.AppSettings.Get("AllowCustomProgress");
				bool result = true;
				if (text != null)
				{
					bool.TryParse(text, out result);
				}
				return result;
			}
		}

		// Token: 0x17004233 RID: 16947
		// (get) Token: 0x0600CDEB RID: 52715 RVA: 0x002DD11C File Offset: 0x002DB31C
		// (set) Token: 0x0600CDEC RID: 52716 RVA: 0x002DD124 File Offset: 0x002DB324
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Obsolete("The UniquePageIdentifier property is now obsolete. The value is generated automatically.", false)]
		public string UniquePageIdentifier
		{
			get
			{
				return this.UploadRequestIdentifier;
			}
			set
			{
				this.UploadRequestIdentifier = value;
			}
		}

		// Token: 0x17004234 RID: 16948
		// (get) Token: 0x0600CDED RID: 52717 RVA: 0x002DD130 File Offset: 0x002DB330
		// (set) Token: 0x0600CDEE RID: 52718 RVA: 0x002DD164 File Offset: 0x002DB364
		[ClientControlProperty]
		[ClientPropertyName("pageGUID")]
		private string UploadRequestIdentifier
		{
			get
			{
				if (this._uploadRequestIdentifier == null)
				{
					this._uploadRequestIdentifier = Guid.NewGuid().ToString();
				}
				return this._uploadRequestIdentifier;
			}
			set
			{
				this._uploadRequestIdentifier = value;
			}
		}

		// Token: 0x0600CDEF RID: 52719 RVA: 0x002DD170 File Offset: 0x002DB370
		public string ApplyUniquePageIdentifier(string url)
		{
			string pattern = "&?" + this.UniqueRequestIdentifier + "=[^&]*";
			Match match = Regex.Match(url, pattern);
			if (match.Success)
			{
				url = url.Replace(match.ToString(), this.UniqueRequestIdentifier + "=" + this.UploadRequestIdentifier);
			}
			else
			{
				string text = (url.IndexOf("?") > -1) ? "&" : "?";
				url = string.Concat(new string[]
				{
					url,
					text,
					this.UniqueRequestIdentifier,
					"=",
					this.UploadRequestIdentifier
				});
			}
			return url;
		}

		// Token: 0x17004235 RID: 16949
		// (get) Token: 0x0600CDF0 RID: 52720 RVA: 0x002DD216 File Offset: 0x002DB416
		[Browsable(false)]
		[DefaultValue(false)]
		public override bool EnableEmbeddedSkins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004236 RID: 16950
		// (get) Token: 0x0600CDF1 RID: 52721 RVA: 0x002DD219 File Offset: 0x002DB419
		[Browsable(false)]
		[DefaultValue(false)]
		public override bool EnableEmbeddedBaseStylesheet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600CDF2 RID: 52722 RVA: 0x002DD21C File Offset: 0x002DB41C
		protected override void OnPreRender(EventArgs e)
		{
			if (!RadProgressManager.IsRegisteredOnPage(this.Page))
			{
				this.RegisterOnPage();
			}
			else
			{
				this.Visible = false;
			}
			base.OnPreRender(e);
		}

		// Token: 0x0600CDF3 RID: 52723 RVA: 0x002DD241 File Offset: 0x002DB441
		private void RegisterOnPage()
		{
			this.Page.Items["Telerik.WebControls.RadProgressManager"] = true;
		}

		// Token: 0x17004237 RID: 16951
		// (get) Token: 0x0600CDF4 RID: 52724 RVA: 0x002DD25E File Offset: 0x002DB45E
		private string UniqueRequestIdentifier
		{
			get
			{
				return Utility.UNIQUE_REQUEST_QUERY_IDENTIFIER;
			}
		}

		// Token: 0x0600CDF5 RID: 52725 RVA: 0x002DD265 File Offset: 0x002DB465
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddProperty("ajaxCallUrl", base.ResolveClientUrl(this.AjaxUrl));
			descriptor.AddProperty("_allowCustomProgress", RadProgressManager.AllowCustomProgress);
		}

		// Token: 0x17004238 RID: 16952
		// (get) Token: 0x0600CDF6 RID: 52726 RVA: 0x002DD29A File Offset: 0x002DB49A
		[Obsolete("RadProgressManager's FormId property is not used anymore. Please, remove any assignments.", false)]
		[DefaultValue("")]
		public string FormId
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x0600CDF7 RID: 52727 RVA: 0x002DD2A4 File Offset: 0x002DB4A4
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<int>(descriptor, "refreshPeriod", this.RefreshPeriod, 500);
			base.DescribeProperty<bool>(descriptor, "shouldRegisterForSubmit", this.RegisterForSubmit, true);
			base.DescribeProperty<bool>(descriptor, "suppressMissingHttpModuleError", this.SuppressMissingHttpModuleError, false);
			base.DescribeProperty<string>(descriptor, "pageGUID", this.UploadRequestIdentifier, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600CDF8 RID: 52728 RVA: 0x002DD308 File Offset: 0x002DB508
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "progressStarted", this.OnClientProgressStarted);
			RadWebControl.DescribeEvent(descriptor, "progressUpdating", this.OnClientProgressUpdating);
			RadWebControl.DescribeEvent(descriptor, "submitting", this.OnClientSubmitting);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x0400370B RID: 14091
		private string _uploadRequestIdentifier;
	}
}
