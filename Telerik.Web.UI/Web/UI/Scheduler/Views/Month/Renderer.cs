using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Month
{
	// Token: 0x02001A7F RID: 6783
	internal class Renderer : RendererBase
	{
		// Token: 0x17004FC6 RID: 20422
		// (get) Token: 0x060106D5 RID: 67285 RVA: 0x003ABBF4 File Offset: 0x003A9DF4
		public new Model Model
		{
			get
			{
				return base.Model as Model;
			}
		}

		// Token: 0x060106D6 RID: 67286 RVA: 0x003ABC01 File Offset: 0x003A9E01
		public Renderer(ISchedulerView view) : base(view, view.Model as ModelBase)
		{
		}

		// Token: 0x060106D7 RID: 67287 RVA: 0x003ABC18 File Offset: 0x003A9E18
		public override Control GetInnerContent()
		{
			Control control = new Control();
			SchedulerTopTable schedulerTopTable = SchedulerRenderer.CreateTopTable(control, this.Model.CssClass);
			schedulerTopTable.ShowRowHeaders = false;
			if (this.Owner.MonthView.ShowDateHeadersResolved)
			{
				base.AddColumnHeaders(schedulerTopTable);
			}
			Table contentTableWidth = this.CreateInnerContentTable(schedulerTopTable.ContentScrollArea);
			this.SetContentTableWidth(contentTableWidth);
			this.SetScrollAreaOverflow(schedulerTopTable);
			return control.Controls[0];
		}

		// Token: 0x060106D8 RID: 67288 RVA: 0x003ABC84 File Offset: 0x003A9E84
		protected override void CreateColumnHeader(Control container)
		{
			SchedulerColumnHeaderPanel child = new SchedulerColumnHeaderPanel(this.Owner, this.View, GroupingDirection.Vertical, "");
			container.Controls.Add(child);
		}

		// Token: 0x060106D9 RID: 67289 RVA: 0x003ABCB8 File Offset: 0x003A9EB8
		private Table CreateInnerContentTable(Control container)
		{
			ContentTable contentTable = new ContentTable();
			container.Controls.Add(contentTable);
			this.PopulateInnerContentTable(contentTable, this.Model);
			return contentTable;
		}
	}
}
