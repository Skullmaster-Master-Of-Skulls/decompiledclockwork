using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Design;
using Telerik.Web.UI.AsyncUpload;
using Telerik.Web.UI.CloudUpload;
using Telerik.Web.UI.Common;
using Telerik.Web.UI.Renderers.CloudUpload;

namespace Telerik.Web.UI
{
	// Token: 0x020001A2 RID: 418
	[RequiredScript(typeof(MaterialRipple))]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[TelerikToolboxCategory("Upload")]
	[EmbeddedSkin("CloudUpload", "Default", typeof(RadCloudUpload))]
	[ToolboxBitmap(typeof(RadCloudUpload), "Telerik.Web.UI.CloudUpload.png")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadCloudUpload))]
	[LightweightRendering]
	[EmbeddedSkin("CloudUpload", typeof(RadCloudUpload))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredScript(typeof(jQueryPlugins))]
	[ClientScriptResource("Telerik.Web.UI.RadCloudUpload", "Telerik.Web.UI.CloudUpload.RadCloudUploadScripts.js")]
	[ToolboxData("<{0}:RadCloudUpload runat=\"server\"></{0}:RadCloudUpload>")]
	[Designer("Telerik.Web.Design.RadCloudUploadDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadCloudUpload : RadWebControl, ILocalizableControl
	{
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000F15 RID: 3861 RVA: 0x00039244 File Offset: 0x00037444
		internal bool IsControlEnabled
		{
			get
			{
				return base.IsEnabled;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0003924C File Offset: 0x0003744C
		internal bool IsDesignMode
		{
			get
			{
				return base.DesignMode;
			}
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x00039254 File Offset: 0x00037454
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			CloudUploadClientState cloudUploadClientState = javaScriptSerializer.Deserialize<CloudUploadClientState>(text);
			foreach (UploadedFileRecord record in cloudUploadClientState.UploadedFiles)
			{
				CloudUploadFileInfo obj = new CloudUploadFileInfo(record);
				this.UploadedFiles.Add(obj);
			}
			return this.UploadedFiles.Count > 0;
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x000392CA File Offset: 0x000374CA
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.CheckCloudUploadConfiguration();
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x000392DC File Offset: 0x000374DC
		protected override void RaisePostDataChangedEvent()
		{
			foreach (object obj in this.UploadedFiles)
			{
				CloudUploadFileInfo cloudUploadFileInfo = (CloudUploadFileInfo)obj;
				CloudFileUploadedEventArgs cloudFileUploadedEventArgs = new CloudFileUploadedEventArgs
				{
					FileInfo = cloudUploadFileInfo,
					IsValid = true
				};
				this.OnCloudUploadedFile(cloudFileUploadedEventArgs);
				if (cloudFileUploadedEventArgs.IsValid)
				{
					HttpRuntime.Cache.Remove(cloudUploadFileInfo.KeyName);
				}
			}
			base.RaisePostDataChangedEvent();
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x00039370 File Offset: 0x00037570
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			CloudUploadConfiguration obj = new CloudUploadConfiguration
			{
				AllowedFileExtensions = this.AllowedFileExtensions,
				MaxFileSize = this.MaxFileSize,
				ProviderType = new ProviderType?(this.ProviderType)
			};
			string clearText = javaScriptSerializer.Serialize(obj);
			string text = CryptoService.GetService("").EncryptWithMachineKey(clearText);
			IHmacService service = HmacService.GetService();
			descriptor.AddProperty("_maxFileSize", this.MaxFileSize);
			descriptor.AddProperty("_allowedFileExtensions", javaScriptSerializer.Serialize(this.AllowedFileExtensions));
			descriptor.AddProperty("_handlerUrl", string.Format(base.ResolveUrl(this.HttpHandlerUrlResolved), new object[0]));
			descriptor.AddProperty("_encryptedConfiguration", text + service.HMAC256(text));
			descriptor.AddProperty("_multipleFileSelection", this.MultipleFileSelection);
			descriptor.AddProperty("_providerType", this.ProviderType);
			descriptor.AddProperty("_enabled", this.IsControlEnabled);
			descriptor.AddProperty("_dropZones", SerializationService.Serialize(this.DropZones, false, 4194304));
			base.DescribeRenderMode(descriptor);
			if (this._panelSettings != null)
			{
				if (!string.IsNullOrEmpty(this.FileListPanelSettings.PanelContainerSelector))
				{
					descriptor.AddProperty("_panelContainerSelector", this.FileListPanelSettings.PanelContainerSelector);
				}
				Unit height = this.FileListPanelSettings.Height;
				descriptor.AddProperty("_panelHeight", this.FileListPanelSettings.Height.ToString().Replace("px", ""));
				Unit maxHeight = this.FileListPanelSettings.MaxHeight;
				descriptor.AddProperty("_panelMaxHeight", this.FileListPanelSettings.MaxHeight.ToString().Replace("px", ""));
				if (this.FileListPanelSettings.ShowEmptyFileListPanel)
				{
					descriptor.AddProperty("_showEmptyFileListPanel", this.FileListPanelSettings.ShowEmptyFileListPanel);
				}
			}
			descriptor.AddScriptProperty("localization", javaScriptSerializer.Serialize(this.Localization));
			this.DescribeEvents(descriptor);
			base.DescribeComponent(descriptor);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x000395A8 File Offset: 0x000377A8
		private void DescribeEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "load", this.OnClientLoad);
			RadWebControl.DescribeEvent(descriptor, "fileSelected", this.OnClientFileSelected);
			RadWebControl.DescribeEvent(descriptor, "filesSelecting", this.OnClientFilesSelecting);
			RadWebControl.DescribeEvent(descriptor, "fileUploading", this.OnClientFileUploading);
			RadWebControl.DescribeEvent(descriptor, "fileUploaded", this.OnClientFileUploaded);
			RadWebControl.DescribeEvent(descriptor, "filesUploaded", this.OnClientFilesUploaded);
			RadWebControl.DescribeEvent(descriptor, "fileUploadFailed", this.OnClientUploadFailed);
			RadWebControl.DescribeEvent(descriptor, "validationFailed", this.OnClientValidationFailed);
			RadWebControl.DescribeEvent(descriptor, "fileUploadRemoving", this.OnClientFileUploadRemoving);
			RadWebControl.DescribeEvent(descriptor, "fileUploadRemoved", this.OnClientFileUploadRemoved);
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0003965F File Offset: 0x0003785F
		private string HttpHandlerUrlResolved
		{
			get
			{
				return string.Format("{0}?{1}={2}", this.HttpHandlerUrl, HandlerRouter.HandlerUrlKey, "rcu");
			}
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0003967B File Offset: 0x0003787B
		protected override IRenderer CreateControlRenderer()
		{
			if (this.ResolvedRenderMode == RenderMode.Lightweight)
			{
				return new CloudUploadLiteRenderer(this);
			}
			return new CloudUploadRenderer(this);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00039694 File Offset: 0x00037894
		protected virtual void OnCloudUploadedFile(CloudFileUploadedEventArgs eventArgs)
		{
			CloudFileUploadedEventHandler cloudFileUploadedEventHandler = (CloudFileUploadedEventHandler)base.Events[RadCloudUpload.CloudUploadedFile];
			if (cloudFileUploadedEventHandler != null)
			{
				cloudFileUploadedEventHandler(this, eventArgs);
			}
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x000396C4 File Offset: 0x000378C4
		protected virtual void CheckCloudUploadConfiguration()
		{
			if (this.ProviderType == ProviderType.NotSet)
			{
				throw new ArgumentException("In order to upload files with RadCloudUpload you need to set the provider type of the control. Please review component's configuration");
			}
			if (this.EnableHandlerDetection && this.HttpHandlerUrl == "~/Telerik.Web.UI.WebResource.axd" && !WebResource.Exists(this.Context, "~/Telerik.Web.UI.WebResource.axd", this.Page.Request.ApplicationPath))
			{
				throw new InvalidOperationException(string.Format("'{0}' is missing in web.config. RadCloudUpload requires a HttpHandler registration in web.config. Please, use the control Smart Tag to add the handler automatically, or see the help for more information: Controls > RadCloudUpload", "~/Telerik.Web.UI.WebResource.axd"));
			}
			this.CheckForAssemblyReferences();
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x0003973C File Offset: 0x0003793C
		protected virtual void CheckForAssemblyReferences()
		{
			if (this.EnableAssembliesDetection)
			{
				string text = "";
				switch (this.ProviderType)
				{
				case ProviderType.Amazon:
					text = this.amazonAssemblyName;
					break;
				case ProviderType.Everlive:
					text = this.everliveAssemblyName;
					break;
				case ProviderType.Azure:
					text = this.azureAssemblyName;
					break;
				}
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				bool flag = false;
				bool flag2 = false;
				foreach (Assembly assembly in assemblies)
				{
					string fullName = assembly.FullName;
					if (fullName.Contains(text))
					{
						flag = true;
					}
					if (this.ProviderType == ProviderType.Everlive && fullName.Contains(this.newtonAssemblyName))
					{
						flag2 = true;
					}
				}
				if (!flag)
				{
					throw new Exception(string.Format("Telerik.Web.UI.RadCloudUpload with ID='{0}' was unable to find a reference to {1}.dll. Please, make sure that you have added a reference to the this assembly in your project.", this.ID, text));
				}
				if (this.ProviderType == ProviderType.Everlive && !flag2)
				{
					throw new Exception(string.Format("Telerik.Web.UI.RadCloudUpload with ID='{0}' was unable to find a reference to {1}.dll. Please, make sure that you have added a reference to the this assembly in your project.", this.ID, this.newtonAssemblyName));
				}
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0003982D File Offset: 0x00037A2D
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return this.Renderer.TagKey;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0003983A File Offset: 0x00037A3A
		protected override string CssClassFormatString
		{
			get
			{
				return this.Renderer.CssClassFormatString;
			}
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00039847 File Offset: 0x00037A47
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			this.Renderer.AddAttributesToRender(writer);
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00039855 File Offset: 0x00037A55
		internal void CallBaseAddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0003985E File Offset: 0x00037A5E
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			this.Renderer.RenderContents(writer);
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x00039873 File Offset: 0x00037A73
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06000F27 RID: 3879 RVA: 0x00039876 File Offset: 0x00037A76
		// (set) Token: 0x06000F28 RID: 3880 RVA: 0x00039897 File Offset: 0x00037A97
		[Description("Gets or sets a value indicating whether RadCloudUpload allows selecting multiple files in the File Selection dialog.")]
		[Category("Behavior")]
		[DefaultValue(MultipleFileSelection.Disabled)]
		public MultipleFileSelection MultipleFileSelection
		{
			get
			{
				return (MultipleFileSelection)(this.ViewState["MultipleFileSelection"] ?? MultipleFileSelection.Disabled);
			}
			set
			{
				this.ViewState["MultipleFileSelection"] = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x000398AF File Offset: 0x00037AAF
		// (set) Token: 0x06000F2A RID: 3882 RVA: 0x000398CF File Offset: 0x00037ACF
		[Category("Advanced")]
		[Description("Specifies the URL of the HTTPHandler from which the image will be served.")]
		[DefaultValue("~/Telerik.Web.UI.WebResource.axd")]
		[UrlProperty("*.ShowOnlyPickUpURL")]
		public string HttpHandlerUrl
		{
			get
			{
				return ((string)this.ViewState["HttpHandlerUrl"]) ?? "~/Telerik.Web.UI.WebResource.axd";
			}
			set
			{
				if (!VirtualPathUtility.IsAppRelative(value))
				{
					throw WebResource.GetHttpHandlerUrlNotAppRelative();
				}
				this.ViewState["HttpHandlerUrl"] = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06000F2B RID: 3883 RVA: 0x000398F0 File Offset: 0x00037AF0
		// (set) Token: 0x06000F2C RID: 3884 RVA: 0x00039925 File Offset: 0x00037B25
		public virtual ProviderType ProviderType
		{
			get
			{
				ProviderType? providerType = (ProviderType?)this.ViewState["Provider"];
				if (providerType == null)
				{
					return ProviderType.NotSet;
				}
				return providerType.GetValueOrDefault();
			}
			set
			{
				this.ViewState["Provider"] = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000F2D RID: 3885 RVA: 0x00039940 File Offset: 0x00037B40
		// (set) Token: 0x06000F2E RID: 3886 RVA: 0x00039976 File Offset: 0x00037B76
		[PersistenceMode(PersistenceMode.Attribute)]
		[DefaultValue(0)]
		[Description("Gets or sets the maximum file size allowed for uploading in bytes. Set to 0 for unlimited size.")]
		[Bindable(true)]
		[Category("Behavior")]
		public long MaxFileSize
		{
			get
			{
				long? num = (long?)this.ViewState["MaxFileSize"];
				if (num == null)
				{
					return 0L;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				this.ViewState["MaxFileSize"] = value;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000F2F RID: 3887 RVA: 0x0003998E File Offset: 0x00037B8E
		// (set) Token: 0x06000F30 RID: 3888 RVA: 0x000399AF File Offset: 0x00037BAF
		[TypeConverter(typeof(ListConverter))]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Gets or sets the allowed file extensions for uploading.")]
		[Bindable(true)]
		public string[] AllowedFileExtensions
		{
			get
			{
				return ((string[])this.ViewState["AllowedFileExtensions"]) ?? new string[0];
			}
			set
			{
				this.ViewState["AllowedFileExtensions"] = value;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x000399C2 File Offset: 0x00037BC2
		// (set) Token: 0x06000F32 RID: 3890 RVA: 0x000399E2 File Offset: 0x00037BE2
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
		[Category("Misc")]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		public CultureInfo Culture
		{
			get
			{
				return ((CultureInfo)this.ViewState["Culture"]) ?? CultureInfo.CurrentUICulture;
			}
			set
			{
				this.ViewState["Culture"] = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x000399F5 File Offset: 0x00037BF5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue(null)]
		[Description("Button settings")]
		public FileListPanelSettings FileListPanelSettings
		{
			get
			{
				if (this._panelSettings == null)
				{
					this._panelSettings = new FileListPanelSettings(this.ViewState);
				}
				return this._panelSettings;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x00039A16 File Offset: 0x00037C16
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public CloudUploadLocalization Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new CloudUploadLocalization(new LocalizationProvider("RadCloudUpload", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x00039A55 File Offset: 0x00037C55
		// (set) Token: 0x06000F36 RID: 3894 RVA: 0x00039A78 File Offset: 0x00037C78
		[Description("Gets or sets a value indicating where RadCloudUpload will look for its .resx localization files.")]
		[DefaultValue("")]
		[Category("Misc")]
		public string LocalizationPath
		{
			get
			{
				return ((string)this.ViewState["LocalizationPath"]) ?? string.Empty;
			}
			set
			{
				string text = value.Replace("\\", "/");
				if (text.Length > 0 && !text.EndsWith("/"))
				{
					text += "/";
				}
				this.ViewState["LocalizationPath"] = text;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00039ACC File Offset: 0x00037CCC
		// (set) Token: 0x06000F38 RID: 3896 RVA: 0x00039B01 File Offset: 0x00037D01
		[DefaultValue(true)]
		[Description("Gets or sets a value indicating if RadCloudUpload should check the Telerik.Web.UI.WebResource handler existence in the application configuration file.")]
		[Category("Behavior")]
		public bool EnableHandlerDetection
		{
			get
			{
				return ((bool?)this.ViewState["EnableHandlerDetection"]) ?? true;
			}
			set
			{
				this.ViewState["EnableHandlerDetection"] = value;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06000F39 RID: 3897 RVA: 0x00039B1C File Offset: 0x00037D1C
		// (set) Token: 0x06000F3A RID: 3898 RVA: 0x00039B51 File Offset: 0x00037D51
		[Description("Gets or sets a value indicating if RadCloudUpload should check for the Amazon, Azure, Everlive(and Newtonsoft) assemblies.")]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool EnableAssembliesDetection
		{
			get
			{
				return ((bool?)this.ViewState["EnableAssembliesDetection"]) ?? true;
			}
			set
			{
				this.ViewState["EnableAssembliesDetection"] = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06000F3B RID: 3899 RVA: 0x00039B69 File Offset: 0x00037D69
		// (set) Token: 0x06000F3C RID: 3900 RVA: 0x00039B8A File Offset: 0x00037D8A
		[TypeConverter(typeof(ListConverter))]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Gets or sets the drop zones for upload.")]
		[Bindable(true)]
		public string[] DropZones
		{
			get
			{
				return ((string[])this.ViewState["DropZones"]) ?? new string[0];
			}
			set
			{
				this.ViewState["DropZones"] = value;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x00039B9D File Offset: 0x00037D9D
		// (set) Token: 0x06000F3E RID: 3902 RVA: 0x00039BA5 File Offset: 0x00037DA5
		[Browsable(false)]
		public CloudUploadFileInfoCollection UploadedFiles { get; internal set; }

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000F3F RID: 3903 RVA: 0x00039BAE File Offset: 0x00037DAE
		// (remove) Token: 0x06000F40 RID: 3904 RVA: 0x00039BC1 File Offset: 0x00037DC1
		public event CloudFileUploadedEventHandler FileUploaded
		{
			add
			{
				base.Events.AddHandler(RadCloudUpload.CloudUploadedFile, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadCloudUpload.CloudUploadedFile, value);
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00039BD4 File Offset: 0x00037DD4
		// (set) Token: 0x06000F42 RID: 3906 RVA: 0x00039BF4 File Offset: 0x00037DF4
		[Description("Gets or sets the name of the client-side function which will be executed after the control is loaded")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientLoad
		{
			get
			{
				return ((string)this.ViewState["OnClientLoad"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientLoad"] = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x00039C07 File Offset: 0x00037E07
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x00039C27 File Offset: 0x00037E27
		[Browsable(true)]
		[Description("Gets or sets the name of the client-side function which will be executed after a file is selected")]
		[Bindable(true)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientFileSelected
		{
			get
			{
				return ((string)this.ViewState["OnClientFileSelected"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientFileSelected"] = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00039C3A File Offset: 0x00037E3A
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x00039C5A File Offset: 0x00037E5A
		[Category("Client-side events")]
		[Browsable(true)]
		[Bindable(true)]
		[Description("Gets or sets the name of the client-side function which will be executed after files are selected. It is applicable when MultipleFileSelection is available.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientFilesSelecting
		{
			get
			{
				return ((string)this.ViewState["OnClientFilesSelecting"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientFilesSelecting"] = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x00039C6D File Offset: 0x00037E6D
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x00039C8D File Offset: 0x00037E8D
		[Description("Gets or sets the name of the client-side function which will be executed before a file is being uploaded.")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientFileUploading
		{
			get
			{
				return ((string)this.ViewState["OnClientFileUploading"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientFileUploading"] = value;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x00039CA0 File Offset: 0x00037EA0
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x00039CC0 File Offset: 0x00037EC0
		[Bindable(true)]
		[Description("Gets or sets the name of the client-side function which will be executed after a file is being uploaded.")]
		[Browsable(true)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientFileUploaded
		{
			get
			{
				return ((string)this.ViewState["OnClientFileUploaded"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientFileUploaded"] = value;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x00039CD3 File Offset: 0x00037ED3
		// (set) Token: 0x06000F4C RID: 3916 RVA: 0x00039CF3 File Offset: 0x00037EF3
		[DefaultValue("")]
		[Description(" Gets or sets the name of the client-side function which will be executed after files is being uploaded. It is applicable when MultipleFileSelection is available and many files are selected at once.")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientFilesUploaded
		{
			get
			{
				return ((string)this.ViewState["OnClientFilesUploaded"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientFilesUploaded"] = value;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00039D06 File Offset: 0x00037F06
		// (set) Token: 0x06000F4E RID: 3918 RVA: 0x00039D26 File Offset: 0x00037F26
		[Bindable(true)]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed when file upload failed.")]
		[Browsable(true)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientUploadFailed
		{
			get
			{
				return ((string)this.ViewState["OnClientUploadFailed"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientUploadFailed"] = value;
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x00039D39 File Offset: 0x00037F39
		// (set) Token: 0x06000F50 RID: 3920 RVA: 0x00039D59 File Offset: 0x00037F59
		[Bindable(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed when file validation failed (by size/type).")]
		[Browsable(true)]
		[Category("Client-side events")]
		public string OnClientValidationFailed
		{
			get
			{
				return ((string)this.ViewState["OnClientValidationFailed"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientValidationFailed"] = value;
			}
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00039D6C File Offset: 0x00037F6C
		// (set) Token: 0x06000F52 RID: 3922 RVA: 0x00039D8C File Offset: 0x00037F8C
		[Description("Gets or sets the name of the client-side function which will be executed when file is going to be removed from uloaded/invalid files collections.")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		public string OnClientFileUploadRemoving
		{
			get
			{
				return ((string)this.ViewState["OnClientFileUploadRemoving"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientFileUploadRemoving"] = value;
			}
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x00039D9F File Offset: 0x00037F9F
		// (set) Token: 0x06000F54 RID: 3924 RVA: 0x00039DBF File Offset: 0x00037FBF
		[Category("Client-side events")]
		[Bindable(true)]
		[Description("Gets or sets the name of the client-side function which will be executed when file is removed from uloaded/invalid files collections.")]
		[Browsable(true)]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientFileUploadRemoved
		{
			get
			{
				return ((string)this.ViewState["OnClientFileUploadRemoved"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientFileUploadRemoved"] = value;
			}
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00039DD2 File Offset: 0x00037FD2
		public RadCloudUpload()
		{
			this.UploadedFiles = new CloudUploadFileInfoCollection();
		}

		// Token: 0x04000450 RID: 1104
		private static readonly object CloudUploadedFile = new object();

		// Token: 0x04000451 RID: 1105
		internal FileListPanelSettings _panelSettings;

		// Token: 0x04000452 RID: 1106
		private CloudUploadLocalization _localization;

		// Token: 0x04000453 RID: 1107
		private string amazonAssemblyName = "AWSSDK.Core";

		// Token: 0x04000454 RID: 1108
		private string azureAssemblyName = "Microsoft.WindowsAzure.Storage";

		// Token: 0x04000455 RID: 1109
		private string everliveAssemblyName = "Telerik.Everlive.Sdk";

		// Token: 0x04000456 RID: 1110
		private string newtonAssemblyName = "Newtonsoft.Json";
	}
}
