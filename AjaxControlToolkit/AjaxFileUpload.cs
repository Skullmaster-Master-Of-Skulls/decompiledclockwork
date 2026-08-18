using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using AjaxControlToolkit.Design;
using AjaxControlToolkit.ToolboxIcons;

namespace AjaxControlToolkit
{
	// Token: 0x0200001C RID: 28
	[RequiredScript(typeof(CommonToolkitScripts))]
	[Designer(typeof(AjaxFileUploadDesigner))]
	[ClientScriptResource("Sys.Extended.UI.AjaxFileUpload.Control", "AjaxFileUpload")]
	[ClientCssResource("AjaxFileUpload")]
	[ToolboxBitmap(typeof(Accessor), "AjaxFileUpload.bmp")]
	public class AjaxFileUpload : ScriptControlBase
	{
		// Token: 0x06000121 RID: 289 RVA: 0x0000487B File Offset: 0x00002A7B
		public AjaxFileUpload() : base(true, HtmlTextWriterTag.Div)
		{
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00004886 File Offset: 0x00002A86
		private bool IsDesignMode
		{
			get
			{
				return HttpContext.Current == null;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00004890 File Offset: 0x00002A90
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00004898 File Offset: 0x00002A98
		public string ContextKeys { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000048A1 File Offset: 0x00002AA1
		// (set) Token: 0x06000126 RID: 294 RVA: 0x000048C1 File Offset: 0x00002AC1
		[DefaultValue("")]
		[Category("Behavior")]
		[Description("ID of Throbber")]
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

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000048D4 File Offset: 0x00002AD4
		// (set) Token: 0x06000128 RID: 296 RVA: 0x000048DC File Offset: 0x00002ADC
		[Browsable(false)]
		[DefaultValue(false)]
		public bool IsInFileUploadPostBack { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000048E5 File Offset: 0x00002AE5
		// (set) Token: 0x0600012A RID: 298 RVA: 0x000048ED File Offset: 0x00002AED
		[DefaultValue(10)]
		[ClientPropertyName("maximumNumberOfFiles")]
		[ExtenderControlProperty]
		public int MaximumNumberOfFiles { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000048F6 File Offset: 0x00002AF6
		// (set) Token: 0x0600012C RID: 300 RVA: 0x000048FE File Offset: 0x00002AFE
		[ExtenderControlProperty]
		[ClientPropertyName("allowedFileTypes")]
		[DefaultValue("")]
		public string AllowedFileTypes { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00004907 File Offset: 0x00002B07
		// (set) Token: 0x0600012E RID: 302 RVA: 0x0000492C File Offset: 0x00002B2C
		[ClientPropertyName("chunkSize")]
		[DefaultValue(4096)]
		[ExtenderControlProperty]
		public int ChunkSize
		{
			get
			{
				return int.Parse(((string)this.ViewState["ChunkSize"]) ?? "4096");
			}
			set
			{
				this.ViewState["ChunkSize"] = value.ToString();
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00004945 File Offset: 0x00002B45
		// (set) Token: 0x06000130 RID: 304 RVA: 0x0000496A File Offset: 0x00002B6A
		[DefaultValue(0)]
		[ClientPropertyName("maxFileSize")]
		[ExtenderControlProperty]
		public int MaxFileSize
		{
			get
			{
				return int.Parse(((string)this.ViewState["MaxFileSize"]) ?? "0");
			}
			set
			{
				this.ViewState["MaxFileSize"] = value.ToString();
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00004983 File Offset: 0x00002B83
		// (set) Token: 0x06000132 RID: 306 RVA: 0x000049A8 File Offset: 0x00002BA8
		[ExtenderControlProperty]
		[DefaultValue(false)]
		[ClientPropertyName("clearFileListAfterUpload")]
		public bool ClearFileListAfterUpload
		{
			get
			{
				return bool.Parse(((string)this.ViewState["ClearFileListAfterUpload"]) ?? "false");
			}
			set
			{
				this.ViewState["ClearFileListAfterUpload"] = value.ToString();
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000133 RID: 307 RVA: 0x000049C1 File Offset: 0x00002BC1
		// (set) Token: 0x06000134 RID: 308 RVA: 0x000049E6 File Offset: 0x00002BE6
		[DefaultValue(true)]
		[ClientPropertyName("useAbsoluteHandlerPath")]
		[ExtenderControlProperty]
		public bool UseAbsoluteHandlerPath
		{
			get
			{
				return bool.Parse(((string)this.ViewState["UseAbsoluteHandlerPath"]) ?? "true");
			}
			set
			{
				this.ViewState["UseAbsoluteHandlerPath"] = value.ToString();
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000135 RID: 309 RVA: 0x000049FF File Offset: 0x00002BFF
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00004A33 File Offset: 0x00002C33
		[ClientPropertyName("mode")]
		[ExtenderControlProperty]
		[DefaultValue(AjaxFileUploadMode.Auto)]
		public AjaxFileUploadMode Mode
		{
			get
			{
				return (AjaxFileUploadMode)Enum.Parse(typeof(AjaxFileUploadMode), ((string)this.ViewState["Mode"]) ?? "Auto");
			}
			set
			{
				this.ViewState["Mode"] = value.ToString();
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00004A50 File Offset: 0x00002C50
		// (set) Token: 0x06000138 RID: 312 RVA: 0x00004A75 File Offset: 0x00002C75
		[ClientPropertyName("autoStartUpload")]
		[DefaultValue(false)]
		[ExtenderControlProperty]
		public bool AutoStartUpload
		{
			get
			{
				return bool.Parse(((string)this.ViewState["AutoStartUpload"]) ?? "false");
			}
			set
			{
				this.ViewState["AutoStartUpload"] = value.ToString();
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000139 RID: 313 RVA: 0x00004A90 File Offset: 0x00002C90
		// (remove) Token: 0x0600013A RID: 314 RVA: 0x00004AC8 File Offset: 0x00002CC8
		[Category("Server Events")]
		[Bindable(true)]
		public event EventHandler<AjaxFileUploadStartEventArgs> UploadStart;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600013B RID: 315 RVA: 0x00004B00 File Offset: 0x00002D00
		// (remove) Token: 0x0600013C RID: 316 RVA: 0x00004B38 File Offset: 0x00002D38
		[Bindable(true)]
		[Category("Server Events")]
		public event EventHandler<AjaxFileUploadEventArgs> UploadComplete;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600013D RID: 317 RVA: 0x00004B70 File Offset: 0x00002D70
		// (remove) Token: 0x0600013E RID: 318 RVA: 0x00004BA8 File Offset: 0x00002DA8
		[Bindable(true)]
		[Category("Server Events")]
		public event EventHandler<AjaxFileUploadCompleteAllEventArgs> UploadCompleteAll;

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00004BDD File Offset: 0x00002DDD
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00004BFD File Offset: 0x00002DFD
		[ClientPropertyName("uploadStart")]
		[DefaultValue("")]
		[Category("Behavior")]
		[ExtenderControlEvent]
		public string OnClientUploadStart
		{
			get
			{
				return (string)(this.ViewState["OnClientUploadStart"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientUploadStart"] = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00004C10 File Offset: 0x00002E10
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00004C30 File Offset: 0x00002E30
		[ClientPropertyName("uploadComplete")]
		[Category("Behavior")]
		[ExtenderControlEvent]
		[DefaultValue("")]
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

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00004C43 File Offset: 0x00002E43
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00004C63 File Offset: 0x00002E63
		[ExtenderControlEvent]
		[ClientPropertyName("uploadCompleteAll")]
		[DefaultValue("")]
		[Category("Behavior")]
		public string OnClientUploadCompleteAll
		{
			get
			{
				return (string)(this.ViewState["OnClientUploadCompleteAll"] ?? string.Empty);
			}
			set
			{
				this.ViewState["OnClientUploadCompleteAll"] = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00004C76 File Offset: 0x00002E76
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00004C96 File Offset: 0x00002E96
		[ClientPropertyName("uploadError")]
		[ExtenderControlEvent]
		[Category("Behavior")]
		[DefaultValue("")]
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

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00004CA9 File Offset: 0x00002EA9
		public bool ServerPollingSupport
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004CAC File Offset: 0x00002EAC
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.IsDesignMode || !this.AreFileUploadParamsPresent())
			{
				return;
			}
			this.IsInFileUploadPostBack = true;
			AjaxFileUpload.UploadRequestProcessor uploadRequestProcessor = new AjaxFileUpload.UploadRequestProcessor
			{
				Context = this.Context,
				UploadStart = this.UploadStart,
				UploadComplete = this.UploadComplete,
				UploadCompleteAll = this.UploadCompleteAll,
				SetUploadedFilePath = new Action<string>(this.SetUploadedFilePath)
			};
			uploadRequestProcessor.ProcessRequest();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004D28 File Offset: 0x00002F28
		private bool AreFileUploadParamsPresent()
		{
			return !string.IsNullOrEmpty(this.Page.Request.QueryString["contextkey"]) && this.Page.Request.QueryString["contextkey"] == "{DA8BEDC8-B952-4d5d-8CC2-59FE922E2923}" && this.Page.Request.QueryString["controlID"] == this.ClientID;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00004DA3 File Offset: 0x00002FA3
		private void SetUploadedFilePath(string path)
		{
			this._uploadedFilePath = path;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00004DAC File Offset: 0x00002FAC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ScriptManager.RegisterOnSubmitStatement(this, typeof(AjaxFileUpload), "AjaxFileUploadOnSubmit", "null;");
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00004DD0 File Offset: 0x00002FD0
		public void SaveAs(string fileName)
		{
			string directoryName = Path.GetDirectoryName(this._uploadedFilePath);
			if (File.Exists(fileName))
			{
				File.Delete(fileName);
			}
			File.Copy(this._uploadedFilePath, fileName);
			File.Delete(this._uploadedFilePath);
			Directory.Delete(directoryName);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004E14 File Offset: 0x00003014
		public static void CleanAllTemporaryData()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(AjaxFileUpload.BuildRootTempFolder());
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				directoryInfo2.Delete(true);
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004E4C File Offset: 0x0000304C
		public static string BuildTempFolder(string fileId)
		{
			return Path.Combine(AjaxFileUpload.BuildRootTempFolder(), fileId);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004E5C File Offset: 0x0000305C
		public static string BuildRootTempFolder()
		{
			string tempFolder = ToolkitConfig.TempFolder;
			if (string.IsNullOrWhiteSpace(tempFolder))
			{
				string text = Path.Combine(Path.GetTempPath(), "_AjaxFileUpload");
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				return text;
			}
			string physicalPath = AjaxFileUpload.GetPhysicalPath(tempFolder);
			if (!Directory.Exists(physicalPath))
			{
				throw new IOException(string.Format("Temp directory '{0}' does not exist.", physicalPath));
			}
			return physicalPath;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004EB9 File Offset: 0x000030B9
		private static string GetPhysicalPath(string path)
		{
			if (path.StartsWith("~"))
			{
				return HttpContext.Current.Server.MapPath(path);
			}
			return path;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00004EDA File Offset: 0x000030DA
		internal void CreateChilds()
		{
			this.Controls.Clear();
			this.CreateChildControls();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00004EED File Offset: 0x000030ED
		protected override void CreateChildControls()
		{
			this.GenerateHtmlInputControls();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00004EF8 File Offset: 0x000030F8
		protected string GenerateHtmlInputControls()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", "ajax__fileupload");
			this.Controls.Add(htmlGenericControl);
			string value = "opacity:0; -moz-opacity: 0.0; filter: alpha(opacity=0);";
			HtmlInputFile htmlInputFile = new HtmlInputFile();
			if (!this.Enabled)
			{
				htmlInputFile.Disabled = true;
			}
			htmlInputFile.Attributes.Add("id", this.ClientID + "_Html5InputFile");
			htmlInputFile.Attributes.Add("multiple", "multiple");
			htmlInputFile.Attributes.Add("style", value);
			this.HideElement(htmlInputFile);
			HtmlInputFile htmlInputFile2 = new HtmlInputFile();
			if (!this.Enabled)
			{
				htmlInputFile2.Disabled = true;
			}
			htmlInputFile2.Attributes.Add("id", this.ClientID + "_InputFileElement");
			htmlInputFile2.Attributes.Add("name", "act-file-data");
			htmlInputFile2.Attributes.Add("style", value);
			this.HideElement(htmlInputFile2);
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("div");
			htmlGenericControl2.Attributes.Add("class", "ajax__fileupload_dropzone");
			htmlGenericControl2.Attributes.Add("id", this.ClientID + "_Html5DropZone");
			htmlGenericControl2.Attributes.Add("style", "width:100%; height:60px;");
			this.HideElement(htmlGenericControl2);
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("div");
			htmlGenericControl3.Attributes.Add("id", this.ClientID + "_FileStatusContainer");
			htmlGenericControl3.Style[HtmlTextWriterStyle.Position] = "absolute";
			htmlGenericControl3.Style["right"] = "0";
			htmlGenericControl3.Style["top"] = "2px";
			htmlGenericControl3.Style["height"] = "20px";
			htmlGenericControl3.Style["line-height"] = "20px";
			this.HideElement(htmlGenericControl3);
			HtmlGenericControl child = this.GenerateHtmlSelectFileContainer(htmlInputFile2, htmlInputFile);
			htmlGenericControl.Controls.Add(child);
			htmlGenericControl.Controls.Add(this.GenerateHtmlTopFileStatus(htmlGenericControl3));
			HtmlGenericControl htmlGenericControl4 = new HtmlGenericControl("div");
			htmlGenericControl4.Attributes.Add("id", this.ClientID + "_QueueContainer");
			htmlGenericControl4.Attributes.Add("class", "ajax__fileupload_queueContainer");
			htmlGenericControl4.Style[HtmlTextWriterStyle.MarginTop] = "28px";
			htmlGenericControl.Controls.Add(htmlGenericControl4);
			this.HideElement(htmlGenericControl4);
			HtmlGenericControl htmlGenericControl5 = new HtmlGenericControl("div");
			htmlGenericControl5.Attributes.Add("id", this.ClientID + "_ProgressBar");
			htmlGenericControl5.Attributes.Add("class", "ajax__fileupload_progressBar");
			htmlGenericControl5.Attributes.Add("style", "width: 100%; display: none; visibility: hidden; overflow:visible;white-space:nowrap; height:20px;");
			HtmlGenericControl child2 = this.GenerateHtmlFooterContainer(htmlGenericControl5);
			htmlGenericControl.Controls.Add(child2);
			return htmlGenericControl.ClientID;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005214 File Offset: 0x00003414
		private HtmlGenericControl GenerateHtmlFooterContainer(Control progressBar)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", "ajax__fileupload_footer");
			htmlGenericControl.Attributes.Add("id", this.ClientID + "_Footer");
			htmlGenericControl.Attributes["align"] = "right";
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("div");
			htmlGenericControl2.Attributes.Add("id", this.ClientID + "_UploadOrCancelButton");
			htmlGenericControl2.Attributes.Add("class", "ajax__fileupload_uploadbutton");
			HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("div");
			htmlGenericControl3.Attributes.Add("id", this.ClientID + "_ProgressBarContainer");
			htmlGenericControl3.Attributes["align"] = "left";
			htmlGenericControl3.Style["float"] = "left";
			htmlGenericControl3.Style["width"] = "100%";
			htmlGenericControl3.Controls.Add(progressBar);
			this.HideElement(htmlGenericControl3);
			HtmlGenericControl htmlGenericControl4 = new HtmlGenericControl("div");
			htmlGenericControl4.Attributes.Add("class", "ajax__fileupload_ProgressBarHolder");
			htmlGenericControl4.Controls.Add(htmlGenericControl3);
			htmlGenericControl.Controls.Add(htmlGenericControl4);
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			return htmlGenericControl;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005378 File Offset: 0x00003578
		private HtmlGenericControl GenerateHtmlSelectFileContainer(Control html5InputFileElement, Control inputFileElement)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("span");
			htmlGenericControl.Attributes.Add("id", this.ClientID + "_SelectFileContainer");
			htmlGenericControl.Attributes.Add("class", "ajax__fileupload_selectFileContainer");
			htmlGenericControl.Style["float"] = "left";
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("span");
			htmlGenericControl2.Attributes.Add("id", this.ClientID + "_SelectFileButton");
			htmlGenericControl2.Attributes.Add("class", "ajax__fileupload_selectFileButton");
			htmlGenericControl.Controls.Add(htmlGenericControl2);
			htmlGenericControl.Controls.Add(inputFileElement);
			htmlGenericControl.Controls.Add(html5InputFileElement);
			return htmlGenericControl;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005440 File Offset: 0x00003640
		private HtmlGenericControl GenerateHtmlTopFileStatus(Control fileStatusContainer)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("div");
			htmlGenericControl.Attributes.Add("class", "ajax__fileupload_topFileStatus");
			htmlGenericControl.Style[HtmlTextWriterStyle.Position] = "relative";
			htmlGenericControl.Controls.Add(fileStatusContainer);
			return htmlGenericControl;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000548C File Offset: 0x0000368C
		private void HideElement(HtmlControl element)
		{
			element.Style["display"] = "none";
			element.Style["visibility"] = "hidden";
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000054B8 File Offset: 0x000036B8
		protected override void DescribeComponent(ScriptComponentDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			if (this.IsDesignMode)
			{
				return;
			}
			descriptor.AddProperty("contextKey", "{DA8BEDC8-B952-4d5d-8CC2-59FE922E2923}");
			descriptor.AddProperty("postBackUrl", this.Page.Request.RawUrl);
			descriptor.AddProperty("serverPollingSupport", this.ServerPollingSupport);
			if (this.ThrobberID != string.Empty)
			{
				Control control = this.FindControl(this.ThrobberID);
				if (control != null)
				{
					descriptor.AddElementProperty("throbber", control.ClientID);
				}
			}
		}

		// Token: 0x04000046 RID: 70
		internal const string ContextKey = "{DA8BEDC8-B952-4d5d-8CC2-59FE922E2923}";

		// Token: 0x04000047 RID: 71
		private const string DefaultTempSubDir = "_AjaxFileUpload";

		// Token: 0x04000048 RID: 72
		private string _uploadedFilePath;

		// Token: 0x0200001D RID: 29
		private class UploadRequestProcessor
		{
			// Token: 0x17000072 RID: 114
			// (get) Token: 0x06000159 RID: 345 RVA: 0x00005549 File Offset: 0x00003749
			private HttpRequest Request
			{
				get
				{
					return this.Context.Request;
				}
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x0600015A RID: 346 RVA: 0x00005556 File Offset: 0x00003756
			private HttpResponse Response
			{
				get
				{
					return this.Context.Response;
				}
			}

			// Token: 0x0600015B RID: 347 RVA: 0x00005564 File Offset: 0x00003764
			public void ProcessRequest()
			{
				string fileId;
				XhrType xhrType = this.ParseRequest(out fileId);
				if (xhrType != XhrType.None)
				{
					this.Response.ClearContent();
					this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
					switch (xhrType)
					{
					case XhrType.Start:
						this.XhrStart();
						break;
					case XhrType.Poll:
						this.XhrPoll(fileId);
						break;
					case XhrType.Cancel:
						this.XhrCancel(fileId);
						break;
					case XhrType.Done:
						this.XhrDone(fileId);
						break;
					case XhrType.Complete:
						this.XhrComplete();
						break;
					}
					this.Response.End();
				}
			}

			// Token: 0x0600015C RID: 348 RVA: 0x000055EC File Offset: 0x000037EC
			private XhrType ParseRequest(out string fileId)
			{
				fileId = this.Request.QueryString["guid"];
				if (!string.IsNullOrEmpty(fileId))
				{
					if (this.Request.QueryString["poll"] == "1")
					{
						return XhrType.Poll;
					}
					if (this.Request.QueryString["cancel"] == "1")
					{
						return XhrType.Cancel;
					}
					if (this.Request.QueryString["done"] == "1")
					{
						return XhrType.Done;
					}
				}
				if (this.Request.QueryString["complete"] == "1")
				{
					return XhrType.Complete;
				}
				if (this.Request.QueryString["start"] == "1")
				{
					return XhrType.Start;
				}
				return XhrType.None;
			}

			// Token: 0x0600015D RID: 349 RVA: 0x000056CC File Offset: 0x000038CC
			private void XhrStart()
			{
				int filesInQueue = int.Parse(this.Request.QueryString["queue"] ?? "0");
				AjaxFileUploadStartEventArgs ajaxFileUploadStartEventArgs = new AjaxFileUploadStartEventArgs(filesInQueue);
				if (this.UploadStart != null)
				{
					this.UploadStart(this, ajaxFileUploadStartEventArgs);
				}
				this.Response.Write(new JavaScriptSerializer().Serialize(ajaxFileUploadStartEventArgs));
			}

			// Token: 0x0600015E RID: 350 RVA: 0x00005730 File Offset: 0x00003930
			private void XhrComplete()
			{
				int filesInQueue = int.Parse(this.Request.QueryString["queue"] ?? "0");
				int filesUploaded = int.Parse(this.Request.QueryString["uploaded"] ?? "0");
				string text = this.Request.QueryString["reason"];
				string a;
				AjaxFileUploadCompleteAllReason reason;
				if ((a = text) != null)
				{
					if (a == "done")
					{
						reason = AjaxFileUploadCompleteAllReason.Success;
						goto IL_8C;
					}
					if (a == "cancel")
					{
						reason = AjaxFileUploadCompleteAllReason.Canceled;
						goto IL_8C;
					}
				}
				reason = AjaxFileUploadCompleteAllReason.Unknown;
				IL_8C:
				AjaxFileUploadCompleteAllEventArgs ajaxFileUploadCompleteAllEventArgs = new AjaxFileUploadCompleteAllEventArgs(filesInQueue, filesUploaded, reason);
				if (this.UploadCompleteAll != null)
				{
					this.UploadCompleteAll(this, ajaxFileUploadCompleteAllEventArgs);
				}
				this.Response.Write(new JavaScriptSerializer().Serialize(ajaxFileUploadCompleteAllEventArgs));
			}

			// Token: 0x0600015F RID: 351 RVA: 0x00005800 File Offset: 0x00003A00
			private void XhrDone(string fileId)
			{
				string path = AjaxFileUpload.BuildTempFolder(fileId);
				if (!Directory.Exists(path))
				{
					return;
				}
				string[] files = Directory.GetFiles(path);
				if (files.Length == 0)
				{
					return;
				}
				FileInfo fileInfo = new FileInfo(files[0]);
				this.SetUploadedFilePath(fileInfo.FullName);
				AjaxFileUploadEventArgs ajaxFileUploadEventArgs = new AjaxFileUploadEventArgs(fileId, AjaxFileUploadState.Success, "Success", fileInfo.Name, (int)fileInfo.Length, fileInfo.Extension);
				if (this.UploadComplete != null)
				{
					this.UploadComplete(this, ajaxFileUploadEventArgs);
				}
				this.Response.Write(new JavaScriptSerializer().Serialize(ajaxFileUploadEventArgs));
			}

			// Token: 0x06000160 RID: 352 RVA: 0x0000588F File Offset: 0x00003A8F
			private void XhrCancel(string fileId)
			{
				AjaxFileUploadHelper.Abort(this.Context, fileId);
			}

			// Token: 0x06000161 RID: 353 RVA: 0x000058A0 File Offset: 0x00003AA0
			private void XhrPoll(string fileId)
			{
				this.Response.Write(new AjaxFileUploadStates(this.Context, fileId).Percent.ToString());
			}

			// Token: 0x04000050 RID: 80
			public HttpContext Context;

			// Token: 0x04000051 RID: 81
			public EventHandler<AjaxFileUploadStartEventArgs> UploadStart;

			// Token: 0x04000052 RID: 82
			public EventHandler<AjaxFileUploadEventArgs> UploadComplete;

			// Token: 0x04000053 RID: 83
			public EventHandler<AjaxFileUploadCompleteAllEventArgs> UploadCompleteAll;

			// Token: 0x04000054 RID: 84
			public Action<string> SetUploadedFilePath;
		}
	}
}
