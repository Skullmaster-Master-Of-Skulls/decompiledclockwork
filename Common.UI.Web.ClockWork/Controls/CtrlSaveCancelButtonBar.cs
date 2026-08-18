using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.ClockWork.Controls
{
	// Token: 0x02000009 RID: 9
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlSaveCancelButtonBottom runat=server></{0}:CtrlSaveCancelButtonBottom>")]
	public class CtrlSaveCancelButtonBar : WebControl, INamingContainer
	{
		// Token: 0x06000070 RID: 112 RVA: 0x00002DCB File Offset: 0x00000FCB
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002926 File Offset: 0x00000B26
		private void InitializeControls()
		{
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002DE0 File Offset: 0x00000FE0
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public string TitleSave
		{
			get
			{
				return this.titleSave;
			}
			set
			{
				this.titleSave = value;
				this.btn_save.Title = this.titleSave;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002E14 File Offset: 0x00001014
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002E2C File Offset: 0x0000102C
		public string TitleCancel
		{
			get
			{
				return this.titleCancel;
			}
			set
			{
				this.titleCancel = value;
				this.btn_cancel.Title = this.titleCancel;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002E48 File Offset: 0x00001048
		protected override void RenderContents(HtmlTextWriter output)
		{
			this.btn_save.RenderControl(output);
			this.btn_cancel.RenderControl(output);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002E65 File Offset: 0x00001065
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.btn_save);
			this.Controls.Add(this.btn_cancel);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002E8C File Offset: 0x0000108C
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.Write("<div class='ButtonBarBottom'>");
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002E9B File Offset: 0x0000109B
		public override void RenderEndTag(HtmlTextWriter writer)
		{
			writer.Write("</div>");
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002EAA File Offset: 0x000010AA
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600007B RID: 123 RVA: 0x00002EBB File Offset: 0x000010BB
		// (remove) Token: 0x0600007C RID: 124 RVA: 0x00002ECB File Offset: 0x000010CB
		public event EventHandler OnSaveClick
		{
			add
			{
				this.btn_save.Click += value;
			}
			remove
			{
				this.btn_save.Click -= value;
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600007D RID: 125 RVA: 0x00002EDB File Offset: 0x000010DB
		// (remove) Token: 0x0600007E RID: 126 RVA: 0x00002EEB File Offset: 0x000010EB
		public event EventHandler OnCancelClick
		{
			add
			{
				this.btn_cancel.Click += value;
			}
			remove
			{
				this.btn_cancel.Click -= value;
			}
		}

		// Token: 0x0400001E RID: 30
		private CtrlSaveButton btn_save = new CtrlSaveButton();

		// Token: 0x0400001F RID: 31
		private CtrlCancelButton btn_cancel = new CtrlCancelButton();

		// Token: 0x04000020 RID: 32
		private string titleSave = "Submit";

		// Token: 0x04000021 RID: 33
		private string titleCancel = "Cancel";
	}
}
