using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Common.Helpers;

namespace Telerik.Web.UI.SchedulerReminderDialog.Native
{
	// Token: 0x0200080A RID: 2058
	internal class Renderer : RendererBase
	{
		// Token: 0x1700189B RID: 6299
		// (get) Token: 0x06004B42 RID: 19266 RVA: 0x000EB581 File Offset: 0x000E9781
		// (set) Token: 0x06004B43 RID: 19267 RVA: 0x000EB589 File Offset: 0x000E9789
		private Panel PopupPanel { get; set; }

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x06004B44 RID: 19268 RVA: 0x000EB592 File Offset: 0x000E9792
		// (set) Token: 0x06004B45 RID: 19269 RVA: 0x000EB59A File Offset: 0x000E979A
		private Panel TitleBarPanel { get; set; }

		// Token: 0x1700189D RID: 6301
		// (get) Token: 0x06004B46 RID: 19270 RVA: 0x000EB5A3 File Offset: 0x000E97A3
		// (set) Token: 0x06004B47 RID: 19271 RVA: 0x000EB5AB File Offset: 0x000E97AB
		private Panel ContentPanel { get; set; }

		// Token: 0x1700189E RID: 6302
		// (get) Token: 0x06004B48 RID: 19272 RVA: 0x000EB5B4 File Offset: 0x000E97B4
		// (set) Token: 0x06004B49 RID: 19273 RVA: 0x000EB5BC File Offset: 0x000E97BC
		private Panel ActionsPanel { get; set; }

		// Token: 0x1700189F RID: 6303
		// (get) Token: 0x06004B4A RID: 19274 RVA: 0x000EB5C5 File Offset: 0x000E97C5
		// (set) Token: 0x06004B4B RID: 19275 RVA: 0x000EB5CD File Offset: 0x000E97CD
		private Panel SnoozePanel { get; set; }

		// Token: 0x06004B4C RID: 19276 RVA: 0x000EB5D6 File Offset: 0x000E97D6
		public Renderer(ReminderDialog owner) : base(owner)
		{
		}

		// Token: 0x06004B4D RID: 19277 RVA: 0x000EB5E0 File Offset: 0x000E97E0
		public override void CreateLayout(Control container)
		{
			this.PopupPanel = new Panel
			{
				CssClass = "rsRemListPanel"
			};
			container.Controls.Add(this.PopupPanel);
			this.TitleBarPanel = new Panel
			{
				CssClass = "rsTitle"
			};
			this.PopupPanel.Controls.Add(this.TitleBarPanel);
			this.ContentPanel = new Panel
			{
				CssClass = "rsBody"
			};
			this.PopupPanel.Controls.Add(this.ContentPanel);
			this.ActionsPanel = new Panel
			{
				CssClass = "rsButtons"
			};
			this.PopupPanel.Controls.Add(this.ActionsPanel);
			this.SnoozePanel = new Panel
			{
				CssClass = "rsRemSnoozePanel"
			};
			this.SnoozePanel.Style.Add(HtmlTextWriterStyle.Display, "none");
			container.Controls.Add(this.SnoozePanel);
		}

		// Token: 0x06004B4E RID: 19278 RVA: 0x000EB6E4 File Offset: 0x000E98E4
		public override void CreateControls()
		{
			WebControl child = IconHelper.CreateIcon("reminder");
			this.TitleBarPanel.Controls.Add(child);
			WebControl child2 = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "rsRemTitleBarText"
			};
			this.TitleBarPanel.Controls.Add(child2);
			WebControl child3 = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rsRemTitleSubject"
			};
			this.ContentPanel.Controls.Add(child3);
			WebControl child4 = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rsRemTitleDate"
			};
			this.ContentPanel.Controls.Add(child4);
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rsRemListWrap"
			};
			this.ContentPanel.Controls.Add(webControl);
			string cssClass = string.Format("{0} {1}", "rsRemList", "rsList");
			WebControl child5 = new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = cssClass
			};
			webControl.Controls.Add(child5);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Button)
			{
				CssClass = "rsRemDismissBtn rsButton"
			};
			webControl2.Controls.Add(new LiteralControl(base.Localization.Dismiss));
			webControl2.Attributes.Add("type", "button");
			this.ActionsPanel.Controls.Add(webControl2);
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Button)
			{
				CssClass = "rsRemSnoozeBtn rsButton"
			};
			webControl3.Controls.Add(new LiteralControl(base.Localization.Snooze));
			webControl3.Attributes.Add("type", "button");
			this.ActionsPanel.Controls.Add(webControl3);
			string cssClass2 = string.Format("{0} {1}", "rsRemSnoozePanelTitle", "rsTitle");
			WebControl webControl4 = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = cssClass2
			};
			this.SnoozePanel.Controls.Add(webControl4);
			webControl4.Controls.Add(new LiteralControl(base.Localization.Snooze));
			WebControl child6 = this.CreateSnoozeList();
			this.SnoozePanel.Controls.Add(child6);
		}

		// Token: 0x06004B4F RID: 19279 RVA: 0x000EB91C File Offset: 0x000E9B1C
		private WebControl CreateSnoozeList()
		{
			string cssClass = string.Format("{0} {1}", "rsRemSnoozePanelList", "rsList");
			WebControl webControl = new WebControl(HtmlTextWriterTag.Ul)
			{
				CssClass = cssClass
			};
			Pair[] snoozeOptions = base.GetSnoozeOptions();
			foreach (Pair pair in snoozeOptions)
			{
				WebControl webControl2 = new WebControl(HtmlTextWriterTag.Li)
				{
					CssClass = "rsLi"
				};
				webControl2.Attributes.Add("value", pair.Second.ToString());
				webControl2.Controls.Add(new LiteralControl(pair.First.ToString()));
				webControl.Controls.Add(webControl2);
			}
			return webControl;
		}
	}
}
