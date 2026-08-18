using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerReminderDialog.Lite
{
	// Token: 0x02000809 RID: 2057
	internal class Renderer : RendererBase
	{
		// Token: 0x17001895 RID: 6293
		// (get) Token: 0x06004B31 RID: 19249 RVA: 0x000EAFC5 File Offset: 0x000E91C5
		// (set) Token: 0x06004B32 RID: 19250 RVA: 0x000EAFCD File Offset: 0x000E91CD
		private Panel TitlePanel { get; set; }

		// Token: 0x17001896 RID: 6294
		// (get) Token: 0x06004B33 RID: 19251 RVA: 0x000EAFD6 File Offset: 0x000E91D6
		// (set) Token: 0x06004B34 RID: 19252 RVA: 0x000EAFDE File Offset: 0x000E91DE
		private Panel BodyPanel { get; set; }

		// Token: 0x17001897 RID: 6295
		// (get) Token: 0x06004B35 RID: 19253 RVA: 0x000EAFE7 File Offset: 0x000E91E7
		// (set) Token: 0x06004B36 RID: 19254 RVA: 0x000EAFEF File Offset: 0x000E91EF
		private Panel RemindersListPanel { get; set; }

		// Token: 0x17001898 RID: 6296
		// (get) Token: 0x06004B37 RID: 19255 RVA: 0x000EAFF8 File Offset: 0x000E91F8
		// (set) Token: 0x06004B38 RID: 19256 RVA: 0x000EB000 File Offset: 0x000E9200
		private Panel ActionsPanel { get; set; }

		// Token: 0x17001899 RID: 6297
		// (get) Token: 0x06004B39 RID: 19257 RVA: 0x000EB009 File Offset: 0x000E9209
		// (set) Token: 0x06004B3A RID: 19258 RVA: 0x000EB011 File Offset: 0x000E9211
		private Panel SnoozePanel { get; set; }

		// Token: 0x1700189A RID: 6298
		// (get) Token: 0x06004B3B RID: 19259 RVA: 0x000EB01A File Offset: 0x000E921A
		// (set) Token: 0x06004B3C RID: 19260 RVA: 0x000EB022 File Offset: 0x000E9222
		private Panel TitleBar { get; set; }

		// Token: 0x06004B3D RID: 19261 RVA: 0x000EB02B File Offset: 0x000E922B
		public Renderer(ReminderDialog owner) : base(owner)
		{
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x000EB034 File Offset: 0x000E9234
		public override void CreateLayout(Control container)
		{
			this.TitleBar = new Panel
			{
				CssClass = string.Format("{0} {1}", "rsTitle", "rsRemTitleBar")
			};
			container.Controls.Add(this.TitleBar);
			this.BodyPanel = new Panel
			{
				CssClass = "rsBody"
			};
			container.Controls.Add(this.BodyPanel);
			this.TitlePanel = new Panel
			{
				CssClass = "rsRemTitle"
			};
			this.BodyPanel.Controls.Add(this.TitlePanel);
			this.RemindersListPanel = new Panel
			{
				CssClass = "rsRemList"
			};
			this.BodyPanel.Controls.Add(this.RemindersListPanel);
			this.ActionsPanel = new Panel
			{
				CssClass = "rsRemActions"
			};
			this.BodyPanel.Controls.Add(this.ActionsPanel);
			this.SnoozePanel = new Panel
			{
				CssClass = "rsRemSnoozePanel"
			};
			this.BodyPanel.Controls.Add(this.SnoozePanel);
		}

		// Token: 0x06004B3F RID: 19263 RVA: 0x000EB160 File Offset: 0x000E9360
		public override void CreateControls()
		{
			WebControl child = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = string.Format("{0} {1}", "p-icon", "p-i-notification")
			};
			this.TitleBar.Controls.Add(child);
			WebControl child2 = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "rsRemTitleBarText"
			};
			this.TitleBar.Controls.Add(child2);
			LinkButton linkButton = new LinkButton
			{
				CssClass = "rsRemTitleBarCloseBtn",
				ToolTip = base.Localization.Close
			};
			this.TitleBar.Controls.Add(linkButton);
			WebControl child3 = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = string.Format("{0} {1}", "p-icon", "p-i-close")
			};
			linkButton.Controls.Add(child3);
			WebControl child4 = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = string.Format("{0} {1}", "p-icon", "p-i-calendar")
			};
			this.TitlePanel.Controls.Add(child4);
			WebControl child5 = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "rsRemTitleSubject"
			};
			this.TitlePanel.Controls.Add(child5);
			this.TitlePanel.Controls.Add(new LiteralControl("<br />"));
			WebControl child6 = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "rsRemTitleDate"
			};
			this.TitlePanel.Controls.Add(child6);
			RadListBox child7 = base.CreateRemindersList();
			this.RemindersListPanel.Controls.Add(child7);
			string cssClass = string.Format("{0} {1}", "rsButton", "rsRemDismissAllBtn");
			LinkButton child8 = new LinkButton
			{
				CssClass = cssClass,
				Text = base.Localization.DismissAll
			};
			this.ActionsPanel.Controls.Add(child8);
			string cssClass2 = string.Format("{0} {1}", "rsButton", "rsRemDismissBtn");
			LinkButton child9 = new LinkButton
			{
				CssClass = cssClass2,
				Text = base.Localization.Dismiss
			};
			this.ActionsPanel.Controls.Add(child9);
			string cssClass3 = string.Format("{0} {1}", "rsButton", "rsRemOpenItemBtn");
			LinkButton child10 = new LinkButton
			{
				CssClass = cssClass3,
				Text = base.Localization.OpenItem
			};
			this.ActionsPanel.Controls.Add(child10);
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rsRemSnoozeLabel"
			};
			webControl.Controls.Add(new LiteralControl(base.Localization.SnoozeHint));
			this.SnoozePanel.Controls.Add(webControl);
			string cssClass4 = string.Format("{0} {1}", "rsButton", "rsRemSnoozeBtn");
			LinkButton child11 = new LinkButton
			{
				CssClass = cssClass4,
				Text = base.Localization.Snooze
			};
			this.SnoozePanel.Controls.Add(child11);
			RadDropDownList child12 = this.CreateSnoozeDropDown();
			this.SnoozePanel.Controls.Add(child12);
		}

		// Token: 0x06004B40 RID: 19264 RVA: 0x000EB498 File Offset: 0x000E9698
		private RadDropDownList CreateSnoozeDropDown()
		{
			RadDropDownList radDropDownList = this.CreateDropDownList("SnoozeTime");
			Pair[] snoozeOptions = base.GetSnoozeOptions();
			radDropDownList.DropDownHeight = Unit.Pixel(200);
			foreach (Pair pair in snoozeOptions)
			{
				radDropDownList.Items.Add(new DropDownListItem(pair.First.ToString(), pair.Second.ToString()));
			}
			return radDropDownList;
		}

		// Token: 0x06004B41 RID: 19265 RVA: 0x000EB508 File Offset: 0x000E9708
		private RadDropDownList CreateDropDownList(string id)
		{
			RadDropDownList radDropDownList = new RadDropDownList
			{
				ID = id,
				EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins,
				EnableEmbeddedScripts = base.Owner.EnableEmbeddedScripts,
				RenderMode = base.Owner.ResolvedRenderMode
			};
			if (radDropDownList.RuntimeSkin != base.Owner.RuntimeSkin)
			{
				radDropDownList.Skin = base.Owner.RuntimeSkin;
			}
			return radDropDownList;
		}
	}
}
