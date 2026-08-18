using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Calendar;

namespace Telerik.Web.UI
{
	// Token: 0x020007E3 RID: 2019
	internal class HeaderControl : HeaderControlBase
	{
		// Token: 0x0600463E RID: 17982 RVA: 0x000DC86C File Offset: 0x000DAA6C
		public HeaderControl(string dateLabel, RadScheduler owner) : base(dateLabel, owner)
		{
		}

		// Token: 0x0600463F RID: 17983 RVA: 0x000DC878 File Offset: 0x000DAA78
		protected override void CreateHeaderControl(string dateLabel)
		{
			this.CssClass = "rsHeader";
			if (base.Owner.ShowNavigationPane)
			{
				this.AddNavigationLinks();
				if (base.Owner.EnableDatePicker && !base.Owner.DesignMode)
				{
					this.AddDatePicker();
				}
			}
			if (base.Owner.ShowViewTabs)
			{
				this.AddViewTabs(base.GetTabItems());
			}
			this.AddDate(dateLabel);
		}

		// Token: 0x06004640 RID: 17984 RVA: 0x000DC8E4 File Offset: 0x000DAAE4
		private void AddDatePicker()
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("a");
			this.Controls.Add(htmlGenericControl);
			htmlGenericControl.Attributes["class"] = "rsDatePickerActivator";
			htmlGenericControl.Attributes["href"] = "#";
			htmlGenericControl.InnerHtml = "Select date";
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			this.Controls.Add(webControl);
			webControl.CssClass = "rsDatePickerWrapper";
			WebControl webControl2 = new WebControl(HtmlTextWriterTag.Div);
			webControl.Controls.Add(webControl2);
			webControl2.CssClass = "rsDatePickerAnimationWrapper";
			RadCalendar child = this.CreateCalendar();
			webControl2.Controls.Add(child);
		}

		// Token: 0x06004641 RID: 17985 RVA: 0x000DC990 File Offset: 0x000DAB90
		protected virtual RadCalendar CreateCalendar()
		{
			RadCalendar radCalendar = new RadCalendar();
			radCalendar.ID = "SelectedDateCalendar";
			radCalendar.AutoPostBack = true;
			radCalendar.SelectionChanged += this.datePicker_SelectedDateChanged;
			if (radCalendar.RuntimeSkin != base.Owner.RuntimeSkinInternal)
			{
				radCalendar.Skin = base.Owner.RuntimeSkinInternal;
			}
			radCalendar.EnableEmbeddedBaseStylesheet = base.Owner.EnableEmbeddedBaseStylesheet;
			radCalendar.EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins;
			radCalendar.EnableEmbeddedScripts = base.Owner.EnableEmbeddedScripts;
			radCalendar.RenderMode = base.Owner.ResolvedRenderMode;
			radCalendar.CultureInfo = base.Owner.Culture;
			radCalendar.SelectedDate = base.Owner.SelectedDate;
			radCalendar.FocusedDate = base.Owner.SelectedDate;
			radCalendar.EnableMultiSelect = false;
			radCalendar.CssClass = "rsDatePickerCalendar";
			radCalendar.ShowRowHeaders = false;
			radCalendar.UseColumnHeadersAsSelectors = false;
			radCalendar.MultiViewColumns = 1;
			radCalendar.ShowOtherMonthsDays = false;
			radCalendar.RangeMinDate = new DateTime(1900, 1, 1);
			radCalendar.FastNavigationSettings.OkButtonCaption = base.Owner.Localization.AdvancedCalendarOK;
			radCalendar.FastNavigationSettings.CancelButtonCaption = base.Owner.Localization.AdvancedCalendarCancel;
			radCalendar.FastNavigationSettings.TodayButtonCaption = base.Owner.Localization.AdvancedCalendarToday;
			return radCalendar;
		}

		// Token: 0x06004642 RID: 17986 RVA: 0x000DCAFC File Offset: 0x000DACFC
		private void datePicker_SelectedDateChanged(object sender, SelectedDatesEventArgs e)
		{
			if (e.SelectedDates.Count == 0)
			{
				return;
			}
			DateTime date = e.SelectedDates[0].Date;
			if (base.Owner.OnSchedulerNavigationCommand(SchedulerNavigationCommand.NavigateToSelectedDate, date))
			{
				base.Owner.SelectedDate = date;
				base.Owner.Rebind();
				base.Owner.OnSchedulerNavigationComplete(SchedulerNavigationCommand.NavigateToSelectedDate);
			}
		}

		// Token: 0x06004643 RID: 17987 RVA: 0x000DCB64 File Offset: 0x000DAD64
		private void AddViewTabs(List<HeaderControlBase.TabItem> tabItems)
		{
			Control control = new WebControl(HtmlTextWriterTag.Ul);
			foreach (HeaderControlBase.TabItem tabItem in tabItems)
			{
				WebControl webControl = new WebControl(HtmlTextWriterTag.Li);
				if (tabItem.Selected)
				{
					webControl.CssClass = "rsSelected";
					this.AddLabel(webControl, tabItem.InvariantTitle, tabItem.Title);
				}
				else
				{
					this.AddLink(webControl, tabItem.InvariantTitle, tabItem.Title);
				}
				control.Controls.Add(webControl);
			}
			if (control.Controls.Count > 0)
			{
				WebControl webControl2 = (WebControl)control.Controls[0];
				webControl2.CssClass += " rsFirst";
				WebControl webControl3 = (WebControl)control.Controls[control.Controls.Count - 1];
				webControl3.CssClass += " rsLast";
				this.Controls.Add(control);
			}
		}

		// Token: 0x06004644 RID: 17988 RVA: 0x000DCC74 File Offset: 0x000DAE74
		private void AddLink(Control container, string cssText, string text)
		{
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("a");
			htmlGenericControl.Attributes["href"] = "#";
			htmlGenericControl.InnerHtml = string.Format("<span>{0}</span>", text);
			htmlGenericControl.Attributes["class"] = "rsHeader" + cssText;
			container.Controls.Add(htmlGenericControl);
		}

		// Token: 0x06004645 RID: 17989 RVA: 0x000DCCDC File Offset: 0x000DAEDC
		private void AddLabel(Control container, string cssText, string text)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Em);
			webControl.Controls.Add(new LiteralControl(text));
			webControl.CssClass = "rsHeader" + cssText;
			container.Controls.Add(webControl);
		}

		// Token: 0x06004646 RID: 17990 RVA: 0x000DCD20 File Offset: 0x000DAF20
		private void AddDate(string dateLabel)
		{
			Control control = new WebControl(HtmlTextWriterTag.H2);
			this.Controls.Add(control);
			LiteralControl literalControl = new LiteralControl(dateLabel);
			literalControl.ID = "DateLabel";
			control.Controls.Add(literalControl);
		}

		// Token: 0x06004647 RID: 17991 RVA: 0x000DCD60 File Offset: 0x000DAF60
		private void AddNavigationLinks()
		{
			Control control = new WebControl(HtmlTextWriterTag.P);
			HtmlGenericControl htmlGenericControl = new HtmlGenericControl("a");
			control.Controls.Add(htmlGenericControl);
			htmlGenericControl.Attributes["class"] = "rsPrevDay";
			htmlGenericControl.Attributes["href"] = "#";
			htmlGenericControl.InnerText = base.Owner.Localization.HeaderPrevDay;
			HtmlGenericControl htmlGenericControl2 = new HtmlGenericControl("a");
			control.Controls.Add(htmlGenericControl2);
			htmlGenericControl2.Attributes["class"] = "rsNextDay";
			htmlGenericControl2.Attributes["href"] = "#";
			htmlGenericControl2.InnerText = base.Owner.Localization.HeaderNextDay;
			this.Controls.Add(control);
			Control control2 = new WebControl(HtmlTextWriterTag.Em);
			control.Controls.Add(control2);
			HtmlGenericControl htmlGenericControl3 = new HtmlGenericControl("a");
			control2.Controls.Add(htmlGenericControl3);
			htmlGenericControl3.Attributes["class"] = "rsToday";
			htmlGenericControl3.InnerText = base.Owner.Localization.HeaderToday;
			htmlGenericControl3.Attributes["href"] = "#";
		}
	}
}
