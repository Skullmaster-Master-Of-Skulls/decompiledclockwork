using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Year.GroupedByResource
{
	// Token: 0x02000851 RID: 2129
	internal class Renderer : RendererBase
	{
		// Token: 0x170019B7 RID: 6583
		// (get) Token: 0x06004E9F RID: 20127 RVA: 0x000F674A File Offset: 0x000F494A
		public new Model Model
		{
			get
			{
				return this.View.Model as Model;
			}
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x000F675C File Offset: 0x000F495C
		public Renderer(ISchedulerView view) : base(view, view.Model as ModelBase)
		{
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x000F6788 File Offset: 0x000F4988
		public override Control GetInnerContent()
		{
			Control control = new Control();
			SchedulerTopTable schedulerTopTable = SchedulerRenderer.CreateTopTable(control, this.Model.CssClass);
			if (this.Owner.YearView.GroupingDirectionResolved == GroupingDirection.Horizontal)
			{
				if (this.Owner.YearView.ShowResourceHeadersResolved)
				{
					base.AddHorizontalHeaders(schedulerTopTable);
				}
				schedulerTopTable.ShowRowHeaders = false;
				this.CreateHorizontalContent(schedulerTopTable.ContentScrollArea);
			}
			else
			{
				if (this.Owner.YearView.ShowResourceHeadersResolved)
				{
					this.AddVerticalHeaders(schedulerTopTable, this.Model.YearModels.Count);
				}
				this.CreateVerticalContent(schedulerTopTable.ContentScrollArea);
			}
			this.SetScrollAreaOverflow(schedulerTopTable);
			return control.Controls[0];
		}

		// Token: 0x06004EA2 RID: 20130 RVA: 0x000F6838 File Offset: 0x000F4A38
		protected void AddVerticalHeaders(SchedulerTopTable topTable, int totalRowCount)
		{
			foreach (ViewHeader viewHeader in this.View.RowHeaders)
			{
				SchedulerHeader schedulerHeader = this.CreateSchedulerHeader(viewHeader);
				schedulerHeader.CssClass = viewHeader.ClassName;
				topTable.VerticalHeaderPanel.AddHeader(schedulerHeader);
			}
			double num = this.Owner.RowHeight.Value * (double)this.MonthRowsCount + (double)this.MonthPadding;
			if (this.Owner.YearView.ShowMonthHeaders)
			{
				num += this.HorizontalHeaderHeight.Value;
			}
			if (this.Owner.YearView.ShowDateHeadersResolved)
			{
				num += this.HorizontalHeaderHeight.Value;
			}
			int count = this.Model.YearModels.Count;
			double value = num * (double)this.Model.YearModels.Count + (double)count;
			topTable.VerticalHeaderPanel.InnerTable.Style[HtmlTextWriterStyle.Height] = SchedulerUnit.GetValue(value, this.Owner.RowHeight.Type);
		}

		// Token: 0x06004EA3 RID: 20131 RVA: 0x000F6970 File Offset: 0x000F4B70
		private void CreateHorizontalContent(Control container)
		{
			SchedulerTable schedulerTable = this.CreateContentTable(container);
			TableRow tableRow = new TableRow();
			schedulerTable.Rows.Add(tableRow);
			for (int i = 0; i < this.Model.YearModels.Count; i++)
			{
				Model model = this.Model.YearModels[i];
				TableCell tableCell = new TableCell();
				tableRow.Controls.Add(tableCell);
				base.AddMonths(tableCell, model);
			}
			this.SetContentTableWidth(schedulerTable);
		}

		// Token: 0x06004EA4 RID: 20132 RVA: 0x000F69EC File Offset: 0x000F4BEC
		private void CreateVerticalContent(Control container)
		{
			SchedulerTable schedulerTable = this.CreateContentTable(container);
			for (int i = 0; i < this.Model.YearModels.Count; i++)
			{
				Model model = this.Model.YearModels[i];
				TableRow tableRow = new TableRow();
				schedulerTable.Rows.Add(tableRow);
				TableCell tableCell = new TableCell();
				tableRow.Controls.Add(tableCell);
				base.AddMonths(tableCell, model);
			}
			string value = string.Empty;
			if (this.Owner.UseHorizontalScrolling)
			{
				double value2 = this.Owner.ColumnWidth.Value * (double)this.MonthsCount;
				value = SchedulerUnit.GetValue(value2, this.Owner.ColumnWidth.Type);
			}
			else
			{
				value = Unit.Percentage(300.0).ToString();
			}
			schedulerTable.Style[HtmlTextWriterStyle.Width] = value;
		}

		// Token: 0x06004EA5 RID: 20133 RVA: 0x000F6AE0 File Offset: 0x000F4CE0
		private SchedulerTable CreateContentTable(Control container)
		{
			SchedulerTable schedulerTable = new SchedulerTable();
			container.Controls.Add(schedulerTable);
			this.SetContentTableWidth(schedulerTable);
			return schedulerTable;
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x000F6B07 File Offset: 0x000F4D07
		protected override void SetColumnWidth(WebControl container)
		{
			if (this.Owner.YearView.GroupingDirectionResolved == GroupingDirection.Horizontal)
			{
				return;
			}
			base.SetColumnWidth(container);
		}

		// Token: 0x0400138F RID: 5007
		private readonly int MonthRowsCount = 6;

		// Token: 0x04001390 RID: 5008
		private readonly int MonthPadding = 20;

		// Token: 0x04001391 RID: 5009
		private readonly int MonthsCount = 12;
	}
}
