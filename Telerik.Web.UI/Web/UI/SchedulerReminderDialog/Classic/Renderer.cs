using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerReminderDialog.Classic
{
	// Token: 0x02000807 RID: 2055
	internal class Renderer : RendererBase
	{
		// Token: 0x1700188F RID: 6287
		// (get) Token: 0x06004B1F RID: 19231 RVA: 0x000EAA10 File Offset: 0x000E8C10
		// (set) Token: 0x06004B20 RID: 19232 RVA: 0x000EAA18 File Offset: 0x000E8C18
		private Panel TitlePanel { get; set; }

		// Token: 0x17001890 RID: 6288
		// (get) Token: 0x06004B21 RID: 19233 RVA: 0x000EAA21 File Offset: 0x000E8C21
		// (set) Token: 0x06004B22 RID: 19234 RVA: 0x000EAA29 File Offset: 0x000E8C29
		private Panel ContentPanel { get; set; }

		// Token: 0x17001891 RID: 6289
		// (get) Token: 0x06004B23 RID: 19235 RVA: 0x000EAA32 File Offset: 0x000E8C32
		// (set) Token: 0x06004B24 RID: 19236 RVA: 0x000EAA3A File Offset: 0x000E8C3A
		private Panel RemindersListPanel { get; set; }

		// Token: 0x17001892 RID: 6290
		// (get) Token: 0x06004B25 RID: 19237 RVA: 0x000EAA43 File Offset: 0x000E8C43
		// (set) Token: 0x06004B26 RID: 19238 RVA: 0x000EAA4B File Offset: 0x000E8C4B
		private Panel ActionsPanel { get; set; }

		// Token: 0x17001893 RID: 6291
		// (get) Token: 0x06004B27 RID: 19239 RVA: 0x000EAA54 File Offset: 0x000E8C54
		// (set) Token: 0x06004B28 RID: 19240 RVA: 0x000EAA5C File Offset: 0x000E8C5C
		private Panel SnoozePanel { get; set; }

		// Token: 0x17001894 RID: 6292
		// (get) Token: 0x06004B29 RID: 19241 RVA: 0x000EAA65 File Offset: 0x000E8C65
		// (set) Token: 0x06004B2A RID: 19242 RVA: 0x000EAA6D File Offset: 0x000E8C6D
		private Panel TitleBar { get; set; }

		// Token: 0x06004B2B RID: 19243 RVA: 0x000EAA76 File Offset: 0x000E8C76
		public Renderer(ReminderDialog owner) : base(owner)
		{
		}

		// Token: 0x06004B2C RID: 19244 RVA: 0x000EAA80 File Offset: 0x000E8C80
		public override void CreateLayout(Control container)
		{
			this.TitleBar = new Panel
			{
				CssClass = "rsRemTitleBar"
			};
			container.Controls.Add(this.TitleBar);
			this.ContentPanel = new Panel
			{
				CssClass = "rsRemContentPanel"
			};
			container.Controls.Add(this.ContentPanel);
			this.TitlePanel = new Panel
			{
				CssClass = "rsRemTitle"
			};
			this.ContentPanel.Controls.Add(this.TitlePanel);
			this.RemindersListPanel = new Panel
			{
				CssClass = "rsRemList"
			};
			this.ContentPanel.Controls.Add(this.RemindersListPanel);
			this.ActionsPanel = new Panel
			{
				CssClass = "rsRemActions"
			};
			this.ContentPanel.Controls.Add(this.ActionsPanel);
			this.SnoozePanel = new Panel
			{
				CssClass = "rsRemSnoozePanel"
			};
			this.ContentPanel.Controls.Add(this.SnoozePanel);
			Panel child = new Panel
			{
				CssClass = "rsModalBgTopLeft"
			};
			Panel child2 = new Panel
			{
				CssClass = "rsModalBgTopRight"
			};
			Panel child3 = new Panel
			{
				CssClass = "rsModalBgBottomLeft"
			};
			Panel child4 = new Panel
			{
				CssClass = "rsModalBgBottomRight"
			};
			container.Controls.Add(child);
			container.Controls.Add(child2);
			container.Controls.Add(child3);
			container.Controls.Add(child4);
		}

		// Token: 0x06004B2D RID: 19245 RVA: 0x000EAC34 File Offset: 0x000E8E34
		public override void CreateControls()
		{
			WebControl child = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "rsRemTitleBarIcon"
			};
			this.TitleBar.Controls.Add(child);
			WebControl child2 = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rsRemTitleBarText"
			};
			this.TitleBar.Controls.Add(child2);
			LinkButton child3 = new LinkButton
			{
				CssClass = "rsRemTitleBarCloseBtn",
				ToolTip = base.Localization.Close
			};
			this.TitleBar.Controls.Add(child3);
			WebControl child4 = new WebControl(HtmlTextWriterTag.Span)
			{
				CssClass = "rsRemTitleIcon"
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
			LinkButton child8 = new LinkButton
			{
				CssClass = "rsRemDismissAllBtn",
				Text = base.Localization.DismissAll
			};
			this.ActionsPanel.Controls.Add(child8);
			LinkButton child9 = new LinkButton
			{
				CssClass = "rsRemDismissBtn",
				Text = base.Localization.Dismiss
			};
			this.ActionsPanel.Controls.Add(child9);
			LinkButton child10 = new LinkButton
			{
				CssClass = "rsRemOpenItemBtn",
				Text = base.Localization.OpenItem
			};
			this.ActionsPanel.Controls.Add(child10);
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div)
			{
				CssClass = "rsRemSnoozeLabel"
			};
			webControl.Controls.Add(new LiteralControl(base.Localization.SnoozeHint));
			this.SnoozePanel.Controls.Add(webControl);
			LinkButton child11 = new LinkButton
			{
				CssClass = "rsRemSnoozeBtn",
				Text = base.Localization.Snooze
			};
			this.SnoozePanel.Controls.Add(child11);
			RadComboBox child12 = this.CreateSnoozeDropDown();
			this.SnoozePanel.Controls.Add(child12);
		}

		// Token: 0x06004B2E RID: 19246 RVA: 0x000EAECC File Offset: 0x000E90CC
		private RadComboBox CreateSnoozeDropDown()
		{
			RadComboBox radComboBox = this.CreateComboBox("SnoozeTime");
			Pair[] snoozeOptions = base.GetSnoozeOptions();
			radComboBox.Width = Unit.Pixel(345);
			radComboBox.Height = Unit.Pixel(200);
			foreach (Pair pair in snoozeOptions)
			{
				radComboBox.Items.Add(new RadComboBoxItem(pair.First.ToString(), pair.Second.ToString()));
			}
			return radComboBox;
		}

		// Token: 0x06004B2F RID: 19247 RVA: 0x000EAF4C File Offset: 0x000E914C
		private RadComboBox CreateComboBox(string id)
		{
			RadComboBox radComboBox = new RadComboBox
			{
				ID = id,
				EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins,
				EnableEmbeddedScripts = base.Owner.EnableEmbeddedScripts,
				RenderMode = base.Owner.RenderMode
			};
			if (radComboBox.RuntimeSkin != base.Owner.RuntimeSkin)
			{
				radComboBox.Skin = base.Owner.RuntimeSkin;
			}
			return radComboBox;
		}
	}
}
