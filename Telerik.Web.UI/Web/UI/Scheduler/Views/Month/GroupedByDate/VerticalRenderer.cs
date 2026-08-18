using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Month.GroupedByDate
{
	// Token: 0x02001A56 RID: 6742
	internal class VerticalRenderer : Renderer
	{
		// Token: 0x060105A8 RID: 66984 RVA: 0x003A6E60 File Offset: 0x003A5060
		public VerticalRenderer(ISchedulerView view) : base(view)
		{
		}

		// Token: 0x060105A9 RID: 66985 RVA: 0x003A6E6C File Offset: 0x003A506C
		protected override void AddContentCells(Control row)
		{
			TableCell tableCell = new TableCell();
			tableCell.VerticalAlign = VerticalAlign.Top;
			tableCell.CssClass = "rsContentContainerCell";
			row.Controls.Add(tableCell);
			SchedulerRowHeaderTable child = new SchedulerRowHeaderTable(this.View, this.Owner);
			tableCell.Controls.Add(child);
			base.AddContentCells(row);
		}

		// Token: 0x060105AA RID: 66986 RVA: 0x003A6EC4 File Offset: 0x003A50C4
		protected override void PopulateInnerContentTable(ContentTable contentTable, ModelBase model)
		{
			for (int i = 0; i < base.Model.WeekLength; i++)
			{
				TableRow tableRow = new TableRow();
				tableRow.CssClass = "rsRow";
				contentTable.Rows.Add(tableRow);
				for (int j = 0; j < model.NumberOfWeeks; j++)
				{
					bool renderDate = true;
					foreach (Model contentModel in base.Model.MonthModels)
					{
						base.AddCell(j, i, tableRow, contentModel, renderDate);
						renderDate = false;
					}
				}
				contentTable.SyncCellHeight(i);
			}
			if (!base.AdaptiveRowHeight)
			{
				contentTable.SyncCellHeight();
			}
		}
	}
}
