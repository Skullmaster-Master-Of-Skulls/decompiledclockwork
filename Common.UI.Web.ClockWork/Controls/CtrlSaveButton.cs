using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.ClockWork.Controls
{
	// Token: 0x02000008 RID: 8
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlSaveButton runat=server></{0}:CtrlSaveButton>")]
	public class CtrlSaveButton : WebControl, INamingContainer, IButtonControl
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002B36 File Offset: 0x00000D36
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B48 File Offset: 0x00000D48
		private void InitializeControls()
		{
			this.btn.ID = "btn_save";
			this.btn.Text = this.title;
			this.btn.CssClass = "ButtonBottom";
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002B80 File Offset: 0x00000D80
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002B98 File Offset: 0x00000D98
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002BB4 File Offset: 0x00000DB4
		private string waitGifUrl
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this._waitGifUrl);
				if (flag)
				{
					this._waitGifUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(CtrlToDoItemChecked), "TechnoPro.Common.UI.Web.ClockWork.Resources.Wait.gif");
				}
				return this._waitGifUrl;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C02 File Offset: 0x00000E02
		protected override void RenderContents(HtmlTextWriter output)
		{
			this.btn.RenderControl(output);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002C12 File Offset: 0x00000E12
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.btn);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002926 File Offset: 0x00000B26
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002926 File Offset: 0x00000B26
		public override void RenderEndTag(HtmlTextWriter writer)
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002C27 File Offset: 0x00000E27
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002C38 File Offset: 0x00000E38
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002C55 File Offset: 0x00000E55
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

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000061 RID: 97 RVA: 0x00002C65 File Offset: 0x00000E65
		// (remove) Token: 0x06000062 RID: 98 RVA: 0x00002C75 File Offset: 0x00000E75
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

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000063 RID: 99 RVA: 0x00002C85 File Offset: 0x00000E85
		// (remove) Token: 0x06000064 RID: 100 RVA: 0x00002C95 File Offset: 0x00000E95
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

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002CA8 File Offset: 0x00000EA8
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002CC5 File Offset: 0x00000EC5
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

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002CD8 File Offset: 0x00000ED8
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002CF5 File Offset: 0x00000EF5
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

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002D08 File Offset: 0x00000F08
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002D25 File Offset: 0x00000F25
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

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002D38 File Offset: 0x00000F38
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002D55 File Offset: 0x00000F55
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

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002D68 File Offset: 0x00000F68
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002D85 File Offset: 0x00000F85
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

		// Token: 0x0400001B RID: 27
		private Button btn = new Button();

		// Token: 0x0400001C RID: 28
		private string title = "Submit";

		// Token: 0x0400001D RID: 29
		private string _waitGifUrl;
	}
}
