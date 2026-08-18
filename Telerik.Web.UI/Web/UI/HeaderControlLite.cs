using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020007E4 RID: 2020
	internal class HeaderControlLite : HeaderControl
	{
		// Token: 0x06004648 RID: 17992 RVA: 0x000DCE9F File Offset: 0x000DB09F
		public HeaderControlLite(string dateLabel, RadScheduler owner) : base(dateLabel, owner)
		{
		}

		// Token: 0x06004649 RID: 17993 RVA: 0x000DCEAC File Offset: 0x000DB0AC
		protected override void CreateHeaderControl(string dateLabel)
		{
			this.CssClass = "rsHeader";
			if (base.Owner.ShowNavigationPane)
			{
				this.AddNavigationLinks();
			}
			if (!base.Owner.DesignMode)
			{
				this.AddDatePicker(dateLabel);
			}
			if (base.Owner.ShowViewTabs)
			{
				this.AddViewTabs(base.GetTabItems());
			}
		}

		// Token: 0x0600464A RID: 17994 RVA: 0x000DCF04 File Offset: 0x000DB104
		private void AddNavigationLinks()
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Ul);
			this.Controls.Add(webControl);
			webControl.CssClass = string.Format("{0} {1}", "rsToolbar", "rsNav");
			webControl.Controls.Add(this.CreateIconLink(base.Owner.Localization.HeaderPrevDay, "rsPrevDay", "p-i-arrow-60-left"));
			webControl.Controls.Add(this.CreateIconLink(base.Owner.Localization.HeaderNextDay, "rsNextDay", "p-i-arrow-60-right"));
			webControl.Controls.Add(this.CreateLink(base.Owner.Localization.HeaderToday, "rsToday"));
		}

		// Token: 0x0600464B RID: 17995 RVA: 0x000DCFBC File Offset: 0x000DB1BC
		private WebControl CreateLink(string text, string cssClass)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Li);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Span);
			webControl2.CssClass = string.Format("{0} {1}", "rsButton", cssClass);
			webControl.Controls.Add(webControl2);
			LiteralControl child = new LiteralControl(text);
			webControl2.Controls.Add(child);
			return webControl;
		}

		// Token: 0x0600464C RID: 17996 RVA: 0x000DD010 File Offset: 0x000DB210
		private WebControl CreateIconLink(string toolTip, string cssClass, string iconClass)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Li);
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Span);
			webControl2.ToolTip = toolTip;
			webControl2.CssClass = string.Format("{0} {1}", "rsButton", cssClass);
			webControl.Controls.Add(webControl2);
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Span);
			webControl3.CssClass = string.Format("{0} {1}", "p-icon", iconClass);
			webControl2.Controls.Add(webControl3);
			return webControl;
		}

		// Token: 0x0600464D RID: 17997 RVA: 0x000DD084 File Offset: 0x000DB284
		private void AddDatePicker(string dateLabel)
		{
			if (base.Owner.EnableDatePicker)
			{
				WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
				this.Controls.Add(webControl);
				webControl.CssClass = "rsDatePickerWrapper";
				WebControl webControl2 = new WebControl(HtmlTextWriterTag.Div);
				webControl.Controls.Add(webControl2);
				webControl2.CssClass = "rsDatePickerAnimationWrapper";
				RadCalendar child = this.CreateCalendar();
				webControl2.Controls.Add(child);
			}
			WebControl webControl3 = new WebControl(HtmlTextWriterTag.Span);
			this.Controls.Add(webControl3);
			webControl3.CssClass = "rsCurrent";
			if (base.Owner.ShowNavigationPane)
			{
				WebControl webControl4 = webControl3;
				webControl4.CssClass += " rsDatePickerActivator";
			}
			if (base.Owner.EnableDatePicker)
			{
				WebControl webControl5 = new WebControl(HtmlTextWriterTag.Span);
				webControl3.Controls.Add(webControl5);
				webControl5.CssClass = string.Format("{0} {1}", "p-icon", "p-i-calendar");
			}
			LiteralControl child2 = new LiteralControl(dateLabel);
			webControl3.Controls.Add(child2);
		}

		// Token: 0x0600464E RID: 17998 RVA: 0x000DD184 File Offset: 0x000DB384
		private void AddViewTabs(List<HeaderControlBase.TabItem> tabItems)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Ul);
			webControl.CssClass = string.Format("{0} {1}", "rsToolbar", "rsViews");
			foreach (HeaderControlBase.TabItem tabItem in tabItems)
			{
				string cssClass = "rsHeader" + tabItem.InvariantTitle;
				WebControl webControl2 = this.CreateLink(tabItem.Title, cssClass);
				if (tabItem.Selected)
				{
					webControl2.CssClass = "rsSelected";
				}
				webControl.Controls.Add(webControl2);
			}
			if (webControl.Controls.Count > 0)
			{
				this.Controls.Add(webControl);
			}
		}
	}
}
