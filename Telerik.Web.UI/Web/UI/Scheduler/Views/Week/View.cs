using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views.Week
{
	// Token: 0x02001A5D RID: 6749
	internal class View : ViewBase
	{
		// Token: 0x17004F7A RID: 20346
		// (get) Token: 0x060105E6 RID: 67046 RVA: 0x003A7B2A File Offset: 0x003A5D2A
		// (set) Token: 0x060105E7 RID: 67047 RVA: 0x003A7B32 File Offset: 0x003A5D32
		public ModelBase WeekModel
		{
			get
			{
				return this._model;
			}
			set
			{
				this._model = value;
			}
		}

		// Token: 0x17004F7B RID: 20347
		// (get) Token: 0x060105E8 RID: 67048 RVA: 0x003A7B3B File Offset: 0x003A5D3B
		internal virtual BaseMultiDayViewSettings EffectiveViewSettings
		{
			get
			{
				return this.Owner.WeekView;
			}
		}

		// Token: 0x17004F7C RID: 20348
		// (get) Token: 0x060105E9 RID: 67049 RVA: 0x003A7B48 File Offset: 0x003A5D48
		public virtual WeekViewSettings EffectiveWeekViewSettings
		{
			get
			{
				return this.Owner.WeekView;
			}
		}

		// Token: 0x17004F7D RID: 20349
		// (get) Token: 0x060105EA RID: 67050 RVA: 0x003A7B55 File Offset: 0x003A5D55
		// (set) Token: 0x060105EB RID: 67051 RVA: 0x003A7B5D File Offset: 0x003A5D5D
		public override ISchedulerModel Model
		{
			get
			{
				return this.WeekModel;
			}
			protected set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17004F7E RID: 20350
		// (get) Token: 0x060105EC RID: 67052 RVA: 0x003A7B64 File Offset: 0x003A5D64
		public override RadScheduler Owner
		{
			get
			{
				return this.Model.Owner as RadScheduler;
			}
		}

		// Token: 0x060105ED RID: 67053 RVA: 0x003A7B76 File Offset: 0x003A5D76
		public View(ModelBase model)
		{
			this.WeekModel = model;
		}

		// Token: 0x060105EE RID: 67054 RVA: 0x003A7B85 File Offset: 0x003A5D85
		protected override void InitializeColumnHeaders()
		{
			if (!this.EffectiveViewSettings.ShowDateHeadersResolved)
			{
				return;
			}
			base.ColumnHeaders = this.CreateDateHeaders();
		}

		// Token: 0x060105EF RID: 67055 RVA: 0x003A7BA4 File Offset: 0x003A5DA4
		protected override void InitializeRowHeaders()
		{
			if (!this.EffectiveViewSettings.ShowHoursColumnResolved)
			{
				return;
			}
			IList<ViewHeader> list = this.CreateTimeLabelHeaders();
			foreach (ViewHeader item in list)
			{
				base.RowHeaders.Add(item);
			}
		}

		// Token: 0x060105F0 RID: 67056 RVA: 0x003A7C08 File Offset: 0x003A5E08
		protected IList<ViewHeader> CreateTimeLabelHeaders()
		{
			IList<ViewHeader> list = new List<ViewHeader>();
			TimeSpan timeSpan = this.EffectiveViewSettings.EffectiveDayStartTime;
			string format = this.Owner.HoursPanelTimeFormat.Replace("tt", "'<span class=\"rsAmPm\">'tt'</span>'");
			while (timeSpan < this.EffectiveViewSettings.EffectiveDayEndTime)
			{
				list.Add(new ViewHeader
				{
					Text = this.Model.SelectedDate.Add(timeSpan).ToString(format, this.Owner.Culture)
				});
				ViewHeader viewHeader = null;
				for (int i = 0; i < this.Owner.TimeLabelRowSpan - 1; i++)
				{
					timeSpan = timeSpan.Add(TimeSpan.FromMinutes((double)this.Owner.MinutesPerRow));
					if (timeSpan < this.EffectiveViewSettings.EffectiveDayEndTime)
					{
						viewHeader = new ViewHeader();
						viewHeader.Text = "&nbsp;";
						list.Add(viewHeader);
					}
				}
				if (viewHeader != null)
				{
					viewHeader.ClassName = "rsAlt";
				}
				timeSpan = timeSpan.Add(TimeSpan.FromMinutes((double)this.Owner.MinutesPerRow));
			}
			return list;
		}

		// Token: 0x060105F1 RID: 67057 RVA: 0x003A7D2C File Offset: 0x003A5F2C
		protected IList<ViewHeader> CreateDateHeaders()
		{
			IList<ViewHeader> list = new List<ViewHeader>();
			DateTime dateTime = this.Owner.UtcToDisplay(this.Model.VisibleRangeStart);
			DateTime visualToday = this.Owner.VisualToday;
			for (int i = 0; i < this.WeekModel.NumberOfDays; i++)
			{
				ViewHeader viewHeader = new ViewHeader();
				viewHeader.Text = dateTime.ToString(this.EffectiveWeekViewSettings.ColumnHeaderDateFormat, this.Owner.Culture);
				list.Add(viewHeader);
				DateTime dateTime2 = this.Owner.UtcToDisplay(this.Owner.UtcDayStart(this.Model.VisibleRangeStart).AddDays((double)i));
				viewHeader.Date = dateTime2;
				if (dateTime2 == visualToday)
				{
					viewHeader.ClassName = "rsTodayCol";
				}
				dateTime = dateTime.AddDays(1.0);
			}
			if (list.Count > 0)
			{
				ViewHeader viewHeader2 = list[list.Count - 1];
				viewHeader2.ClassName += " rsLastCell";
			}
			return list;
		}

		// Token: 0x04004995 RID: 18837
		private ModelBase _model;
	}
}
