using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduler.Views.Agenda
{
	// Token: 0x02000833 RID: 2099
	internal abstract class RendererBase : SchedulerRenderer
	{
		// Token: 0x1700196C RID: 6508
		// (get) Token: 0x06004DC9 RID: 19913 RVA: 0x000F4173 File Offset: 0x000F2373
		// (set) Token: 0x06004DCA RID: 19914 RVA: 0x000F417B File Offset: 0x000F237B
		public ModelBase Model
		{
			get
			{
				return this._model;
			}
			protected set
			{
				this._model = value;
			}
		}

		// Token: 0x1700196D RID: 6509
		// (get) Token: 0x06004DCB RID: 19915 RVA: 0x000F4184 File Offset: 0x000F2384
		// (set) Token: 0x06004DCC RID: 19916 RVA: 0x000F418C File Offset: 0x000F238C
		public override ISchedulerView View
		{
			get
			{
				return this._view;
			}
			protected set
			{
				this._view = value;
			}
		}

		// Token: 0x1700196E RID: 6510
		// (get) Token: 0x06004DCD RID: 19917 RVA: 0x000F4195 File Offset: 0x000F2395
		protected override RadScheduler Owner
		{
			get
			{
				return this.Model.Owner as RadScheduler;
			}
		}

		// Token: 0x1700196F RID: 6511
		// (get) Token: 0x06004DCE RID: 19918 RVA: 0x000F41A7 File Offset: 0x000F23A7
		public override bool ShouldRenderFooter
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001970 RID: 6512
		// (get) Token: 0x06004DCF RID: 19919 RVA: 0x000F41AA File Offset: 0x000F23AA
		// (set) Token: 0x06004DD0 RID: 19920 RVA: 0x000F41B2 File Offset: 0x000F23B2
		protected SchedulerContentPanel ContentPanel
		{
			get
			{
				return this._contentPanel;
			}
			set
			{
				this._contentPanel = value;
			}
		}

		// Token: 0x17001971 RID: 6513
		// (get) Token: 0x06004DD1 RID: 19921 RVA: 0x000F41BB File Offset: 0x000F23BB
		protected string ContentTableCssClass
		{
			get
			{
				return "rsAgendaTable";
			}
		}

		// Token: 0x06004DD2 RID: 19922 RVA: 0x000F41C2 File Offset: 0x000F23C2
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected RendererBase(ISchedulerView view, ModelBase model)
		{
			this.View = view;
			this.Model = model;
		}

		// Token: 0x06004DD3 RID: 19923 RVA: 0x000F41D8 File Offset: 0x000F23D8
		protected override void CreateNavigationPane(Control container)
		{
			DateTime dateTime = this.Owner.UtcToDisplay(this.Model.VisibleRangeStart);
			DateTime dateTime2 = this.Owner.UtcToDisplay(this.Model.VisibleRangeEnd.AddDays(-1.0));
			string headerDateFormat = this.Owner.AgendaView.HeaderDateFormat;
			string dateLabel = string.Format("{0} - {1}", dateTime.ToString(headerDateFormat, this.Owner.Culture), dateTime2.ToString(headerDateFormat, this.Owner.Culture));
			container.Controls.Add(base.GetHeaderFactory(dateLabel, this.Owner).CreateHeaderControl());
		}

		// Token: 0x06004DD4 RID: 19924 RVA: 0x000F4284 File Offset: 0x000F2484
		public override Control GetInnerContent()
		{
			Control control = new Control();
			SchedulerTopTable schedulerTopTable = SchedulerRenderer.CreateTopTable(control, this.Model.CssClass);
			schedulerTopTable.ShowRowHeaders = false;
			if (this.Owner.AgendaView.ShowColumnHeaders)
			{
				base.AddColumnHeaders(schedulerTopTable);
			}
			if (this.Owner.AgendaView.GroupingDirectionResolved == GroupingDirection.Horizontal)
			{
				this.CreateHorizontalContent(schedulerTopTable);
			}
			else
			{
				this.CreateVerticalContent(schedulerTopTable);
			}
			this.SetScrollAreaOverflow(schedulerTopTable);
			return control.Controls[0];
		}

		// Token: 0x06004DD5 RID: 19925
		protected abstract void CreateHorizontalContent(SchedulerTopTable topTable);

		// Token: 0x06004DD6 RID: 19926
		protected abstract void CreateVerticalContent(SchedulerTopTable topTable);

		// Token: 0x06004DD7 RID: 19927 RVA: 0x000F4300 File Offset: 0x000F2500
		protected SchedulerTable CreateInnerContentTable(Control container, IList<TimeSlot> slotLists)
		{
			SchedulerTable schedulerTable = new SchedulerTable();
			schedulerTable.CssClass = this.ContentTableCssClass;
			if (this.Owner.UsingWebServiceBinding)
			{
				this.AddEmptyCell(schedulerTable);
			}
			else
			{
				this.PopulateInnerContentTable(schedulerTable, slotLists);
			}
			container.Controls.Add(schedulerTable);
			return schedulerTable;
		}

		// Token: 0x06004DD8 RID: 19928 RVA: 0x000F434C File Offset: 0x000F254C
		protected virtual void PopulateInnerContentTable(Table contentTable, IList<TimeSlot> slotLists)
		{
			List<TableRow> list = this.CreateViewRows(slotLists);
			foreach (TableRow child in list)
			{
				contentTable.Controls.Add(child);
			}
		}

		// Token: 0x06004DD9 RID: 19929 RVA: 0x000F43A8 File Offset: 0x000F25A8
		protected List<TableRow> CreateViewRows(IList<TimeSlot> daySlots)
		{
			List<TableRow> list = new List<TableRow>();
			AgendaRowBuilder rowBuilder = this.GetRowBuilder(daySlots);
			for (int i = 0; i < rowBuilder.RowCount; i++)
			{
				TableRow tableRow = new TableRow();
				list.Add(tableRow);
				tableRow.CssClass = "rsAgendaRow";
				foreach (Control child in rowBuilder.GetRowContent(i))
				{
					tableRow.Controls.Add(child);
				}
				tableRow.Style[HtmlTextWriterStyle.Height] = this.Owner.RowHeight.ToString();
			}
			return list;
		}

		// Token: 0x06004DDA RID: 19930 RVA: 0x000F446C File Offset: 0x000F266C
		protected virtual AgendaRowBuilder GetRowBuilder(IList<TimeSlot> slots)
		{
			return new AgendaRowBuilder(slots);
		}

		// Token: 0x06004DDB RID: 19931 RVA: 0x000F4474 File Offset: 0x000F2674
		protected void AddEmptyCell(Table table)
		{
			TableCell tableCell = new TableCell();
			TableRow tableRow = new TableRow();
			tableRow.Cells.Add(tableCell);
			tableCell.Style.Add(HtmlTextWriterStyle.Display, "none");
			table.Rows.Add(tableRow);
		}

		// Token: 0x06004DDC RID: 19932 RVA: 0x000F44BC File Offset: 0x000F26BC
		protected void CreateVerticalHeader(Control container, string headerText)
		{
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.CssClass = "rsSubHeader";
			if (this.Owner.UsingWebServiceBinding)
			{
				webControl.Style.Add(HtmlTextWriterStyle.Display, "none");
			}
			webControl.Controls.Add(new LiteralControl(headerText));
			container.Controls.Add(webControl);
		}

		// Token: 0x04001369 RID: 4969
		private ModelBase _model;

		// Token: 0x0400136A RID: 4970
		private ISchedulerView _view;

		// Token: 0x0400136B RID: 4971
		private SchedulerContentPanel _contentPanel;
	}
}
