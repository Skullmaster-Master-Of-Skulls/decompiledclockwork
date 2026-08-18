using System;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.UI.Web.DynamicControls.Entity;

namespace TechnoPro.Common.UI.Web.DynamicControls.Controls
{
	// Token: 0x02000006 RID: 6
	[ToolboxData("<{0}:CtrlFileChooser runat=server></{0}:CtrlFileChooser>")]
	public class CtrlFileChooser : WebControl, IDynamicWebControl, INamingContainer
	{
		// Token: 0x06000047 RID: 71 RVA: 0x000032D0 File Offset: 0x000014D0
		public CtrlFileChooser()
		{
			this.EnableViewState = false;
			Page page = (Page)HttpContext.Current.Handler;
			if (page != null)
			{
				bool isPostBack = page.IsPostBack;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003330 File Offset: 0x00001530
		public CtrlFileChooser(DynamicFieldDTO Field)
		{
			this.DynamicField = Field;
			this.EnableViewState = false;
			Page page = (Page)HttpContext.Current.Handler;
			if (page != null)
			{
				bool isPostBack = page.IsPostBack;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003398 File Offset: 0x00001598
		public override void Dispose()
		{
			if (this.fileUpload != null)
			{
				this.fileUpload.Dispose();
			}
			if (this.lbl != null)
			{
				this.lbl.Dispose();
			}
			if (this.btn != null)
			{
				this.btn.Dispose();
			}
			if (this.throbber != null)
			{
				this.throbber.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000033F7 File Offset: 0x000015F7
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003405 File Offset: 0x00001605
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003414 File Offset: 0x00001614
		private void InitializeControls()
		{
			int num;
			string text;
			if (this.DynamicField != null)
			{
				num = this.DynamicField.ControlId;
				text = this.DynamicField.ControlCaption;
			}
			else
			{
				num = 0;
				text = "Select a file:";
			}
			this.lbl.Text = text;
			this.lbl.CssClass = "cxformtitle";
			string str = num.ToString();
			this.fileUpload.ID = "fpk_" + str;
			this.lbl.ID = "hlbl_fpk_" + num.ToString();
			this.btn.ID = "hbtn_fpk_" + num.ToString();
			this.throbber.ID = "hthrobber_fpk_" + num.ToString();
			this.lbl.AssociatedControlID = this.fileUpload.ID;
			this.throbber.Style.Add(HtmlTextWriterStyle.Display, "none");
			this.throbber.ImageUrl = this.Page.ClientScript.GetWebResourceUrl(base.GetType(), "TechnoPro.Common.UI.Web.DynamicControls.img.progress.gif");
			this.fileUpload.ThrobberID = this.throbber.ID;
			this.fileUpload.CssClass = "cxformctrl";
			this.btn.Text = "Clear";
			this.fileUpload.UploadedComplete += this.fileUpload_UploadedComplete;
			this.btn.Click += this.btn_Click;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003590 File Offset: 0x00001790
		protected override void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "cxform");
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			this.lbl.RenderControl(writer);
			if (!string.IsNullOrEmpty(this.clientFileName))
			{
				writer.Write(Path.GetFileName(this.clientFileName));
				writer.Write("&nbsp;&nbsp;&nbsp;");
				this.btn.RenderControl(writer);
			}
			else
			{
				this.throbber.RenderControl(writer);
				this.fileUpload.RenderControl(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003613 File Offset: 0x00001813
		private void btn_Click(object sender, EventArgs e)
		{
			this.ClearData();
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600004F RID: 79 RVA: 0x0000361C File Offset: 0x0000181C
		// (remove) Token: 0x06000050 RID: 80 RVA: 0x00003654 File Offset: 0x00001854
		public event EventHandler<FileUploadedArgs> OnUploadCompleted;

		// Token: 0x06000051 RID: 81 RVA: 0x00003689 File Offset: 0x00001889
		private void FireOnUploadCompleted(object sender, FileUploadedArgs e)
		{
			if (this.OnUploadCompleted != null)
			{
				this.OnUploadCompleted(this, e);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000036A0 File Offset: 0x000018A0
		private void fileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
		{
			string text = Path.GetTempFileName();
			string text2 = Path.Combine(Path.GetDirectoryName(text), "TechnoPro");
			text2 = Path.Combine(text2, "ClockWork");
			if (!Directory.Exists(text2))
			{
				Directory.CreateDirectory(text2);
			}
			text = Path.Combine(text2, Path.GetFileName(text));
			this.fileUpload.SaveAs(text);
			this.clientFileName = e.FileName;
			this.serverFileName = text;
			this.FireOnUploadCompleted(sender, new FileUploadedArgs
			{
				clientFileName = this.clientFileName,
				serverFileName = this.serverFileName
			});
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003730 File Offset: 0x00001930
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00003770 File Offset: 0x00001970
		private string clientFileName
		{
			get
			{
				HttpSessionState session = HttpContext.Current.Session;
				string name = "val_" + this.ViewStateKey;
				string text = (string)session[name];
				if (text == null)
				{
					return "";
				}
				return text;
			}
			set
			{
				HttpSessionState session = HttpContext.Current.Session;
				string name = "val_" + this.ViewStateKey;
				session[name] = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000037A0 File Offset: 0x000019A0
		// (set) Token: 0x06000056 RID: 86 RVA: 0x000037E0 File Offset: 0x000019E0
		private string serverFileName
		{
			get
			{
				HttpSessionState session = HttpContext.Current.Session;
				string name = "val2_" + this.ViewStateKey;
				string text = (string)session[name];
				if (text == null)
				{
					return "";
				}
				return text;
			}
			set
			{
				HttpSessionState session = HttpContext.Current.Session;
				string name = "val2_" + this.ViewStateKey;
				session[name] = value;
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003810 File Offset: 0x00001A10
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.lbl);
			this.Controls.Add(this.throbber);
			this.Controls.Add(this.fileUpload);
			this.Controls.Add(this.btn);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003861 File Offset: 0x00001A61
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00003869 File Offset: 0x00001A69
		public DynamicFieldDTO DynamicField { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003872 File Offset: 0x00001A72
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000387A File Offset: 0x00001A7A
		public DynamicDataDTO DynamicData { get; set; }

		// Token: 0x0600005C RID: 92 RVA: 0x00003883 File Offset: 0x00001A83
		public void ChildLoadViewState(object dataFromViewState)
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003885 File Offset: 0x00001A85
		public object ChildSaveViewState()
		{
			return null;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003888 File Offset: 0x00001A88
		public string ViewStateKey
		{
			get
			{
				if (!string.IsNullOrEmpty(this.fileUpload.ID))
				{
					return "v" + this.fileUpload.ID;
				}
				if (this.DynamicField != null)
				{
					this.fileUpload.ID = "fpk_" + this.DynamicField.ControlId.ToString();
					return "v" + this.fileUpload.ID;
				}
				return "lbl_nocid";
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003908 File Offset: 0x00001B08
		public void ClearData()
		{
			string serverFileName = this.serverFileName;
			if (!string.IsNullOrEmpty(serverFileName) && File.Exists(serverFileName))
			{
				File.Delete(serverFileName);
			}
			this.clientFileName = "";
			this.serverFileName = "";
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003883 File Offset: 0x00001A83
		public void ShowData(DynamicDataDTO data)
		{
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003948 File Offset: 0x00001B48
		public DynamicDataDTO GetCurrentData(out bool isEmpty)
		{
			isEmpty = true;
			return null;
		}

		// Token: 0x04000014 RID: 20
		private const string ID_PREFIX = "fpk_";

		// Token: 0x04000015 RID: 21
		private Button btn = new Button();

		// Token: 0x04000016 RID: 22
		private Label lbl = new Label();

		// Token: 0x04000017 RID: 23
		private AsyncFileUpload fileUpload = new AsyncFileUpload();

		// Token: 0x04000018 RID: 24
		private Image throbber = new Image();
	}
}
