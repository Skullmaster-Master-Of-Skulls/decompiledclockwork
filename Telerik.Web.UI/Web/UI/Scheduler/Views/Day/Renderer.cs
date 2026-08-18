using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduler.Views.Week;

namespace Telerik.Web.UI.Scheduler.Views.Day
{
	// Token: 0x02001A74 RID: 6772
	internal class Renderer : Renderer
	{
		// Token: 0x06010694 RID: 67220 RVA: 0x003AACEC File Offset: 0x003A8EEC
		public Renderer(Telerik.Web.UI.Scheduler.Views.Week.View view) : base(view)
		{
		}

		// Token: 0x06010695 RID: 67221 RVA: 0x003AACF8 File Offset: 0x003A8EF8
		public override Control GetInnerContent()
		{
			Control control = new Control();
			SchedulerTopTable schedulerTopTable = SchedulerRenderer.CreateTopTable(control, base.Model.CssClass);
			base.AddHoursColumn(schedulerTopTable);
			base.AddAllDayRowContent(schedulerTopTable);
			this.CreateInnerContentTable(schedulerTopTable.ContentScrollArea, base.Model.DaySlots);
			this.SetScrollAreaOverflow(schedulerTopTable);
			this.SetTopTableStyles(schedulerTopTable);
			return control.Controls[0];
		}

		// Token: 0x06010696 RID: 67222 RVA: 0x003AAD5D File Offset: 0x003A8F5D
		protected override void SetScrollAreaOverflow(SchedulerTopTable topTable)
		{
			base.SetScrollAreaOverflow(topTable);
			topTable.ContentScrollArea.Style[HtmlTextWriterStyle.OverflowX] = "visible";
		}

		// Token: 0x06010697 RID: 67223 RVA: 0x003AAD7D File Offset: 0x003A8F7D
		protected override void SetTopTableStyles(SchedulerTopTable topTable)
		{
			base.SetTopTableStyles(topTable);
			topTable.ContentWrapper.Style[HtmlTextWriterStyle.Width] = "100%";
		}

		// Token: 0x06010698 RID: 67224 RVA: 0x003AADA0 File Offset: 0x003A8FA0
		protected override void SetContentTableWidth(Table contentTable)
		{
			if (HttpContext.Current != null)
			{
				HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
				if (!browser.IsBrowser("IE") || browser.MajorVersion >= 8)
				{
					contentTable.Width = Unit.Percentage(100.0);
				}
			}
		}

		// Token: 0x06010699 RID: 67225 RVA: 0x003AADF0 File Offset: 0x003A8FF0
		protected override void CreateNavigationPane(Control container)
		{
			string dateLabel = string.Format("{0}", this.Owner.UtcToDisplay(base.Model.VisibleRangeStart).Date.ToString(this.Owner.DayView.HeaderDateFormat, this.Owner.Culture));
			container.Controls.Add(base.GetHeaderFactory(dateLabel, this.Owner).CreateHeaderControl());
		}
	}
}
