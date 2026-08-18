using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Scheduler.Views.Week.GroupedByResource;

namespace Telerik.Web.UI.Scheduler.Views.Week
{
	// Token: 0x02001A70 RID: 6768
	internal abstract class RendererBase : SchedulerRenderer
	{
		// Token: 0x17004FA6 RID: 20390
		// (get) Token: 0x06010666 RID: 67174 RVA: 0x003A9B88 File Offset: 0x003A7D88
		// (set) Token: 0x06010667 RID: 67175 RVA: 0x003A9B90 File Offset: 0x003A7D90
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

		// Token: 0x17004FA7 RID: 20391
		// (get) Token: 0x06010668 RID: 67176 RVA: 0x003A9B99 File Offset: 0x003A7D99
		// (set) Token: 0x06010669 RID: 67177 RVA: 0x003A9BA1 File Offset: 0x003A7DA1
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

		// Token: 0x17004FA8 RID: 20392
		// (get) Token: 0x0601066A RID: 67178 RVA: 0x003A9BAA File Offset: 0x003A7DAA
		public override bool ShouldRenderFooter
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17004FA9 RID: 20393
		// (get) Token: 0x0601066B RID: 67179 RVA: 0x003A9BAD File Offset: 0x003A7DAD
		protected override RadScheduler Owner
		{
			get
			{
				return this.Model.Owner as RadScheduler;
			}
		}

		// Token: 0x17004FAA RID: 20394
		// (get) Token: 0x0601066C RID: 67180 RVA: 0x003A9BBF File Offset: 0x003A7DBF
		protected virtual string ContentTableCssClass
		{
			get
			{
				return "rsContentTable";
			}
		}

		// Token: 0x17004FAB RID: 20395
		// (get) Token: 0x0601066D RID: 67181 RVA: 0x003A9BC6 File Offset: 0x003A7DC6
		protected virtual int MaxColumnWidth
		{
			get
			{
				if (this.Owner.ReadOnly)
				{
					return 100;
				}
				return 90;
			}
		}

		// Token: 0x0601066E RID: 67182 RVA: 0x003A9BDA File Offset: 0x003A7DDA
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		protected RendererBase(ModelBase model, ISchedulerView view)
		{
			this.Model = model;
			this.View = view;
		}

		// Token: 0x0601066F RID: 67183 RVA: 0x003A9BF0 File Offset: 0x003A7DF0
		protected override void CreateNavigationPane(Control container)
		{
			DateTime dateTime = this.Owner.UtcToDisplay(this.Model.VisibleRangeStart);
			DateTime dateTime2 = this.Owner.UtcToDisplay(this.Model.VisibleRangeEnd);
			string headerDateFormat = (this.View as Telerik.Web.UI.Scheduler.Views.Week.View).EffectiveWeekViewSettings.HeaderDateFormat;
			if (dateTime2.TimeOfDay.CompareTo(TimeSpan.Zero) == 0)
			{
				dateTime2 = dateTime2.AddDays(-1.0);
			}
			string dateLabel = string.Format("{0} - {1}", dateTime.Date.ToString(headerDateFormat, this.Owner.Culture), dateTime2.Date.ToString(headerDateFormat, this.Owner.Culture));
			container.Controls.Add(base.GetHeaderFactory(dateLabel, this.Owner).CreateHeaderControl());
		}

		// Token: 0x06010670 RID: 67184 RVA: 0x003A9CCC File Offset: 0x003A7ECC
		protected virtual SchedulerTable CreateInnerContentTable(Control container, IList<IList<TimeSlot>> slotLists)
		{
			SchedulerTable schedulerTable = new SchedulerTable();
			schedulerTable.CssClass = this.ContentTableCssClass;
			List<TableRow> list = this.CreateViewRows(slotLists);
			foreach (TableRow child in list)
			{
				schedulerTable.Controls.Add(child);
			}
			this.SetContentTableWidth(schedulerTable);
			this.SetContentTableHeight(schedulerTable, 0);
			container.Controls.Add(schedulerTable);
			return schedulerTable;
		}

		// Token: 0x06010671 RID: 67185 RVA: 0x003A9D58 File Offset: 0x003A7F58
		protected virtual List<TableRow> CreateViewRows(IList<IList<TimeSlot>> slotLists)
		{
			List<TableRow> list = new List<TableRow>();
			bool renderEmptySpace = this.Owner.ResolvedRenderMode != RenderMode.Lightweight;
			foreach (IList<TimeSlot> slotList in slotLists)
			{
				RowBuilder rowBuilder = new RowBuilder(slotList, this.MaxColumnWidth, renderEmptySpace);
				for (int i = 0; i < rowBuilder.RowCount; i++)
				{
					if (list.Count <= i)
					{
						TableRow tableRow = new TableRow();
						list.Add(tableRow);
						if (this.Owner.ResolvedRenderMode != RenderMode.Lightweight)
						{
							tableRow.Style[HtmlTextWriterStyle.Height] = this.Owner.RowHeight.ToString();
						}
						if ((i + 1) % this.Owner.TimeLabelRowSpan == 0 || i + 1 == rowBuilder.RowCount)
						{
							tableRow.CssClass = (tableRow.CssClass + " rsAlt").Trim();
						}
					}
					foreach (Control child in rowBuilder.GetRowContent(i))
					{
						list[i].Controls.Add(child);
					}
				}
			}
			return list;
		}

		// Token: 0x06010672 RID: 67186 RVA: 0x003A9EE0 File Offset: 0x003A80E0
		protected void SetContentTableHeight(Table contentTable, int extraRowCount = 0)
		{
			if (this.Owner.ResolvedRenderMode != RenderMode.Lightweight)
			{
				return;
			}
			Unit unit = new Unit(this.Owner.RowHeight.Value * (double)(contentTable.Rows.Count + extraRowCount), this.Owner.RowHeight.Type);
			contentTable.Style[HtmlTextWriterStyle.Height] = unit.ToString();
		}

		// Token: 0x06010673 RID: 67187 RVA: 0x003A9F54 File Offset: 0x003A8154
		protected void AddHoursColumn(SchedulerTopTable topTable)
		{
			topTable.ShowRowHeaders = this.Owner.WeekView.ShowHoursColumnResolved;
			if (!topTable.ShowRowHeaders)
			{
				return;
			}
			foreach (ViewHeader viewHeader in this.View.RowHeaders)
			{
				SchedulerHeader schedulerHeader = new SchedulerHeader(new LiteralControl(viewHeader.Text));
				schedulerHeader.CssClass = viewHeader.ClassName;
				if (this.Owner.ResolvedRenderMode != RenderMode.Lightweight)
				{
					schedulerHeader.Style[HtmlTextWriterStyle.Height] = this.Owner.RowHeight.ToString();
				}
				topTable.VerticalHeaderPanel.AddHeader(schedulerHeader);
			}
			this.ResolveVerticalHeaderTableHeight(topTable);
		}

		// Token: 0x06010674 RID: 67188 RVA: 0x003AA024 File Offset: 0x003A8224
		protected void AddAllDayRowContent(SchedulerTopTable topTable)
		{
			topTable.ShowAllDayRow = this.Owner.ShowAllDayRow;
			if (!topTable.ShowAllDayRow)
			{
				return;
			}
			WebControl webControl = new WebControl(HtmlTextWriterTag.Div);
			webControl.Controls.Add(new LiteralControl(this.Owner.Localization.AllDay));
			topTable.AllDayHeaderCell.Controls.Add(webControl);
			this.CreateAllDayContent(topTable.AllDayContentWrapper);
		}

		// Token: 0x06010675 RID: 67189
		protected abstract void CreateAllDayContent(WebControl allDayContentWrapper);

		// Token: 0x06010676 RID: 67190 RVA: 0x003AA090 File Offset: 0x003A8290
		protected void AddVerticalHeaders(SchedulerTopTable topTable)
		{
			foreach (ViewHeader viewHeader in this.View.RowHeaders)
			{
				SchedulerHeader schedulerHeader = this.CreateSchedulerRowHeader(viewHeader);
				if (viewHeader.SubHeadersVisible)
				{
					this.AddSubHeaders(viewHeader, schedulerHeader);
				}
				else if (viewHeader.SubHeaders.Count > 0)
				{
					schedulerHeader.Style[HtmlTextWriterStyle.Height] = new Unit(this.Owner.RowHeight.Value * (double)viewHeader.SubHeaders.Count, this.Owner.RowHeight.Type).ToString();
				}
				topTable.VerticalHeaderPanel.AddHeader(schedulerHeader);
			}
			this.ResolveVerticalHeaderTableHeight(topTable);
		}

		// Token: 0x06010677 RID: 67191 RVA: 0x003AA174 File Offset: 0x003A8374
		private SchedulerHeader CreateSchedulerRowHeader(ViewHeader viewHeader)
		{
			SchedulerHeader schedulerHeader;
			if (viewHeader.Resource != null)
			{
				SchedulerResourceContainer schedulerResourceContainer = new SchedulerResourceContainer(this.Owner);
				schedulerResourceContainer.Resource = viewHeader.Resource;
				viewHeader.Resource.HeaderControls.Add(schedulerResourceContainer);
				schedulerHeader = new SchedulerHeader(schedulerResourceContainer);
				schedulerHeader.CssClass = viewHeader.ClassName;
				this.Owner.ResourceHeaderTemplate.InstantiateIn(schedulerResourceContainer);
			}
			else
			{
				schedulerHeader = new SchedulerHeader(new LiteralControl(viewHeader.Text));
				schedulerHeader.CssClass = viewHeader.ClassName;
				schedulerHeader.InnerHeight = viewHeader.InnerHeight;
				if (this.Owner.ResolvedRenderMode != RenderMode.Lightweight)
				{
					schedulerHeader.Style[HtmlTextWriterStyle.Height] = this.Owner.RowHeight.ToString();
				}
			}
			return schedulerHeader;
		}

		// Token: 0x06010678 RID: 67192 RVA: 0x003AA23C File Offset: 0x003A843C
		private void AddSubHeaders(ViewHeader viewHeader, SchedulerHeader schedulerHeader)
		{
			foreach (ViewHeader viewHeader2 in viewHeader.SubHeaders)
			{
				SchedulerHeader schedulerHeader2 = new SchedulerHeader(new WebControl(HtmlTextWriterTag.Div)
				{
					Controls = 
					{
						new LiteralControl(viewHeader2.Text)
					}
				});
				schedulerHeader2.CssClass = viewHeader2.ClassName;
				schedulerHeader2.InnerHeight = viewHeader2.InnerHeight;
				if (this.Owner.ResolvedRenderMode != RenderMode.Lightweight)
				{
					schedulerHeader2.Style[HtmlTextWriterStyle.Height] = this.Owner.RowHeight.ToString();
				}
				schedulerHeader.SubHeaders.Add(schedulerHeader2);
			}
		}

		// Token: 0x06010679 RID: 67193 RVA: 0x003AA308 File Offset: 0x003A8508
		protected static List<ISchedulerTimeSlot> ConvertTimeSlotList(ICollection<TimeSlot> slots)
		{
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>(slots.Count);
			foreach (TimeSlot timeSlot in slots)
			{
				TimeSlot item = (TimeSlot)timeSlot;
				list.Add(item);
			}
			return list;
		}

		// Token: 0x0601067A RID: 67194 RVA: 0x003AA364 File Offset: 0x003A8564
		private void ResolveVerticalHeaderTableHeight(SchedulerTopTable topTable)
		{
			if (this.Owner.ResolvedRenderMode != RenderMode.Lightweight)
			{
				return;
			}
			int num = 0;
			foreach (ViewHeader viewHeader in this.View.RowHeaders)
			{
				if (viewHeader.SubHeaders.Count > 0)
				{
					num += viewHeader.SubHeaders.Count;
				}
				else
				{
					num++;
				}
			}
			string value = SchedulerUnit.GetValue((double)num * this.Owner.RowHeight.Value, this.Owner.RowHeight.Type);
			topTable.VerticalHeaderPanel.InnerTable.Style[HtmlTextWriterStyle.Height] = value;
		}

		// Token: 0x040049A0 RID: 18848
		private ModelBase _model;

		// Token: 0x040049A1 RID: 18849
		private ISchedulerView _view;
	}
}
