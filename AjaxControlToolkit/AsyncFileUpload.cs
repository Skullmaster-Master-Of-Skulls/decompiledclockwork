using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200003D RID: 61
	[Designer(typeof(AsyncFileUploadDesigner))]
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ToolboxBitmap(typeof(Accessor), "AsyncFileUpload.bmp")]
	[ClientScriptResource("Sys.Extended.UI.AsyncFileUpload", "AsyncFileUpload")]
	public class AsyncFileUpload : ScriptControlBase
	{
		// Token: 0x06000210 RID: 528 RVA: 0x000076D4 File Offset: 0x000058D4
		public AsyncFileUpload() : base(true, HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000211 RID: 529 RVA: 0x00007700 File Offset: 0x00005900
		// (remove) Token: 0x06000212 RID: 530 RVA: 0x00007738 File Offset: 0x00005938
		[Bindable(true)]
		[Category("Server Events")]
		public event EventHandler<AsyncFileUploadEventArgs> UploadedComplete;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000213 RID: 531 RVA: 0x00007770 File Offset: 0x00005970
		// (remove) Token: 0x06000214 RID: 532 RVA: 0x000077A8 File Offset: 0x000059A8
		[Bindable(true)]
		[Category("Server Events")]
		public event EventHandler<AsyncFileUploadEventArgs> UploadedFileError;

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000077DD File Offset: 0x000059DD
		private bool IsDesignMode
		{
			get
			{
				return HttpContext.Current == null;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000216 RID: 534 RVA: 0x000077E7 File Offset: 0x000059E7
		private HttpPostedFile CurrentFile
		{
			get
			{
				if (!this._persistFile)
				{
					return this._postedFile;
				}
				return PersistentStoreManager.Instance.GetFileFromSession(this.ClientID);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00007808 File Offset: 0x00005A08
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00007828 File Offset: 0x00005A28
		[ExtenderControlEvent]
		[DefaultValue("")]
		[Category("Behavior")]
		[ClientPropertyName("uploadStarted")]
		public string OnClientUploadStarted
		{
			get
			{
				return (string)(this.ViewState["OnClientUploadStarted"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientUploadStarted"] = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000783B File Offset: 0x00005A3B
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000785B File Offset: 0x00005A5B
		[ClientPropertyName("uploadComplete")]
		[DefaultValue("")]
		[Category("Behavior")]
		[ExtenderControlEvent]
		public string OnClientUploadComplete
		{
			get
			{
				return (string)(this.ViewState["OnClientUploadComplete"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientUploadComplete"] = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000786E File Offset: 0x00005A6E
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000788E File Offset: 0x00005A8E
		[Category("Behavior")]
		[DefaultValue("")]
		[ExtenderControlEvent]
		[ClientPropertyName("uploadError")]
		public string OnClientUploadError
		{
			get
			{
				return (string)(this.ViewState["OnClientUploadError"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientUploadError"] = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000078A4 File Offset: 0x00005AA4
		[Browsable(false)]
		public byte[] FileBytes
		{
			get
			{
				this.PopulateObjectPriorToRender(this.ClientID);
				HttpPostedFile currentFile = this.CurrentFile;
				if (currentFile != null)
				{
					try
					{
						return this.GetBytesFromStream(currentFile.InputStream);
					}
					catch
					{
					}
				}
				return null;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600021E RID: 542 RVA: 0x000078EC File Offset: 0x00005AEC
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000790C File Offset: 0x00005B0C
		[Category("Behavior")]
		[Description("ID of Throbber")]
		[DefaultValue("")]
		public string ThrobberID
		{
			get
			{
				return (string)(this.ViewState["ThrobberID"] ?? string.Empty);
			}
			set
			{
				this.ViewState["ThrobberID"] = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000791F File Offset: 0x00005B1F
		// (set) Token: 0x06000221 RID: 545 RVA: 0x00007944 File Offset: 0x00005B44
		[TypeConverter(typeof(WebColorConverter))]
		[Category("Appearance")]
		[Description("Control's background color on upload complete.")]
		[DefaultValue(typeof(Color), "Lime")]
		public Color CompleteBackColor
		{
			get
			{
				return (Color)(this.ViewState["CompleteBackColor"] ?? Color.Lime);
			}
			set
			{
				this.ViewState["CompleteBackColor"] = value;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000222 RID: 546 RVA: 0x0000795C File Offset: 0x00005B5C
		// (set) Token: 0x06000223 RID: 547 RVA: 0x00007981 File Offset: 0x00005B81
		[TypeConverter(typeof(WebColorConverter))]
		[Category("Appearance")]
		[DefaultValue(typeof(Color), "White")]
		[Description("Control's background color when uploading is in progress.")]
		public Color UploadingBackColor
		{
			get
			{
				return (Color)(this.ViewState["UploadingBackColor"] ?? Color.White);
			}
			set
			{
				this.ViewState["UploadingBackColor"] = value;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00007999 File Offset: 0x00005B99
		// (set) Token: 0x06000225 RID: 549 RVA: 0x000079BE File Offset: 0x00005BBE
		[Category("Appearance")]
		[DefaultValue(typeof(Color), "Red")]
		[TypeConverter(typeof(WebColorConverter))]
		[Description("Control's background color on upload error.")]
		public Color ErrorBackColor
		{
			get
			{
				return (Color)(this.ViewState["ErrorBackColor"] ?? Color.Red);
			}
			set
			{
				this.ViewState["ErrorBackColor"] = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000079D6 File Offset: 0x00005BD6
		// (set) Token: 0x06000227 RID: 551 RVA: 0x000079DE File Offset: 0x00005BDE
		[Category("Layout")]
		[DefaultValue(typeof(Unit), "")]
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000228 RID: 552 RVA: 0x000079E7 File Offset: 0x00005BE7
		// (set) Token: 0x06000229 RID: 553 RVA: 0x000079EF File Offset: 0x00005BEF
		[Browsable(false)]
		public bool FailedValidation
		{
			get
			{
				return this._failedValidation;
			}
			set
			{
				this._failedValidation = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600022A RID: 554 RVA: 0x000079F8 File Offset: 0x00005BF8
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00007A00 File Offset: 0x00005C00
		[Bindable(true)]
		[Category("Appearance")]
		[Browsable(true)]
		[DefaultValue(AsyncFileUploaderStyle.Traditional)]
		public AsyncFileUploaderStyle UploaderStyle
		{
			get
			{
				return this._controlStyle;
			}
			set
			{
				this._controlStyle = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00007A09 File Offset: 0x00005C09
		[Browsable(false)]
		public HttpPostedFile PostedFile
		{
			get
			{
				this.PopulateObjectPriorToRender(this.ClientID);
				return this.CurrentFile;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00007A1D File Offset: 0x00005C1D
		[Browsable(false)]
		public bool HasFile
		{
			get
			{
				this.PopulateObjectPriorToRender(this.ClientID);
				if (this._persistFile)
				{
					return PersistentStoreManager.Instance.FileExists(this.ClientID);
				}
				return this._postedFile != null;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00007A50 File Offset: 0x00005C50
		[Browsable(false)]
		public string FileName
		{
			get
			{
				this.PopulateObjectPriorToRender(this.ClientID);
				if (this._persistFile)
				{
					return Path.GetFileName(PersistentStoreManager.Instance.GetFileName(this.ClientID));
				}
				if (this._postedFile != null)
				{
					return Path.GetFileName(this._postedFile.FileName);
				}
				return string.Empty;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00007AA5 File Offset: 0x00005CA5
		[Browsable(false)]
		public string ContentType
		{
			get
			{
				this.PopulateObjectPriorToRender(this.ClientID);
				if (this._persistFile)
				{
					return PersistentStoreManager.Instance.GetContentType(this.ClientID);
				}
				if (this._postedFile != null)
				{
					return this._postedFile.ContentType;
				}
				return string.Empty;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00007AE8 File Offset: 0x00005CE8
		[Browsable(false)]
		public Stream FileContent
		{
			get
			{
				this.PopulateObjectPriorToRender(this.ClientID);
				HttpPostedFile currentFile = this.CurrentFile;
				if (currentFile == null || currentFile.InputStream == null)
				{
					return null;
				}
				return currentFile.InputStream;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00007B1B File Offset: 0x00005D1B
		[Browsable(false)]
		public bool IsUploading
		{
			get
			{
				return this.Page.Request.QueryString["AsyncFileUploadID"] != null;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00007B3D File Offset: 0x00005D3D
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00007B45 File Offset: 0x00005D45
		[Browsable(true)]
		[Bindable(true)]
		[DefaultValue(false)]
		public bool PersistFile
		{
			get
			{
				return this._persistFile;
			}
			set
			{
				this._persistFile = value;
			}
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007B4E File Offset: 0x00005D4E
		public void ClearAllFilesFromPersistedStore()
		{
			PersistentStoreManager.Instance.ClearAllFilesFromSession(this.ClientID);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007B60 File Offset: 0x00005D60
		public void ClearFileFromPersistedStore()
		{
			PersistentStoreManager.Instance.RemoveFileFromSession(this.ClientID);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007B74 File Offset: 0x00005D74
		public void SaveAs(string fileName)
		{
			this.PopulateObjectPriorToRender(this.ClientID);
			HttpPostedFile currentFile = this.CurrentFile;
			currentFile.SaveAs(fileName);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007B9C File Offset: 0x00005D9C
		private void PopulateObjectPriorToRender(string controlId)
		{
			bool flag;
			if (this._persistFile)
			{
				flag = PersistentStoreManager.Instance.FileExists(controlId);
			}
			else
			{
				flag = (this._postedFile != null);
			}
			if (!flag && this.Page != null && this.Page.Request.Files.Count != 0)
			{
				this.ReceivedFile(controlId);
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00007BF5 File Offset: 0x00005DF5
		protected virtual void OnUploadedFileError(AsyncFileUploadEventArgs e)
		{
			if (this.UploadedFileError != null)
			{
				this.UploadedFileError(this, e);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00007C0C File Offset: 0x00005E0C
		protected virtual void OnUploadedComplete(AsyncFileUploadEventArgs e)
		{
			if (this.UploadedComplete != null)
			{
				this.UploadedComplete(this, e);
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007C24 File Offset: 0x00005E24
		private void ReceivedFile(string sendingControlID)
		{
			this._lastError = string.Empty;
			if (this.Page.Request.Files.Count > 0)
			{
				HttpPostedFile httpPostedFile = null;
				if (sendingControlID == null || sendingControlID == string.Empty)
				{
					httpPostedFile = this.Page.Request.Files[0];
				}
				else
				{
					foreach (object obj in this.Page.Request.Files)
					{
						string text = (string)obj;
						string text2 = text;
						string text3 = "$ctl02";
						if (text2.EndsWith(text3))
						{
							text2 = text2.Remove(text2.Length - text3.Length);
						}
						if (text2.Replace("$", "_").EndsWith(sendingControlID))
						{
							httpPostedFile = this.Page.Request.Files[text];
							break;
						}
					}
				}
				AsyncFileUploadEventArgs e;
				if (httpPostedFile == null)
				{
					this._lastError = "The file attached is invalid.";
					e = new AsyncFileUploadEventArgs(AsyncFileUploadState.Failed, "The file attached is invalid.", string.Empty, string.Empty);
					this.OnUploadedFileError(e);
					return;
				}
				if (httpPostedFile.FileName == string.Empty)
				{
					this._lastError = "The file attached has an invalid filename.";
					e = new AsyncFileUploadEventArgs(AsyncFileUploadState.Unknown, "The file attached has an invalid filename.", httpPostedFile.FileName, httpPostedFile.ContentLength.ToString());
					this.OnUploadedFileError(e);
					return;
				}
				if (httpPostedFile.InputStream == null)
				{
					this._lastError = "The file attached has an invalid filename.";
					e = new AsyncFileUploadEventArgs(AsyncFileUploadState.Failed, "The file attached has an invalid filename.", httpPostedFile.FileName, httpPostedFile.ContentLength.ToString());
					this.OnUploadedFileError(e);
					return;
				}
				if (httpPostedFile.ContentLength < 1)
				{
					this._lastError = "The file attached is empty.";
					e = new AsyncFileUploadEventArgs(AsyncFileUploadState.Unknown, "The file attached is empty.", httpPostedFile.FileName, httpPostedFile.ContentLength.ToString());
					this.OnUploadedFileError(e);
					return;
				}
				e = new AsyncFileUploadEventArgs(AsyncFileUploadState.Success, string.Empty, httpPostedFile.FileName, httpPostedFile.ContentLength.ToString());
				if (this._persistFile)
				{
					GC.SuppressFinalize(httpPostedFile);
					PersistentStoreManager.Instance.AddFileToSession(this.ClientID, httpPostedFile.FileName, httpPostedFile);
				}
				else
				{
					this._postedFile = httpPostedFile;
				}
				this.OnUploadedComplete(e);
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00007E78 File Offset: 0x00006078
		public byte[] GetBytesFromStream(Stream stream)
		{
			byte[] array = new byte[32768];
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				stream.Seek(0L, SeekOrigin.Begin);
				for (;;)
				{
					int num = stream.Read(array, 0, array.Length);
					if (num <= 0)
					{
						break;
					}
					memoryStream.Write(array, 0, num);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00007EE0 File Offset: 0x000060E0
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			string text = this.Page.Request.QueryString["AsyncFileUploadID"];
			if (text == null || text == this.ClientID)
			{
				this.ReceivedFile(this.ClientID);
				if (text != null && text.StartsWith(this.ClientID))
				{
					string value;
					if (this._lastError == string.Empty)
					{
						byte[] fileBytes = this.FileBytes;
						if (fileBytes != null)
						{
							value = fileBytes.Length.ToString() + "------" + this.ContentType;
						}
						else
						{
							value = string.Empty;
						}
					}
					else
					{
						value = "error------" + this._lastError;
					}
					TextWriter output = this.Page.Response.Output;
					output.Write("<div id='" + this.ClientID + "'>");
					output.Write(value);
					output.Write("</div>");
				}
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00007FD8 File Offset: 0x000061D8
		internal void CreateChilds()
		{
			this.Controls.Clear();
			this.CreateChildControls();
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00007FEC File Offset: 0x000061EC
		protected override void CreateChildControls()
		{
			PersistentStoreManager.Instance.ExtendedFileUploadGUID = "b3b89160-3224-476e-9076-70b500c816cf";
			string value = null;
			if (!this.IsDesignMode)
			{
				value = this.Page.Request.QueryString["AsyncFileUploadID"];
			}
			if (this.IsDesignMode || string.IsNullOrEmpty(value))
			{
				this._hiddenFieldID = this.GenerateHtmlInputHiddenControl();
				string lastFileName = string.Empty;
				if (this._persistFile)
				{
					if (PersistentStoreManager.Instance.FileExists(this.ClientID))
					{
						lastFileName = PersistentStoreManager.Instance.GetFileName(this.ClientID);
					}
				}
				else if (this._postedFile != null)
				{
					lastFileName = this._postedFile.FileName;
				}
				this.GenerateHtmlInputFileControl(lastFileName);
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00008098 File Offset: 0x00006298
		protected string GenerateHtmlInputHiddenControl()
		{
			HiddenField hiddenField = new HiddenField();
			this.Controls.Add(hiddenField);
			return hiddenField.ClientID;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000080C0 File Offset: 0x000062C0
		protected string GenerateHtmlInputFileControl(string lastFileName)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			this.Controls.Add(htmlGenericControl);
			if (this.UploaderStyle == AsyncFileUploaderStyle.Modern)
			{
				string imageHref = ToolkitResourceManager.GetImageHref("AsyncFileUpload.Button.png", this, true);
				string text = "background:url(" + imageHref + ") no-repeat 100% 1px; height:24px; margin:0px; text-align:right;";
				if (!this.Width.IsEmpty)
				{
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						"min-width:",
						this.Width.ToString(),
						";width:",
						this.Width.ToString(),
						" !important;"
					});
				}
				else
				{
					text += "width:355px;";
				}
				htmlGenericControl.Attributes.Add("style", text);
			}
			if (this.UploaderStyle != AsyncFileUploaderStyle.Modern || !this.IsDesignMode)
			{
				this._inputFile = new HtmlInputFile();
				if (!this.Enabled)
				{
					this._inputFile.Disabled = true;
				}
				htmlGenericControl.Controls.Add(this._inputFile);
				this._inputFile.Attributes.Add("id", this._inputFile.Name.Replace("$", "_"));
				if (this.UploaderStyle != AsyncFileUploaderStyle.Modern)
				{
					if (this.BackColor != Color.Empty)
					{
						this._inputFile.Style[HtmlTextWriterStyle.BackgroundColor] = ColorTranslator.ToHtml(this.BackColor);
					}
					if (!this.Width.IsEmpty)
					{
						this._inputFile.Style[HtmlTextWriterStyle.Width] = this.Width.ToString();
					}
					else
					{
						this._inputFile.Style[HtmlTextWriterStyle.Width] = "355px";
					}
				}
			}
			if (this.UploaderStyle == AsyncFileUploaderStyle.Modern)
			{
				string text3 = "opacity:0.0; -moz-opacity: 0.0; filter: alpha(opacity=00); font-size:14px;";
				if (!this.Width.IsEmpty)
				{
					text3 = text3 + "width:" + this.Width.ToString() + ";";
				}
				if (this._inputFile != null)
				{
					this._inputFile.Attributes.Add("style", text3);
				}
				TextBox textBox = new TextBox();
				if (!this.IsDesignMode)
				{
					HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("div");
					htmlGenericControl.Controls.Add(htmlGenericControl2);
					text3 = "margin-top:-23px;text-align:left;";
					htmlGenericControl2.Attributes.Add("style", text3);
					htmlGenericControl2.Attributes.Add("type", "text");
					htmlGenericControl2.Controls.Add(textBox);
					text3 = "height:17px; font-size:12px; font-family:Tahoma;";
				}
				else
				{
					htmlGenericControl.Controls.Add(textBox);
					text3 = "height:23px; font-size:12px; font-family:Tahoma;";
				}
				if (!this.Width.IsEmpty && this.Width.ToString().IndexOf("px") > 0)
				{
					text3 = text3 + "width:" + (int.Parse(this.Width.ToString().Substring(0, this.Width.ToString().IndexOf("px"))) - 107).ToString() + "px;";
				}
				else
				{
					text3 += "width:248px;";
				}
				if (lastFileName != string.Empty || this._failedValidation)
				{
					if (this.FileBytes != null && this.FileBytes.Length > 0 && !this._failedValidation)
					{
						text3 += "background-color:#00FF00;";
					}
					else
					{
						this._failedValidation = false;
						text3 += "background-color:#FF0000;";
					}
					textBox.Text = lastFileName;
				}
				else if (this.BackColor != Color.Empty)
				{
					text3 = text3 + "background-color:" + ColorTranslator.ToHtml(this.BackColor) + ";";
				}
				textBox.ReadOnly = true;
				textBox.Attributes.Add("style", text3);
				this._innerTBID = textBox.ClientID;
			}
			else if (this.IsDesignMode)
			{
				this.Controls.Clear();
				this.Controls.Add(this._inputFile);
			}
			return htmlGenericControl.ClientID;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00008504 File Offset: 0x00006704
		protected override void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (!this.IsDesignMode)
			{
				if (this._hiddenFieldID != string.Empty)
				{
					descriptor.AddElementProperty("hiddenField", this._hiddenFieldID);
				}
				if (this._innerTBID != string.Empty)
				{
					descriptor.AddElementProperty("innerTB", this._innerTBID);
				}
				if (this._inputFile != null)
				{
					descriptor.AddElementProperty("inputFile", this._inputFile.Name.Replace("$", "_"));
				}
				descriptor.AddProperty("postBackUrl", this.Page.Request.RawUrl);
				descriptor.AddProperty("formName", Path.GetFileName(this.Page.Form.Name));
				if (this.CompleteBackColor != Color.Empty)
				{
					descriptor.AddProperty("completeBackColor", ColorTranslator.ToHtml(this.CompleteBackColor));
				}
				if (this.ErrorBackColor != Color.Empty)
				{
					descriptor.AddProperty("errorBackColor", ColorTranslator.ToHtml(this.ErrorBackColor));
				}
				if (this.UploadingBackColor != Color.Empty)
				{
					descriptor.AddProperty("uploadingBackColor", ColorTranslator.ToHtml(this.UploadingBackColor));
				}
				if (this.ThrobberID != string.Empty)
				{
					Control control = this.FindControl(this.ThrobberID);
					if (control != null)
					{
						descriptor.AddElementProperty("throbber", control.ClientID);
					}
				}
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000867C File Offset: 0x0000687C
		protected override Style CreateControlStyle()
		{
			return new AsyncFileUpload.AsyncFileUploadStyleWrapper(this.ViewState);
		}

		// Token: 0x040000A9 RID: 169
		private HttpPostedFile _postedFile;

		// Token: 0x040000AA RID: 170
		private HtmlInputFile _inputFile;

		// Token: 0x040000AB RID: 171
		private string _lastError = string.Empty;

		// Token: 0x040000AC RID: 172
		private string _hiddenFieldID = string.Empty;

		// Token: 0x040000AD RID: 173
		private string _innerTBID = string.Empty;

		// Token: 0x040000AE RID: 174
		private bool _persistFile;

		// Token: 0x040000AF RID: 175
		private bool _failedValidation;

		// Token: 0x040000B0 RID: 176
		private AsyncFileUploaderStyle _controlStyle;

		// Token: 0x0200003E RID: 62
		public static class Constants
		{
			// Token: 0x040000B3 RID: 179
			public const string FileUploadIDKey = "AsyncFileUploadID";

			// Token: 0x040000B4 RID: 180
			public const string InternalErrorInvalidIFrame = "The ExtendedFileUpload control has encountered an error with the uploader in this page. Please refresh the page and try again.";

			// Token: 0x040000B5 RID: 181
			public const string fileUploadGUID = "b3b89160-3224-476e-9076-70b500c816cf";

			// Token: 0x0200003F RID: 63
			public static class Errors
			{
				// Token: 0x040000B6 RID: 182
				public const string NoFiles = "No files are attached to the upload.";

				// Token: 0x040000B7 RID: 183
				public const string FileNull = "The file attached is invalid.";

				// Token: 0x040000B8 RID: 184
				public const string NoFileName = "The file attached has an invalid filename.";

				// Token: 0x040000B9 RID: 185
				public const string InputStreamNull = "The file attached could not be read.";

				// Token: 0x040000BA RID: 186
				public const string EmptyContentLength = "The file attached is empty.";
			}

			// Token: 0x02000040 RID: 64
			public static class StatusMessages
			{
				// Token: 0x040000BB RID: 187
				public const string UploadSuccessful = "The file uploaded successfully.";
			}
		}

		// Token: 0x02000041 RID: 65
		private sealed class AsyncFileUploadStyleWrapper : Style
		{
			// Token: 0x06000243 RID: 579 RVA: 0x00008689 File Offset: 0x00006889
			public AsyncFileUploadStyleWrapper(StateBag state) : base(state)
			{
			}

			// Token: 0x06000244 RID: 580 RVA: 0x00008692 File Offset: 0x00006892
			protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
			{
				base.FillStyleAttributes(attributes, urlResolver);
				attributes.Remove(HtmlTextWriterStyle.BackgroundColor);
				attributes.Remove(HtmlTextWriterStyle.Width);
			}
		}
	}
}
