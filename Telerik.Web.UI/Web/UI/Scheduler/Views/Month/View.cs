using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.Scheduler.Views.Month
{
	// Token: 0x02001A57 RID: 6743
	internal class View : ViewBase
	{
		// Token: 0x17004F64 RID: 20324
		// (get) Token: 0x060105AB RID: 66987 RVA: 0x003A6F84 File Offset: 0x003A5184
		public string CssClass
		{
			get
			{
				return "rsMonthView";
			}
		}

		// Token: 0x17004F65 RID: 20325
		// (get) Token: 0x060105AC RID: 66988 RVA: 0x003A6F8B File Offset: 0x003A518B
		// (set) Token: 0x060105AD RID: 66989 RVA: 0x003A6F93 File Offset: 0x003A5193
		public override ISchedulerModel Model
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

		// Token: 0x17004F66 RID: 20326
		// (get) Token: 0x060105AE RID: 66990 RVA: 0x003A6F9C File Offset: 0x003A519C
		public override RadScheduler Owner
		{
			get
			{
				return this.Model.Owner as RadScheduler;
			}
		}

		// Token: 0x060105AF RID: 66991 RVA: 0x003A6FAE File Offset: 0x003A51AE
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public View(ModelBase model)
		{
			this.Model = model;
		}

		// Token: 0x060105B0 RID: 66992 RVA: 0x003A6FC0 File Offset: 0x003A51C0
		protected override void InitializeColumnHeaders()
		{
			if (this.Owner.MonthView.ShowDateHeadersResolved)
			{
				foreach (ViewHeader item in this.CreateDateHeaders())
				{
					base.ColumnHeaders.Add(item);
				}
			}
		}

		// Token: 0x060105B1 RID: 66993 RVA: 0x003A7024 File Offset: 0x003A5224
		protected IList<ViewHeader> CreateDateHeaders()
		{
			List<ViewHeader> list = new List<ViewHeader>();
			DateTime visualToday = this.Owner.VisualToday;
			DateTime selectedDate = this.Model.SelectedDate;
			DateTime dateTime = this.Owner.UtcToDisplay(this.Model.VisibleRangeStart);
			for (int i = 0; i < this.Owner.WeekLength; i++)
			{
				ViewHeader viewHeader = new ViewHeader();
				viewHeader.Text = dateTime.ToString(this.Owner.MonthView.ColumnHeaderDateFormat, this.Owner.Culture);
				list.Add(viewHeader);
				if (this.Owner.UtcToDisplay(this.Owner.UtcDayStart(this.Model.VisibleRangeStart).AddDays((double)i)).DayOfWeek == visualToday.DayOfWeek && selectedDate.Month == visualToday.Month)
				{
					viewHeader.ClassName = "rsTodayCol";
				}
				dateTime = dateTime.AddDays(1.0);
			}
			ViewHeader viewHeader2 = list[list.Count - 1];
			viewHeader2.ClassName += " rsLastCell";
			return list;
		}

		// Token: 0x060105B2 RID: 66994 RVA: 0x003A714E File Offset: 0x003A534E
		protected override void InitializeRowHeaders()
		{
		}

		// Token: 0x04004988 RID: 18824
		private ISchedulerModel _model;
	}
}
