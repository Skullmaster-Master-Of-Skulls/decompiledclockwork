using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

namespace TechnoPro.Common.UI.Web.ClockWork.Controls
{
	// Token: 0x0200000E RID: 14
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlToDoItemChecked runat=server></{0}:CtrlToDoItemChecked>")]
	public class CtrlToDoItemChecked : WebControl, INamingContainer
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003FCE File Offset: 0x000021CE
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003FD6 File Offset: 0x000021D6
		public bool DisallowEditing { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003FE0 File Offset: 0x000021E0
		public bool IsChecked
		{
			get
			{
				return this.isChecked;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003FF8 File Offset: 0x000021F8
		public new bool IsEnabled
		{
			get
			{
				return this.isEnabled;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004010 File Offset: 0x00002210
		public bool IsCurrent
		{
			get
			{
				return this.isCurrent;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004028 File Offset: 0x00002228
		public new void Init(string Title, string Link, string Description, bool IsChecked, bool IsEnabled, bool IsCurrent, bool disallowEditing)
		{
			this.url = Link;
			this.titleText = Title;
			this.isChecked = IsChecked;
			this.isEnabled = IsEnabled;
			this.isCurrent = IsCurrent;
			this.DisallowEditing = disallowEditing;
			this.title.Text = string.Format("<span class='TaskCheckItemTitle'>{0}</span>", Title);
			this.description.Text = Description + " <br />";
			bool flag = this.isChecked;
			if (flag)
			{
				this.panel.CssClass = "TaskCheckItemBox selected";
			}
			else
			{
				this.panel.CssClass = "TaskCheckItemBox";
			}
			bool flag2 = this.isChecked;
			if (flag2)
			{
				this.status.Text = string.Format("<span class='TaskCheckItemStatus'>{0}</span>", "Completed");
			}
			else
			{
				bool flag3 = this.isCurrent;
				if (flag3)
				{
					this.status.Text = string.Format("<span class='TaskCheckItemStatus'>{0}</span>", "Pending");
				}
				else
				{
					this.status.Text = string.Format("<span class='TaskCheckItemStatus'>{0}</span>", "&nbsp;");
				}
			}
			this.hr.Text = "<hr class='TaskCheckItemSeparator'/>";
			this.panel_description.CssClass = "TaskItemDescription";
			bool flag4 = !string.IsNullOrEmpty(this.url) && IsEnabled && (IsCurrent || !this.DisallowEditing);
			if (flag4)
			{
				this.linkButton.PostBackUrl = this.url;
				if (IsCurrent)
				{
					this.linkButton.Text = "Click here to begin this step ...";
				}
				else
				{
					this.linkButton.Text = "Click here to edit this step ...";
				}
				this.linkButton.Style.Add("margin-top", "20px");
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00002619 File Offset: 0x00000819
		public override void RenderBeginTag(HtmlTextWriter writer)
		{
			writer.RenderBeginTag("div");
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000041D1 File Offset: 0x000023D1
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000041E4 File Offset: 0x000023E4
		private string checkedUrl
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this._checkedUrl);
				if (flag)
				{
					this._checkedUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(CtrlToDoItemChecked), "TechnoPro.Common.UI.Web.ClockWork.Resources.checkedlarge.png");
				}
				return this._checkedUrl;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004234 File Offset: 0x00002434
		private string uncheckedUrl
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this._uncheckedUrl);
				if (flag)
				{
					this._uncheckedUrl = this.Page.ClientScript.GetWebResourceUrl(typeof(CtrlToDoItemChecked), "TechnoPro.Common.UI.Web.ClockWork.Resources.uncheckedlarge.png");
				}
				return this._uncheckedUrl;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00004282 File Offset: 0x00002482
		protected override void RenderContents(HtmlTextWriter writer)
		{
			this.panel.RenderControl(writer);
			this.roundedCornersExtender.RenderControl(writer);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000429F File Offset: 0x0000249F
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000042B4 File Offset: 0x000024B4
		private void InitializeControls()
		{
			this.panel.ID = "p" + this.ID;
			this.panel.Width = 800;
			this.panel.Height = 120;
			this.roundedCornersExtender.Corners = BoxCorners.All;
			this.roundedCornersExtender.Radius = 6;
			this.roundedCornersExtender.TargetControlID = this.panel.ID;
			this.spacer1.Text = "<br />";
			this.spacer2.Text = "<br />";
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000435C File Offset: 0x0000255C
		private void BuildControlHeiarchy()
		{
			this.panel_description.Controls.Add(this.description);
			bool flag = this.IsEnabled && !string.IsNullOrEmpty(this.url);
			if (flag)
			{
				this.panel_description.Controls.Add(this.spacer1);
				this.panel_description.Controls.Add(this.linkButton);
			}
			IList<Control> list = this.FireOnSubControlsRequested();
			bool flag2 = list != null;
			if (flag2)
			{
				this.panel_description.Controls.Add(this.spacer2);
				foreach (Control child in list)
				{
					this.panel_description.Controls.Add(child);
				}
			}
			this.panel.Controls.Add(this.title);
			this.panel.Controls.Add(this.status);
			this.panel.Controls.Add(this.hr);
			this.panel.Controls.Add(this.panel_description);
			this.Controls.Add(this.panel);
			this.Controls.Add(this.roundedCornersExtender);
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060000D2 RID: 210 RVA: 0x000044C0 File Offset: 0x000026C0
		// (remove) Token: 0x060000D3 RID: 211 RVA: 0x000044F8 File Offset: 0x000026F8
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event EventHandler<SubControlsRequiredArgs> OnSubControlsRequired;

		// Token: 0x060000D4 RID: 212 RVA: 0x00004530 File Offset: 0x00002730
		private IList<Control> FireOnSubControlsRequested()
		{
			EventHandler<SubControlsRequiredArgs> onSubControlsRequired = this.OnSubControlsRequired;
			bool flag = onSubControlsRequired != null;
			IList<Control> result;
			if (flag)
			{
				SubControlsRequiredArgs subControlsRequiredArgs = new SubControlsRequiredArgs();
				onSubControlsRequired(this, subControlsRequiredArgs);
				result = ((subControlsRequiredArgs == null) ? null : subControlsRequiredArgs.Controls);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000042 RID: 66
		private Panel panel = new Panel();

		// Token: 0x04000043 RID: 67
		private LiteralControl title = new LiteralControl();

		// Token: 0x04000044 RID: 68
		private LiteralControl status = new LiteralControl();

		// Token: 0x04000045 RID: 69
		private LiteralControl hr = new LiteralControl();

		// Token: 0x04000046 RID: 70
		private LinkButton linkButton = new LinkButton();

		// Token: 0x04000047 RID: 71
		private Panel panel_description = new Panel();

		// Token: 0x04000048 RID: 72
		private LiteralControl description = new LiteralControl();

		// Token: 0x04000049 RID: 73
		private LiteralControl spacer1 = new LiteralControl();

		// Token: 0x0400004A RID: 74
		private LiteralControl spacer2 = new LiteralControl();

		// Token: 0x0400004B RID: 75
		private RoundedCornersExtender roundedCornersExtender = new RoundedCornersExtender();

		// Token: 0x0400004C RID: 76
		private bool isChecked = false;

		// Token: 0x0400004D RID: 77
		private bool isEnabled = false;

		// Token: 0x0400004E RID: 78
		private bool isCurrent = false;

		// Token: 0x04000050 RID: 80
		private string url = "";

		// Token: 0x04000051 RID: 81
		private string titleText = "";

		// Token: 0x04000052 RID: 82
		private string _checkedUrl;

		// Token: 0x04000053 RID: 83
		private string _uncheckedUrl;
	}
}
