using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Month.GroupedByResource
{
	// Token: 0x02001A78 RID: 6776
	internal class Renderer : RendererBase
	{
		// Token: 0x17004FBD RID: 20413
		// (get) Token: 0x060106AE RID: 67246 RVA: 0x003AB210 File Offset: 0x003A9410
		public new Model Model
		{
			get
			{
				return this.View.Model as Model;
			}
		}

		// Token: 0x060106AF RID: 67247 RVA: 0x003AB222 File Offset: 0x003A9422
		public Renderer(ISchedulerView view) : base(view, view.Model as ModelBase)
		{
		}

		// Token: 0x17004FBE RID: 20414
		// (get) Token: 0x060106B0 RID: 67248 RVA: 0x003AB236 File Offset: 0x003A9436
		protected override string ContentPanelCssClass
		{
			get
			{
				return "rs" + this.Owner.MonthView.GroupingDirectionResolved;
			}
		}

		// Token: 0x060106B1 RID: 67249 RVA: 0x003AB258 File Offset: 0x003A9458
		public override Control GetInnerContent()
		{
			Control control = new Control();
			SchedulerTopTable schedulerTopTable = SchedulerRenderer.CreateTopTable(control, this.Model.CssClass);
			if (this.Owner.MonthView.ShowResourceHeadersResolved || this.Owner.MonthView.ShowDateHeadersResolved)
			{
				base.AddHorizontalHeaders(schedulerTopTable);
			}
			if (this.Owner.MonthView.GroupingDirectionResolved == GroupingDirection.Horizontal)
			{
				schedulerTopTable.ShowRowHeaders = false;
				this.CreateHorizontalContent(schedulerTopTable.ContentScrollArea);
			}
			else
			{
				this.CreateVerticalContent(schedulerTopTable);
			}
			this.SetScrollAreaOverflow(schedulerTopTable);
			return control.Controls[0];
		}

		// Token: 0x060106B2 RID: 67250 RVA: 0x003AB2EA File Offset: 0x003A94EA
		protected override void AddContentCells(Control row)
		{
			this.AddHorizontalContentCells(row);
		}

		// Token: 0x060106B3 RID: 67251 RVA: 0x003AB2F4 File Offset: 0x003A94F4
		protected override void CreateColumnHeader(Control container)
		{
			SchedulerColumnHeaderPanel child = new SchedulerColumnHeaderPanel(this.Owner, this.View, this.Owner.MonthView.GroupingDirectionResolved, "rs" + this.Owner.MonthView.GroupingDirectionResolved);
			container.Controls.Add(child);
		}

		// Token: 0x060106B4 RID: 67252 RVA: 0x003AB350 File Offset: 0x003A9550
		private void CreateHorizontalContent(Control container)
		{
			this.CreateContent(container);
			SchedulerContentPanel schedulerContentPanel = container.Controls[container.Controls.Count - 1] as SchedulerContentPanel;
			if (schedulerContentPanel != null)
			{
				schedulerContentPanel.CssClass = schedulerContentPanel.CssClass.Replace("rsContent", string.Empty);
			}
		}

		// Token: 0x060106B5 RID: 67253 RVA: 0x003AB3A0 File Offset: 0x003A95A0
		private void CreateVerticalContent(SchedulerTopTable topTable)
		{
			ContentTable contentTable = null;
			foreach (Model model in this.Model.MonthModels)
			{
				ContentTable contentTable2 = this.CreateInnerContentTable(topTable.ContentScrollArea, model);
				if (contentTable != null)
				{
					contentTable2.SyncRowHeight(contentTable);
				}
				contentTable = contentTable2;
			}
			int num = (contentTable != null) ? contentTable.GetMaxRowHeight(0) : 0;
			int totalBorderHeight = this.Model.NumberOfWeeks * this.Model.MonthModels.Count;
			base.AddVerticalHeaders(topTable, this.Model.NumberOfWeeks * num * this.Model.MonthModels.Count, totalBorderHeight);
		}

		// Token: 0x060106B6 RID: 67254 RVA: 0x003AB460 File Offset: 0x003A9660
		private ContentTable CreateInnerContentTable(Control container, ModelBase model)
		{
			ContentTable contentTable = new ContentTable();
			container.Controls.Add(contentTable);
			this.PopulateInnerContentTable(contentTable, model);
			this.SetContentTableWidth(contentTable);
			return contentTable;
		}

		// Token: 0x060106B7 RID: 67255 RVA: 0x003AB490 File Offset: 0x003A9690
		private void AddHorizontalContentCells(Control row)
		{
			ContentTable contentTable = null;
			foreach (Model model in this.Model.MonthModels)
			{
				TableCell tableCell = new TableCell();
				tableCell.VerticalAlign = VerticalAlign.Top;
				tableCell.CssClass = "rsContentContainerCell";
				row.Controls.Add(tableCell);
				ContentTable contentTable2 = this.CreateInnerContentTable(tableCell, model);
				if (contentTable != null)
				{
					contentTable2.SyncRowHeight(contentTable);
				}
				base.ApplyContentTableCellStyles(contentTable2);
				contentTable = contentTable2;
			}
			if (this.Owner.UseHorizontalScrolling && contentTable != null)
			{
				Unit unit = Unit.Parse(contentTable.Style[HtmlTextWriterStyle.Width]);
				if (unit.Type == UnitType.Pixel)
				{
					unit = Unit.Pixel((int)unit.Value * this.Model.MonthModels.Count);
					base.ContentPanel.ContentTable.Style[HtmlTextWriterStyle.Width] = unit.ToString();
				}
			}
		}
	}
}
