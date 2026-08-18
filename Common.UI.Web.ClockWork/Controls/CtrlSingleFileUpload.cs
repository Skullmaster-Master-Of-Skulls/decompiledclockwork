using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.ClockWork.Controls
{
	// Token: 0x0200000A RID: 10
	public class CtrlSingleFileUpload : FileUpload
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00002F5A File Offset: 0x0000115A
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002F65 File Offset: 0x00001165
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002F78 File Offset: 0x00001178
		private void InitializeControls()
		{
			this.btnReUpload.ID = this.ID + "_btn";
			this.lbl.ID = this.ID + "_lbl";
			this.validator.ID = this.ID + "_validator";
			this.lblTitle.ID = this.ID + "_title";
			this.lblTitle.AssociatedControlID = this.ID;
			this.btnReUpload.Text = "Re-upload";
			this.lbl.Text = (this.alreadyUploadedFileName ?? "Already uploaded");
			this.validator.Display = ValidatorDisplay.Dynamic;
			this.validator.ErrorMessage = "This is a required field";
			this.validator.Text = "* required";
			this.validator.SetFocusOnError = true;
			this.validator.Enabled = this.isRequiredField;
			this.validator.Visible = this.isRequiredField;
			this.lblTitle.Text = this.title;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000030A8 File Offset: 0x000012A8
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000030C0 File Offset: 0x000012C0
		public string AlreadyUploadedFileName
		{
			get
			{
				return this.alreadyUploadedFileName;
			}
			set
			{
				this.alreadyUploadedFileName = value;
				this.lbl.Text = this.alreadyUploadedFileName;
				bool flag = !string.IsNullOrEmpty(this.alreadyUploadedFileName);
				if (flag)
				{
					this.IsRequiredField = false;
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00003104 File Offset: 0x00001304
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000311C File Offset: 0x0000131C
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
				this.lblTitle.Text = this.title;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003138 File Offset: 0x00001338
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003150 File Offset: 0x00001350
		public string LabelCssClass
		{
			get
			{
				return this.labelCssClass;
			}
			set
			{
				this.labelCssClass = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000315C File Offset: 0x0000135C
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003174 File Offset: 0x00001374
		public bool IsRequiredField
		{
			get
			{
				return this.isRequiredField;
			}
			set
			{
				this.isRequiredField = value;
				bool flag = this.isRequiredField;
				if (flag)
				{
					this.validator.Enabled = true;
					this.validator.Visible = true;
				}
				else
				{
					this.validator.Enabled = false;
					this.validator.Visible = false;
				}
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000031D0 File Offset: 0x000013D0
		public BaseValidator Validator
		{
			get
			{
				return this.validator;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000031E8 File Offset: 0x000013E8
		protected override void RenderContents(HtmlTextWriter output)
		{
			output.RenderBeginTag(HtmlTextWriterTag.Table);
			output.RenderBeginTag(HtmlTextWriterTag.Tr);
			output.RenderBeginTag(HtmlTextWriterTag.Td);
			this.lblTitle.RenderControl(output);
			output.RenderEndTag();
			output.RenderBeginTag(HtmlTextWriterTag.Td);
			output.Write("&nbsp;&nbsp;&nbsp;");
			output.RenderEndTag();
			output.RenderBeginTag(HtmlTextWriterTag.Td);
			base.RenderContents(output);
			bool flag = !string.IsNullOrEmpty(this.alreadyUploadedFileName);
			if (flag)
			{
				output.Write("<br />");
				output.Write("<div style='font-size: .75em; font-weight: bold;'>");
				this.lbl.RenderControl(output);
				output.Write("</div>");
			}
			output.RenderEndTag();
			output.RenderEndTag();
			output.RenderEndTag();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000032AC File Offset: 0x000014AC
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.lbl);
			this.Controls.Add(this.btnReUpload);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002619 File Offset: 0x00000819
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.RenderBeginTag("div");
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000032E5 File Offset: 0x000014E5
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000032F8 File Offset: 0x000014F8
		public int ContentLength
		{
			get
			{
				return (base.PostedFile == null) ? 0 : base.PostedFile.ContentLength;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003320 File Offset: 0x00001520
		public string ContentType
		{
			get
			{
				return (base.PostedFile == null) ? null : base.PostedFile.ContentType;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003348 File Offset: 0x00001548
		public Stream InputStream
		{
			get
			{
				return (base.PostedFile == null) ? null : base.PostedFile.InputStream;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003370 File Offset: 0x00001570
		public new string FileName
		{
			get
			{
				return (base.PostedFile.FileName == null) ? "" : base.PostedFile.FileName;
			}
		}

		// Token: 0x04000022 RID: 34
		private Label lblTitle = new Label();

		// Token: 0x04000023 RID: 35
		private Label lbl = new Label();

		// Token: 0x04000024 RID: 36
		private Button btnReUpload = new Button();

		// Token: 0x04000025 RID: 37
		private RequiredFieldValidator validator = new RequiredFieldValidator();

		// Token: 0x04000026 RID: 38
		private string alreadyUploadedFileName = null;

		// Token: 0x04000027 RID: 39
		private string title = "";

		// Token: 0x04000028 RID: 40
		private string labelCssClass = "label";

		// Token: 0x04000029 RID: 41
		private bool isRequiredField;
	}
}
