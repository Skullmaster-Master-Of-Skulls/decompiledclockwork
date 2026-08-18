using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Timeline
{
	// Token: 0x02001A95 RID: 6805
	internal class Renderer : RendererBase
	{
		// Token: 0x17004FF6 RID: 20470
		// (get) Token: 0x06010774 RID: 67444 RVA: 0x003AE305 File Offset: 0x003AC505
		public new Model Model
		{
			get
			{
				return base.Model as Model;
			}
		}

		// Token: 0x06010775 RID: 67445 RVA: 0x003AE312 File Offset: 0x003AC512
		public Renderer(View view) : base(view, view.Model as ModelBase)
		{
		}

		// Token: 0x06010776 RID: 67446 RVA: 0x003AE328 File Offset: 0x003AC528
		public override Control GetInnerContent()
		{
			Control control = new Control();
			SchedulerTopTable schedulerTopTable = SchedulerRenderer.CreateTopTable(control, this.Model.CssClass);
			schedulerTopTable.ShowRowHeaders = false;
			if (this.Owner.TimelineView.ShowDateHeadersResolved)
			{
				base.AddHorizontalHeaders(schedulerTopTable);
			}
			Unit unit = Unit.Empty;
			if (this.Owner.UseHorizontalScrolling)
			{
				unit = new Unit(this.Owner.ColumnWidth.Value * (double)this.Owner.TimelineView.NumberOfSlots, this.Owner.ColumnWidth.Type);
			}
			else if (HttpContext.Current != null)
			{
				HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
				if (!browser.IsBrowser("IE") || browser.MajorVersion >= 8)
				{
					unit = Unit.Percentage(100.0);
				}
			}
			Table table = base.CreateInnerContentTable(schedulerTopTable.ContentScrollArea, this.Model);
			table.Style[HtmlTextWriterStyle.Width] = unit.ToString();
			this.SetScrollAreaOverflow(schedulerTopTable);
			return control.Controls[0];
		}
	}
}
