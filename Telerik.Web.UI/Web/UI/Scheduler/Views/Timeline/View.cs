using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Web.UI.Scheduler.Views.Timeline
{
	// Token: 0x02001A84 RID: 6788
	internal class View : ViewBase
	{
		// Token: 0x17004FD1 RID: 20433
		// (get) Token: 0x060106FB RID: 67323 RVA: 0x003AC4C2 File Offset: 0x003AA6C2
		public string CssClass
		{
			get
			{
				return "rsTimelineView";
			}
		}

		// Token: 0x17004FD2 RID: 20434
		// (get) Token: 0x060106FC RID: 67324 RVA: 0x003AC4C9 File Offset: 0x003AA6C9
		// (set) Token: 0x060106FD RID: 67325 RVA: 0x003AC4D1 File Offset: 0x003AA6D1
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

		// Token: 0x17004FD3 RID: 20435
		// (get) Token: 0x060106FE RID: 67326 RVA: 0x003AC4DA File Offset: 0x003AA6DA
		public override RadScheduler Owner
		{
			get
			{
				return this.Model.Owner as RadScheduler;
			}
		}

		// Token: 0x060106FF RID: 67327 RVA: 0x003AC4EC File Offset: 0x003AA6EC
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public View(ModelBase model)
		{
			this.Model = model;
		}

		// Token: 0x06010700 RID: 67328 RVA: 0x003AC4FC File Offset: 0x003AA6FC
		protected override void InitializeColumnHeaders()
		{
			if (this.Owner.TimelineView.ShowDateHeadersResolved)
			{
				foreach (ViewHeader item in this.CreateSlotHeaders())
				{
					base.ColumnHeaders.Add(item);
				}
				base.ColumnHeaders[base.ColumnHeaders.Count - 1].ClassName = "rsLastCell";
			}
		}

		// Token: 0x06010701 RID: 67329 RVA: 0x003AC584 File Offset: 0x003AA784
		protected IList<ViewHeader> CreateSlotHeaders()
		{
			List<ViewHeader> list = new List<ViewHeader>();
			TimeSpan slotDuration = this.Owner.TimelineView.SlotDuration;
			DateTime utcDate = this.Model.VisibleRangeStart;
			for (int i = 0; i < this.Owner.TimelineView.NumberOfSlots; i++)
			{
				DateTime dateTime = this.Owner.UtcToDisplay(utcDate);
				if (i % this.Owner.TimelineView.TimeLabelSpan == 0)
				{
					ViewHeader viewHeader = new ViewHeader();
					viewHeader.Text = dateTime.ToString(this.Owner.TimelineView.ColumnHeaderDateFormat, this.Owner.Culture);
					if (this.Owner.TimelineView.TimeLabelSpan > 1)
					{
						viewHeader.SubHeaders.Add(new ViewHeader());
						viewHeader.SubHeadersVisible = false;
					}
					list.Add(viewHeader);
				}
				else
				{
					list[list.Count - 1].SubHeaders.Add(new ViewHeader());
				}
				utcDate = utcDate.Add(slotDuration);
			}
			return list;
		}

		// Token: 0x06010702 RID: 67330 RVA: 0x003AC684 File Offset: 0x003AA884
		protected override void InitializeRowHeaders()
		{
		}

		// Token: 0x040049B2 RID: 18866
		private ISchedulerModel _model;
	}
}
