using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;

namespace Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByResource
{
	// Token: 0x02001A88 RID: 6792
	internal class Model : ModelBase
	{
		// Token: 0x17004FE1 RID: 20449
		// (get) Token: 0x0601071E RID: 67358 RVA: 0x003ACB94 File Offset: 0x003AAD94
		// (set) Token: 0x0601071F RID: 67359 RVA: 0x003ACB9C File Offset: 0x003AAD9C
		public IList<Resource> Resources
		{
			get
			{
				return this._resources;
			}
			protected set
			{
				this._resources = value;
			}
		}

		// Token: 0x17004FE2 RID: 20450
		// (get) Token: 0x06010720 RID: 67360 RVA: 0x003ACBA5 File Offset: 0x003AADA5
		// (set) Token: 0x06010721 RID: 67361 RVA: 0x003ACBAD File Offset: 0x003AADAD
		public IList<Model> TimelineModels
		{
			get
			{
				return this._timelineModels;
			}
			protected set
			{
				this._timelineModels = value;
			}
		}

		// Token: 0x17004FE3 RID: 20451
		// (get) Token: 0x06010722 RID: 67362 RVA: 0x003ACBB6 File Offset: 0x003AADB6
		public string GroupingResourceName
		{
			get
			{
				return this._groupingResourceName;
			}
		}

		// Token: 0x17004FE4 RID: 20452
		// (get) Token: 0x06010723 RID: 67363 RVA: 0x003ACBBE File Offset: 0x003AADBE
		protected IList<Resource> GroupingResources
		{
			get
			{
				return new List<Resource>(this.Owner.Resources.GetResourcesByType(this.GroupingResourceName));
			}
		}

		// Token: 0x17004FE5 RID: 20453
		// (get) Token: 0x06010724 RID: 67364 RVA: 0x003ACBDC File Offset: 0x003AADDC
		public int MaximumRowCount
		{
			get
			{
				int num = 0;
				foreach (Model model in this.TimelineModels)
				{
					foreach (TimeSlot timeSlot in model.IntervalSlots)
					{
						TimeSlot item = (TimeSlot)timeSlot;
						TimelineLayout timelineLayout = new TimelineLayout(new List<ISchedulerTimeSlot>
						{
							item
						}, false);
						num = Math.Max(num, timelineLayout.ActualRowCount);
					}
				}
				return num;
			}
		}

		// Token: 0x06010725 RID: 67365 RVA: 0x003ACC90 File Offset: 0x003AAE90
		public Model(IScheduler owner, string groupingResourceName) : base(owner)
		{
			this._groupingResourceName = groupingResourceName;
		}

		// Token: 0x06010726 RID: 67366 RVA: 0x003ACCA0 File Offset: 0x003AAEA0
		public override IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			list.AddRange(base.GetScriptReferences());
			list.Add(new ScriptReference("Telerik.Web.UI.Scheduler.Views.Timeline.GroupedByResource.Model.js", Assembly.GetExecutingAssembly().FullName));
			return list;
		}

		// Token: 0x06010727 RID: 67367 RVA: 0x003ACCF8 File Offset: 0x003AAEF8
		public override void DataBind(AppointmentCollection appointments)
		{
			this.Appointments.Clear();
			this.Resources = this.GroupingResources;
			this.TimelineModels = new List<Model>(this.Resources.Count);
			int num = 0;
			List<Appointment> list = new List<Appointment>(appointments);
			foreach (Resource resource in this.Resources)
			{
				TimeSlotFactory slotFactory = new TimeSlotFactory(num, resource);
				Model model = this.CreateModel(slotFactory);
				this.TimelineModels.Add(model);
				num++;
				Resource filteringResource = resource;
				IList<Appointment> list2 = list.FindAll((Appointment apt) => apt.Resources.Contains(filteringResource));
				model.DataBind(new AppointmentCollection(list2));
				this.Appointments.AddRange(list2);
			}
		}

		// Token: 0x06010728 RID: 67368 RVA: 0x003ACDDC File Offset: 0x003AAFDC
		protected virtual Model CreateModel(ITimelineTimeSlotFactory slotFactory)
		{
			return new Model(this.Owner, slotFactory);
		}

		// Token: 0x06010729 RID: 67369 RVA: 0x003ACDEC File Offset: 0x003AAFEC
		public override void HandleMove(Appointment appointment, ISchedulerTimeSlot sourceSlot, ISchedulerTimeSlot targetSlot, bool editSeries)
		{
			int modelIndex = ((TimeSlot)sourceSlot).ModelIndex;
			int modelIndex2 = ((TimeSlot)targetSlot).ModelIndex;
			Resource oldResource = this.Resources[modelIndex];
			Resource newResource = this.Resources[modelIndex2];
			ResourceUpdateInfo resourceUpdateInfo = new ResourceUpdateInfo(oldResource, newResource);
			DateTime date = this.Owner.UtcToDisplay(targetSlot.Start) + (this.Owner.UtcToDisplay(appointment.Start) - this.Owner.UtcToDisplay(sourceSlot.Start));
			TimeSpan value = this.Owner.UtcToDisplay(appointment.End) - this.Owner.UtcToDisplay(appointment.Start);
			DateTime end = this.Owner.DisplayToUtc(date.Add(value));
			DateTime start = this.Owner.DisplayToUtc(date);
			this.Owner.HandleMove(appointment, start, end, editSeries, resourceUpdateInfo);
		}

		// Token: 0x0601072A RID: 67370 RVA: 0x003ACED8 File Offset: 0x003AB0D8
		public override void HandleInsert(ISchedulerTimeSlot targetSlot, ISchedulerTimeSlot lastSlot, Appointment appointmentToInsert)
		{
			int modelIndex = ((TimeSlot)targetSlot).ModelIndex;
			appointmentToInsert.Resources.Add(this.Resources[modelIndex]);
			this.TimelineModels[modelIndex].HandleInsert(targetSlot, lastSlot, appointmentToInsert);
		}

		// Token: 0x0601072B RID: 67371 RVA: 0x003ACF1C File Offset: 0x003AB11C
		public override void HandleResize(Appointment appointment, ISchedulerTimeSlot sourceSlot, DateTime appointmentStart, DateTime appointmentEnd, bool editSeries)
		{
			int modelIndex = ((TimeSlot)sourceSlot).ModelIndex;
			this.TimelineModels[modelIndex].HandleResize(appointment, sourceSlot, appointmentStart, appointmentEnd, editSeries);
		}

		// Token: 0x0601072C RID: 67372 RVA: 0x003ACF50 File Offset: 0x003AB150
		public override ISchedulerRenderer GetRenderer()
		{
			View view;
			if (this.Owner.TimelineView.GroupingDirectionResolved == GroupingDirection.Vertical)
			{
				view = new VerticalView(this);
			}
			else
			{
				view = new HorizontalView(this);
			}
			return new Renderer(view);
		}

		// Token: 0x0601072D RID: 67373 RVA: 0x003ACF88 File Offset: 0x003AB188
		public override ISchedulerTimeSlot GetSlotByIndex(string index)
		{
			string[] array = index.Split(new char[]
			{
				':'
			});
			int num;
			if (!int.TryParse(array[0], out num))
			{
				throw new InvalidOperationException("Cannot parse slot index.");
			}
			if (num > this.TimelineModels.Count - 1)
			{
				throw new IndexOutOfRangeException("Resource index out of range: " + num);
			}
			string index2 = string.Join(":", array, 1, array.Length - 1);
			return this.TimelineModels[num].GetSlotByIndex(index2);
		}

		// Token: 0x0601072E RID: 67374 RVA: 0x003AD00C File Offset: 0x003AB20C
		public override ISchedulerTimeSlot GetAppointmentSlot(Appointment appointment)
		{
			foreach (Model model in this.TimelineModels)
			{
				ISchedulerTimeSlot appointmentSlot = model.GetAppointmentSlot(appointment);
				if (appointmentSlot != null)
				{
					return appointmentSlot;
				}
			}
			return null;
		}

		// Token: 0x0601072F RID: 67375 RVA: 0x003AD064 File Offset: 0x003AB264
		public override IList<ISchedulerTimeSlot> GetTimeSlots()
		{
			List<ISchedulerTimeSlot> list = new List<ISchedulerTimeSlot>();
			foreach (Model model in this.TimelineModels)
			{
				list.AddRange(model.GetTimeSlots());
			}
			return list;
		}

		// Token: 0x040049BA RID: 18874
		private IList<Resource> _resources;

		// Token: 0x040049BB RID: 18875
		private IList<Model> _timelineModels;

		// Token: 0x040049BC RID: 18876
		private readonly string _groupingResourceName;
	}
}
