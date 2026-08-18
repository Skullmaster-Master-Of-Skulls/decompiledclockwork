using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.ClockWork.Controls
{
	// Token: 0x02000006 RID: 6
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlCancelButton runat=server></{0}:CtrlCancelButton>")]
	public class CtrlCancelButton : WebControl, INamingContainer, IButtonControl
	{
		// Token: 0x06000035 RID: 53 RVA: 0x0000284E File Offset: 0x00000A4E
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002860 File Offset: 0x00000A60
		private void InitializeControls()
		{
			this.btn.ID = "btn_cancel";
			this.btn.OnClientClick = "return confirm('Are you sure you want to cancel?');";
			this.btn.Text = this.title;
			this.btn.CausesValidation = false;
			this.btn.CssClass = "ButtonBottom";
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000028C0 File Offset: 0x00000AC0
		// (set) Token: 0x06000038 RID: 56 RVA: 0x000028D8 File Offset: 0x00000AD8
		public string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
				this.btn.Text = this.title;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028F4 File Offset: 0x00000AF4
		protected override void RenderContents(HtmlTextWriter output)
		{
			this.btn.CausesValidation = false;
			this.btn.RenderControl(output);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002911 File Offset: 0x00000B11
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.btn);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002926 File Offset: 0x00000B26
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002926 File Offset: 0x00000B26
		public override void RenderEndTag(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002929 File Offset: 0x00000B29
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000293C File Offset: 0x00000B3C
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002959 File Offset: 0x00000B59
		public bool CausesValidation
		{
			get
			{
				return this.btn.CausesValidation;
			}
			set
			{
				this.btn.CausesValidation = value;
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000040 RID: 64 RVA: 0x00002969 File Offset: 0x00000B69
		// (remove) Token: 0x06000041 RID: 65 RVA: 0x00002979 File Offset: 0x00000B79
		public event EventHandler Click
		{
			add
			{
				this.btn.Click += value;
			}
			remove
			{
				this.btn.Click -= value;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000042 RID: 66 RVA: 0x00002989 File Offset: 0x00000B89
		// (remove) Token: 0x06000043 RID: 67 RVA: 0x00002999 File Offset: 0x00000B99
		public event CommandEventHandler Command
		{
			add
			{
				this.btn.Command += value;
			}
			remove
			{
				this.btn.Command -= value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000029AC File Offset: 0x00000BAC
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000029C9 File Offset: 0x00000BC9
		public string CommandArgument
		{
			get
			{
				return this.btn.CommandArgument;
			}
			set
			{
				this.btn.CommandArgument = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000029DC File Offset: 0x00000BDC
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000029F9 File Offset: 0x00000BF9
		public string CommandName
		{
			get
			{
				return this.btn.CommandName;
			}
			set
			{
				this.btn.CommandName = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002A0C File Offset: 0x00000C0C
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002A29 File Offset: 0x00000C29
		public string PostBackUrl
		{
			get
			{
				return this.btn.PostBackUrl;
			}
			set
			{
				this.btn.PostBackUrl = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002A3C File Offset: 0x00000C3C
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002A59 File Offset: 0x00000C59
		public string Text
		{
			get
			{
				return this.btn.Text;
			}
			set
			{
				this.btn.Text = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002A6C File Offset: 0x00000C6C
		// (set) Token: 0x0600004D RID: 77 RVA: 0x00002A89 File Offset: 0x00000C89
		public string ValidationGroup
		{
			get
			{
				return this.btn.ValidationGroup;
			}
			set
			{
				this.btn.ValidationGroup = value;
			}
		}

		// Token: 0x04000018 RID: 24
		private Button btn = new Button();

		// Token: 0x04000019 RID: 25
		private string title = "Cancel";
	}
}
