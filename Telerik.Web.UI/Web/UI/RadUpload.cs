using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.Design;
using Telerik.Web.UI.Upload;

namespace Telerik.Web.UI
{
	// Token: 0x0200134B RID: 4939
	[TelerikToolboxCategory("Upload")]
	[ToolboxBitmap(typeof(RadUpload), "Telerik.Web.UI.Upload.png")]
	[EmbeddedSkin("Upload", "Default", typeof(RadUpload))]
	[ToolboxData("<{0}:RadUpload Runat=server></{0}:RadUpload>")]
	[RequiredScript(typeof(Core))]
	[EmbeddedSkin("Upload", typeof(RadUpload))]
	[RequiredScript(typeof(jQueryPlugins))]
	[ClientScriptResource("Telerik.Web.UI.RadUpload", "Telerik.Web.UI.Upload.RadUpload.js")]
	[Designer("Telerik.Web.Design.RadUploadDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	public class RadUpload : RadWebControl, ILocalizableControl
	{
		// Token: 0x17004239 RID: 16953
		// (get) Token: 0x0600CDFA RID: 52730 RVA: 0x002DD34C File Offset: 0x002DB54C
		protected override string CssClassFormatString
		{
			get
			{
				string text = "RadUpload RadUpload_{0}";
				if (base.Attributes["dir"] == "rtl")
				{
					text += " RadUpload_rtl RadUpload_{0}_rtl";
				}
				return text;
			}
		}

		// Token: 0x1700423A RID: 16954
		// (get) Token: 0x0600CDFB RID: 52731 RVA: 0x002DD388 File Offset: 0x002DB588
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x0600CDFC RID: 52732 RVA: 0x002DD38C File Offset: 0x002DB58C
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Unit height = this.Height;
			this.Height = Unit.Empty;
			string accessKey = this.AccessKey;
			this.AccessKey = "";
			base.AddAttributesToRender(writer);
			this.Height = height;
			this.AccessKey = accessKey;
		}

		// Token: 0x0600CDFD RID: 52733 RVA: 0x002DD3D4 File Offset: 0x002DB5D4
		protected override void RenderContents(HtmlTextWriter writer)
		{
			if (base.DesignMode)
			{
				writer.Write(SkinRegistrar.GetDesignTimeStyleSheet(this));
			}
			if (base.DesignMode || this.DisplayAddButton || this.DisplayDeleteSelectedButton)
			{
				this.RenderListContainer(writer);
				if (base.DesignMode)
				{
					this.RenderFileInputRows(writer);
				}
				this.RenderPostListButtons(writer);
				writer.RenderEndTag();
			}
		}

		// Token: 0x0600CDFE RID: 52734 RVA: 0x002DD430 File Offset: 0x002DB630
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

		// Token: 0x0600CDFF RID: 52735 RVA: 0x002DD498 File Offset: 0x002DB698
		private void RenderPostListButtons(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ruActions");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}ButtonArea", this.ClientID));
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			if (this.DisplayAddButton)
			{
				this.RenderButtonInput(writer, "AddButton", "ruAdd", this.Localization.Add);
			}
			if (this.DisplayDeleteSelectedButton)
			{
				this.RenderButtonInput(writer, "DeleteButton", "ruDelete", this.Localization.Delete);
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600CE00 RID: 52736 RVA: 0x002DD520 File Offset: 0x002DB720
		private void RenderFileInputRows(HtmlTextWriter writer)
		{
			for (int i = 0; i < this.InitialFileInputsCount; i++)
			{
				this.RenderFileInputRow(writer, i);
			}
		}

		// Token: 0x0600CE01 RID: 52737 RVA: 0x002DD548 File Offset: 0x002DB748
		private void RenderFileInputRow(HtmlTextWriter writer, int rowIndex)
		{
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			if ((this.ControlObjectsVisibility & ControlObjectsVisibility.CheckBoxes) > ControlObjectsVisibility.None)
			{
				RadUpload.RenderCheckBox(writer);
			}
			this.RenderFileInputField(writer, rowIndex);
			if ((this.ControlObjectsVisibility & ControlObjectsVisibility.ClearButtons) > ControlObjectsVisibility.None)
			{
				this.RenderButtonInput(writer, string.Format("Clear{0}", rowIndex), "ruClear", this.Localization.Clear);
			}
			if ((this.ControlObjectsVisibility & ControlObjectsVisibility.RemoveButtons) > ControlObjectsVisibility.None)
			{
				this.RenderButtonInput(writer, string.Format("Remove{0}", rowIndex), "ruRemove", this.Localization.Remove);
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600CE02 RID: 52738 RVA: 0x002DD5E0 File Offset: 0x002DB7E0
		private static void RenderCheckBox(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ruCheck");
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x0600CE03 RID: 52739 RVA: 0x002DD60A File Offset: 0x002DB80A
		private void RenderFileInputField(HtmlTextWriter writer, int rowIndex)
		{
			if (this.EnableFileInputSkinning)
			{
				this.RenderSkinnedFileInputField(writer, rowIndex);
				return;
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ruFileWrap");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			this.RenderFileInput(writer, rowIndex);
			writer.RenderEndTag();
		}

		// Token: 0x0600CE04 RID: 52740 RVA: 0x002DD640 File Offset: 0x002DB840
		private void RenderFileInput(HtmlTextWriter writer, int rowIndex)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "file");
			string value = string.Format("{0}file{1}", this.ClientID, rowIndex);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, value);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, value);
			if (!this.Enabled)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
			}
			if (this.ReadOnlyFileInputs)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "true");
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Size, this.InputSize.ToString());
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x0600CE05 RID: 52741 RVA: 0x002DD6D4 File Offset: 0x002DB8D4
		private void RenderSkinnedFileInputField(HtmlTextWriter writer, int rowIndex)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ruFileWrap, ruStyled");
			writer.RenderBeginTag(HtmlTextWriterTag.Span);
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "ruFakeInput radPreventDecorate");
			writer.AddAttribute(HtmlTextWriterAttribute.Size, (this.InputSize - 1).ToString());
			if (this.ReadOnlyFileInputs)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.ReadOnly, "true");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
			this.RenderButtonInput(writer, string.Format("Select{0}", rowIndex), "ruBrowse", this.Localization.Select);
			writer.RenderEndTag();
		}

		// Token: 0x0600CE06 RID: 52742 RVA: 0x002DD778 File Offset: 0x002DB978
		private void RenderButtonInput(HtmlTextWriter writer, string shortId, string buttonSpecificClassName)
		{
			this.RenderButtonInput(writer, shortId, buttonSpecificClassName, null);
		}

		// Token: 0x0600CE07 RID: 52743 RVA: 0x002DD784 File Offset: 0x002DB984
		private void RenderButtonInput(HtmlTextWriter writer, string shortId, string buttonSpecificClassName, string value)
		{
			if (!string.IsNullOrEmpty(buttonSpecificClassName))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, string.Format("ruButton {0}", buttonSpecificClassName));
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			writer.AddAttribute(HtmlTextWriterAttribute.Id, string.Format("{0}{1}", this.ClientID, shortId));
			if (!string.IsNullOrEmpty(value))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Value, value);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		// Token: 0x1700423B RID: 16955
		// (get) Token: 0x0600CE08 RID: 52744 RVA: 0x002DD7F3 File Offset: 0x002DB9F3
		private bool DisplayAddButton
		{
			get
			{
				return (this.ControlObjectsVisibility & ControlObjectsVisibility.AddButton) > ControlObjectsVisibility.None;
			}
		}

		// Token: 0x1700423C RID: 16956
		// (get) Token: 0x0600CE09 RID: 52745 RVA: 0x002DD800 File Offset: 0x002DBA00
		private bool DisplayDeleteSelectedButton
		{
			get
			{
				return (this.ControlObjectsVisibility & ControlObjectsVisibility.DeleteSelectedButton) > ControlObjectsVisibility.None;
			}
		}

		// Token: 0x1700423D RID: 16957
		// (get) Token: 0x0600CE0A RID: 52746 RVA: 0x002DD80E File Offset: 0x002DBA0E
		private bool CheckingFileSize
		{
			get
			{
				return this.MaxFileSize > 0;
			}
		}

		// Token: 0x1700423E RID: 16958
		// (get) Token: 0x0600CE0B RID: 52747 RVA: 0x002DD819 File Offset: 0x002DBA19
		private bool CheckingFileExtension
		{
			get
			{
				return this.AllowedFileExtensions.Length > 0;
			}
		}

		// Token: 0x1700423F RID: 16959
		// (get) Token: 0x0600CE0C RID: 52748 RVA: 0x002DD826 File Offset: 0x002DBA26
		private bool CheckingMimeType
		{
			get
			{
				return this.AllowedMimeTypes.Length > 0;
			}
		}

		// Token: 0x0600CE0D RID: 52749 RVA: 0x002DD834 File Offset: 0x002DBA34
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			RadUploadClientSide radUploadClientSide = javaScriptSerializer.Deserialize<RadUploadClientSide>(text);
			if (radUploadClientSide.IsEnabled)
			{
				this.Enabled = true;
				return true;
			}
			return false;
		}

		// Token: 0x0600CE0E RID: 52750 RVA: 0x002DD878 File Offset: 0x002DBA78
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.PopulateUploadedFiles();
		}

		// Token: 0x0600CE0F RID: 52751 RVA: 0x002DD887 File Offset: 0x002DBA87
		protected override void OnLoad(EventArgs e)
		{
			this.ValidateUploadedFiles();
			this.ProcessValidFiles();
			base.OnLoad(e);
		}

		// Token: 0x0600CE10 RID: 52752 RVA: 0x002DD89C File Offset: 0x002DBA9C
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			string jsArray = Utility.GetJsArray(this.AllowedFileExtensions);
			descriptor.AddProperty("allowedFileExtensions", jsArray.ToString());
			descriptor.AddProperty("readOnlyFileInputs", !this.Enabled || this.ReadOnlyFileInputs);
			descriptor.AddScriptProperty("localization", new JavaScriptSerializer().Serialize(this.Localization));
			descriptor.AddProperty("_accessKey", this.AccessKey);
			if (!this.DisplayAddButton && !this.DisplayDeleteSelectedButton && this.Height != Unit.Empty)
			{
				descriptor.AddProperty("_height", this.Height.ToString());
			}
			descriptor.AddProperty("_skin", base.RuntimeSkin);
		}

		// Token: 0x0600CE11 RID: 52753 RVA: 0x002DD96C File Offset: 0x002DBB6C
		protected bool IsValidUploadedFile(UploadedFile file)
		{
			return this.IsValidUploadedFile(file, this.CheckingFileSize, this.CheckingFileExtension, this.CheckingMimeType);
		}

		// Token: 0x0600CE12 RID: 52754 RVA: 0x002DD988 File Offset: 0x002DBB88
		protected bool IsValidUploadedFile(UploadedFile file, bool checkingFileSize, bool checkingFileExtension, bool checkingMimeType)
		{
			ValidateFileEventArgs validateFileEventArgs = new ValidateFileEventArgs(file);
			if (!this.OnValidatingFile(validateFileEventArgs))
			{
				return false;
			}
			if (!validateFileEventArgs.SkipInternalValidation)
			{
				if (checkingFileSize && !this.IsValidSize(file))
				{
					return false;
				}
				if (checkingFileExtension && !this.IsValidExtension(file))
				{
					return false;
				}
				if (checkingMimeType && !this.IsValidMimeType(file))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600CE13 RID: 52755 RVA: 0x002DD9DB File Offset: 0x002DBBDB
		protected bool IsValidSize(UploadedFile file)
		{
			return file.ContentLength <= (long)this.MaxFileSize;
		}

		// Token: 0x0600CE14 RID: 52756 RVA: 0x002DD9F0 File Offset: 0x002DBBF0
		protected bool IsValidExtension(UploadedFile file)
		{
			foreach (string text in this.AllowedFileExtensions)
			{
				string text2 = text.Trim();
				int num = text2.IndexOf('.');
				string text3 = text2;
				if (num == -1)
				{
					text3 = '.' + text2;
				}
				if (file.GetExtension().ToLower() == text3.ToLower())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600CE15 RID: 52757 RVA: 0x002DDA64 File Offset: 0x002DBC64
		protected bool IsValidMimeType(UploadedFile file)
		{
			foreach (string text in this.AllowedMimeTypes)
			{
				if (file.ContentType.ToLower() == text.ToLower())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600CE16 RID: 52758 RVA: 0x002DDAAC File Offset: 0x002DBCAC
		private HtmlForm GetContainerForm(Control parent)
		{
			HtmlForm htmlForm = parent as HtmlForm;
			if (htmlForm != null || parent == null)
			{
				return htmlForm;
			}
			return this.GetContainerForm(parent.Parent);
		}

		// Token: 0x0600CE17 RID: 52759 RVA: 0x002DDAD4 File Offset: 0x002DBCD4
		private string GetTargetFolder()
		{
			bool flag = this.TargetPhysicalFolder.Length > 0;
			string text = flag ? this.TargetPhysicalFolder : this.TargetFolder;
			if (text.Length == 0)
			{
				return string.Empty;
			}
			if (!flag)
			{
				text = this.Context.Server.MapPath(text);
			}
			return text;
		}

		// Token: 0x0600CE18 RID: 52760 RVA: 0x002DDB28 File Offset: 0x002DBD28
		protected virtual void PopulateUploadedFiles()
		{
			Regex regex = new Regex("(?<FileInputIdEndSubstring>file\\d+)$");
			if (!base.DesignMode)
			{
				HttpFileCollection files = HttpContext.Current.Request.Files;
				foreach (string text in files.AllKeys)
				{
					Match match = regex.Match(text);
					if (match.Success)
					{
						string text2 = match.Result("${FileInputIdEndSubstring}");
						string a = text.Substring(0, text.Length - text2.Length);
						if (a == this.ClientID && !string.IsNullOrEmpty(files[text].FileName))
						{
							this._uploadedFiles.Add(new PostedFile(text, files[text]));
						}
					}
				}
			}
		}

		// Token: 0x0600CE19 RID: 52761 RVA: 0x002DDBF8 File Offset: 0x002DBDF8
		protected virtual void ValidateUploadedFiles()
		{
			bool checkingFileSize = this.CheckingFileSize;
			bool checkingFileExtension = this.CheckingFileExtension;
			bool checkingMimeType = this.CheckingMimeType;
			for (int i = this.UploadedFiles.Count - 1; i >= 0; i--)
			{
				UploadedFile uploadedFile = this.UploadedFiles[i];
				if (!this.IsValidUploadedFile(uploadedFile, checkingFileSize, checkingFileExtension, checkingMimeType))
				{
					this._invalidFiles.Add(this._uploadedFiles.Remove(uploadedFile));
				}
			}
		}

		// Token: 0x0600CE1A RID: 52762 RVA: 0x002DDC68 File Offset: 0x002DBE68
		protected virtual void ProcessValidFiles()
		{
			string targetFolder = this.GetTargetFolder();
			if (targetFolder.Length == 0)
			{
				return;
			}
			bool overwriteExistingFiles = this.OverwriteExistingFiles;
			foreach (object obj in this.UploadedFiles)
			{
				UploadedFile uploadedFile = (UploadedFile)obj;
				string text = Path.Combine(targetFolder, uploadedFile.GetName());
				if (overwriteExistingFiles || (!overwriteExistingFiles && !File.Exists(text)))
				{
					uploadedFile.SaveAs(text, overwriteExistingFiles);
				}
				else
				{
					this.OnFileExists(new UploadedFileEventArgs(uploadedFile));
				}
			}
		}

		// Token: 0x0600CE1B RID: 52763 RVA: 0x002DDD0C File Offset: 0x002DBF0C
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<ControlObjectsVisibility>(descriptor, "controlObjectsVisibility", this.ControlObjectsVisibility, ControlObjectsVisibility.Default);
			base.DescribeProperty<bool>(descriptor, "enableFileInputSkinning", this.EnableFileInputSkinning, true);
			base.DescribeProperty<bool>(descriptor, "focusOnLoad", this.FocusOnLoad, false);
			base.DescribeProperty<int>(descriptor, "initialFileInputsCount", this.InitialFileInputsCount, 1);
			base.DescribeProperty<int>(descriptor, "inputSize", this.InputSize, 23);
			base.DescribeProperty<int>(descriptor, "maxFileCount", this.MaxFileInputsCount, 0);
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600CE1C RID: 52764 RVA: 0x002DDD94 File Offset: 0x002DBF94
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadWebControl.DescribeEvent(descriptor, "added", this.OnClientAdded);
			RadWebControl.DescribeEvent(descriptor, "adding", this.OnClientAdding);
			RadWebControl.DescribeEvent(descriptor, "clearing", this.OnClientClearing);
			RadWebControl.DescribeEvent(descriptor, "deleting", this.OnClientDeleting);
			RadWebControl.DescribeEvent(descriptor, "deletingSelected", this.OnClientDeletingSelected);
			RadWebControl.DescribeEvent(descriptor, "fileSelected", this.OnClientFileSelected);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x140001A7 RID: 423
		// (add) Token: 0x0600CE1D RID: 52765 RVA: 0x002DDE10 File Offset: 0x002DC010
		// (remove) Token: 0x0600CE1E RID: 52766 RVA: 0x002DDE48 File Offset: 0x002DC048
		[Description("Occurs before the internal validation of every file in the UploadedFiles collection.")]
		public event ValidateFileEventHandler ValidatingFile;

		// Token: 0x0600CE1F RID: 52767 RVA: 0x002DDE7D File Offset: 0x002DC07D
		protected virtual bool OnValidatingFile(ValidateFileEventArgs e)
		{
			if (this.ValidatingFile != null)
			{
				this.ValidatingFile(this, e);
			}
			return e.IsValid;
		}

		// Token: 0x140001A8 RID: 424
		// (add) Token: 0x0600CE20 RID: 52768 RVA: 0x002DDE9C File Offset: 0x002DC09C
		// (remove) Token: 0x0600CE21 RID: 52769 RVA: 0x002DDED4 File Offset: 0x002DC0D4
		public event UploadedFileEventHandler FileExists;

		// Token: 0x0600CE22 RID: 52770 RVA: 0x002DDF09 File Offset: 0x002DC109
		protected virtual void OnFileExists(UploadedFileEventArgs e)
		{
			if (this.FileExists != null)
			{
				this.FileExists(this, e);
			}
		}

		// Token: 0x17004240 RID: 16960
		// (get) Token: 0x0600CE23 RID: 52771 RVA: 0x002DDF20 File Offset: 0x002DC120
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public UploadStrings Localization
		{
			get
			{
				if (this._localization == null)
				{
					this._localization = new UploadStrings(new LocalizationProvider("RadUpload", this, this.LocalizationPath));
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._localization).TrackViewState();
					}
				}
				return this._localization;
			}
		}

		// Token: 0x17004241 RID: 16961
		// (get) Token: 0x0600CE24 RID: 52772 RVA: 0x002DDF5F File Offset: 0x002DC15F
		// (set) Token: 0x0600CE25 RID: 52773 RVA: 0x002DDF80 File Offset: 0x002DC180
		[Description("Gets or sets a value indicating where RadUpload will look for its .resx localization files.")]
		[Category("Misc")]
		[DefaultValue("")]
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

		// Token: 0x17004242 RID: 16962
		// (get) Token: 0x0600CE26 RID: 52774 RVA: 0x002DDFD3 File Offset: 0x002DC1D3
		// (set) Token: 0x0600CE27 RID: 52775 RVA: 0x002DDFE7 File Offset: 0x002DC1E7
		[ClientControlProperty]
		[DefaultValue(23)]
		[Description("Gets or sets the size of the file input field.")]
		[Bindable(true)]
		[ClientPropertyName("inputSize")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Category("Behavior")]
		public int InputSize
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "InputSize", 23);
			}
			set
			{
				this.ViewState["InputSize"] = value;
			}
		}

		// Token: 0x17004243 RID: 16963
		// (get) Token: 0x0600CE28 RID: 52776 RVA: 0x002DDFFF File Offset: 0x002DC1FF
		// (set) Token: 0x0600CE29 RID: 52777 RVA: 0x002DE017 File Offset: 0x002DC217
		[TypeConverter(typeof(ListConverter))]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Gets or sets the allowed file extensions for uploading.")]
		[Bindable(true)]
		public string[] AllowedFileExtensions
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "AllowedFileExtensions", new string[0]);
			}
			set
			{
				this.ViewState["AllowedFileExtensions"] = value;
			}
		}

		// Token: 0x17004244 RID: 16964
		// (get) Token: 0x0600CE2A RID: 52778 RVA: 0x002DE02A File Offset: 0x002DC22A
		// (set) Token: 0x0600CE2B RID: 52779 RVA: 0x002DE042 File Offset: 0x002DC242
		[Category("Behavior")]
		[TypeConverter(typeof(ListConverter))]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Gets or sets the allowed MIME types for uploading.")]
		[Bindable(true)]
		public string[] AllowedMimeTypes
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "AllowedMimeTypes", new string[0]);
			}
			set
			{
				this.ViewState["AllowedMimeTypes"] = value;
			}
		}

		// Token: 0x17004245 RID: 16965
		// (get) Token: 0x0600CE2C RID: 52780 RVA: 0x002DE055 File Offset: 0x002DC255
		// (set) Token: 0x0600CE2D RID: 52781 RVA: 0x002DE069 File Offset: 0x002DC269
		[Editor("Telerik.Web.Design.Common.FlagEnumUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Description("Gets or sets the value indicating which control objects will be displayed.")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue(ControlObjectsVisibility.Default)]
		[ClientControlProperty]
		public virtual ControlObjectsVisibility ControlObjectsVisibility
		{
			get
			{
				return (ControlObjectsVisibility)Utility.GetValueFromViewState(this.ViewState, "ControlObjectsVisibility", 27);
			}
			set
			{
				this.ViewState["ControlObjectsVisibility"] = (int)value;
			}
		}

		// Token: 0x17004246 RID: 16966
		// (get) Token: 0x0600CE2E RID: 52782 RVA: 0x002DE081 File Offset: 0x002DC281
		// (set) Token: 0x0600CE2F RID: 52783 RVA: 0x002DE094 File Offset: 0x002DC294
		[Description("Gets or sets the value indicating whether the file input fields skinning will be enabled.")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Appearance")]
		[ClientPropertyName("enableFileInputSkinning")]
		[ClientControlProperty]
		[DefaultValue(true)]
		public bool EnableFileInputSkinning
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "EnableFileInputSkinning", true);
			}
			set
			{
				this.ViewState["EnableFileInputSkinning"] = value;
			}
		}

		// Token: 0x17004247 RID: 16967
		// (get) Token: 0x0600CE30 RID: 52784 RVA: 0x002DE0AC File Offset: 0x002DC2AC
		// (set) Token: 0x0600CE31 RID: 52785 RVA: 0x002DE0BF File Offset: 0x002DC2BF
		[Browsable(true)]
		[Description("Gets or sets the initial count of file input fields, which will appear in RadUpload.")]
		[Bindable(true)]
		[Category("Behavior")]
		[ClientPropertyName("initialFileInputsCount")]
		[ClientControlProperty]
		[DefaultValue(1)]
		public int InitialFileInputsCount
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "InitialFileInputsCount", 1);
			}
			set
			{
				this.ViewState["InitialFileInputsCount"] = value;
			}
		}

		// Token: 0x17004248 RID: 16968
		// (get) Token: 0x0600CE32 RID: 52786 RVA: 0x002DE0D7 File Offset: 0x002DC2D7
		[Browsable(false)]
		public virtual UploadedFileCollection InvalidFiles
		{
			get
			{
				return this._invalidFiles;
			}
		}

		// Token: 0x17004249 RID: 16969
		// (get) Token: 0x0600CE33 RID: 52787 RVA: 0x002DE0DF File Offset: 0x002DC2DF
		// (set) Token: 0x0600CE34 RID: 52788 RVA: 0x002DE0F6 File Offset: 0x002DC2F6
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("Gets or sets the localization language of the RadUpload user interface.")]
		[Obsolete("Use the Culture property")]
		[Bindable(true)]
		[Browsable(true)]
		public string Language
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "Language", "");
			}
			set
			{
				this.ViewState["Language"] = value;
			}
		}

		// Token: 0x1700424A RID: 16970
		// (get) Token: 0x0600CE35 RID: 52789 RVA: 0x002DE109 File Offset: 0x002DC309
		// (set) Token: 0x0600CE36 RID: 52790 RVA: 0x002DE129 File Offset: 0x002DC329
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

		// Token: 0x1700424B RID: 16971
		// (get) Token: 0x0600CE37 RID: 52791 RVA: 0x002DE13C File Offset: 0x002DC33C
		// (set) Token: 0x0600CE38 RID: 52792 RVA: 0x002DE14F File Offset: 0x002DC34F
		[ClientControlProperty]
		[DefaultValue(0)]
		[Bindable(true)]
		[ClientPropertyName("maxFileCount")]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Gets or sets the maximum file input fields that can be added to the control.")]
		public int MaxFileInputsCount
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "MaxFileInputsCount", 0);
			}
			set
			{
				this.ViewState["MaxFileInputsCount"] = value;
			}
		}

		// Token: 0x1700424C RID: 16972
		// (get) Token: 0x0600CE39 RID: 52793 RVA: 0x002DE167 File Offset: 0x002DC367
		// (set) Token: 0x0600CE3A RID: 52794 RVA: 0x002DE17A File Offset: 0x002DC37A
		[DefaultValue(0)]
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.Attribute)]
		[Description("Gets or sets the maximum file size allowed for uploading in bytes. Set to 0 for unlimited size.")]
		[Bindable(true)]
		public virtual int MaxFileSize
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "MaxFileSize", 0);
			}
			set
			{
				this.ViewState["MaxFileSize"] = value;
			}
		}

		// Token: 0x1700424D RID: 16973
		// (get) Token: 0x0600CE3B RID: 52795 RVA: 0x002DE192 File Offset: 0x002DC392
		// (set) Token: 0x0600CE3C RID: 52796 RVA: 0x002DE1A9 File Offset: 0x002DC3A9
		[ClientPropertyName("adding")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed before a new file input is added to a RadUpload instance.")]
		public string OnClientAdding
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "OnClientAdding", string.Empty);
			}
			set
			{
				this.ViewState["OnClientAdding"] = value;
			}
		}

		// Token: 0x1700424E RID: 16974
		// (get) Token: 0x0600CE3D RID: 52797 RVA: 0x002DE1BC File Offset: 0x002DC3BC
		// (set) Token: 0x0600CE3E RID: 52798 RVA: 0x002DE1D3 File Offset: 0x002DC3D3
		[Browsable(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Bindable(true)]
		[Category("Client-side events")]
		[ClientPropertyName("added")]
		[Description("Gets or sets the name of the client-side function which will be executed after a new file input is added to a RadUpload instance.")]
		[ClientControlEvent]
		[DefaultValue("")]
		public string OnClientAdded
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "OnClientAdded", string.Empty);
			}
			set
			{
				this.ViewState["OnClientAdded"] = value;
			}
		}

		// Token: 0x1700424F RID: 16975
		// (get) Token: 0x0600CE3F RID: 52799 RVA: 0x002DE1E6 File Offset: 0x002DC3E6
		// (set) Token: 0x0600CE40 RID: 52800 RVA: 0x002DE1FD File Offset: 0x002DC3FD
		[DefaultValue("")]
		[Bindable(true)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("deleting")]
		[Browsable(true)]
		[Description("Gets or sets the name of the client-side function which will be executed before a file input is deleted from a RadUpload instance.")]
		public string OnClientDeleting
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "OnClientDeleting", string.Empty);
			}
			set
			{
				this.ViewState["OnClientDeleting"] = value;
			}
		}

		// Token: 0x17004250 RID: 16976
		// (get) Token: 0x0600CE41 RID: 52801 RVA: 0x002DE210 File Offset: 0x002DC410
		// (set) Token: 0x0600CE42 RID: 52802 RVA: 0x002DE227 File Offset: 0x002DC427
		[Browsable(true)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("clearing")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed before a file input field cleared in RadUpload by using the Clear button.")]
		[Bindable(true)]
		public string OnClientClearing
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "OnClientClearing", string.Empty);
			}
			set
			{
				this.ViewState["OnClientClearing"] = value;
			}
		}

		// Token: 0x17004251 RID: 16977
		// (get) Token: 0x0600CE43 RID: 52803 RVA: 0x002DE23A File Offset: 0x002DC43A
		// (set) Token: 0x0600CE44 RID: 52804 RVA: 0x002DE251 File Offset: 0x002DC451
		[Bindable(true)]
		[Browsable(true)]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("fileSelected")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed when a file input value changed.")]
		[Category("Client-side events")]
		public string OnClientFileSelected
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "OnClientFileSelected", string.Empty);
			}
			set
			{
				this.ViewState["OnClientFileSelected"] = value;
			}
		}

		// Token: 0x17004252 RID: 16978
		// (get) Token: 0x0600CE45 RID: 52805 RVA: 0x002DE264 File Offset: 0x002DC464
		// (set) Token: 0x0600CE46 RID: 52806 RVA: 0x002DE27B File Offset: 0x002DC47B
		[Bindable(true)]
		[Category("Client-side events")]
		[Editor("Telerik.Web.Design.Common.ClientSideEventUIEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[ClientControlEvent]
		[ClientPropertyName("deletingSelected")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the client-side function which will be executed before the selected file inputs are removed.")]
		[Browsable(true)]
		public string OnClientDeletingSelected
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "OnClientDeletingSelected", string.Empty);
			}
			set
			{
				this.ViewState["OnClientDeletingSelected"] = value;
			}
		}

		// Token: 0x17004253 RID: 16979
		// (get) Token: 0x0600CE47 RID: 52807 RVA: 0x002DE28E File Offset: 0x002DC48E
		// (set) Token: 0x0600CE48 RID: 52808 RVA: 0x002DE2A1 File Offset: 0x002DC4A1
		[Description("Gets or sets the value indicating whether RadUpload should overwrite existing files having same name in the TargetFolder.")]
		[Browsable(true)]
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool OverwriteExistingFiles
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "OverwriteExistingFiles", false);
			}
			set
			{
				this.ViewState["OverwriteExistingFiles"] = value;
			}
		}

		// Token: 0x17004254 RID: 16980
		// (get) Token: 0x0600CE49 RID: 52809 RVA: 0x002DE2B9 File Offset: 0x002DC4B9
		// (set) Token: 0x0600CE4A RID: 52810 RVA: 0x002DE2CC File Offset: 0x002DC4CC
		[DefaultValue(false)]
		[Bindable(true)]
		[Category("Behavior")]
		[Browsable(true)]
		[Description("Gets or sets a value indicating if the file input fields should be read-only.")]
		public bool ReadOnlyFileInputs
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "ReadOnlyFileInputs", false);
			}
			set
			{
				this.ViewState["ReadOnlyFileInputs"] = value;
			}
		}

		// Token: 0x17004255 RID: 16981
		// (get) Token: 0x0600CE4B RID: 52811 RVA: 0x002DE2E4 File Offset: 0x002DC4E4
		// (set) Token: 0x0600CE4C RID: 52812 RVA: 0x002DE2FB File Offset: 0x002DC4FB
		[Bindable(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		[Browsable(true)]
		[Description("Gets or sets the virtual path of the folder, where RadUpload will automatically save the valid files after the upload completes.")]
		public string TargetFolder
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "TargetFolder", string.Empty);
			}
			set
			{
				this.ViewState["TargetFolder"] = value;
			}
		}

		// Token: 0x17004256 RID: 16982
		// (get) Token: 0x0600CE4D RID: 52813 RVA: 0x002DE30E File Offset: 0x002DC50E
		// (set) Token: 0x0600CE4E RID: 52814 RVA: 0x002DE325 File Offset: 0x002DC525
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Gets or sets the physical path of the folder, where RadUpload will automatically save the valid files after the upload completes.")]
		[Browsable(true)]
		[Bindable(true)]
		public string TargetPhysicalFolder
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "TargetPhysicalFolder", string.Empty);
			}
			set
			{
				this.ViewState["TargetPhysicalFolder"] = value;
			}
		}

		// Token: 0x17004257 RID: 16983
		// (get) Token: 0x0600CE4F RID: 52815 RVA: 0x002DE338 File Offset: 0x002DC538
		[Browsable(false)]
		public UploadedFileCollection UploadedFiles
		{
			get
			{
				return this._uploadedFiles;
			}
		}

		// Token: 0x17004258 RID: 16984
		// (get) Token: 0x0600CE50 RID: 52816 RVA: 0x002DE340 File Offset: 0x002DC540
		// (set) Token: 0x0600CE51 RID: 52817 RVA: 0x002DE353 File Offset: 0x002DC553
		[DefaultValue(false)]
		[Browsable(true)]
		[Description("Gets or sets the value indicating whether the first file input field of RadUpload should get the focus on itself on load.")]
		[Category("Behavior")]
		[ClientPropertyName("focusOnLoad")]
		[Bindable(true)]
		[ClientControlProperty]
		public bool FocusOnLoad
		{
			get
			{
				return Utility.GetValueFromViewState(this.ViewState, "FocusOnLoad", false);
			}
			set
			{
				this.ViewState["FocusOnLoad"] = value;
			}
		}

		// Token: 0x17004259 RID: 16985
		// (get) Token: 0x0600CE52 RID: 52818 RVA: 0x002DE36B File Offset: 0x002DC56B
		[Browsable(false)]
		public bool IsUploadModuleRegistered
		{
			get
			{
				return RadUploadHttpModule.IsRegistered;
			}
		}

		// Token: 0x04003715 RID: 14101
		private const string _xapResourceName = "Telerik.Web.UI.Upload.SilverlightHelper.xap";

		// Token: 0x04003716 RID: 14102
		private UploadedFileCollection _uploadedFiles = new UploadedFileCollection();

		// Token: 0x04003717 RID: 14103
		private UploadedFileCollection _invalidFiles = new UploadedFileCollection();

		// Token: 0x0400371A RID: 14106
		private UploadStrings _localization;
	}
}
