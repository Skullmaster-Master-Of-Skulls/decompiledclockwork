using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.Scheduler.Views.Year
{
	// Token: 0x0200084C RID: 2124
	internal class View : ViewBase
	{
		// Token: 0x1700199A RID: 6554
		// (get) Token: 0x06004E59 RID: 20057 RVA: 0x000F59F5 File Offset: 0x000F3BF5
		public string CssClass
		{
			get
			{
				return "rsYearView";
			}
		}

		// Token: 0x1700199B RID: 6555
		// (get) Token: 0x06004E5A RID: 20058 RVA: 0x000F59FC File Offset: 0x000F3BFC
		// (set) Token: 0x06004E5B RID: 20059 RVA: 0x000F5A04 File Offset: 0x000F3C04
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

		// Token: 0x1700199C RID: 6556
		// (get) Token: 0x06004E5C RID: 20060 RVA: 0x000F5A0D File Offset: 0x000F3C0D
		// (set) Token: 0x06004E5D RID: 20061 RVA: 0x000F5A2E File Offset: 0x000F3C2E
		public IList<ViewHeader> MonthHeaders
		{
			get
			{
				if (this._monthHeaders == null)
				{
					this._monthHeaders = new List<ViewHeader>();
					this.InitializeMonthHeaders();
				}
				return this._monthHeaders;
			}
			set
			{
				this._monthHeaders = value;
			}
		}

		// Token: 0x1700199D RID: 6557
		// (get) Token: 0x06004E5E RID: 20062 RVA: 0x000F5A37 File Offset: 0x000F3C37
		public Model YearModel
		{
			get
			{
				return (Model)this.Model;
			}
		}

		// Token: 0x1700199E RID: 6558
		// (get) Token: 0x06004E5F RID: 20063 RVA: 0x000F5A44 File Offset: 0x000F3C44
		public override RadScheduler Owner
		{
			get
			{
				return this.Model.Owner as RadScheduler;
			}
		}

		// Token: 0x1700199F RID: 6559
		// (get) Token: 0x06004E60 RID: 20064 RVA: 0x000F5A56 File Offset: 0x000F3C56
		public int MonthHeadersDepth
		{
			get
			{
				return ViewBase.GetHeadersDepth(this.MonthHeaders);
			}
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x000F5A63 File Offset: 0x000F3C63
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public View(ModelBase model)
		{
			this.Model = model;
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x000F5A72 File Offset: 0x000F3C72
		protected override void InitializeColumnHeaders()
		{
		}

		// Token: 0x06004E63 RID: 20067 RVA: 0x000F5A74 File Offset: 0x000F3C74
		protected override void InitializeRowHeaders()
		{
		}

		// Token: 0x06004E64 RID: 20068 RVA: 0x000F5A76 File Offset: 0x000F3C76
		protected virtual void InitializeMonthHeaders()
		{
			if (this.Owner.YearView.ShowMonthHeaders || this.Owner.YearView.ShowDateHeadersResolved)
			{
				this.MonthHeaders = this.CreateDateHeaders(this.YearModel);
			}
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x000F5AB0 File Offset: 0x000F3CB0
		protected IList<ViewHeader> CreateDateHeaders(ModelBase model)
		{
			List<ViewHeader> list = new List<ViewHeader>();
			DateTime dateTime = this.Owner.UtcToDisplay(this.Model.VisibleRangeStart);
			for (int i = 0; i < model.NumberOfMonths; i++)
			{
				ViewHeader viewHeader = new ViewHeader();
				viewHeader.Text = dateTime.ToString(this.Owner.YearView.MonthHeaderDateFormat, this.Owner.Culture);
				viewHeader.ColumnSpan = model.WeekLength;
				viewHeader.Date = new DateTime(dateTime.Ticks);
				list.Add(viewHeader);
				if (this.Owner.YearView.ShowDateHeadersResolved)
				{
					DateTime dateTime2 = new DateTime(dateTime.Ticks);
					int num = dateTime.DayOfWeek - this.Owner.FirstDayOfWeek;
					dateTime2 = dateTime2.AddDays((double)(-(double)num));
					for (int j = 0; j < model.WeekLength; j++)
					{
						ViewHeader viewHeader2 = new ViewHeader();
						viewHeader2.Text = dateTime2.AddDays((double)j).ToString(this.Owner.YearView.ColumnHeaderDateFormat, this.Owner.Culture);
						viewHeader.SubHeaders.Add(viewHeader2);
					}
				}
				dateTime = dateTime.AddMonths(1);
			}
			return list;
		}

		// Token: 0x0400137F RID: 4991
		private ISchedulerModel _model;

		// Token: 0x04001380 RID: 4992
		private IList<ViewHeader> _monthHeaders;
	}
}
