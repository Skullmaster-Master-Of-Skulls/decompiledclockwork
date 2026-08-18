using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Security;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Design;
using Telerik.Web.UI.AsyncUpload;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x020009A7 RID: 2471
	[RequiredScript(typeof(Core))]
	[ToolboxBitmap(typeof(RadAsyncUpload), "Telerik.Web.UI.AsyncUpload.png")]
	[ToolboxData("<{0}:RadAsyncUpload runat=server></{0}:RadAsyncUpload>")]
	[EmbeddedSkin("Upload", typeof(RadAsyncUpload))]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(MaterialRipple))]
	[ClientScriptResource("Telerik.Web.UI.RadAsyncUpload", "Telerik.Web.UI.AsyncUpload.RadAsyncUploadScripts.js")]
	[LightweightRendering]
	[Designer("Telerik.Web.Design.RadAsyncUploadDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[EmbeddedSkin("Upload", "Default", typeof(RadAsyncUpload))]
	[TelerikToolboxCategory("Upload")]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadAsyncUpload))]
	public class RadAsyncUpload : RadWebControl, ILocalizableControl
	{
		// Token: 0x06005E44 RID: 24132 RVA: 0x0011FCB0 File Offset: 0x0011DEB0
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "_autoAddFileInputs", this.AutoAddFileInputs, true);
			base.DescribeProperty<bool>(descriptor, "_disableChunkUpload", this.DisableChunkUpload, false);
			base.DescribeProperty<bool>(descriptor, "_disablePlugins", this.DisablePlugins, false);
			base.DescribeProperty<bool>(descriptor, "enableAriaSupport", this.EnableAriaSupport, false);
			base.DescribeProperty<bool>(descriptor, "enableFileInputSkinning", this.EnableFileInputSkinning, true);
			base.DescribeProperty<bool>(descriptor, "_enableInlineProgress", this.EnableInlineProgress, true);
			base.DescribeProperty<bool>(descriptor, "_hideFileInput", this.HideFileInput, false);
			base.DescribeProperty<int>(descriptor, "initialFileInputsCount", this.InitialFileInputsCount, 1);
			base.DescribeProperty<int>(descriptor, "inputSize", this.InputSize, 23);
			base.DescribeProperty<bool>(descriptor, "_manualUpload", this.ManualUpload, false);
			base.DescribeProperty<int>(descriptor, "maxFileCount", this.MaxFileInputsCount, 0);
			base.DescribeProperty<int>(descriptor, "_maxFileSize", this.MaxFileSize, 0);
			base.DescribeProperty<MultipleFileSelection>(descriptor, "_multipleFileSelection", this.MultipleFileSelection, MultipleFileSelection.Disabled);
			base.DescribeProperty<UploadedFilesRendering>(descriptor, "_uploadedFilesRendering", this.UploadedFilesRendering, UploadedFilesRendering.AboveFileInput);
			base.DescribeProperty<string>(descriptor, "_pageGUID", this.UploadRequestIdentifier, null);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06005E45 RID: 24133 RVA: 0x0011FDE4 File Offset: 0x0011DFE4
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "added", this.OnClientAdded);
			RadWebControl.DescribeEvent(descriptor, "adding", this.OnClientAdding);
			RadWebControl.DescribeEvent(descriptor, "fileDropped", this.OnClientFileDropped);
			RadWebControl.DescribeEvent(descriptor, "fileSelected", this.OnClientFileSelected);
			RadWebControl.DescribeEvent(descriptor, "filesSelected", this.OnClientFilesSelected);
			RadWebControl.DescribeEvent(descriptor, "filesUploaded", this.OnClientFilesUploaded);
			RadWebControl.DescribeEvent(descriptor, "fileUploaded", this.OnClientFileUploaded);
			RadWebControl.DescribeEvent(descriptor, "fileUploadFailed", this.OnClientFileUploadFailed);
			RadWebControl.DescribeEvent(descriptor, "fileUploading", this.OnClientFileUploading);
			RadWebControl.DescribeEvent(descriptor, "fileUploadRemoved", this.OnClientFileUploadRemoved);
			RadWebControl.DescribeEvent(descriptor, "fileUploadRemoving", this.OnClientFileUploadRemoving);
			RadWebControl.DescribeEvent(descriptor, "progressUpdating", this.OnClientProgressUpdating);
			RadWebControl.DescribeEvent(descriptor, "validationFailed", this.OnClientValidationFailed);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x17001F11 RID: 7953
		// (get) Token: 0x06005E46 RID: 24134 RVA: 0x0011FED5 File Offset: 0x0011E0D5
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17001F12 RID: 7954
		// (get) Token: 0x06005E47 RID: 24135 RVA: 0x0011FED9 File Offset: 0x0011E0D9
		// (set) Token: 0x06005E48 RID: 24136 RVA: 0x0011FEE1 File Offset: 0x0011E0E1
		private string ClientStateValue { get; set; }

		// Token: 0x06005E49 RID: 24137 RVA: 0x0011FEEC File Offset: 0x0011E0EC
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Unit height = this.Height;
			this.Height = Unit.Empty;
			base.AddAttributesToRender(writer);
			this.Height = height;
		}

		// Token: 0x06005E4A RID: 24138 RVA: 0x0011FF19 File Offset: 0x0011E119
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.RenderTrialMessage(writer);
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			if (base.DesignMode)
			{
				this.RenderListContainer(writer);
				this.RenderFileInputRow(writer);
				writer.RenderEndTag();
			}
		}

		// Token: 0x06005E4B RID: 24139 RVA: 0x0011FF54 File Offset: 0x0011E154
		private void RenderListContainer(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}ListContainer", this.ClientID));
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ruInputs");
			if (this.Height != Unit.Empty)
			{
				writer.AddStyleAttribute(HtmlTextWriterStyle.Height, this.Height.ToString());
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
		}

		// Token: 0x06005E4C RID: 24140 RVA: 0x0011FFBC File Offset: 0x0011E1BC
		private void RenderFileInputRow(HtmlTextWriter writer)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			this.RenderFakeInputWrap(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06005E4D RID: 24141 RVA: 0x0011FFD4 File Offset: 0x0011E1D4
		private void RenderFakeInputWrap(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ruFileWrap");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ruStyled");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Size, this.InputSize.ToString());
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ruFakeInput radPreventDecorate");
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
			this.RenderInput(writer, "ruBrowse", this.Localization.Select);
			writer.RenderEndTag();
		}

		// Token: 0x06005E4E RID: 24142 RVA: 0x00120060 File Offset: 0x0011E260
		private void RenderInput(HtmlTextWriter writer, string cssClass, string value)
		{
			string value2 = cssClass + " ruButton";
			writer.AddAttribute(HtmlTextWriterAttribute.Class, value2);
			writer.AddAttribute(HtmlTextWriterAttribute.Value, value);
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x17001F13 RID: 7955
		// (get) Token: 0x06005E4F RID: 24143 RVA: 0x00120099 File Offset: 0x0011E299
		protected FilterFormatter FilterFormatter
		{
			get
			{
				if (this._filterFormatter == null)
				{
					this._filterFormatter = new FilterFormatter();
				}
				return this._filterFormatter;
			}
		}

		// Token: 0x17001F14 RID: 7956
		// (get) Token: 0x06005E50 RID: 24144 RVA: 0x001200B4 File Offset: 0x0011E2B4
		protected override string CssClassFormatString
		{
			get
			{
				string text = "RadUpload RadUpload_{0}";
				if (base.Attributes["dir"] == "rtl")
				{
					text += " RadUpload_rtl RadUpload_{0}_rtl";
				}
				return "RadAsyncUpload " + text;
			}
		}

		// Token: 0x17001F15 RID: 7957
		// (get) Token: 0x06005E51 RID: 24145 RVA: 0x001200FC File Offset: 0x0011E2FC
		// (set) Token: 0x06005E52 RID: 24146 RVA: 0x00120135 File Offset: 0x0011E335
		[ClientControlProperty]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[ClientPropertyName("_pageGUID")]
		public string UploadRequestIdentifier
		{
			get
			{
				if (string.IsNullOrEmpty(this._uploadRequestIdentifier))
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

		// Token: 0x17001F16 RID: 7958
		// (get) Token: 0x06005E53 RID: 24147 RVA: 0x0012013E File Offset: 0x0011E33E
		private bool UseDefaultTemporaryFolder
		{
			get
			{
				return this.TemporaryFolder == "~/App_Data/RadUploadTemp";
			}
		}

		// Token: 0x17001F17 RID: 7959
		// (get) Token: 0x06005E54 RID: 24148 RVA: 0x00120150 File Offset: 0x0011E350
		private string DefaultTemporaryFolderPath
		{
			get
			{
				return Path.Combine(this.AppDataPath, "RadUploadTemp");
			}
		}

		// Token: 0x17001F18 RID: 7960
		// (get) Token: 0x06005E55 RID: 24149 RVA: 0x00120162 File Offset: 0x0011E362
		private string AppDataPath
		{
			get
			{
				return Path.Combine(this.Context.Request.PhysicalApplicationPath, "App_Data");
			}
		}

		// Token: 0x17001F19 RID: 7961
		// (get) Token: 0x06005E56 RID: 24150 RVA: 0x0012017E File Offset: 0x0011E37E
		internal static string HandlerRouterKey
		{
			get
			{
				return "rau";
			}
		}

		// Token: 0x17001F1A RID: 7962
		// (get) Token: 0x06005E57 RID: 24151 RVA: 0x00120185 File Offset: 0x0011E385
		internal string EncryptedTemporaryFolder
		{
			get
			{
				if (this._encryptedTemporaryFolder == null)
				{
					this._encryptedTemporaryFolder = CryptoService.GetService("").Encrypt(this.MappedTemporaryFolder);
				}
				return this._encryptedTemporaryFolder;
			}
		}

		// Token: 0x17001F1B RID: 7963
		// (get) Token: 0x06005E58 RID: 24152 RVA: 0x001201B0 File Offset: 0x0011E3B0
		internal string EncryptedTargetFolder
		{
			get
			{
				if (this._encryptedTargetFolder == null)
				{
					this._encryptedTargetFolder = CryptoService.GetService("").Encrypt(this.TargetFolder);
				}
				return this._encryptedTargetFolder;
			}
		}

		// Token: 0x17001F1C RID: 7964
		// (get) Token: 0x06005E59 RID: 24153 RVA: 0x001201DB File Offset: 0x0011E3DB
		internal string MappedTemporaryFolder
		{
			get
			{
				if (!Path.IsPathRooted(this.TemporaryFolder))
				{
					return this.Context.Server.MapPath(this.TemporaryFolder);
				}
				return this.TemporaryFolder;
			}
		}

		// Token: 0x17001F1D RID: 7965
		// (get) Token: 0x06005E5A RID: 24154 RVA: 0x00120207 File Offset: 0x0011E407
		internal string MappedTargetFolder
		{
			get
			{
				if (string.IsNullOrEmpty(this.TargetFolder))
				{
					return "";
				}
				if (!Path.IsPathRooted(this.TargetFolder))
				{
					return this.Context.Server.MapPath(this.TargetFolder);
				}
				return this.TargetFolder;
			}
		}

		// Token: 0x17001F1E RID: 7966
		// (get) Token: 0x06005E5B RID: 24155 RVA: 0x00120246 File Offset: 0x0011E446
		private string HttpHandlerUrlResolved
		{
			get
			{
				return string.Format("{0}?{1}={2}", this.HttpHandlerUrl, HandlerRouter.HandlerUrlKey, RadAsyncUpload.HandlerRouterKey);
			}
		}

		// Token: 0x06005E5C RID: 24156 RVA: 0x00120264 File Offset: 0x0011E464
		protected internal virtual void TestTemporaryFolderPermissions()
		{
			string path = Path.Combine(this.MappedTemporaryFolder, "RadUploadTestFile");
			if (File.Exists(path))
			{
				return;
			}
			Exception ex = new Exception("RadAsyncUpload does not have permission to write files in the TemporaryFolder. In Medium Trust scenarios, the TemporaryFolder should be a subfolder of the Application Path.");
			try
			{
				File.Create(path);
			}
			catch (SecurityException)
			{
				throw ex;
			}
			catch (UnauthorizedAccessException)
			{
				throw ex;
			}
		}

		// Token: 0x06005E5D RID: 24157 RVA: 0x001202C4 File Offset: 0x0011E4C4
		internal void UpdateFileFilter(string[] extensions)
		{
			if (this.FileFilters.Count > 0)
			{
				return;
			}
			this.FileFilters.Add(new FileFilter(FileFilter.GetFilter(extensions, true), extensions));
		}

		// Token: 0x06005E5E RID: 24158 RVA: 0x001202F0 File Offset: 0x0011E4F0
		private void UpdateAllowedExtensions(FileFilterCollection filters)
		{
			if (filters.Count == 0)
			{
				return;
			}
			List<string> list = new List<string>();
			foreach (object obj in filters)
			{
				FileFilter fileFilter = (FileFilter)obj;
				list.AddRange(fileFilter.Extensions);
			}
			this.AllowedFileExtensions = list.ToArray();
		}

		// Token: 0x06005E5F RID: 24159 RVA: 0x00120364 File Offset: 0x0011E564
		protected internal virtual void EnsureDefaultTemporaryFolderExists()
		{
			if (!Directory.Exists(this.AppDataPath))
			{
				this.CreateAppDataFolder();
			}
			if (!Directory.Exists(this.DefaultTemporaryFolderPath))
			{
				this.CreateTempFolder();
			}
		}

		// Token: 0x06005E60 RID: 24160 RVA: 0x0012038C File Offset: 0x0011E58C
		private void CreateTempFolder()
		{
			try
			{
				Directory.CreateDirectory(this.DefaultTemporaryFolderPath);
			}
			catch (UnauthorizedAccessException)
			{
				throw new UnauthorizedAccessException("RadAsyncUpload could not create App_Data\\RadUploadTemp folder. Ensure the App_Data folder is writable or set the TemporaryFolder property to a writable location.");
			}
		}

		// Token: 0x06005E61 RID: 24161 RVA: 0x001203C4 File Offset: 0x0011E5C4
		private void CreateAppDataFolder()
		{
			try
			{
				Directory.CreateDirectory(this.AppDataPath);
			}
			catch (UnauthorizedAccessException)
			{
				throw new UnauthorizedAccessException("RadAsyncUpload could not create App_Data folder. Ensure the App_Data's location is writable or set the TemporaryFolder property to a writable location.");
			}
		}

		// Token: 0x17001F1F RID: 7967
		// (get) Token: 0x06005E62 RID: 24162 RVA: 0x001203FC File Offset: 0x0011E5FC
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005E63 RID: 24163 RVA: 0x00120400 File Offset: 0x0011E600
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			if (this.RenderMode == RenderMode.Lightweight)
			{
				this.DisablePlugins = true;
			}
			bool flag = this.uploadConfiguration != null;
			IAsyncUploadConfiguration asyncUploadConfiguration = (!flag) ? this.CreateDefaultUploadConfiguration<AsyncUploadConfiguration>() : this.uploadConfiguration;
			AsyncUploadConfiguration asyncUploadConfiguration2 = asyncUploadConfiguration as AsyncUploadConfiguration;
			if (!flag && asyncUploadConfiguration2 != null)
			{
				asyncUploadConfiguration2.UseApplicationPoolImpersonation = this.UseApplicationPoolImpersonation;
				asyncUploadConfiguration2.AllowedFileExtensions = this.AllowedFileExtensions;
				asyncUploadConfiguration = asyncUploadConfiguration2;
			}
			string value = CryptoService.GetService("").Encrypt(asyncUploadConfiguration.GetType().AssemblyQualifiedName);
			IHmacService service = HmacService.GetService();
			asyncUploadConfiguration.TargetFolder = this.EncryptedTargetFolder + service.HMAC256(this.EncryptedTargetFolder);
			asyncUploadConfiguration.TempTargetFolder = this.EncryptedTemporaryFolder + service.HMAC256(this.EncryptedTemporaryFolder);
			if (this.FileFilters.Count == 0 && this.AllowedFileExtensions.Length > 0)
			{
				this.UpdateFileFilter(this.AllowedFileExtensions);
			}
			if (this.ChunkSize > 0)
			{
				descriptor.AddProperty("_chunkSize", this.ChunkSize);
			}
			base.DescribeRenderMode(descriptor);
			descriptor.AddProperty("_fileFilter", this.FilterFormatter.Serialize(this.FileFilters, true));
			descriptor.AddScriptProperty("localization", SerializationService.Serialize(this.Localization, 4194304));
			descriptor.AddProperty("allowedFileExtensions", SerializationService.Serialize(this.AllowedFileExtensions, false, 4194304));
			descriptor.AddProperty("dropZones", SerializationService.Serialize(this.DropZones, false, 4194304));
			descriptor.AddProperty("_serializedConfiguration", SerializationService.Serialize(asyncUploadConfiguration, true, 4194304));
			descriptor.AddProperty("_serializedConfigurationType", value);
			descriptor.AddProperty("_handlerUrl", string.Format(base.ResolveUrl(this.HttpHandlerUrlResolved), new object[0]));
			descriptor.AddProperty("_progressHandlerUrl", base.ResolveClientUrl(this.ProgressHandlerUrl));
			descriptor.AddProperty("_skin", base.RuntimeSkin);
			descriptor.AddProperty("_accessKey", this.AccessKey);
			descriptor.AddProperty("_tabIndex", this.TabIndex);
			if (!this.DisablePlugins)
			{
				descriptor.AddProperty("_flashModuleUrl", this.Page.ClientScript.GetWebResourceUrl(typeof(RadAsyncUpload), "Telerik.Web.UI.AsyncUpload.Modules.Flash.AsyncUploadModule.swf"));
				descriptor.AddProperty("_silverlightModuleUrl", this.Page.ClientScript.GetWebResourceUrl(typeof(RadAsyncUpload), "Telerik.Web.UI.AsyncUpload.Modules.Silverlight.src.AsyncUploadModule.xap"));
			}
			if (this._keyboardNavigationSettings != null)
			{
				this.KeyboardNavigationSettings.Describe(descriptor);
			}
			if (this.Height != Unit.Empty)
			{
				descriptor.AddProperty("_height", this.Height.ToString());
			}
			this.AriaSettings.Describe(descriptor);
			base.DescribeComponent(descriptor);
		}

		// Token: 0x06005E64 RID: 24164 RVA: 0x001206C4 File Offset: 0x0011E8C4
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.UseApplicationPoolImpersonation)
			{
				using (HostingEnvironment.Impersonate())
				{
					this.MarshalTemporaryFolder();
					goto IL_2D;
				}
			}
			this.MarshalTemporaryFolder();
			IL_2D:
			this.CheckForHandlerRegistration();
		}

		// Token: 0x06005E65 RID: 24165 RVA: 0x00120714 File Offset: 0x0011E914
		protected internal virtual void CheckForHandlerRegistration()
		{
			if (this.EnableHandlerDetection && this.HttpHandlerUrl == "~/Telerik.Web.UI.WebResource.axd" && !WebResource.Exists(this.Context, "~/Telerik.Web.UI.WebResource.axd", this.Page.Request.ApplicationPath))
			{
				throw new InvalidOperationException(string.Format("'{0}' is missing in web.config. RadAsyncUpload requires a HttpHandler registration in web.config. Please, use the control Smart Tag to add the handler automatically, or see the help for more information: Controls > RadAsyncUpload", "~/Telerik.Web.UI.WebResource.axd"));
			}
		}

		// Token: 0x06005E66 RID: 24166 RVA: 0x00120772 File Offset: 0x0011E972
		private void MarshalTemporaryFolder()
		{
			if (this.EnablePermissionsCheck)
			{
				if (this.UseDefaultTemporaryFolder)
				{
					this.EnsureDefaultTemporaryFolderExists();
				}
				this.TestTemporaryFolderPermissions();
			}
		}

		// Token: 0x06005E67 RID: 24167 RVA: 0x00120790 File Offset: 0x0011E990
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Page.RegisterRequiresControlState(this);
			if (this.EnableCustomValidation)
			{
				this.Page.LoadComplete += this.Page_LoadComplete;
			}
		}

		// Token: 0x06005E68 RID: 24168 RVA: 0x001207C4 File Offset: 0x0011E9C4
		private void Page_LoadComplete(object sender, EventArgs e)
		{
			if (this.EnableCustomValidation && this.ClientStateValue != null)
			{
				bool flag;
				try
				{
					flag = this.Page.IsValid;
				}
				catch (Exception)
				{
					flag = false;
				}
				if (flag)
				{
					this.RaiseDataChangedEvent();
					this.ClientStateValue = null;
				}
			}
			this.Page.LoadComplete -= this.Page_LoadComplete;
		}

		// Token: 0x06005E69 RID: 24169 RVA: 0x0012082C File Offset: 0x0011EA2C
		protected override object SaveControlState()
		{
			if (this.PersistConfiguration && this.uploadConfiguration != null)
			{
				return new object[]
				{
					base.SaveControlState(),
					this.uploadConfiguration
				};
			}
			return base.SaveControlState();
		}

		// Token: 0x06005E6A RID: 24170 RVA: 0x0012086C File Offset: 0x0011EA6C
		protected override void LoadControlState(object savedState)
		{
			object[] array = savedState as object[];
			if (array != null && array.Length == 2 && array[1] is IAsyncUploadConfiguration)
			{
				base.LoadControlState(array[0]);
				this.uploadConfiguration = (IAsyncUploadConfiguration)array[1];
				return;
			}
			base.LoadControlState(savedState);
		}

		// Token: 0x06005E6B RID: 24171 RVA: 0x001208B4 File Offset: 0x0011EAB4
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			if (this.IsClientStatePlayable())
			{
				this.PlayClientState(text);
				if (this.EnableCustomValidation)
				{
					this.ClientStateValue = text;
				}
			}
			else
			{
				this.ClientStateValue = text;
			}
			return this.UploadedFiles.Count > 0;
		}

		// Token: 0x06005E6C RID: 24172 RVA: 0x00120910 File Offset: 0x0011EB10
		protected internal virtual bool IsClientStatePlayable()
		{
			if (this.PostbackTriggers.Length != 0)
			{
				string postBackTriggerID = this.GetPostBackTriggerID();
				foreach (string text in this.PostbackTriggers)
				{
					if (text.Trim() == postBackTriggerID)
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06005E6D RID: 24173 RVA: 0x00120964 File Offset: 0x0011EB64
		protected internal virtual string GetPostBackTriggerID()
		{
			Control control = null;
			string text = this.Page.Request.Params.Get("__EVENTTARGET");
			if (text != null && text != string.Empty)
			{
				control = this.Page.FindControl(text);
			}
			else
			{
				foreach (object obj in this.Page.Request.Form)
				{
					string text2 = (string)obj;
					Control control2;
					if (text2.EndsWith(".x") || text2.EndsWith(".y"))
					{
						control2 = this.Page.FindControl(text2.Substring(0, text2.Length - 2));
					}
					else
					{
						control2 = this.Page.FindControl(text2);
					}
					if (control2 is Button || control2 is ImageButton || control2 is IButtonControl)
					{
						control = control2;
						break;
					}
				}
			}
			if (control != null)
			{
				return control.ID;
			}
			return text;
		}

		// Token: 0x06005E6E RID: 24174 RVA: 0x00120A74 File Offset: 0x0011EC74
		protected internal virtual void PlayClientState(string clientStateValue)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer
			{
				MaxJsonLength = clientStateValue.Length
			};
			javaScriptSerializer.RegisterConverters(new AsyncUploadClientStateConverter[]
			{
				new AsyncUploadClientStateConverter()
			});
			RadAsyncUploadClientState clientState = javaScriptSerializer.Deserialize<RadAsyncUploadClientState>(clientStateValue);
			this.AddValidFilesFromClientState(clientState);
		}

		// Token: 0x06005E6F RID: 24175 RVA: 0x00120ABC File Offset: 0x0011ECBC
		internal void AddValidFilesFromClientState(RadAsyncUploadClientState clientState)
		{
			foreach (UploadedFileInfo fileInfo in clientState.UploadedFiles)
			{
				AsyncUploadedFile asyncUploadedFile = new AsyncUploadedFile(this, fileInfo);
				if (this.ValidateFile(asyncUploadedFile))
				{
					this.UploadedFiles.Add(asyncUploadedFile);
				}
			}
		}

		// Token: 0x06005E70 RID: 24176 RVA: 0x00120B00 File Offset: 0x0011ED00
		protected override string SaveClientState()
		{
			if (this.ClientStateValue != null)
			{
				return this.ClientStateValue;
			}
			return base.SaveClientState();
		}

		// Token: 0x06005E71 RID: 24177 RVA: 0x00120B17 File Offset: 0x0011ED17
		protected override void RaisePostDataChangedEvent()
		{
			if (!this.EnableCustomValidation)
			{
				this.RaiseDataChangedEvent();
			}
			base.RaisePostDataChangedEvent();
		}

		// Token: 0x06005E72 RID: 24178 RVA: 0x00120B30 File Offset: 0x0011ED30
		private void RaiseDataChangedEvent()
		{
			foreach (object obj in this.UploadedFiles)
			{
				AsyncUploadedFile asyncUploadedFile = (AsyncUploadedFile)obj;
				bool flag = this.ValidateFile(asyncUploadedFile);
				if (flag)
				{
					FileUploadedEventArgs fileUploadedEventArgs = new FileUploadedEventArgs(asyncUploadedFile, flag);
					this.OnFileUploaded(fileUploadedEventArgs);
					if (!string.IsNullOrEmpty(this.MappedTargetFolder))
					{
						string fileName = Path.Combine(this.MappedTargetFolder, asyncUploadedFile.GetName());
						flag = fileUploadedEventArgs.IsValid;
						if (File.Exists(asyncUploadedFile.TempFilePath) && flag)
						{
							asyncUploadedFile.SaveAs(fileName);
						}
						HttpRuntime.Cache.Remove(Path.GetFileName(asyncUploadedFile.TempFilePath));
					}
				}
			}
		}

		// Token: 0x06005E73 RID: 24179 RVA: 0x00120BFC File Offset: 0x0011EDFC
		protected virtual void OnFileUploaded(FileUploadedEventArgs e)
		{
			FileUploadedEventHandler fileUploadedEventHandler = (FileUploadedEventHandler)base.Events[RadAsyncUpload.FileUploadedEvent];
			if (fileUploadedEventHandler != null)
			{
				fileUploadedEventHandler(this, e);
			}
		}

		// Token: 0x06005E74 RID: 24180 RVA: 0x00120C2C File Offset: 0x0011EE2C
		internal bool ValidateFile(AsyncUploadedFile file)
		{
			bool flag = this.ValidateFileExtension(file.GetExtension());
			bool flag2 = this.CheckFileNameForInvalidChars(file.GetName());
			return flag && !flag2;
		}

		// Token: 0x06005E75 RID: 24181 RVA: 0x00120C5C File Offset: 0x0011EE5C
		protected internal virtual bool ValidateFileExtension(string fileExtension)
		{
			if (this.AllowedFileExtensions != null && this.AllowedFileExtensions.Length != 0)
			{
				foreach (string text in this.AllowedFileExtensions)
				{
					if (fileExtension.ToLower().Trim(new char[]
					{
						'.'
					}) == text.ToLower().Trim(new char[]
					{
						'.'
					}))
					{
						return true;
					}
				}
				return false;
			}
			return true;
		}

		// Token: 0x06005E76 RID: 24182 RVA: 0x00120CDA File Offset: 0x0011EEDA
		protected internal virtual bool CheckFileNameForInvalidChars(string fileName)
		{
			return string.IsNullOrEmpty(fileName) || fileName.IndexOfAny(Path.GetInvalidFileNameChars()) > -1;
		}

		// Token: 0x17001F20 RID: 7968
		// (get) Token: 0x06005E77 RID: 24183 RVA: 0x00120CF4 File Offset: 0x0011EEF4
		// (set) Token: 0x06005E78 RID: 24184 RVA: 0x00120D15 File Offset: 0x0011EF15
		[DefaultValue(false)]
		[ClientPropertyName("enableAriaSupport")]
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("When set to true enables support for WAI-ARIA.")]
		public bool EnableAriaSupport
		{
			get
			{
				return (bool)(this.ViewState["EnableAriaSupport"] ?? false);
			}
			set
			{
				this.ViewState["EnableAriaSupport"] = value;
			}
		}

		// Token: 0x17001F21 RID: 7969
		// (get) Token: 0x06005E79 RID: 24185 RVA: 0x00120D30 File Offset: 0x0011EF30
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets the object that controls the Wai-Aria settings applied on the control's element.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[MergableProperty(false)]
		public WaiAriaSettings AriaSettings
		{
			get
			{
				WaiAriaSettings result;
				if ((result = this._ariaSettings) == null)
				{
					result = (this._ariaSettings = new WaiAriaSettings());
				}
				return result;
			}
		}

		// Token: 0x17001F22 RID: 7970
		// (get) Token: 0x06005E7A RID: 24186 RVA: 0x00120D58 File Offset: 0x0011EF58
		[Description("Keyboard navigation settings")]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public KeyboardNavigationSettings KeyboardNavigationSettings
		{
			get
			{
				KeyboardNavigationSettings result;
				if ((result = this._keyboardNavigationSettings) == null)
				{
					result = (this._keyboardNavigationSettings = new KeyboardNavigationSettings());
				}
				return result;
			}
		}

		// Token: 0x17001F23 RID: 7971
		// (get) Token: 0x06005E7B RID: 24187 RVA: 0x00120D80 File Offset: 0x0011EF80
		// (set) Token: 0x06005E7C RID: 24188 RVA: 0x00120DB5 File Offset: 0x0011EFB5
		[Description("The control will fires its event and process the files if the Page is valid, after the server side validation events. If the Page is not valid the uploaded files are persisted between postbacks.")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[DefaultValue(false)]
		[Category("Behavior")]
		public bool EnableCustomValidation
		{
			get
			{
				return ((bool?)this.ViewState["EnableCustomValidation"]) ?? false;
			}
			set
			{
				this.ViewState["EnableCustomValidation"] = value;
			}
		}

		// Token: 0x17001F24 RID: 7972
		// (get) Token: 0x06005E7D RID: 24189 RVA: 0x00120DD0 File Offset: 0x0011EFD0
		// (set) Token: 0x06005E7E RID: 24190 RVA: 0x00120E05 File Offset: 0x0011F005
		[DefaultValue(false)]
		[Category("Behavior")]
		[ClientControlProperty]
		[Description("Gets or sets whether to render the file input.")]
		[ClientPropertyName("_hideFileInput")]
		[PersistenceMode(PersistenceMode.Attribute)]
		public bool HideFileInput
		{
			get
			{
				return ((bool?)this.ViewState["HideFileInput"]) ?? false;
			}
			set
			{
				this.ViewState["HideFileInput"] = value;
			}
		}

		// Token: 0x17001F25 RID: 7973
		// (get) Token: 0x06005E7F RID: 24191 RVA: 0x00120E20 File Offset: 0x0011F020
		// (set) Token: 0x06005E80 RID: 24192 RVA: 0x00120E55 File Offset: 0x0011F055
		[Category("Behavior")]
		[Description("Gets or sets whether the application pool impersonation should be used.")]
		[DefaultValue(false)]
		[PersistenceMode(PersistenceMode.Attribute)]
		public bool UseApplicationPoolImpersonation
		{
			get
			{
				return ((bool?)this.ViewState["UseApplicationPoolImpersonation"]) ?? false;
			}
			set
			{
				this.ViewState["UseApplicationPoolImpersonation"] = value;
			}
		}

		// Token: 0x17001F26 RID: 7974
		// (get) Token: 0x06005E81 RID: 24193 RVA: 0x00120E6D File Offset: 0x0011F06D
		// (set) Token: 0x06005E82 RID: 24194 RVA: 0x00120E8E File Offset: 0x0011F08E
		[Category("Behavior")]
		[TypeConverter(typeof(ListConverter))]
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

		// Token: 0x17001F27 RID: 7975
		// (get) Token: 0x06005E83 RID: 24195 RVA: 0x00120EA4 File Offset: 0x0011F0A4
		// (set) Token: 0x06005E84 RID: 24196 RVA: 0x00120ED9 File Offset: 0x0011F0D9
		[DefaultValue(false)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Gets or sets whether the upload will be in chunks (2MB each) or the file will be uploaded with one request.")]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("_disableChunkUpload")]
		public bool DisableChunkUpload
		{
			get
			{
				return ((bool?)this.ViewState["DisableChunkUpload"]) ?? false;
			}
			set
			{
				if (value)
				{
					this.DisablePlugins = true;
				}
				this.ViewState["DisableChunkUpload"] = value;
			}
		}

		// Token: 0x17001F28 RID: 7976
		// (get) Token: 0x06005E85 RID: 24197 RVA: 0x00120EFC File Offset: 0x0011F0FC
		// (set) Token: 0x06005E86 RID: 24198 RVA: 0x00120F31 File Offset: 0x0011F131
		[ClientPropertyName("_manualUpload")]
		[DefaultValue(false)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Behavior")]
		[Description("Gets or sets whether the upload will start automatically after the files are selected")]
		[ClientControlProperty]
		public bool ManualUpload
		{
			get
			{
				return ((bool?)this.ViewState["ManualUpload"]) ?? false;
			}
			set
			{
				if (value)
				{
					this.DisablePlugins = true;
				}
				this.ViewState["ManualUpload"] = value;
			}
		}

		// Token: 0x17001F29 RID: 7977
		// (get) Token: 0x06005E87 RID: 24199 RVA: 0x00120F54 File Offset: 0x0011F154
		// (set) Token: 0x06005E88 RID: 24200 RVA: 0x00120F89 File Offset: 0x0011F189
		[DefaultValue(false)]
		[ClientControlProperty]
		[ClientPropertyName("_disablePlugins")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Behavior")]
		[Description("Gets or sets a value to control whether RadAsyncUpload will use 3rd party plug-ins like Flash/Silverlight or will stick to the native modules only (IFrame, File API)")]
		public bool DisablePlugins
		{
			get
			{
				return ((bool?)this.ViewState["DisablePlugins"]) ?? false;
			}
			set
			{
				this.ViewState["DisablePlugins"] = value;
			}
		}

		// Token: 0x17001F2A RID: 7978
		// (get) Token: 0x06005E89 RID: 24201 RVA: 0x00120FA1 File Offset: 0x0011F1A1
		// (set) Token: 0x06005E8A RID: 24202 RVA: 0x00120FC1 File Offset: 0x0011F1C1
		[Description("Gets or sets the URL which for the progress handler that takes care of the progress monitoring when the IFrame module is used")]
		[Bindable(true)]
		[DefaultValue("~/Telerik.RadUploadProgressHandler.ashx")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Behavior")]
		public string ProgressHandlerUrl
		{
			get
			{
				return ((string)this.ViewState["ProgressHandlerUrl"]) ?? "~/Telerik.RadUploadProgressHandler.ashx";
			}
			set
			{
				this.ViewState["ProgressHandlerUrl"] = value;
			}
		}

		// Token: 0x17001F2B RID: 7979
		// (get) Token: 0x06005E8B RID: 24203 RVA: 0x00120FD4 File Offset: 0x0011F1D4
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public AsyncUploadStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new AsyncUploadStrings(new LocalizationProvider("RadAsyncUpload", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17001F2C RID: 7980
		// (get) Token: 0x06005E8C RID: 24204 RVA: 0x00121013 File Offset: 0x0011F213
		// (set) Token: 0x06005E8D RID: 24205 RVA: 0x00121034 File Offset: 0x0011F234
		[DefaultValue("")]
		[Category("Misc")]
		[Description("Gets or sets a value indicating where RadAsyncUpload will look for its .resx localization files.")]
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

		// Token: 0x17001F2D RID: 7981
		// (get) Token: 0x06005E8E RID: 24206 RVA: 0x00121087 File Offset: 0x0011F287
		[Description("A collection of filters to be applied to the OpenFileDialog")]
		[Category("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[MergableProperty(false)]
		public FileFilterCollection FileFilters
		{
			get
			{
				if (this._filters == null)
				{
					this._filters = new FileFilterCollection();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._filters).TrackViewState();
					}
				}
				return this._filters;
			}
		}

		// Token: 0x17001F2E RID: 7982
		// (get) Token: 0x06005E8F RID: 24207 RVA: 0x001210B5 File Offset: 0x0011F2B5
		// (set) Token: 0x06005E90 RID: 24208 RVA: 0x001210D5 File Offset: 0x0011F2D5
		[Category("Misc")]
		[DefaultValue(typeof(CultureInfo), "en-US")]
		[Description("The selected culture. Localization strings will be loaded based on this value.")]
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

		// Token: 0x17001F2F RID: 7983
		// (get) Token: 0x06005E91 RID: 24209 RVA: 0x001210E8 File Offset: 0x0011F2E8
		// (set) Token: 0x06005E92 RID: 24210 RVA: 0x00121109 File Offset: 0x0011F309
		[DefaultValue(true)]
		[Description("Gets or sets a value indicating whether a new File Input should be automatically added upon selecting a file to upload.")]
		[ClientPropertyName("_autoAddFileInputs")]
		[Category("Behavior")]
		[ClientControlProperty]
		public bool AutoAddFileInputs
		{
			get
			{
				return (bool)(this.ViewState["AutoAddFileInputs"] ?? true);
			}
			set
			{
				this.ViewState["AutoAddFileInputs"] = value;
			}
		}

		// Token: 0x17001F30 RID: 7984
		// (get) Token: 0x06005E93 RID: 24211 RVA: 0x00121121 File Offset: 0x0011F321
		// (set) Token: 0x06005E94 RID: 24212 RVA: 0x0012114E File Offset: 0x0011F34E
		[Description("Gets or sets the allowed file extensions for uploading.")]
		[Bindable(true)]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[TypeConverter(typeof(ListConverter))]
		public string[] AllowedFileExtensions
		{
			get
			{
				this.UpdateAllowedExtensions(this.FileFilters);
				return ((string[])this.ViewState["AllowedFileExtensions"]) ?? new string[0];
			}
			set
			{
				this.ViewState["AllowedFileExtensions"] = value;
			}
		}

		// Token: 0x17001F31 RID: 7985
		// (get) Token: 0x06005E95 RID: 24213 RVA: 0x00121161 File Offset: 0x0011F361
		// (set) Token: 0x06005E96 RID: 24214 RVA: 0x00121182 File Offset: 0x0011F382
		[Bindable(true)]
		[Description("Gets or sets the allowed MIME types for uploading.")]
		[Category("Behavior")]
		[TypeConverter(typeof(ListConverter))]
		[PersistenceMode(PersistenceMode.Attribute)]
		public string[] AllowedMimeTypes
		{
			get
			{
				return ((string[])this.ViewState["AllowedMimeTypes"]) ?? new string[0];
			}
			set
			{
				this.ViewState["AllowedMimeTypes"] = value;
			}
		}

		// Token: 0x17001F32 RID: 7986
		// (get) Token: 0x06005E97 RID: 24215 RVA: 0x00121198 File Offset: 0x0011F398
		// (set) Token: 0x06005E98 RID: 24216 RVA: 0x001211CD File Offset: 0x0011F3CD
		[DefaultValue(true)]
		[Category("Behavior")]
		[ClientControlProperty]
		[ClientPropertyName("_enableInlineProgress")]
		[Description("Gets or sets a value indicating whether RadAsyncUpload Specifies whether RadAsyncUpload displays an inline progress next to each file being uploaded.")]
		public bool EnableInlineProgress
		{
			get
			{
				return ((bool?)this.ViewState["EnableInlineProgress"]) ?? true;
			}
			set
			{
				this.ViewState["EnableInlineProgress"] = value;
			}
		}

		// Token: 0x17001F33 RID: 7987
		// (get) Token: 0x06005E99 RID: 24217 RVA: 0x001211E8 File Offset: 0x0011F3E8
		// (set) Token: 0x06005E9A RID: 24218 RVA: 0x0012121D File Offset: 0x0011F41D
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether RadAsyncUpload Specifies whether RadAsyncUpload displays an inline progress next to each file being uploaded.")]
		[DefaultValue(true)]
		public bool EnablePermissionsCheck
		{
			get
			{
				return ((bool?)this.ViewState["EnablePermissionsCheck"]) ?? true;
			}
			set
			{
				this.ViewState["EnablePermissionsCheck"] = value;
			}
		}

		// Token: 0x17001F34 RID: 7988
		// (get) Token: 0x06005E9B RID: 24219 RVA: 0x00121238 File Offset: 0x0011F438
		// (set) Token: 0x06005E9C RID: 24220 RVA: 0x0012126E File Offset: 0x0011F46E
		[DefaultValue(ControlObjectsVisibility.Default)]
		[Bindable(true)]
		[Description("Gets or sets the value indicating which control objects will be displayed. This property is obsolete in RadAsyncUpload and is not used.")]
		[Obsolete("This property is obsolete in RadAsyncUpload")]
		[Browsable(false)]
		public virtual ControlObjectsVisibility ControlObjectsVisibility
		{
			get
			{
				ControlObjectsVisibility? controlObjectsVisibility = (ControlObjectsVisibility?)this.ViewState["ControlObjectsVisibility"];
				if (controlObjectsVisibility == null)
				{
					return ControlObjectsVisibility.Default;
				}
				return controlObjectsVisibility.GetValueOrDefault();
			}
			set
			{
				this.ViewState["ControlObjectsVisibility"] = (int)value;
			}
		}

		// Token: 0x17001F35 RID: 7989
		// (get) Token: 0x06005E9D RID: 24221 RVA: 0x00121286 File Offset: 0x0011F486
		// (set) Token: 0x06005E9E RID: 24222 RVA: 0x001212A6 File Offset: 0x0011F4A6
		[Description("Gets or sets the name of the client-side function which will be executed all selected files have been uploaded.")]
		[ClientControlEvent]
		[ClientPropertyName("filesUploaded")]
		[DefaultValue("")]
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

		// Token: 0x17001F36 RID: 7990
		// (get) Token: 0x06005E9F RID: 24223 RVA: 0x001212B9 File Offset: 0x0011F4B9
		// (set) Token: 0x06005EA0 RID: 24224 RVA: 0x001212D9 File Offset: 0x0011F4D9
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[Bindable(true)]
		[ClientPropertyName("adding")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed before a new fileinput is added to a RadAsyncUpload instance.")]
		[Category("Client-side events")]
		[Browsable(false)]
		protected string OnClientAdding
		{
			get
			{
				return ((string)this.ViewState["OnClientAdding"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientAdding"] = value;
			}
		}

		// Token: 0x17001F37 RID: 7991
		// (get) Token: 0x06005EA1 RID: 24225 RVA: 0x001212EC File Offset: 0x0011F4EC
		// (set) Token: 0x06005EA2 RID: 24226 RVA: 0x0012130C File Offset: 0x0011F50C
		[Bindable(true)]
		[Description("Gets or sets the name of the client-side function which will be executed after a new file input is added to a RadUAsyncpload instance.")]
		[Browsable(true)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("added")]
		[DefaultValue("")]
		public string OnClientAdded
		{
			get
			{
				return ((string)this.ViewState["OnClientAdded"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientAdded"] = value;
			}
		}

		// Token: 0x17001F38 RID: 7992
		// (get) Token: 0x06005EA3 RID: 24227 RVA: 0x0012131F File Offset: 0x0011F51F
		// (set) Token: 0x06005EA4 RID: 24228 RVA: 0x00121340 File Offset: 0x0011F540
		[ClientControlProperty]
		[Description("Gets or sets a value indicating whether RadAsyncUpload allows selecting multiple files in the File Selection dialog.")]
		[DefaultValue(MultipleFileSelection.Disabled)]
		[Category("Behavior")]
		[ClientPropertyName("_multipleFileSelection")]
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

		// Token: 0x17001F39 RID: 7993
		// (get) Token: 0x06005EA5 RID: 24229 RVA: 0x00121358 File Offset: 0x0011F558
		// (set) Token: 0x06005EA6 RID: 24230 RVA: 0x00121379 File Offset: 0x0011F579
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether RadAsyncUpload will render the uploaded files above/below the current file input.")]
		[DefaultValue(UploadedFilesRendering.AboveFileInput)]
		[ClientControlProperty]
		[ClientPropertyName("_uploadedFilesRendering")]
		public UploadedFilesRendering UploadedFilesRendering
		{
			get
			{
				return (UploadedFilesRendering)(this.ViewState["UploadedFilesRendering"] ?? UploadedFilesRendering.AboveFileInput);
			}
			set
			{
				this.ViewState["UploadedFilesRendering"] = value;
			}
		}

		// Token: 0x17001F3A RID: 7994
		// (get) Token: 0x06005EA7 RID: 24231 RVA: 0x00121391 File Offset: 0x0011F591
		// (set) Token: 0x06005EA8 RID: 24232 RVA: 0x00121399 File Offset: 0x0011F599
		[Browsable(false)]
		[Description("Sets upload configuration. The generic object can be obtained using the CreateUploadConfiguration<T> method, where T is custom class that implements IAsyncUploadConfiguration")]
		public IAsyncUploadConfiguration UploadConfiguration
		{
			get
			{
				return this.uploadConfiguration;
			}
			set
			{
				this.uploadConfiguration = value;
			}
		}

		// Token: 0x17001F3B RID: 7995
		// (get) Token: 0x06005EA9 RID: 24233 RVA: 0x001213A2 File Offset: 0x0011F5A2
		// (set) Token: 0x06005EAA RID: 24234 RVA: 0x001213C2 File Offset: 0x0011F5C2
		[UrlProperty("*.ShowOnlyPickUpURL")]
		[Description("Specifies the URL of the HTTPHandler from which the image will be served.")]
		[DefaultValue("~/Telerik.Web.UI.WebResource.axd")]
		[Category("Advanced")]
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

		// Token: 0x140000DF RID: 223
		// (add) Token: 0x06005EAB RID: 24235 RVA: 0x001213E3 File Offset: 0x0011F5E3
		// (remove) Token: 0x06005EAC RID: 24236 RVA: 0x001213F6 File Offset: 0x0011F5F6
		public event FileUploadedEventHandler FileUploaded
		{
			add
			{
				base.Events.AddHandler(RadAsyncUpload.FileUploadedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadAsyncUpload.FileUploadedEvent, value);
			}
		}

		// Token: 0x17001F3C RID: 7996
		// (get) Token: 0x06005EAD RID: 24237 RVA: 0x0012140C File Offset: 0x0011F60C
		// (set) Token: 0x06005EAE RID: 24238 RVA: 0x00121465 File Offset: 0x0011F665
		[Category("Behavior")]
		[Description("Gets or sets the path to a folder where RadAsyncUpload should save files temporarily until a postback occurs.")]
		[DefaultValue("~/App_Data/RadUploadTemp")]
		public string TemporaryFolder
		{
			get
			{
				string text = ConfigurationManager.AppSettings["Telerik.AsyncUpload.TemporaryFolder"];
				if (!string.IsNullOrEmpty((string)this.ViewState["TemporaryFolder"]))
				{
					return (string)this.ViewState["TemporaryFolder"];
				}
				return text ?? "~/App_Data/RadUploadTemp";
			}
			set
			{
				this.ViewState["TemporaryFolder"] = value;
			}
		}

		// Token: 0x17001F3D RID: 7997
		// (get) Token: 0x06005EAF RID: 24239 RVA: 0x00121478 File Offset: 0x0011F678
		// (set) Token: 0x06005EB0 RID: 24240 RVA: 0x001214B4 File Offset: 0x0011F6B4
		[Category("Behavior")]
		[DefaultValue(typeof(TimeSpan), "04:00:00")]
		[Description("Sets how long temporary files should be kept before automatically deleting them.")]
		public TimeSpan TemporaryFileExpiration
		{
			get
			{
				TimeSpan? timeSpan = (TimeSpan?)this.ViewState["TemporaryFileExpiration"];
				if (timeSpan == null)
				{
					return new TimeSpan(4, 0, 0);
				}
				return timeSpan.GetValueOrDefault();
			}
			set
			{
				this.ViewState["TemporaryFileExpiration"] = value;
			}
		}

		// Token: 0x17001F3E RID: 7998
		// (get) Token: 0x06005EB1 RID: 24241 RVA: 0x001214CC File Offset: 0x0011F6CC
		// (set) Token: 0x06005EB2 RID: 24242 RVA: 0x001214EC File Offset: 0x0011F6EC
		[ClientControlEvent]
		[ClientPropertyName("fileUploading")]
		[Bindable(true)]
		[DefaultValue("")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the client-side function which will be executed when a file upload starts.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
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

		// Token: 0x17001F3F RID: 7999
		// (get) Token: 0x06005EB3 RID: 24243 RVA: 0x001214FF File Offset: 0x0011F6FF
		// (set) Token: 0x06005EB4 RID: 24244 RVA: 0x0012151F File Offset: 0x0011F71F
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed when a file upload finishes successfully.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Bindable(true)]
		[ClientControlEvent]
		[ClientPropertyName("fileUploaded")]
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

		// Token: 0x17001F40 RID: 8000
		// (get) Token: 0x06005EB5 RID: 24245 RVA: 0x00121532 File Offset: 0x0011F732
		// (set) Token: 0x06005EB6 RID: 24246 RVA: 0x00121549 File Offset: 0x0011F749
		[Category("Client-side events")]
		[ClientPropertyName("filesSelected")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed after files have been selected.")]
		[Browsable(true)]
		[Bindable(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		public string OnClientFilesSelected
		{
			get
			{
				return (string)this.ViewState["OnClientFilesSelected"];
			}
			set
			{
				this.ViewState["OnClientFilesSelected"] = value;
			}
		}

		// Token: 0x17001F41 RID: 8001
		// (get) Token: 0x06005EB7 RID: 24247 RVA: 0x0012155C File Offset: 0x0011F75C
		// (set) Token: 0x06005EB8 RID: 24248 RVA: 0x00121573 File Offset: 0x0011F773
		[Category("Client-side events")]
		[ClientPropertyName("fileDropped")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed after a file has been dropped.")]
		[Bindable(true)]
		[ClientControlEvent]
		[Browsable(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string OnClientFileDropped
		{
			get
			{
				return (string)this.ViewState["OnClientFileDropped"];
			}
			set
			{
				this.ViewState["OnClientFileDropped"] = value;
			}
		}

		// Token: 0x17001F42 RID: 8002
		// (get) Token: 0x06005EB9 RID: 24249 RVA: 0x00121586 File Offset: 0x0011F786
		// (set) Token: 0x06005EBA RID: 24250 RVA: 0x0012159D File Offset: 0x0011F79D
		[Category("Client-side events")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed after a file has been selected.")]
		[Bindable(true)]
		[Browsable(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("fileSelected")]
		public string OnClientFileSelected
		{
			get
			{
				return (string)this.ViewState["OnClientFileSelected"];
			}
			set
			{
				this.ViewState["OnClientFileSelected"] = value;
			}
		}

		// Token: 0x17001F43 RID: 8003
		// (get) Token: 0x06005EBB RID: 24251 RVA: 0x001215B0 File Offset: 0x0011F7B0
		// (set) Token: 0x06005EBC RID: 24252 RVA: 0x001215D0 File Offset: 0x0011F7D0
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		[Bindable(true)]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the client-side function which will be executed when a file upload ends unsuccessfully.")]
		[ClientPropertyName("fileUploadFailed")]
		public string OnClientFileUploadFailed
		{
			get
			{
				return ((string)this.ViewState["OnClientFileUploadFailed"]) ?? "";
			}
			set
			{
				this.ViewState["OnClientFileUploadFailed"] = value;
			}
		}

		// Token: 0x17001F44 RID: 8004
		// (get) Token: 0x06005EBD RID: 24253 RVA: 0x001215E3 File Offset: 0x0011F7E3
		// (set) Token: 0x06005EBE RID: 24254 RVA: 0x00121603 File Offset: 0x0011F803
		[Description("Gets or sets the name of the client-side function which will be executed if the selected file has invalid extension")]
		[DefaultValue("")]
		[ClientPropertyName("validationFailed")]
		[Bindable(true)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
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

		// Token: 0x17001F45 RID: 8005
		// (get) Token: 0x06005EBF RID: 24255 RVA: 0x00121616 File Offset: 0x0011F816
		// (set) Token: 0x06005EC0 RID: 24256 RVA: 0x00121636 File Offset: 0x0011F836
		[Description("Gets or sets the name of the client-side function which will be executed before a file input is deleted from a RadAsyncUpload instance.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Bindable(true)]
		[Browsable(true)]
		[ClientControlEvent]
		[ClientPropertyName("fileUploadRemoving")]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnClientFileUploadRemoving
		{
			get
			{
				return ((string)this.ViewState["OnClientFileUploadRemoving"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientFileUploadRemoving"] = value;
			}
		}

		// Token: 0x17001F46 RID: 8006
		// (get) Token: 0x06005EC1 RID: 24257 RVA: 0x00121649 File Offset: 0x0011F849
		// (set) Token: 0x06005EC2 RID: 24258 RVA: 0x00121669 File Offset: 0x0011F869
		[ClientPropertyName("fileUploadRemoved")]
		[Category("Client-side events")]
		[Description("Gets or sets the name of the client-side function which will be executed after a file input has been deleted from a RadAsyncUpload instance.")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		[Browsable(true)]
		[Bindable(true)]
		public string OnClientFileUploadRemoved
		{
			get
			{
				return ((string)this.ViewState["OnClientFileUploadRemoved"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientFileUploadRemoved"] = value;
			}
		}

		// Token: 0x17001F47 RID: 8007
		// (get) Token: 0x06005EC3 RID: 24259 RVA: 0x0012167C File Offset: 0x0011F87C
		// (set) Token: 0x06005EC4 RID: 24260 RVA: 0x0012169C File Offset: 0x0011F89C
		[ClientControlEvent]
		[Category("Client-side events")]
		[DefaultValue("")]
		[Browsable(true)]
		[Bindable(true)]
		[Description("Gets or sets the name of the client-side function which will be executed on an inline progress update")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientPropertyName("progressUpdating")]
		public string OnClientProgressUpdating
		{
			get
			{
				return ((string)this.ViewState["OnClientProgressUpdating"]) ?? string.Empty;
			}
			set
			{
				this.ViewState["OnClientProgressUpdating"] = value;
			}
		}

		// Token: 0x17001F48 RID: 8008
		// (get) Token: 0x06005EC5 RID: 24261 RVA: 0x001216B0 File Offset: 0x0011F8B0
		// (set) Token: 0x06005EC6 RID: 24262 RVA: 0x001216E5 File Offset: 0x0011F8E5
		[Bindable(true)]
		[ClientControlProperty]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Gets or sets the maximum file size allowed for uploading in bytes. Set to 0 for unlimited size.")]
		[ClientPropertyName("_maxFileSize")]
		[Category("Behavior")]
		[DefaultValue(0)]
		public int MaxFileSize
		{
			get
			{
				int? num = (int?)this.ViewState["MaxFileSize"];
				if (num == null)
				{
					return 0;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				this.ViewState["MaxFileSize"] = value;
			}
		}

		// Token: 0x17001F49 RID: 8009
		// (get) Token: 0x06005EC7 RID: 24263 RVA: 0x00121700 File Offset: 0x0011F900
		// (set) Token: 0x06005EC8 RID: 24264 RVA: 0x00121739 File Offset: 0x0011F939
		[PersistenceMode(PersistenceMode.Attribute)]
		[DefaultValue(2097152)]
		[Description("Gets or sets the size of the uploading chunks.")]
		[Category("Behavior")]
		public int ChunkSize
		{
			get
			{
				int? num = (int?)this.ViewState["ChunkSize"];
				if (num == null)
				{
					return 2097152;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				this.ViewState["ChunkSize"] = value;
			}
		}

		// Token: 0x17001F4A RID: 8010
		// (get) Token: 0x06005EC9 RID: 24265 RVA: 0x00121754 File Offset: 0x0011F954
		// (set) Token: 0x06005ECA RID: 24266 RVA: 0x00121789 File Offset: 0x0011F989
		[Browsable(true)]
		[DefaultValue(false)]
		[Description("Gets or sets whether the upload configuration to be persisted into ControlState(if the upload configuration is different than null).")]
		[Category("Behavior")]
		public bool PersistConfiguration
		{
			get
			{
				return ((bool?)this.ViewState["PersistConfiguration"]) ?? false;
			}
			set
			{
				this.ViewState["PersistConfiguration"] = value;
			}
		}

		// Token: 0x17001F4B RID: 8011
		// (get) Token: 0x06005ECB RID: 24267 RVA: 0x001217A1 File Offset: 0x0011F9A1
		// (set) Token: 0x06005ECC RID: 24268 RVA: 0x001217C2 File Offset: 0x0011F9C2
		[Bindable(true)]
		[Description("Gets or sets whether the client state to be persisted(if the postback is triggered by particular control).")]
		[TypeConverter(typeof(ListConverter))]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Behavior")]
		public string[] PostbackTriggers
		{
			get
			{
				return ((string[])this.ViewState["PostbackTriggers"]) ?? new string[0];
			}
			set
			{
				this.ViewState["PostbackTriggers"] = value;
			}
		}

		// Token: 0x17001F4C RID: 8012
		// (get) Token: 0x06005ECD RID: 24269 RVA: 0x001217D8 File Offset: 0x0011F9D8
		// (set) Token: 0x06005ECE RID: 24270 RVA: 0x0012180D File Offset: 0x0011FA0D
		[ClientControlProperty]
		[Description("Gets or sets the initial count of file input fields, which will appear in RadAsyncUpload.")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[ClientPropertyName("initialFileInputsCount")]
		[DefaultValue(1)]
		public int InitialFileInputsCount
		{
			get
			{
				int? num = (int?)this.ViewState["InitialFileInputsCount"];
				if (num == null)
				{
					return 1;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				this.ViewState["InitialFileInputsCount"] = value;
			}
		}

		// Token: 0x17001F4D RID: 8013
		// (get) Token: 0x06005ECF RID: 24271 RVA: 0x00121828 File Offset: 0x0011FA28
		// (set) Token: 0x06005ED0 RID: 24272 RVA: 0x0012185D File Offset: 0x0011FA5D
		[DefaultValue(0)]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Gets or sets the maximum file input fields that can be added to the control. MaxFileInputs count is only applicable when MultipleFileSelection is set to Disabled")]
		[Bindable(true)]
		[ClientPropertyName("maxFileCount")]
		[ClientControlProperty]
		[Category("Behavior")]
		public int MaxFileInputsCount
		{
			get
			{
				int? num = (int?)this.ViewState["MaxFileInputsCount"];
				if (num == null)
				{
					return 0;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				this.ViewState["MaxFileInputsCount"] = value;
			}
		}

		// Token: 0x17001F4E RID: 8014
		// (get) Token: 0x06005ED1 RID: 24273 RVA: 0x00121878 File Offset: 0x0011FA78
		// (set) Token: 0x06005ED2 RID: 24274 RVA: 0x001218AE File Offset: 0x0011FAAE
		[PersistenceMode(PersistenceMode.Attribute)]
		[ClientPropertyName("inputSize")]
		[ClientControlProperty]
		[Bindable(true)]
		[DefaultValue(23)]
		[Description("Gets or sets the size of the file input field.")]
		[Category("Behavior")]
		public int InputSize
		{
			get
			{
				int? num = (int?)this.ViewState["InputSize"];
				if (num == null)
				{
					return 23;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				this.ViewState["InputSize"] = value;
			}
		}

		// Token: 0x17001F4F RID: 8015
		// (get) Token: 0x06005ED3 RID: 24275 RVA: 0x001218C6 File Offset: 0x0011FAC6
		[Browsable(false)]
		public UploadedFileCollection UploadedFiles
		{
			get
			{
				return this._uploadedFiles;
			}
		}

		// Token: 0x17001F50 RID: 8016
		// (get) Token: 0x06005ED4 RID: 24276 RVA: 0x001218CE File Offset: 0x0011FACE
		// (set) Token: 0x06005ED5 RID: 24277 RVA: 0x001218EE File Offset: 0x0011FAEE
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("Gets or sets the virtual path of the folder, where RadUpload will automatically save the valid files after the upload completes.")]
		[Browsable(true)]
		[Bindable(true)]
		public string TargetFolder
		{
			get
			{
				return ((string)this.ViewState["TargetFolder"]) ?? "";
			}
			set
			{
				this.ViewState["TargetFolder"] = value;
			}
		}

		// Token: 0x17001F51 RID: 8017
		// (get) Token: 0x06005ED6 RID: 24278 RVA: 0x00121904 File Offset: 0x0011FB04
		// (set) Token: 0x06005ED7 RID: 24279 RVA: 0x00121939 File Offset: 0x0011FB39
		[ClientControlProperty]
		[Description("Gets or sets the value indicating whether the file input fields skinning will be enabled.")]
		[ClientPropertyName("enableFileInputSkinning")]
		[Browsable(true)]
		[Category("Appearance")]
		[Bindable(true)]
		[DefaultValue(true)]
		public bool EnableFileInputSkinning
		{
			get
			{
				return ((bool?)this.ViewState["EnableFileInputSkinning"]) ?? true;
			}
			set
			{
				this.ViewState["EnableFileInputSkinning"] = value;
			}
		}

		// Token: 0x17001F52 RID: 8018
		// (get) Token: 0x06005ED8 RID: 24280 RVA: 0x00121954 File Offset: 0x0011FB54
		// (set) Token: 0x06005ED9 RID: 24281 RVA: 0x00121989 File Offset: 0x0011FB89
		[Category("Behavior")]
		[Description("Gets or sets a value indicating if RadAsyncUpload should check the Telerik.Web.UI.WebResource handler existence in the application configuration file.")]
		[DefaultValue(true)]
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

		// Token: 0x06005EDA RID: 24282 RVA: 0x001219A4 File Offset: 0x0011FBA4
		public T CreateDefaultUploadConfiguration<T>() where T : IAsyncUploadConfiguration, new()
		{
			T result = (default(T) == null) ? Activator.CreateInstance<T>() : default(T);
			result.MaxFileSize = this.MaxFileSize;
			result.TargetFolder = this.TargetFolder;
			result.TempTargetFolder = this.TemporaryFolder;
			result.TimeToLive = this.TemporaryFileExpiration;
			return result;
		}

		// Token: 0x06005EDC RID: 24284 RVA: 0x00121A1F File Offset: 0x0011FC1F
		// Note: this type is marked as 'beforefieldinit'.
		static RadAsyncUpload()
		{
			RadAsyncUpload.FileUploadedEvent = new object();
		}

		// Token: 0x040016BA RID: 5818
		internal const string DefaultProgressHandlerUrl = "~/Telerik.RadUploadProgressHandler.ashx";

		// Token: 0x040016BB RID: 5819
		internal const string FlashModuleWebResourceName = "Telerik.Web.UI.AsyncUpload.Modules.Flash.AsyncUploadModule.swf";

		// Token: 0x040016BC RID: 5820
		internal const string SilverlightModuleWebResourceName = "Telerik.Web.UI.AsyncUpload.Modules.Silverlight.src.AsyncUploadModule.xap";

		// Token: 0x040016BD RID: 5821
		internal const string InputsCssClass = "ruInputs";

		// Token: 0x040016BE RID: 5822
		internal const string FileWrapCssClass = "ruFileWrap";

		// Token: 0x040016BF RID: 5823
		internal const string StyledWrapCssClass = "ruStyled";

		// Token: 0x040016C0 RID: 5824
		internal const string FakeInputCssClss = "ruFakeInput radPreventDecorate";

		// Token: 0x040016C1 RID: 5825
		internal const string RemoveButtonCssClass = "ruRemove";

		// Token: 0x040016C2 RID: 5826
		internal const string BrowseButtonCssClass = "ruBrowse";

		// Token: 0x040016C3 RID: 5827
		internal const string ButtonCssClass = "ruButton";

		// Token: 0x040016C4 RID: 5828
		internal const string TemporaryDefaultPath = "~/App_Data/RadUploadTemp";

		// Token: 0x040016C5 RID: 5829
		internal const string TemporaryFolderKey = "Telerik.AsyncUpload.TemporaryFolder";

		// Token: 0x040016C6 RID: 5830
		private const string handlerDefaultUrl = "~/Telerik.Web.UI.WebResource.axd";

		// Token: 0x040016C7 RID: 5831
		private string _encryptedTemporaryFolder;

		// Token: 0x040016C8 RID: 5832
		private string _encryptedTargetFolder;

		// Token: 0x040016C9 RID: 5833
		private UploadedFileCollection _uploadedFiles = new UploadedFileCollection();

		// Token: 0x040016CA RID: 5834
		private FilterFormatter _filterFormatter;

		// Token: 0x040016CB RID: 5835
		private string _uploadRequestIdentifier;

		// Token: 0x040016CD RID: 5837
		private IAsyncUploadConfiguration uploadConfiguration;

		// Token: 0x040016CE RID: 5838
		private WaiAriaSettings _ariaSettings;

		// Token: 0x040016CF RID: 5839
		private KeyboardNavigationSettings _keyboardNavigationSettings;

		// Token: 0x040016D0 RID: 5840
		private AsyncUploadStrings _localization;

		// Token: 0x040016D1 RID: 5841
		private FileFilterCollection _filters;
	}
}
